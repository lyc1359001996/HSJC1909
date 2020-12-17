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
    /// TextDialog.xaml 的交互逻辑
    /// </summary>
    public partial class TextDialog : UserControl
    {
        public string address = "";
        public TextDialog()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Message.SelectAll();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            address = Message.Text;
        }

        private void Message_KeyDown(object sender, KeyEventArgs e)
        {
            Button_Click(this.aceept, null);
        }
    }
}
