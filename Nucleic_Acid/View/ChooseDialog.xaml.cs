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
    /// ChooseDialog.xaml 的交互逻辑
    /// </summary>
    public partial class ChooseDialog : UserControl
    {
        public int ChooseIndex = 1;
        public bool isCancel = false;
        public ChooseDialog()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            isCancel = false;
            if (radio1.IsChecked??false)
            {
                ChooseIndex = 1;
            }
            else
            {
                ChooseIndex = 0;
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            isCancel = true;
        }
    }
}
