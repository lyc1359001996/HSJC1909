using Acid.common.Library.config;
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

namespace Nucleic_Acid.View
{
    /// <summary>
    /// AddDialog.xaml 的交互逻辑
    /// </summary>
    public partial class AddDialog : UserControl
    {
        public InfoListModel InfoListModel = new InfoListModel();
        public AddDialog()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Text_homeAddress.SelectAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            InfoListModel.homeAddress = Text_homeAddress.Text;
            InfoListModel.company = Text_company.Text;
            InfoListModel.userName = Text_Name.Text;
            InfoListModel.sex = Text_Sex.Text != "男" ? "女" : "男";
            InfoListModel.cardNo = Text_Card.Text;
            InfoListModel.address = Text_CardAddress.Text;
            InfoListModel.iscancel = false;
        }

        private void Message_KeyDown(object sender, KeyEventArgs e)
        {
            Button_Click(this.aceept, null);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            InfoListModel.iscancel = true;
        }
    }

}
