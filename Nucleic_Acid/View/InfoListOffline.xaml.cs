using Acid.common.Library.config;
using Acid.http.Library.RequestModel;
using Nucleic_Acid.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private string staticName = "";
        private string staticCardNo = "";
        private string staticTestValue = "-1";
        public InfoListOffline()
        {
            InitializeComponent();
            pageControl.OnPagesChanged += PageControl_OnPagesChanged;
            InitDataGrid();
        }

        private void PageControl_OnPagesChanged(object sender, WpfPaging.PagesChangedArgs e)
        {
            QuerySelect_page(((PagingControl)sender).CurrentPage);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }
        private void Init()
        {
            
        }

        private void SetInfoList(RequestInfoListModel requestInfoListModel)
        {
            List<InfoListModel> lists = SettingJsonConfig.readData();
            if (lists!=null)
            {
                if (requestInfoListModel.testValue!=null)
                {
                    lists = lists.Where(u => u.testingValue.ToString() == requestInfoListModel.testValue).ToList();
                }
                if (requestInfoListModel.name!=null)
                {
                    lists = lists.Where(u => u.userName.ToString() == requestInfoListModel.name).ToList();
                }
                if (requestInfoListModel.cardNo!=null)
                {
                    lists = lists.Where(u => u.cardNo.ToString() == requestInfoListModel.cardNo).ToList();
                }
                List<InfoListModel> data = lists.Skip((requestInfoListModel.pageNo - 1) * requestInfoListModel.pageSize).Take(requestInfoListModel.pageSize).ToList();
                pageControl.DataTote = lists.Count();
                pageControl.CurrentPage = requestInfoListModel.pageNo;
                //添加序号
                int current = 1;
                foreach (var item in data)
                {
                    item.index = current;
                    current++;
                }
                dataGrid.ItemsSource = data;
            }
            else
            {
                dataGrid.ItemsSource = new List<InfoListModel>();
            }
            
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
        /// 改变检测状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataResult_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_update_Click(object sender, RoutedEventArgs e)
        {

        }
    }
    }
