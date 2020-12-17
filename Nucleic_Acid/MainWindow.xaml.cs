using Acid.common.Library.config;
using Acid.http.Library.RequestModel;
using Acid.http.Library.ResponseModel;
using Acid.http.Library.Service;
using Acid.SDK.Library;
using MaterialDesignThemes.Wpf;
using Nucleic_Acid.Util;
using Nucleic_Acid.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Nucleic_Acid
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        SettingModel settingModel = new SettingModel();
        public static Index index;
        public static IndexOffline indexoffline;
        private const int INITIALIZED_INDEX = 1;
        private uint m_VersionNum = 0;
        //private string szLogPath = "C:/UsbSDKLog/";
        private int g_nEnumDevIndex = INITIALIZED_INDEX;
        private CHCUsbSDK.USB_SDK_DEVICE_INFO[] m_aHidDevInfo;//这个存储着遍历到的设备，列表索引1开始，所以添加
        public static CHCUsbSDK.EnumDeviceCallBack m_OnEnumDeviceCallBack = null;//遍历设备的回调
        //private int IndexOfInfo = INITIALIZED_INDEX;//信息列表更新信息的索引,初始值为1
        private int SelectedItemsIndex = INITIALIZED_INDEX;//登录设备时选中的索引
        private CHCUsbSDK.USB_SDK_USER_LOGIN_INFO StruCurUsbLoginInfo = new CHCUsbSDK.USB_SDK_USER_LOGIN_INFO();
        private List<DeviceModel> deviceModels = new List<DeviceModel>();//存储遍历的设备列表
        DispatcherTimer autoRead_Timer = new DispatcherTimer();//自动读卡
        public MainWindow()
        {
            InitializeComponent();
            bool res = CHCUsbSDK.USB_SDK_Init();//USB initialize
            m_VersionNum = CHCUsbSDK.USB_SDK_GetSDKVersion();
            TraverseDevice();//遍历设备
            login_device();//登录设备
            autoRead_Timer.Tick += AutoRead_Timer_Tick;
            autoRead_Timer.Interval = TimeSpan.FromMilliseconds(1000);
            SettingModel settingModel1 = SettingJsonConfig.readJson();
            settingModel = settingModel1 == null ? new SettingModel() : settingModel1;
            userNameBox.Text = settingModel.userName == null ? "" : settingModel.userName;
            passWordBox.Password = settingModel.passWord == null ? "" : settingModel.passWord;
            CheckBox_isRember.IsChecked = settingModel.isRember;
        }
        public MainWindow(string username,string password,string name)
        {
            InitializeComponent();
            bool res = CHCUsbSDK.USB_SDK_Init();//USB initialize
            m_VersionNum = CHCUsbSDK.USB_SDK_GetSDKVersion();
            TraverseDevice();//遍历设备
            login_device();//登录设备
            autoRead_Timer.Tick += AutoRead_Timer_Tick;
            autoRead_Timer.Interval = TimeSpan.FromMilliseconds(1000);
            userNameBox.Text = username;
            passWordBox.Password = password;
            nameBox.Text = name;
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            // 获取鼠标相对标题栏位置  
            Point position = e.GetPosition(backGrid);
            // 如果鼠标位置在标题栏内，允许拖动  
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < backGrid.ActualWidth && position.Y >= 0 && position.Y < backGrid.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        private void backGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
            autoRead_Timer.Start();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Init()
        {
            Util.Logger.Default.Info("------------------------start------------------------------");
        }

        public void Login_Click(object sender, RoutedEventArgs e)
        {
            if (nameBox.Text == null || nameBox.Text == "")
            {
                MessageTips("请先刷卡认证您的资料", sender, e);
                return;
            }
            CommonHelper.userName = userNameBox.Text.Trim() ?? "";
            CommonHelper.passWord = passWordBox.Password.Trim() ?? "";
            CommonHelper.detectionName = nameBox.Text.Trim() ?? "";
            indexoffline = new IndexOffline(CommonHelper.detectionName);
            indexoffline.Show();
            this.Close();
        }
        public void login() 
        {
            if (userNameBox.Text == null || userNameBox.Text == "")
            {
                MessageTips("请输入Username用户名");
                return;
            }
            else if (passWordBox.Password == null || passWordBox.Password == "")
            {
                MessageTips("请输入Password密码");
                return;
            }
            else if (nameBox.Text == null || nameBox.Text == "")
            {
                MessageTips("请先刷卡认证您的资料");
                return;
            }
            LogindTips();
            string username = userNameBox.Text;
            string password = passWordBox.Password;
            bool? isRember = CheckBox_isRember.IsChecked;
            Task.Run(() =>
            {
                Thread.Sleep(500);//看效果
                ResultJson<LoginModel> dictionaries = LoginService.Login_Ex(username, password, isRember);
                this.Dispatcher.Invoke(() =>
                {
                    DialogHost.Close("LoginDialog");
                    if (dictionaries.code == "20000")
                    {
                        CommonHelper.userName = userNameBox.Text.Trim() ?? "";
                        CommonHelper.passWord = passWordBox.Password.Trim() ?? "";
                        CommonHelper.detectionName = nameBox.Text.Trim() ?? "";
                        index = new Index(CommonHelper.detectionName);
                        index.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageTips(dictionaries.message);
                    }
                });
            });
        }


        public async void MessageTips(string message, object sender = null, RoutedEventArgs e = null)
        {
            var sampleMessageDialog = new MessageDialog()
            {
                Message = { Text = message }
            };
            await DialogHost.Show(sampleMessageDialog, "LoginDialog");
        }
        /// <summary>
        /// 确定取消弹窗
        /// </summary>
        /// <param name="message"></param>
        /// <param name="action"></param>
        /// <param name="e"></param>
        public async void CancelTips(string message, Action<bool> action, DialogClosingEventHandler e = null)
        {
            if (e == null)
                e = closingEventHandler;
            var sampleMessageDialog = new CanCancel()
            {
                Message = { Text = message }
            };
            await DialogHost.Show(sampleMessageDialog, "LoginDialog", e);
            action(sampleMessageDialog.IsTrue);
        }

        private void closingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;

            //OK, lets cancel the close...
            eventArgs.Cancel();

            //...now, lets update the "session" with some new content!
            eventArgs.Session.UpdateContent(new SampleProgressDialog());
            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            //lets run a fake operation for 3 seconds then close this baby.
            Task.Delay(TimeSpan.FromMilliseconds(500))
                .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }

        public async void LogindTips()
        {
            var sampleMessageDialog = new SampleProgressDialog();
            await DialogHost.Show(sampleMessageDialog, "LoginDialog");
        }

        private void Wmain_Loaded(object sender, RoutedEventArgs e)
        {
            CheckForUpdates();
        }
        private void CheckForUpdates()
        {
            //dynamic temp = UpdateUtil.HasNewVersion();
            //if (Convert.ToBoolean(temp.result))
            //{
            //    //CancelTips("检测到有新版本可用，是否进行更新", CheckUpdate);
            //}
        }

        private void CheckUpdate(bool obj)
        {
            if (obj)
            {
                if (UpdateUtil.IsSystemPath() && !UpdateUtil.IsAdministrator())
                {
                    UpdateUtil.ReStartAppWithAdministrator();
                }
                else
                {
                    ProcessStartInfo p = new ProcessStartInfo();
                    p.FileName = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\  \Nucleic_Acid.exe";
                    p.UseShellExecute = false;
                    p.WorkingDirectory = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                    p.CreateNoWindow = true;
                    Process.Start(p);
                    Application.Current.Shutdown();
                    //TimeHelper.flagnotifyIcon1 = false;
                }
            }
        }

        #region 遍历设备
        private void TraverseDevice()
        {
            m_aHidDevInfo = new CHCUsbSDK.USB_SDK_DEVICE_INFO[CHCUsbSDK.MAX_USB_DEV_LEN];
            g_nEnumDevIndex = 1;
            deviceModels.Clear();//不把列表头都清空了，得用item.clear
            m_OnEnumDeviceCallBack = new CHCUsbSDK.EnumDeviceCallBack(OnEnumDeviceCallBack);
            if (CHCUsbSDK.USB_SDK_EnumDevice(m_OnEnumDeviceCallBack, IntPtr.Zero))//枚举设备
            {
                Console.WriteLine(CHCUsbSDK.INF_LEVEL + "SUCCESS USB_SDK_EnumDevice()");
            }
            else
            {
                Console.WriteLine(CHCUsbSDK.ERR_LEVEL + "FAILED USB _SDK_EnumDevice()");
            }
        }

        /// <summary>
        /// 回调函数用于在空间框中显示遍历到的设备的信息
        /// </summary>
        /// <param name="pDevceInfo"></param>
        /// <param name="pUser"></param>
        /// <returns></returns>
        public void OnEnumDeviceCallBack(ref CHCUsbSDK.USB_SDK_DEVICE_INFO pDevceInfo, IntPtr pUser)//枚举设备的回调函数
        {
            CHCUsbSDK.USB_SDK_DEVICE_INFO CopyDeviceInfo = new CHCUsbSDK.USB_SDK_DEVICE_INFO();
            CopyDeviceInfo = pDevceInfo;
            CopyDeviceInfo.dwSize = (uint)Marshal.SizeOf(CopyDeviceInfo);//结构体本身的大小
            if (g_nEnumDevIndex == 64)
            {
                Console.WriteLine("The total number of devices could not be over 64numbers");
            }
            deviceModels.Add(new DeviceModel(
                    (g_nEnumDevIndex),
                    CopyDeviceInfo.dwVID.ToString(),
                    CopyDeviceInfo.dwPID.ToString(),
                    CopyDeviceInfo.szManufacturer,
                    CopyDeviceInfo.szDeviceName,
                    CopyDeviceInfo.szSerialNumber));
            m_aHidDevInfo[g_nEnumDevIndex - 1] = CopyDeviceInfo;//存储到数组中方便登录获取数据
            g_nEnumDevIndex++;//更新索引
        }


        #endregion

        #region 登录设备
        private void login_device()
        {
            if (!JudgeValidOfLoginInfo())
            {
                return;
            }
            StruCurUsbLoginInfo.dwSize = (uint)Marshal.SizeOf(StruCurUsbLoginInfo);
            StruCurUsbLoginInfo.dwTimeout = 5000;//登录超时时间是5秒
            StruCurUsbLoginInfo.dwVID = m_aHidDevInfo[SelectedItemsIndex].dwVID;
            StruCurUsbLoginInfo.dwPID = m_aHidDevInfo[SelectedItemsIndex].dwPID;
            StruCurUsbLoginInfo.szSerialNumber = m_aHidDevInfo[SelectedItemsIndex].szSerialNumber;
            StruCurUsbLoginInfo.szUserName = "admin";
            StruCurUsbLoginInfo.szPassword = "12345";
            CHCUsbSDK.USB_SDK_DEVICE_REG_RES StruDeviceRegRes = new CHCUsbSDK.USB_SDK_DEVICE_REG_RES();
            StruDeviceRegRes.dwSize = (uint)Marshal.SizeOf(StruDeviceRegRes);
            int UserIDTemp = CHCUsbSDK.UserID;
            CHCUsbSDK.UserID = CHCUsbSDK.USB_SDK_Login(ref StruCurUsbLoginInfo, ref StruDeviceRegRes);
            if (CHCUsbSDK.UserID == CHCUsbSDK.INVALID_USER_ID)
            {
                CHCUsbSDK.UserID = UserIDTemp;
                Console.WriteLine(CHCUsbSDK.UserID);
                //为了解决重复登录时的问题，但是这次只考虑了只能登录一个设备，两个设备同时能登录的话，ID会覆盖得继续解决ID的问题
            }
            else
            {
                string SuccesfulLoginInfo = "The device whose serial number is " + StruCurUsbLoginInfo.szSerialNumber + "login in successfully";
                Console.WriteLine(SuccesfulLoginInfo);
            }
        }

        /// <summary>
        /// 判断登录设备的相关信息是否是有效的
        /// </summary>
        /// <returns></returns>
        private bool JudgeValidOfLoginInfo()
        {
            foreach (var item in deviceModels)
            {
                if (item.szManufacturer == "HIKVISION" || item.szDeviceName == "DS-K1F110-I")
                {
                    SelectedItemsIndex = item.Index - 1;
                    return true;
                }
            }
            return false;
        }

        #endregion

        #region 读卡
        public CHCUsbSDK.USB_SDK_CERTIFICATE_INFO CertificateInfo;
        private void Read_Card()
        {
            Task task = new Task(() =>
            {
                CHCUsbSDK.USB_SDK_CERTIFICATE_INFO struCertificateInfo = new CHCUsbSDK.USB_SDK_CERTIFICATE_INFO();
                struCertificateInfo.dwSize = (uint)Marshal.SizeOf(struCertificateInfo);

                CHCUsbSDK.USB_CONFIG_OUTPUT_INFO struConfigOutputInfo = new CHCUsbSDK.USB_CONFIG_OUTPUT_INFO();
                struConfigOutputInfo.dwOutBufferSize = struCertificateInfo.dwSize;
                IntPtr ptrstruCertificateInfo = Marshal.AllocHGlobal((int)struCertificateInfo.dwSize);
                Marshal.StructureToPtr(struCertificateInfo, ptrstruCertificateInfo, false);
                struConfigOutputInfo.lpOutBuffer = ptrstruCertificateInfo;

                CHCUsbSDK.USB_CONFIG_INPUT_INFO strConfigInputConfig = new CHCUsbSDK.USB_CONFIG_INPUT_INFO();
                if (CHCUsbSDK.USB_SDK_GetDeviceConfig(CHCUsbSDK.UserID, CHCUsbSDK.USB_SDK_GET_CERTIFICATE_INFO, ref strConfigInputConfig, ref struConfigOutputInfo))
                {
                    struCertificateInfo = (CHCUsbSDK.USB_SDK_CERTIFICATE_INFO)Marshal.PtrToStructure(struConfigOutputInfo.lpOutBuffer, typeof(CHCUsbSDK.USB_SDK_CERTIFICATE_INFO));
                    CertificateInfo = struCertificateInfo;
                    ReadCertificateInfo();
                    Marshal.FreeHGlobal(ptrstruCertificateInfo);
                }
                else
                {
                    Marshal.FreeHGlobal(ptrstruCertificateInfo);
                    Console.WriteLine(CHCUsbSDK.ERR_LEVEL + "Fail to Read CardInfo");

                }
            });
            task.Start();
        }
        public void ReadCertificateInfo()
        {
            ProcessChineseCardInfo();
        }
        /// <summary>
        /// 读
        /// </summary>
        private void ProcessChineseCardInfo()
        {
            string name = ReadChineseIDcardName();
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                nameBox.Text = name;
            }));
        }
        /// <summary>
        /// 姓名
        /// </summary>
        private string ReadChineseIDcardName()
        {
            byte[] ChineseIDName = new byte[30];
            Buffer.BlockCopy(CertificateInfo.byWordInfo, 0, ChineseIDName, 0, 30);
            return Encoding.Unicode.GetString(ChineseIDName);
        }
        /// <summary>
        /// 读卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoRead_Timer_Tick(object sender, EventArgs e)
        {
            if (CHCUsbSDK.UserID != -1)
            {
                Task.Factory.StartNew(Read_Card);
            }
            else
            {
                TraverseDevice();//遍历设备
                login_device();//登录设备
            }
        }
        #endregion

        private void Wmain_Unloaded(object sender, RoutedEventArgs e)
        {
            autoRead_Timer.Stop();
        }

        private void Offline_Click(object sender, RoutedEventArgs e)
        {
            //if (nameBox.Text == null || nameBox.Text == "")
            //{
            //    MessageTips("请先刷卡认证您的资料", sender, e);
            //    return;
            //}
            //CommonHelper.userName = userNameBox.Text.Trim() ?? "";
            //CommonHelper.passWord = passWordBox.Password.Trim() ?? "";
            //CommonHelper.detectionName = nameBox.Text.Trim() ?? "";
            //indexoffline = new IndexOffline(CommonHelper.detectionName);
            //indexoffline.Show();
            //this.Close();
        }

        private void Click_Min(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

    }
}
