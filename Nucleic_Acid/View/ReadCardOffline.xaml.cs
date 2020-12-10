﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Acid.print.Library;
using Acid.SDK.Library;
using MaterialDesignThemes.Wpf;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Acid.common.Library.config;

namespace Nucleic_Acid.View
{
    /// <summary>
    /// ReadCardOffline.xaml 的交互逻辑
    /// </summary>
    public partial class ReadCardOffline : UserControl
    {
        public List<DataModel> Items2 { get; set; }//datagrid数据源
        DispatcherTimer autoRead_Timer = new DispatcherTimer();//自动读卡
        PrintDialog dialog = new PrintDialog();//打印对象
        private const int INITIALIZED_INDEX = 1;
        private uint m_VersionNum = 0;
        private string szLogPath = "C:/UsbSDKLog/";
        private int g_nEnumDevIndex = INITIALIZED_INDEX;
        private CHCUsbSDK.USB_SDK_DEVICE_INFO[] m_aHidDevInfo;//这个存储着遍历到的设备，列表索引1开始，所以添加
        public static CHCUsbSDK.EnumDeviceCallBack m_OnEnumDeviceCallBack = null;//遍历设备的回调
        //private int IndexOfInfo = INITIALIZED_INDEX;//信息列表更新信息的索引,初始值为1
        private int SelectedItemsIndex = INITIALIZED_INDEX;//登录设备时选中的索引
        private CHCUsbSDK.USB_SDK_USER_LOGIN_INFO StruCurUsbLoginInfo = new CHCUsbSDK.USB_SDK_USER_LOGIN_INFO();
        private int UserID = -1;
        private List<DeviceModel> deviceModels = new List<DeviceModel>();//存储遍历的设备列表
        public ReadCardOffline()
        {
            InitializeComponent();
            Items2 = new List<DataModel>();
            bool res = CHCUsbSDK.USB_SDK_Init();//USB initialize
            IntPtr ptrLogPath = Marshal.StringToHGlobalAnsi(szLogPath);//写日志
            CHCUsbSDK.USB_SDK_SetLogToFile(3, ptrLogPath, false);//这里用枚举参数不匹配，直接写了3,
            Marshal.FreeHGlobal(ptrLogPath);
            m_VersionNum = CHCUsbSDK.USB_SDK_GetSDKVersion();
            TraverseDevice();//遍历设备
            login_device();//登录设备
            autoRead_Timer.Tick += AutoRead_Timer_Tick;
            autoRead_Timer.Interval = TimeSpan.FromMilliseconds(1000);
            
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }
        private void Init()
        {
            datagrid.Visibility = Visibility.Hidden;
            autoRead_Timer.Start();
        }


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

            CHCUsbSDK.USB_SDK_DEVICE_REG_RES StruDeviceRegRes = new CHCUsbSDK.USB_SDK_DEVICE_REG_RES();
            StruDeviceRegRes.dwSize = (uint)Marshal.SizeOf(StruDeviceRegRes);
            int UserIDTemp = UserID;
            UserID = CHCUsbSDK.USB_SDK_Login(ref StruCurUsbLoginInfo, ref StruDeviceRegRes);
            if (UserID == CHCUsbSDK.INVALID_USER_ID)
            {
                Console.WriteLine(UserID);
                UserID = UserIDTemp;//为了解决重复登录时的问题，但是这次只考虑了只能登录一个设备，两个设备同时能登录的话，ID会覆盖得继续解决ID的问题
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
        private int gIndex = 1;
        public CHCUsbSDK.USB_SDK_CERTIFICATE_INFO CertificateInfo;
        private DataModel dataModel = new DataModel();//读卡信息
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
                if (CHCUsbSDK.USB_SDK_GetDeviceConfig(UserID, CHCUsbSDK.USB_SDK_GET_CERTIFICATE_INFO, ref strConfigInputConfig, ref struConfigOutputInfo))
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
            ReadChineseIDcardName();
            ReadChineseCardSex();
            ReadBirthDate();
            ReadHomeAddress();
            ReadChineseNationality();
            ReadIDnum();
            gIndex = 1;
            dataModel.gIndex = gIndex;
            this.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (Items2.Count > 0)
                {

                    if (Items2[0].temp != dataModel.temp)
                    {
                        Items2.Clear();
                        Items2.Add(dataModel);
                        datagrid.ItemsSource = null;
                        datagrid.ItemsSource = Items2;
                    }
                    datagrid.Visibility = Visibility.Visible;


                }
                else
                {
                    Items2.Add(dataModel);
                    datagrid.ItemsSource = Items2;
                    datagrid.Visibility = Visibility.Visible;
                    //打印......//image1.Source = MemoryStreamToBitmapImage(ZXingHelper.BitmapToMemoryStream(ZXingHelper.Generate2DBarcode(dataModel.temp, 130, 40)));
                }
                Console.WriteLine(Items2[0].SName);
            }));
        }
        /// <summary>
        /// 姓名
        /// </summary>
        private void ReadChineseIDcardName()
        {
            byte[] ChineseIDName = new byte[30];
            Buffer.BlockCopy(CertificateInfo.byWordInfo, 0, ChineseIDName, 0, 30);
            string SName = Encoding.Unicode.GetString(ChineseIDName);
            dataModel.SName = SName.Trim();
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
            dataModel.Sex = ChineseCardSex;
        }
        /// <summary>
        /// 民族
        /// </summary>
        private void ReadChineseNationality()
        {
            byte[] ByteNationality = new byte[4];
            Buffer.BlockCopy(CertificateInfo.byWordInfo, 32, ByteNationality, 0, 4);
            string nationality = Encoding.Unicode.GetString(ByteNationality);
            nationality = Nationality.GetNationality(ref nationality);
            dataModel.SNationality = nationality;
        }
        /// <summary>
        /// 出生日期
        /// </summary>
        private void ReadBirthDate()
        {
            byte[] Bbirthdate = new byte[16];
            Buffer.BlockCopy(CertificateInfo.byWordInfo, 36, Bbirthdate, 0, 16);
            string BirthDate = Encoding.Unicode.GetString(Bbirthdate);
            StandardFormalOfBrithDate(ref BirthDate);
            dataModel.Sbirthdate = BirthDate;

        }
        private void StandardFormalOfBrithDate(ref string BirthDate)
        {
            string yyyy = BirthDate.Substring(0, 4);
            string mm = BirthDate.Substring(4, 2);
            string dd = BirthDate.Substring(6, 2);
            BirthDate = yyyy + "-" + mm + "-" + dd;
        }
        /// <summary>
        /// 家庭住址
        /// </summary>
        private void ReadHomeAddress()
        {
            byte[] HomeAddress = new byte[70];
            Buffer.BlockCopy(CertificateInfo.byWordInfo, 52, HomeAddress, 0, 70);
            string Address = Encoding.Unicode.GetString(HomeAddress);
            dataModel.home = Address;
        }
        /// <summary>
        /// 身份证号
        /// </summary>
        private void ReadIDnum()
        {
            byte[] IDnumber = new byte[36];
            Buffer.BlockCopy(CertificateInfo.byWordInfo, 122, IDnumber, 0, 36);
            string CertificateNum = Encoding.Unicode.GetString(IDnumber);
            dataModel.temp = CertificateNum;
        }
        /// <summary>
        /// 读卡
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AutoRead_Timer_Tick(object sender, EventArgs e)
        {
            Task.Factory.StartNew(Read_Card);
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
        /// <summary>
        /// 弹窗
        /// </summary>
        /// <param name="message"></param>
        public void MessageTips(string message)
        {
            MainWindow.indexoffline.MessageTips(message);
        }

        public void CancelTips(string message, Action<bool> action, DialogClosingEventHandler e = null)
        {
            MainWindow.indexoffline.CancelTips(message, action, e);
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            CancelTips("确定要打印吗？", new Action<bool>(isTrue =>
            {
                if (isTrue)
                {
                    Console.WriteLine("打印ing......................");
                    savedata();
                    //PrintHelper.print("330411199811190011");
                }
                else
                {
                    Console.WriteLine("取消打印....................");
                }
            }));

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            CancelTips("确定要删除吗？", new Action<bool>(isTrue =>
            {
                if (isTrue)
                {
                    Console.WriteLine("删除ing......................");
                    //PrintHelper.print("330411199811190011");
                }
                else
                {
                    Console.WriteLine("取消删除....................");
                }
            }));
        }

        private void savedata() 
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {
            //停止定时器
            autoRead_Timer.Stop();
        }
    }
}
