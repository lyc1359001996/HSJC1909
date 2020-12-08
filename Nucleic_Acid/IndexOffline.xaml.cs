using MaterialDesignThemes.Wpf;
using Nucleic_Acid.View;
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
using System.Windows.Shapes;

namespace Nucleic_Acid
{
    /// <summary>
    /// IndexOffline.xaml 的交互逻辑
    /// </summary>
    public partial class IndexOffline : Window
    {
        ReadCardOffline V_readCard;
        InfoListOffline V_infoList;
        public IndexOffline()
        {
            InitializeComponent();
            if (V_readCard == null)
            {
                V_readCard = new ReadCardOffline();
            }
            DataContext = V_readCard;
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            // 获取鼠标相对标题栏位置  
            Point position = e.GetPosition(backGrid);
            // 如果鼠标位置在标题栏内，允许拖动  
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (position.X >= 0 && position.X < backGrid.ActualWidth && position.Y >= 0 && position.Y < backGrid.ActualHeight)
                {
                    this.DragMove();
                }
            }
        }

        private void RadioButton_Click(object sender, RoutedEventArgs e)
        {
            MessageTips("ggod", sender, e);
            if (V_readCard == null)
            {
                V_readCard = new ReadCardOffline();
            }
            DataContext = V_readCard;
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            MessageTips("ggod", sender, e);
            if (V_infoList == null)
            {
                V_infoList = new InfoListOffline();
            }
            DataContext = V_infoList;
        }

        public async void MessageTips(string message, object sender = null, RoutedEventArgs e = null)
        {
            var sampleMessageDialog = new ReadDialog()
            {
                Message = { Text = message }
            };
            await DialogHost.Show(sampleMessageDialog, "ReadDialog");
        }
    }
}
