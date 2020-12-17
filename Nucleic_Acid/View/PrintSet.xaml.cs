using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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
    /// PrintSet.xaml 的交互逻辑
    /// </summary>
    public partial class PrintSet : UserControl
    {
        public PrintSet()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(SetPrintList);
        }

        public List<string> GetPrintList()
        {
            List<string> lt = new List<string>();
            LocalPrintServer printServer = new LocalPrintServer();
            PrintQueueCollection printQueuesOnLocalServer = printServer.GetPrintQueues(new[] { EnumeratedPrintQueueTypes.Local });
            foreach (PrintQueue printer in printQueuesOnLocalServer)
                lt.Add(printer.Name);
            return lt;
        }

        private void SetPrintList() 
        {
            try
            {
                List<string> lists = GetPrintList();
                this.Dispatcher.Invoke(() =>
                {
                    ComboBox_printList.ItemsSource = lists;
                    Console.WriteLine(ComboBox_printList.Text);
                });
            }
            catch (Exception ex)
            {
                Util.Logger.Default.Error(ex.Message);
            }
        }
    }
}
