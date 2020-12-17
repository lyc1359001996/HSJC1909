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
        private string detectionName = CommonHelper.detectionName;
        private string staticName = "";
        private string staticCardNo = "";
        private bool isfirt = false;
        public InfoList()
        {
            InitializeComponent();
            pageControl.OnPagesChanged += PageControl_OnPagesChanged;
            //InitDataGrid();
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
                ResultJson<ResponseInfoListModel> resultJson = InfoListService.getQuery(requestInfoListModel);
                this.Dispatcher.Invoke(() =>
                {
                    if (resultJson.code == "20000")
                    {
                        pageControl.DataTote = resultJson.data.total;
                        dataGrid.ItemsSource = ToDataGrid(resultJson.data.addIndex(resultJson.data.data) ?? new List<Acid.http.Library.ResponseModel.InfoListModel>());
                        loding.Visibility = Visibility.Hidden;
                    }
                    else
                    {
                        MessageTips(resultJson.message);
                        dataGrid.ItemsSource = ToDataGrid(new List<Acid.http.Library.ResponseModel.InfoListModel>());
                        loding.Visibility = Visibility.Hidden;
                    }
                });
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
                name = TextBox_Name.Text == "" ? null : TextBox_Name.Text,
            };
            SetInfoList(requestInfoListModel);
            //绑定静态值
            staticName = TextBox_Name.Text;
            staticCardNo = TextBox_CardNo.Text;
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
                Acid.http.Library.ResponseModel.InfoListModel obj = (Acid.http.Library.ResponseModel.InfoListModel)dataGrid.SelectedItem;
                obj.Editor_homeAddress = true;
                obj.Editor_workUnit = true;
                List<Acid.http.Library.ResponseModel.InfoListModel> source = (List<Acid.http.Library.ResponseModel.InfoListModel>)dataGrid.ItemsSource;
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
                         List<Acid.http.Library.ResponseModel.InfoListModel> infoListModels = new List<Acid.http.Library.ResponseModel.InfoListModel>();
                         Acid.http.Library.ResponseModel.InfoListModel obj = (Acid.http.Library.ResponseModel.InfoListModel)dataGrid.SelectedItem;
                         obj.updateName = detectionName;
                         obj.updateTime = thisTime;
                         infoListModels.Add(obj);
                         #region 服务端更新
                         ResultJson<string> resultJson = InfoListService.updateNucleic(ToList(infoListModels));
                         #endregion
                         if (resultJson.code == "20000")
                         {
                             #region 本地更新
                             List<Acid.common.Library.config.InfoListModel> lists = SettingJsonConfig.readData() ?? new List<Acid.common.Library.config.InfoListModel>();
                             if (lists.Where(u => u.acidNo == obj.acidNo).Count() > 0)
                             {
                                 Acid.common.Library.config.InfoListModel infoListModel = lists.Where(u => u.acidNo == obj.acidNo).SingleOrDefault();
                                 infoListModel.testingValue = obj.testingValue;
                                 infoListModel.updateName = detectionName;
                                 infoListModel.updateTime = thisTime;
                             }
                             SettingJsonConfig.saveData(lists);
                             #endregion
                             obj.Editor_homeAddress = false;
                             obj.Editor_workUnit = false;
                             List<Acid.http.Library.ResponseModel.InfoListModel> source = (List<Acid.http.Library.ResponseModel.InfoListModel>)dataGrid.ItemsSource;
                             obj.updateText = "修改";
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
        //private void delete_click(object sender, RoutedEventArgs e)
        //{
        //    CancelDelete();
        //}
        //private void CancelDelete()
        //{
        //    CancelTips("确认要删除?", new Action<bool>(arg =>
        //    {
        //        if (arg)
        //        {
        //            #region 本地删除
        //            Acid.http.Library.ResponseModel.InfoListModel obj = (Acid.http.Library.ResponseModel.InfoListModel)dataGrid.SelectedItem;
        //            List<Acid.common.Library.config.InfoListModel> lists = SettingJsonConfig.readData() ?? new List<Acid.common.Library.config.InfoListModel>();
        //            List<Acid.common.Library.config.InfoListModel> infoListModel = lists.Where(u => u.acidNo == obj.acidNo).ToList();
        //            foreach (var item in infoListModel)
        //            {
        //                lists.Remove(item);//移除
        //            }
        //            SettingJsonConfig.saveData(lists);//保存
        //            #endregion
        //            #region 服务器删除
        //            ResultJson<string> resultJson = InfoListService.deleteNucleic(new Acid.http.Library.ResponseModel.InfoListModel() { acidNo = obj.acidNo });
        //            #endregion
        //            QuerySelect_page(pageControl.CurrentPage);
        //            //删除
        //            Console.WriteLine("删除：" + obj.acidNo);
        //        }
        //    }));
        //}
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
                item.sex = CommonHelper.ToSex(item.sex);
            }
            return infoListModels;
        }
        private List<Acid.http.Library.ResponseModel.InfoListModel> ToList(List<Acid.http.Library.ResponseModel.InfoListModel> infoListModels)
        {
            foreach (var item in infoListModels)
            {
                item.sex = CommonHelper.SexToInt(item.sex);
            }
            return infoListModels;
        }
    }
}
