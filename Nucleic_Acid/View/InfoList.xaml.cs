using Acid.common.Library.config;
using Acid.http.Library.RequestModel;
using Acid.http.Library.ResponseModel;
using Acid.http.Library.Service;
using Acid.print.Library;
using MaterialDesignThemes.Wpf;
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
    /// InfoList.xaml 的交互逻辑
    /// </summary>
    public partial class InfoList : UserControl
    {
        private string staticName = "";
        private string staticCardNo = "";
        private string staticTestValue = "-1";
        private bool ispage = true;//是否触发分页查询
        public InfoList()
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
            ispage = true;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }
        private void Init()
        {

        }
        private void SetInfoList(RequestInfoListModel requestInfoListModel, bool show = true, bool hide = true)
        {
            if (show)
                lodings();
            Task.Run(() =>
            {
                ResultJson<ResponseInfoListModel> resultJson = InfoListService.getQuery(requestInfoListModel);
                Thread.Sleep(500);
                this.Dispatcher.Invoke(() =>
                {
                    if (resultJson.code == "20000")
                    {
                        pageControl.DataTote = resultJson.data.total;
                        dataGrid.ItemsSource = ToDataGrid(resultJson.data.addIndex(resultJson.data.data) ?? new List<Acid.http.Library.ResponseModel.InfoListModel>());
                    }
                    else
                    {
                        dataGrid.ItemsSource = ToDataGrid(new List<Acid.http.Library.ResponseModel.InfoListModel>());
                    }
                    if (hide)
                        lodings_close();
                });
            });
        }

        private void InitDataGrid()
        {
            ispage = false;
            SetInfoList(new RequestInfoListModel(1, 10));
        }

        private void QuerySelect_page(int page, bool show = true, bool hide = true)
        {
            ispage = false;
            RequestInfoListModel requestInfoListModel = new RequestInfoListModel()
            {
                pageNo = page,
                pageSize = pageControl.PageSize,
                cardNo = staticCardNo == "" ? null : staticCardNo,
                name = staticName == "" ? null : staticName,
                testValue = staticTestValue == "-1" ? null : staticTestValue
            };
            SetInfoList(requestInfoListModel, show, hide);
        }

        private void QuerySelect_click(int page)
        {
            ispage = false;
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

        public void MessageTips(string message)
        {
            MainWindow.index.MessageTips(message);
        }

        public void CancelTips(string message, Action<bool> action, DialogClosingEventHandler e = null)
        {
            MainWindow.index.CancelTips(message, action, e);
        }
        public void lodings()
        {
            MainWindow.index.Loding();
        }
        public void lodings_close()
        {
            MainWindow.index.Loding_close();
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
            staticTestValue = "-1";
            TextBox_CardNo.Clear();
            TextBox_Name.Clear();
            ComboBox_TestValue.SelectedIndex = -1;
            InitDataGrid();
        }

        /// <summary>
        /// 条件查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
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
            if (tag.Content.ToString() == "修改")
            {
                Acid.http.Library.ResponseModel.InfoListModel obj = (Acid.http.Library.ResponseModel.InfoListModel)dataGrid.SelectedItem;
                obj.Editor = true;
                List<Acid.http.Library.ResponseModel.InfoListModel> source = ToDataGrid((List<Acid.http.Library.ResponseModel.InfoListModel>)dataGrid.ItemsSource);
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
                         List<Acid.http.Library.ResponseModel.InfoListModel> infoListModels = new List<Acid.http.Library.ResponseModel.InfoListModel>();
                         Acid.http.Library.ResponseModel.InfoListModel obj = (Acid.http.Library.ResponseModel.InfoListModel)dataGrid.SelectedItem;
                         infoListModels.Add(obj);
                         #region 服务端更新
                         ResultJson<string> resultJson = InfoListService.updateNucleic(infoListModels);
                         #endregion
                         if (resultJson.code == "20000")
                         {
                             #region 本地更新
                             List<Acid.common.Library.config.InfoListModel> lists = SettingJsonConfig.readData() ?? new List<Acid.common.Library.config.InfoListModel>();
                             if (lists.Where(u => u.acidNo == obj.acidNo).Count() > 0)
                             {
                                 lists.Where(u => u.acidNo == obj.acidNo).SingleOrDefault().testingValue = obj.testingValue;
                             }
                             SettingJsonConfig.saveData(lists);
                             #endregion
                             obj.Editor = false;
                             List<Acid.http.Library.ResponseModel.InfoListModel> source = ToDataGrid((List<Acid.http.Library.ResponseModel.InfoListModel>)dataGrid.ItemsSource);
                             obj.updateText = "修改";
                             dataGrid.ItemsSource = null;
                             dataGrid.ItemsSource = source;
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
                    Acid.http.Library.ResponseModel.InfoListModel obj = (Acid.http.Library.ResponseModel.InfoListModel)dataGrid.SelectedItem;
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
                    #region 本地删除
                    Acid.http.Library.ResponseModel.InfoListModel obj = (Acid.http.Library.ResponseModel.InfoListModel)dataGrid.SelectedItem;
                    List<Acid.common.Library.config.InfoListModel> lists = SettingJsonConfig.readData() ?? new List<Acid.common.Library.config.InfoListModel>();
                    Acid.common.Library.config.InfoListModel infoListModel = lists.Where(u => u.acidNo == obj.acidNo).SingleOrDefault();
                    lists.Remove(infoListModel);//移除
                    SettingJsonConfig.saveData(lists);//保存
                    #endregion
                    #region 服务器删除
                    ResultJson<string> resultJson = InfoListService.deleteNucleic(new Acid.http.Library.ResponseModel.InfoListModel() { acidNo = obj.acidNo });
                    #endregion
                    QuerySelect_page(pageControl.CurrentPage, false, false);
                    //删除
                    Console.WriteLine("删除：" + obj.acidNo);
                }
            }));
        }
        /// <summary>
        /// 修改检测结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (sender as ComboBox);
            Acid.http.Library.ResponseModel.InfoListModel obj = (Acid.http.Library.ResponseModel.InfoListModel)dataGrid.SelectedItem;
            if (obj != null)
            {
                obj.testingValue = comboBox.SelectedIndex;
            }
        }

        private List<Acid.http.Library.ResponseModel.InfoListModel> ToDataGrid(List<Acid.http.Library.ResponseModel.InfoListModel> infoListModels)
        {
            foreach (var item in infoListModels)
            {
                item.sex = CommonHelper.ToSex(int.Parse(item.sex));
            }
            return infoListModels;
        }
    }
}
