using Acid.common.Library.config;
using Acid.http.Library.RequestModel;
using Acid.print.Library;
using Acid.SDK.Library;
using MaterialDesignThemes.Wpf;
using Nucleic_Acid.Model;
using Nucleic_Acid.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WpfPaging;

namespace Nucleic_Acid.View
{
    /// <summary>
    /// InfoListOffline.xaml 的交互逻辑
    /// </summary>
    public partial class InfoListOffline : UserControl
    {
        #region 读卡定义
        private bool clearData = false;
        DispatcherTimer autoRead_Timer = new DispatcherTimer();//自动读卡
        PrintDialog dialog = new PrintDialog();//打印对象
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
        private string deviceSerialNumber = "";//设备编号
        #endregion

        #region 读卡初始化
        private void ReadInit()
        {
            bool res = CHCUsbSDK.USB_SDK_Init();//USB initialize
            m_VersionNum = CHCUsbSDK.USB_SDK_GetSDKVersion();
            TraverseDevice();//遍历设备
            login_device();//登录设备
            autoRead_Timer.Tick += AutoRead_Timer_Tick;
            autoRead_Timer.Interval = TimeSpan.FromMilliseconds(1000);
        }
        #endregion

        #region 读卡事件

        #region 遍历设备
        private bool TraverseDevice()
        {
            m_aHidDevInfo = new CHCUsbSDK.USB_SDK_DEVICE_INFO[CHCUsbSDK.MAX_USB_DEV_LEN];
            g_nEnumDevIndex = 1;
            deviceModels.Clear();//不把列表头都清空了，得用item.clear
            m_OnEnumDeviceCallBack = new CHCUsbSDK.EnumDeviceCallBack(OnEnumDeviceCallBack);
            if (CHCUsbSDK.USB_SDK_EnumDevice(m_OnEnumDeviceCallBack, IntPtr.Zero))//枚举设备
            {
                Console.WriteLine(CHCUsbSDK.INF_LEVEL + "SUCCESS USB_SDK_EnumDevice()");
                return true;
            }
            else
            {
                Console.WriteLine(CHCUsbSDK.ERR_LEVEL + "FAILED USB _SDK_EnumDevice()");
                return false;
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
            deviceSerialNumber = StruCurUsbLoginInfo.szSerialNumber;
            CHCUsbSDK.USB_SDK_DEVICE_REG_RES StruDeviceRegRes = new CHCUsbSDK.USB_SDK_DEVICE_REG_RES();
            StruDeviceRegRes.dwSize = (uint)Marshal.SizeOf(StruDeviceRegRes);
            int UserIDTemp = CHCUsbSDK.UserID;
            CHCUsbSDK.UserID = CHCUsbSDK.USB_SDK_Login(ref StruCurUsbLoginInfo, ref StruDeviceRegRes);
            if (CHCUsbSDK.UserID == CHCUsbSDK.INVALID_USER_ID)
            {
                CHCUsbSDK.UserID = UserIDTemp;
                Console.WriteLine(CHCUsbSDK.UserID);
                changeConn(CHCUsbSDK.UserID != -1);
                //为了解决重复登录时的问题，但是这次只考虑了只能登录一个设备，两个设备同时能登录的话，ID会覆盖得继续解决ID的问题
            }
            else
            {
                changeConn(true);
                string SuccesfulLoginInfo = "The device whose serial number is " + StruCurUsbLoginInfo.szSerialNumber + "login in successfully";
                Console.WriteLine(SuccesfulLoginInfo);
            }
        }

        private void changeConn(bool TF)
        {
            if (TF)
            {
                conn.Visibility = Visibility.Visible;
                disconn.Visibility = Visibility.Hidden;
                connect_Text.Text = "已连接";
            }
            else
            {
                conn.Visibility = Visibility.Hidden;
                disconn.Visibility = Visibility.Visible;
                connect_Text.Text = "未连接";
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
        private int gIndex = 1;
        public CHCUsbSDK.USB_SDK_CERTIFICATE_INFO CertificateInfo;
        private List<InfoListModel> Items2 = new List<InfoListModel>();
        private InfoListModel dataModel = new InfoListModel();
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
                    //CHCUsbSDK.Beep(CHCUsbSDK.UserID, 2,1,3,2);
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
        object obj = new object();
        /// <summary>
        /// 读
        /// </summary>
        private void ProcessChineseCardInfo()
        {
            if (clearData)
            {
                Items2.Clear();
                clearData = false;
            }
            dataModel = new InfoListModel();
            ReadIDnum();
            ReadChineseIDcardName();
            ReadChineseCardSex();
            //ReadBirthDate();//生日
            //ReadChineseNationality();//民族
            ReadHomeAddress();
            gIndex = 1;
            dataModel.index = gIndex;
            dataModel.createTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (Items2.Count > 0)
                {
                    if (Items2[0].cardNo != dataModel.cardNo)
                    {
                        autoRead_Timer.Stop();
                        string homeAddress = "";
                        string company = "";
                        SelectManIsUse(dataModel.cardNo, ref homeAddress, ref company);
                        dataModel.homeAddress = homeAddress;
                        dataModel.company = company;
                        TextTips(dataModel, Addressaction);
                        Items2.Clear();
                        Items2.Add(dataModel);
                        //dataGrid.ItemsSource = null;
                        //dataGrid.ItemsSource = Items2;
                    }
                }
                else
                {
                    autoRead_Timer.Stop();
                    string homeAddress = "";
                    string company = "";
                    SelectManIsUse(dataModel.cardNo, ref homeAddress, ref company);
                    dataModel.homeAddress = homeAddress;
                    dataModel.company = company;
                    TextTips(dataModel, Addressaction);
                    Items2.Add(dataModel);
                    //dataGrid.ItemsSource = null;
                    //dataGrid.ItemsSource = Items2;
                }
            }));
        }

        private void Addressaction(InfoListModel obj)
        {
            if (obj.iscancel)
            {
                Console.WriteLine("取消。。。。");
                clearData = true;
            }
            else
            {
                dataModel.homeAddress = obj.homeAddress;
                dataModel.company = obj.company;
                Items2.Clear();
                Items2.Add(dataModel);
                Console.WriteLine(Items2[0].userName);
                ////打印......
                if (UrlModel.autoPrint)
                {
                    saveAndPrintoffline(dataModel);
                    Button_Click(null, null);
                }
                Console.WriteLine(Items2[0].userName);
            }
            autoRead_Timer.Start();
        }
        /// <summary>
        /// 警告是否以前检测过
        /// </summary>
        /// <param name="CardId"></param>
        private void SelectManIsUse(string CardId, ref string homeAddress, ref string company)
        {
            List<InfoListModel> lists = SettingJsonConfig.readData() ?? new List<InfoListModel>();
            List<InfoListModel> listsWhere = lists.Where(u => u.cardNo == CardId).ToList();
            if (listsWhere.Count() > 0)
            {
                listsWhere.Reverse();
                homeAddress = listsWhere[0].homeAddress;
                company = listsWhere[0].company;
                this.Dispatcher.Invoke(() => { ShowWarn(listsWhere[0].userName, listsWhere[0].createTime); });
            }
            else
            {
                return;
            }
        }
        /// <summary>
        /// 姓名
        /// </summary>
        private void ReadChineseIDcardName()
        {
            byte[] ChineseIDName = new byte[30];
            Buffer.BlockCopy(CertificateInfo.byWordInfo, 0, ChineseIDName, 0, 30);
            string SName = Encoding.Unicode.GetString(ChineseIDName);
            dataModel.userName = SName.Trim();
        }
        /// <summary>
        /// 性别
        /// </summary>
        private void ReadChineseCardSex()
        {
            byte[] ChineseSex = new byte[2];
            Buffer.BlockCopy(CertificateInfo.byWordInfo, 30, ChineseSex, 0, 2);
            string ChineseCardSex = Encoding.Unicode.GetString(ChineseSex);
            if (ChineseCardSex == "1")
            {
                ChineseCardSex = "男";
            }
            else
            {
                ChineseCardSex = "女";
            }
            dataModel.sex = ChineseCardSex;
        }
        /// <summary>
        /// 民族
        /// </summary>
        //private void ReadChineseNationality()
        //{
        //    byte[] ByteNationality = new byte[4];
        //    Buffer.BlockCopy(CertificateInfo.byWordInfo, 32, ByteNationality, 0, 4);
        //    string nationality = Encoding.Unicode.GetString(ByteNationality);
        //    nationality = Nationality.GetNationality(ref nationality);
        //    dataModel.SNationality = nationality;
        //}
        /// <summary>
        /// 出生日期
        /// </summary>
        //private void ReadBirthDate()
        //{
        //    byte[] Bbirthdate = new byte[16];
        //    Buffer.BlockCopy(CertificateInfo.byWordInfo, 36, Bbirthdate, 0, 16);
        //    string BirthDate = Encoding.Unicode.GetString(Bbirthdate);
        //    StandardFormalOfBrithDate(ref BirthDate);
        //    dataModel.Sbirthdate = BirthDate;

        //}
        //private void StandardFormalOfBrithDate(ref string BirthDate)
        //{
        //    string yyyy = BirthDate.Substring(0, 4);
        //    string mm = BirthDate.Substring(4, 2);
        //    string dd = BirthDate.Substring(6, 2);
        //    BirthDate = yyyy + "-" + mm + "-" + dd;
        //}
        /// <summary>
        /// 家庭住址
        /// </summary>
        private void ReadHomeAddress()
        {
            byte[] HomeAddress = new byte[70];
            Buffer.BlockCopy(CertificateInfo.byWordInfo, 52, HomeAddress, 0, 70);
            string Address = Encoding.Unicode.GetString(HomeAddress);
            dataModel.address = Address.Trim();
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        private void ReadIDnum()
        {
            byte[] IDnumber = new byte[36];
            Buffer.BlockCopy(CertificateInfo.byWordInfo, 122, IDnumber, 0, 36);
            string CertificateNum = Encoding.Unicode.GetString(IDnumber);
            dataModel.cardNo = CertificateNum;
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
        /// <summary>
        /// ms转bi
        /// </summary>
        /// <param name="ms"></param>
        /// <returns></returns>
        public static BitmapImage MemoryStreamToBitmapImage(MemoryStream ms)
        {
            BitmapImage bit3 = new BitmapImage();
            bit3.BeginInit();
            bit3.StreamSource = ms;
            bit3.EndInit();
            return bit3;
        }
        #endregion

        private void saveAndPrintoffline(InfoListModel dataModel)
        {
            savedata(dataModel);
        }
        private void savedata(InfoListModel dataModel1)
        {
            dataModel1.acidNo = UniqueData.Gener("");
            List<InfoListModel> json = SettingJsonConfig.readData() ?? new List<InfoListModel>();
            InfoListModel infoListModel = new InfoListModel()
            {
                versions = 0,
                address = dataModel1.address,
                cardNo = dataModel1.cardNo,
                createTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                sex = dataModel1.sex == "男" ? "1" : "0",
                userName = dataModel1.userName,
                serialNumber = deviceSerialNumber,
                updateText = "编辑",
                acidNo = dataModel1.acidNo.ToString(),
                detectionName = "",
                updateName = "",
                homeAddress = dataModel1.homeAddress,
                company = dataModel1.company,
                 jcdName = jcdName
            };
            json.Add(infoListModel);
            SettingJsonConfig.saveData(json);
            Console.WriteLine("打印：" + dataModel1.cardNo);
            PrintHelper.print(dataModel1.cardNo);
        }

        #endregion
        private string jcdName = CommonHelper.jcdName;
        private string staticName = "";
        private string staticCardNo = "";
        private bool ispage = false;
        public InfoListOffline()
        {
            InitializeComponent();
            pageControl.OnPagesChanged += PageControl_OnPagesChanged;//初始化分页
            //InitDataGrid();//初始化表格
            ReadInit();//初始化读卡
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
            autoRead_Timer.Start();
        }
        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            autoRead_Timer.Stop();
        }

        #region List操作
        private void PageControl_OnPagesChanged(object sender, WpfPaging.PagesChangedArgs e)
        {
            if (ispage)
            {
                QuerySelect_page(((PagingControl)sender).CurrentPage);
            }
            else
            {
                ispage = true;
            }
        }


        private void Init()
        {
            QuerySelect_page(pageControl.CurrentPage);
        }

        private void SetInfoList(RequestInfoListModel requestInfoListModel)
        {
            loding.Visibility = Visibility.Visible;
            Task.Run(() =>
            {
                List<InfoListModel> lists = SettingJsonConfig.readData() ?? new List<InfoListModel>();
                if (lists != null)
                {
                    if (requestInfoListModel.name != null)
                    {
                        lists = lists.Where(u => u.userName.ToString() == requestInfoListModel.name).ToList();
                    }
                    if (requestInfoListModel.cardNo != null)
                    {
                        lists = lists.Where(u => u.cardNo.ToString() == requestInfoListModel.cardNo).ToList();
                    }
                    lists.Reverse();
                    List<InfoListModel> data = lists.Skip((requestInfoListModel.pageNo - 1) * requestInfoListModel.pageSize).Take(requestInfoListModel.pageSize).ToList();
                    //添加序号
                    int current = 1;
                    //Thread.Sleep(1000);
                    foreach (var item in data)
                    {
                        item.index = current;
                        item.Editor_company = false;
                        item.Editor_homeAddress = false;
                        current++;
                    }
                    List<InfoListModel> newlist = ToDataGrid(data);
                    this.Dispatcher.Invoke(() =>
                    {

                        pageControl.DataTote = lists.Count();
                        pageControl.CurrentPage = requestInfoListModel.pageNo;
                        dataGrid.ItemsSource = newlist;
                        loding.Visibility = Visibility.Hidden;


                    });
                }
                else
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        dataGrid.ItemsSource = null;
                        dataGrid.ItemsSource = new List<InfoListModel>();
                        loding.Visibility = Visibility.Hidden;
                    });
                }
            });
        }
        private void InitDataGrid()
        {
            SetInfoList(new RequestInfoListModel(1, pageControl.PageSize));
        }
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <returns></returns>
        private void QuerySelect_page(int page)
        {
            RequestInfoListModel requestInfoListModel = new RequestInfoListModel()
            {
                pageNo = page,
                pageSize = pageControl.PageSize,
                cardNo = staticCardNo == "" ? null : staticCardNo,
                name = staticName == "" ? null : staticName
            };
            SetInfoList(requestInfoListModel);
        }
        /// <summary>
        /// 条件
        /// </summary>
        /// <param name="page"></param>
        private void QuerySelect_click(int page)
        {
            RequestInfoListModel requestInfoListModel = new RequestInfoListModel()
            {
                pageNo = page,
                pageSize = pageControl.PageSize,
                cardNo = TextBox_CardNo.Text == "" ? null : TextBox_CardNo.Text,
                name = TextBox_Name.Text == "" ? null : TextBox_Name.Text
            };
            SetInfoList(requestInfoListModel);
            //绑定静态值
            staticName = TextBox_Name.Text;
            staticCardNo = TextBox_CardNo.Text;
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            staticName = "";
            staticCardNo = "";
            TextBox_CardNo.Clear();
            TextBox_Name.Clear();
            pageControl.CurrentPage = 1;
            InitDataGrid();
        }
        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            pageControl.CurrentPage = 1;
            QuerySelect_click(1);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_update_Click(object sender, RoutedEventArgs e)
        {
            Button tag = (sender as Button);
            if (tag.Content.ToString() == "编辑")
            {
                InfoListModel obj = (InfoListModel)dataGrid.SelectedItem;
                obj.Editor_homeAddress = true;
                obj.Editor_company = true;
                List<InfoListModel> source = (List<InfoListModel>)dataGrid.ItemsSource;
                obj.updateText = "保存";
                dataGrid.ItemsSource = null;
                dataGrid.ItemsSource = source;
            }
            else
            {
                //确认保存
                CancelSave();
            }
        }
        /// <summary>
        /// 确认保存
        /// </summary>
        private void CancelSave()
        {
            CancelTips("确认要修改?", new Action<bool>(arg =>
            {
                if (arg)
                {
                    try
                    {
                        InfoListModel obj = (InfoListModel)dataGrid.SelectedItem;
                        List<InfoListModel> lists = SettingJsonConfig.readData() ?? new List<InfoListModel>();
                        InfoListModel infoListModel = lists.Where(u => u.acidNo == obj.acidNo).SingleOrDefault();
                        infoListModel.homeAddress = obj.homeAddress;
                        infoListModel.company = obj.company;
                        infoListModel.updateName = infoListModel.detectionName;
                        infoListModel.updateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        infoListModel.versions = obj.versions == 0 ? 0 : 3;
                        SettingJsonConfig.saveData(lists);
                        List<InfoListModel> source = (List<InfoListModel>)dataGrid.ItemsSource;
                        obj.updateText = "编辑";
                        obj.Editor_homeAddress = false;
                        obj.Editor_company = false;
                        dataGrid.ItemsSource = null;
                        dataGrid.ItemsSource = source;
                    }
                    catch (Exception ex)
                    {
                        MessageTips(ex.Message);
                    }
                }
            }));
        }
        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print_click(object sender, RoutedEventArgs e)
        {
            CancelPrint();
        }
        private void CancelPrint()
        {
            CancelTips("确认要打印?", new Action<bool>(arg =>
            {
                if (arg)
                {
                    InfoListModel obj = (InfoListModel)dataGrid.SelectedItem;
                    string cardid = obj.cardNo;//身份证号
                    PrintHelper.print(cardid);//打印
                    Console.WriteLine("打印：" + cardid);
                }
            }));
        }

        private List<InfoListModel> ToDataGrid(List<InfoListModel> infoListModels)
        {
            foreach (var item in infoListModels)
            {
                item.sex = CommonHelper.ToSex(item.sex);
            }
            return infoListModels;
        }

        //private void company_Changed(object sender, TextChangedEventArgs e)
        //{
        //    InfoListModel obj = (InfoListModel)dataGrid.SelectedItem;
        //    if (obj != null)
        //    {
        //        TextBox textBox = sender as TextBox;
        //        obj.company = textBox.Text;
        //    }
        //}

        //private void homeAddress_Changed(object sender, TextChangedEventArgs e)
        //{
        //    InfoListModel obj = (InfoListModel)dataGrid.SelectedItem;
        //    if (obj != null)
        //    {
        //        TextBox textBox = sender as TextBox;
        //        obj.homeAddress = textBox.Text;
        //    }
        //}
        #endregion

        #region 弹框操作
        public void MessageTips(string message)
        {
            MainWindow.indexoffline.MessageTips(message);
        }
        public void ShowWarn(string name, string datetime)
        {
            MainWindow.indexoffline.ShowWarn(name, datetime);
        }
        public void CancelTips(string message, Action<bool> action)
        {
            MainWindow.indexoffline.CancelTips(message, action, null);
        }
        public void lodings()
        {
            MainWindow.indexoffline.Loding();
        }
        public void lodings_close()
        {
            MainWindow.indexoffline.Loding_close();
        }
        public void TextTips(InfoListModel message, Action<InfoListModel> action, DialogClosingEventHandler e = null)
        {
            MainWindow.indexoffline.TextTips(message, action, e);
        }
        #endregion


    }
}
