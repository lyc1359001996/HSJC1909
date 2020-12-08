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
    /// InfoList.xaml 的交互逻辑
    /// </summary>
    public partial class InfoList : UserControl
    {
        public InfoList()
        {
            InitializeComponent();
            pageControl.OnPagesChanged += PageControl_OnPagesChanged;
        }

        private void PageControl_OnPagesChanged(object sender, WpfPaging.PagesChangedArgs e)
        {
            Console.WriteLine(((PagingControl)sender).CurrentPage);
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }
        private void Init()
        {
            List<ReadInfoModel> readInfoModels = new List<ReadInfoModel>();
            ReadInfoModel Items2;
            for (int i = 0; i < 100; i++)
            {
                Items2 = new ReadInfoModel()
                {
                    index = i,
                    code = "123",
                    name = "爸爸",
                    idCard = "33041119981190011",
                    address = "浙江省金华市义乌市稠江街道总部经济园",
                    date = "2020-12-05 12:00:00",
                    sex = "男"
                };
                readInfoModels.Add(Items2);
            }
            dataGrid.ItemsSource = readInfoModels;
        }
    }
}
