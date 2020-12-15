using Acid.common.Library.config;
using Acid.http.Library.RequestModel;
using Acid.print.Library;
using Nucleic_Acid.Model;
using Nucleic_Acid.Util;
using System;
using System.Collections.Generic;
using System.Linq;
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
using WpfPaging;

namespace Nucleic_Acid.View
{
    /// <summary>
    /// InfoListOffline.xaml 的交互逻辑
    /// </summary>
    public partial class InfoListOffline : UserControl
    {
        private string detectionName = CommonHelper.detectionName;
        private string staticName = "";
        private string staticCardNo = "";
        private string staticTestValue = "-1";
        private bool ispage = false;
        public InfoListOffline()
        {
            InitializeComponent();
            pageControl.OnPagesChanged += PageControl_OnPagesChanged;
            InitDataGrid();
        }

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

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
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
                    if (requestInfoListModel.testValue != null)
                    {
                        lists = lists.Where(u => u.testingValue.ToString() == requestInfoListModel.testValue).ToList();
                    }
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
                    foreach (var item in data)
                    {
                        item.index = current;
                        current++;
                    }
                    this.Dispatcher.Invoke(() =>
                    {
                        pageControl.DataTote = lists.Count();
                        pageControl.CurrentPage = requestInfoListModel.pageNo;
                        dataGrid.ItemsSource = ToDataGrid(data);
                        loding.Visibility = Visibility.Hidden;

                    });
                }
                else
                {
                    this.Dispatcher.Invoke(() =>
                    {
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
                name = staticName == "" ? null : staticName,
                testValue = staticTestValue == "-1" ? null : staticTestValue
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
                testValue = ComboBox_TestValue.SelectedIndex == -1 ? null : ComboBox_TestValue.SelectedIndex.ToString()
            };
            SetInfoList(requestInfoListModel);
            //绑定静态值
            staticName = TextBox_Name.Text;
            staticCardNo = TextBox_CardNo.Text;
            staticTestValue = ComboBox_TestValue.SelectedIndex.ToString();
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
            staticTestValue = "-1";
            TextBox_CardNo.Clear();
            TextBox_Name.Clear();
            ComboBox_TestValue.SelectedIndex = -1;
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
        /// 改变检测状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (sender as ComboBox);
            InfoListModel obj = (InfoListModel)dataGrid.SelectedItem;
            if (obj != null)
            {
                obj.testingValue = comboBox.SelectedIndex;
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_update_Click(object sender, RoutedEventArgs e)
        {
            Button tag = (sender as Button);
            if (tag.Content.ToString() == "修改")
            {
                InfoListModel obj = (InfoListModel)dataGrid.SelectedItem;
                obj.Editor = true;
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
                        infoListModel.testingValue = obj.testingValue;
                        infoListModel.updateName = detectionName;
                        infoListModel.updateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        infoListModel.versions = obj.versions == 0 ? 0 : 3;
                        SettingJsonConfig.saveData(lists);
                        obj.Editor = false;
                        List<InfoListModel> source = (List<InfoListModel>)dataGrid.ItemsSource;
                        obj.updateText = "修改";
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
        public void MessageTips(string message)
        {
            MainWindow.indexoffline.MessageTips(message);
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
                    InfoListModel obj = (InfoListModel)dataGrid.SelectedItem;
                    List<InfoListModel> lists = SettingJsonConfig.readData() ?? new List<InfoListModel>();
                    InfoListModel infoListModel = lists.Where(u => u.acidNo == obj.acidNo).SingleOrDefault();
                    lists.Remove(infoListModel);//移除
                    SettingJsonConfig.saveData(lists);//保存
                    QuerySelect_page(pageControl.CurrentPage);
                    //删除
                    Console.WriteLine("删除：" + obj.acidNo);
                }
            }));
        }

        private List<Acid.http.Library.ResponseModel.InfoListModel> ToDataGrid(List<Acid.http.Library.ResponseModel.InfoListModel> infoListModels)
        {
            foreach (var item in infoListModels)
            {
                item.sex = CommonHelper.ToSex(item.sex);
            }
            return infoListModels;
        }

        private List<InfoListModel> ToDataGrid(List<InfoListModel> infoListModels)
        {
            foreach (var item in infoListModels)
            {
                item.sex = CommonHelper.ToSex(item.sex);
            }
            return infoListModels;
        }
    }
}
