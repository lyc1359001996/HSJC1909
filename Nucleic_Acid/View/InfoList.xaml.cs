﻿using Acid.common.Library.config;
using Acid.http.Library.RequestModel;
using Acid.http.Library.ResponseModel;
using Acid.http.Library.Service;
using Acid.NPOI.Library;
using Acid.print.Library;
using Acid.SDK.Library;
using MaterialDesignThemes.Wpf;
using Nucleic_Acid.Model;
using Nucleic_Acid.Util;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// InfoList.xaml 的交互逻辑
    /// </summary>
    public partial class InfoList : UserControl
    {
        #region 读卡定义
        private bool clearData = false;
        DispatcherTimer autoRead_Timer = new DispatcherTimer();//自动读卡
        PrintDialog dialog = new PrintDialog();//打印对象
        private const int INITIALIZED_INDEX = 1;
        private uint m_VersionNum;
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
            Login_device();//登录设备
            autoRead_Timer.Tick += AutoRead_Timer_Tick;
            autoRead_Timer.Interval = TimeSpan.FromMilliseconds(1000);
        }
        #endregion

        private string detectionName = CommonHelper.detectionName;
        private string staticName = "";
        private string staticCardNo = "";
        private string staticStartTime = "";
        private string staticEndTime = "";
        private bool isfirt = false;
        public InfoList()
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
        private void PageControl_OnPagesChanged(object sender, WpfPaging.PagesChangedArgs e)
        {
            if (isfirt)
            {
                QuerySelect_page(((PagingControl)sender).CurrentPage);
            }
            else
            {
                isfirt = true;
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
        private void Login_device()
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
                ChangeConn(CHCUsbSDK.UserID != -1);
                Console.WriteLine(CHCUsbSDK.UserID);
                //为了解决重复登录时的问题，但是这次只考虑了只能登录一个设备，两个设备同时能登录的话，ID会覆盖得继续解决ID的问题
            }
            else
            {
                ChangeConn(true);
                string SuccesfulLoginInfo = "The device whose serial number is " + StruCurUsbLoginInfo.szSerialNumber + "login in successfully";
                Console.WriteLine(SuccesfulLoginInfo);
            }
        }

        private void ChangeConn(bool TF)
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

                CHCUsbSDK.USB_CONFIG_OUTPUT_INFO struConfigOutputInfo = new CHCUsbSDK.USB_CONFIG_OUTPUT_INFO
                {
                    dwOutBufferSize = struCertificateInfo.dwSize
                };
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
            if (clearData)
            {
                Items2 = new List<InfoListModel>();
                clearData = false;
            }
            dataModel = new InfoListModel();
            ReadChineseIDcardName();
            ReadChineseCardSex();
            //ReadBirthDate();
            ReadHomeAddress();
            //ReadChineseNationality();
            ReadIDnum();
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
                //打印......
                if (UrlModel.autoPrint)
                {
                    SaveAndPrint(dataModel);
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
                this.Dispatcher.Invoke(() => { ShowWarn(listsWhere[0].userName, listsWhere[0].createTime.ToString()); });
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
                Login_device();//登录设备
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

        /// <summary>
        /// 保存本地数据
        /// </summary>
        /// <param name="dataModel"></param>
        /// <param name="snowID"></param>
        private void Savedata(InfoListModel dataModel, int versions)
        {
            List<InfoListModel> json = SettingJsonConfig.readData() ?? new List<InfoListModel>();
            InfoListModel infoListModel = new InfoListModel()
            {
                versions = versions,
                address = dataModel.address,
                cardNo = dataModel.cardNo,
                createTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                sex = dataModel.sex == "男" ? "1" : "0",
                userName = dataModel.userName,
                serialNumber = deviceSerialNumber,
                updateText = "编辑",
                acidNo = dataModel.acidNo.ToString(),
                detectionName = detectionName,
                updateName = detectionName,
                homeAddress = dataModel.homeAddress,
                company = dataModel.company,
                jcdName = CommonHelper.jcdName
            };
            json.Add(infoListModel);
            SettingJsonConfig.saveData(json);
        }
        /// <summary>
        /// 上传数据到线上
        /// </summary>
        /// <param name="dataModel"></param>
        /// <param name="snowID"></param>
        /// <returns></returns>
        private ResultJson<string> Saveonline(InfoListModel dataModel)
        {
            List<InfoListModel> infoListModels = new List<InfoListModel>();
            InfoListModel infoListModel = new InfoListModel()
            {
                address = dataModel.address,
                cardNo = dataModel.cardNo,
                createTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                sex = dataModel.sex == "男" ? "1" : "0",
                userName = dataModel.userName,
                serialNumber = deviceSerialNumber,
                updateText = "编辑",
                acidNo = dataModel.acidNo.ToString(),
                detectionName = detectionName,
                updateName = detectionName,
                homeAddress = dataModel.homeAddress,
                company = dataModel.company,
                jcdName = CommonHelper.jcdName,
                xzjdName = CommonHelper.xzjdName,
                cydName = CommonHelper.cydName,
                districtName = CommonHelper.districtName
            };
            infoListModels.Add(infoListModel);
            Acid.http.Library.ResponseModel.ResultJson<string> resultJson = InfoListService.addNucleic(infoListModels);
            return resultJson;
        }

        private void SaveAndPrint(InfoListModel selectedItem)
        {
            Console.WriteLine("打印ing......................");
            selectedItem.acidNo = UniqueData.Gener("");
            //同步线上
            Acid.http.Library.ResponseModel.ResultJson<string> resultJson = Saveonline(selectedItem);
            if (resultJson.code == "20000")
            {
                //保存本地
                Savedata(selectedItem, 1);
            }
            else
            {
                //保存本地
                Savedata(selectedItem, 0);
            }
            Console.WriteLine("打印：" + selectedItem.cardNo);
            PrintHelper.Print(selectedItem.cardNo.Trim());
        }

        #endregion

        #region InfoList

        private void Init()
        {
            dateTimeStart.Text = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
            dateTimeEnd.Text = DateTime.Now.AddDays(1).ToString("yyyy-MM-dd 00:00:00");
            dateTimeStart.Text = "";
            dateTimeEnd.Text = "";
            QuerySelect_page(pageControl.CurrentPage);
        }
        private void SetInfoList(RequestInfoListModel requestInfoListModel)
        {
            loding.Visibility = Visibility.Visible;
            Task.Run(() =>
            {
                ResultJson<ResponseInfoListModel> resultJson = InfoListService.getQuery(requestInfoListModel);

                if (resultJson.code == "20000")
                {
                    List<InfoListModel> newList = ToDataGrid(resultJson.data.addIndex(resultJson.data.data) ?? new List<InfoListModel>());
                    this.Dispatcher.Invoke(() =>
                    {
                        pageControl.DataTote = resultJson.data.total;
                        dataGrid.ItemsSource = newList;
                        loding.Visibility = Visibility.Hidden;
                    });
                }
                else
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        MessageTips(resultJson.message);
                        dataGrid.ItemsSource = ToDataGrid(new List<InfoListModel>());
                        loding.Visibility = Visibility.Hidden;
                    });
                }

            });
        }

        private void InitDataGrid()
        {
            SetInfoList(new RequestInfoListModel(1, pageControl.PageSize));
        }

        private void QuerySelect_page(int page)
        {
            RequestInfoListModel requestInfoListModel = new RequestInfoListModel()
            {
                pageNo = page,
                pageSize = pageControl.PageSize,
                cardNo = staticCardNo == "" ? null : staticCardNo,
                name = staticName == "" ? null : staticName,
                startTime = staticStartTime == "" ? null : staticStartTime,
                endTime = staticEndTime == "" ? null : staticEndTime
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
                name = TextBox_Name.Text == "" ? null : TextBox_Name.Text,
                startTime = dateTimeStart.Text == "" ? null : dateTimeStart.Text,
                endTime = dateTimeEnd.Text == "" ? null : dateTimeStart.Text
            };
            SetInfoList(requestInfoListModel);
            //绑定静态值
            staticName = TextBox_Name.Text;
            staticCardNo = TextBox_CardNo.Text;
            staticStartTime = dateTimeStart.Text;
            staticEndTime = dateTimeEnd.Text;
        }



        /// <summary>
        /// 重置查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            staticName = "";
            staticCardNo = "";
            staticStartTime = "";
            staticEndTime = "";
            dateTimeStart.Text = "";
            dateTimeEnd.Text = "";
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
            //staticName = TextBox_Name.Text;
            //staticCardNo = TextBox_CardNo.Text;
            //staticTestValue = ComboBox_TestValue.SelectedIndex.ToString();
            pageControl.CurrentPage = 1;
            QuerySelect_click(1);

        }
        /// <summary>
        /// 点击修改
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
                         string thisTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                         List<InfoListModel> infoListModels = new List<InfoListModel>();
                         InfoListModel obj = (InfoListModel)dataGrid.SelectedItem;
                         obj.updateName = detectionName;
                         obj.updateTime = thisTime;
                         infoListModels.Add(obj);
                         #region 服务端更新
                         ResultJson<string> resultJson = InfoListService.updateNucleic(ToList(infoListModels));
                         #endregion
                         if (resultJson.code == "20000")
                         {
                             #region 本地更新
                             List<InfoListModel> lists = SettingJsonConfig.readData() ?? new List<InfoListModel>();
                             if (lists.Where(u => u.acidNo == obj.acidNo).Count() > 0)
                             {
                                 InfoListModel infoListModel = lists.Where(u => u.acidNo == obj.acidNo).SingleOrDefault();
                                 infoListModel.updateName = detectionName;
                                 infoListModel.updateTime = thisTime;
                             }
                             SettingJsonConfig.saveData(lists);
                             #endregion
                             obj.Editor_homeAddress = false;
                             obj.Editor_company = false;
                             List<InfoListModel> source = (List<InfoListModel>)dataGrid.ItemsSource;
                             obj.updateText = "编辑";
                             dataGrid.ItemsSource = null;
                             dataGrid.ItemsSource = ToDataGrid(source);
                         }
                         else
                         {
                             MessageTips(resultJson.message);
                         }
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
                    PrintHelper.Print(cardid);//打印
                    Console.WriteLine("打印：" + cardid);
                }
            }));
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            autoRead_Timer.Stop();
            AddTips(AddAction);
        }

        private void AddAction(InfoListModel obj)
        {
            if (!obj.iscancel)//不是取消
            {
                ////打印......
                if (UrlModel.autoPrint)
                {
                    SaveAndPrint(obj);
                    Button_Click(null, null);
                }
                Console.WriteLine(obj.userName);
            }
            autoRead_Timer.Start();
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void delete_click(object sender, RoutedEventArgs e)
        {
            CancelDelete();
        }
        private void CancelDelete()
        {
            CancelTips("确认要删除?", new Action<bool>(arg =>
            {
                if (arg)
                {
                    #region 本地删除
                    InfoListModel obj = (InfoListModel)dataGrid.SelectedItem;
                    List<Acid.common.Library.config.InfoListModel> lists = SettingJsonConfig.readData() ?? new List<Acid.common.Library.config.InfoListModel>();
                    List<Acid.common.Library.config.InfoListModel> infoListModel = lists.Where(u => u.acidNo == obj.acidNo).ToList();
                    foreach (var item in infoListModel)
                    {
                        lists.Remove(item);//移除
                    }
                    SettingJsonConfig.saveData(lists);//保存
                    #endregion
                    #region 服务器删除
                    ResultJson<string> resultJson = InfoListService.deleteNucleic(new InfoListModel() { acidNo = obj.acidNo });
                    #endregion
                    QuerySelect_page(pageControl.CurrentPage);
                    //删除
                    Console.WriteLine("删除：" + obj.acidNo);
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
        private List<InfoListModel> ToList(List<InfoListModel> infoListModels)
        {
            foreach (var item in infoListModels)
            {
                item.sex = CommonHelper.SexToInt(item.sex);
            }
            return infoListModels;
        }

        #endregion

        #region 弹窗
        public void TextTips(InfoListModel message, Action<InfoListModel> action, DialogClosingEventHandler e = null)
        {
            MainWindow.index.TextTips(message, action, e);
        }
        public void MessageTips(string message)
        {
            MainWindow.index.MessageTips(message);
        }
        public void ShowWarn(string name, string datetime)
        {
            MainWindow.index.ShowWarn(name, datetime);
        }
        public void CancelTips(string message, Action<bool> action, DialogClosingEventHandler e = null)
        {
            MainWindow.index.CancelTips(message, action, e);
        }
        public void ChooseTips(Action<bool, int> action)
        {
            MainWindow.index.ChooseTips(action, null);
        }
        public void Lodings()
        {
            MainWindow.index.Loding();
        }
        public void Lodings_close()
        {
            MainWindow.index.Loding_close();
        }

        public void AddTips(Action<InfoListModel> action, DialogClosingEventHandler e = null)
        {
            MainWindow.index.AddTips(action, e);
        }
        public void ShowExportLoding(string message)
        {
            MainWindow.index.ShowExport(message);
        }
        public void CloseExportLoding()
        {
            MainWindow.index.CloseExport();
        }
        public void ShowOK(string message)
        {
            MainWindow.index.ShowInfo(message);
        }
        #endregion

        #region 导入导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            autoRead_Timer.Stop();
            ChooseTips(ExportAction);
        }

        private void ExportAction(bool iscancel, int index)
        {
            if (iscancel)
            {
                autoRead_Timer.Start();
                return;
            }
            else
            {
                List<InfoListModel> lists = SettingJsonConfig.readData() ?? new List<InfoListModel>();
                if (TextBox_Name.Text != "")
                {
                    lists = lists.Where(u => u.userName.ToString() == TextBox_Name.Text).ToList();
                }
                if (TextBox_CardNo.Text != "")
                {
                    lists = lists.Where(u => u.cardNo.ToString() == TextBox_CardNo.Text).ToList();
                }
                if (dateTimeStart.Text != "")
                {
                    lists = lists.Where(u => DateTime.Parse(u.createTime) >= DateTime.Parse(dateTimeStart.Text)).ToList();
                }
                if (dateTimeEnd.Text != "")
                {
                    lists = lists.Where(u => DateTime.Parse(u.createTime) <= DateTime.Parse(dateTimeEnd.Text)).ToList();
                }
                if (index == 1)//选择导出Excel
                {
                    ExportExcel(lists);
                }
                else//导出json
                {
                    ExportJson(lists);
                }
            }
        }

        /// <summary>
        /// 导出本地excel
        /// </summary>
        /// <param name="lists"></param>
        public void ExportExcel(List<InfoListModel> lists)
        {
            try
            {
                string file = NPOIUtil.OpenSaveExcelDialog();
                if (file != null)
                {
                    Task.Run(() =>
                    {
                        this.Dispatcher.Invoke(() => { ShowExportLoding("数据正在导出...请勿关闭电源或录入信息"); });
                        DataTable dt = new DataTable();
                        Dictionary<string, string> header = NPOIUtil.InfoListModel2Head();
                        dt = NPOIUtil.List2DataTable(lists, header);
                        NPOIUtil.RenderDataTableToExcel(dt, file);
                        this.Dispatcher.Invoke(() =>
                        {
                            CloseExportLoding();
                            ShowOK("数据导出完成");
                        });
                    });
                }
            }
            catch (Exception ex)
            {
                CloseExportLoding();
                MessageTips(ex.Message);
            }
            finally
            {
                autoRead_Timer.Start();
            }
        }
        public void ExportJson(List<InfoListModel> lists)
        {
            try
            {
                string file = NPOIUtil.OpenSaveJsonDialog();
                if (file != null)
                {
                    Task.Run(() =>
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            ShowExportLoding("数据正在导出...请勿关闭电源或录入信息");
                        });
                        foreach (var item in lists)
                        {
                            item.versions = 1;
                        }
                        SettingJsonConfig.saveJson(lists, file);
                        this.Dispatcher.Invoke(() =>
                        {
                            CloseExportLoding();
                            ShowOK("数据导出完成");
                        });
                    });
                }
            }
            catch (Exception ex)
            {
                CloseExportLoding();
                MessageTips(ex.Message);
            }
            finally
            {
                autoRead_Timer.Start();
            }
        }
        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InData_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                autoRead_Timer.Stop();
                string file = NPOIUtil.OpenJsonDialog();
                if (file != null)
                {
                    Task.Run(() =>
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            ShowExportLoding("数据正在导入...请勿关闭电源或录入信息");
                        });
                        List<InfoListModel> newlists = SettingJsonConfig.readData(file) ?? new List<InfoListModel>();
                        List<InfoListModel> oldlists = SettingJsonConfig.readData() ?? new List<InfoListModel>();
                        foreach (var item in newlists)
                        {
                            if (!oldlists.Any(u => u.acidNo == item.acidNo))
                            {
                                oldlists.Add(item);
                            }
                        }
                        SettingJsonConfig.saveData(oldlists);
                        this.Dispatcher.Invoke(() =>
                        {
                            CloseExportLoding();
                            ShowOK("数据导入完成");
                        });
                    });
                }
            }
            catch (Exception ex)
            {
                MessageTips(ex.Message);
            }

            finally
            {
                autoRead_Timer.Start();
            }
        }
        #endregion

        #region 回传
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            CancelTips("回传数据将覆盖本地数据,请确认本地数据已同步成功,防止数据丢失", new Action<bool>(arg =>
            {
                if (arg)
                {
                    PassBack();
                }
            }));
        }

        public void PassBack()
        {
            try
            {
                ShowExportLoding("数据正在回传到本地...请勿关闭电源");
                BackList();
            }
            catch (Exception ex)
            {
                MessageTips(ex.Message);
            }
            finally
            {
                CloseExportLoding();
            }
        }
        /// <summary>
        /// 回传专用
        /// </summary>
        private void BackList()
        {
            Task.Run(() =>
            {
                try
                {
                    List<InfoListModel> allList = new List<InfoListModel>();
                    RequestInfoListModel requestInfoListModel = new RequestInfoListModel() { pageNo = 1, pageSize = 1 };
                    ResultJson<ResponseInfoListModel> resultJson = InfoListService.getQuery(requestInfoListModel);
                    if (resultJson.code == "20000")
                    {
                        int times = resultJson.data.total / 1000 + 1;
                        requestInfoListModel.pageSize = 1000;
                        for (int i = 1; i <= times; i++)
                        {
                            requestInfoListModel.pageNo = i;
                            resultJson = InfoListService.getQuery(requestInfoListModel);
                            List<InfoListModel> newList = ToBack(resultJson.data.data ?? new List<InfoListModel>());
                            allList.AddRange(newList);
                        }
                        allList.Reverse();
                        SettingJsonConfig.saveData(allList);
                        this.Dispatcher.Invoke(() => { ShowOK("数据回传完成"); });
                        return;
                    }
                    else
                    {
                        this.Dispatcher.Invoke(() => { ShowOK("数据回传失败,请稍后重试"); });
                        return;
                    }
                }
                catch (Exception ex)
                {
                    this.Dispatcher.Invoke(() => { ShowOK(ex.Message); });
                    return;
                }

            });
        }
        private List<InfoListModel> ToBack(List<InfoListModel> infoListModels)
        {
            if (infoListModels == null)
            {
                return null;
            }
            foreach (var item in infoListModels)
            {
                item.updateText = "编辑";
                item.iscancel = false;
                item.Editor_company = false;
                item.Editor_homeAddress = false;
                item.versions = 1;
            }
            return infoListModels;
        }
        #endregion
    }
}
