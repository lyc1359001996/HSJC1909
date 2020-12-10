using Acid.common.Library.config;
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
            if (V_readCard == null)
            {
                V_readCard = new ReadCardOffline();
            }
            DataContext = V_readCard;
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (V_infoList == null)
            {
                V_infoList = new InfoListOffline();
            }
            DataContext = V_infoList;
        }
        /// <summary>
        /// 消息弹窗
        /// </summary>
        /// <param name="message"></param>
        public async void MessageTips(string message)
        {
            var sampleMessageDialog = new MessageDialog()
            {
                Message = { Text = message }
            };
            await DialogHost.Show(sampleMessageDialog, "ReadDialog");
        }
        /// <summary>
        /// 确定取消弹窗
        /// </summary>
        /// <param name="message"></param>
        /// <param name="action"></param>
        /// <param name="e"></param>
        public async void CancelTips(string message, Action<bool> action, DialogClosingEventHandler e = null)
        {
            if (e == null)
                e = closingEventHandler;
            var sampleMessageDialog = new CanCancel()
            {
                Message = { Text = message }
            };
            await DialogHost.Show(sampleMessageDialog, "ReadDialog", e);
            action(sampleMessageDialog.IsTrue);
        }
        private void closingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (eventArgs.Parameter is bool parameter &&
                parameter == false) return;

            //OK, lets cancel the close...
            eventArgs.Cancel();

            //...now, lets update the "session" with some new content!
            eventArgs.Session.UpdateContent(new SampleProgressDialog());
            //note, you can also grab the session when the dialog opens via the DialogOpenedEventHandler

            //lets run a fake operation for 3 seconds then close this baby.
            Task.Delay(TimeSpan.FromMilliseconds(500))
                .ContinueWith((t, _) => eventArgs.Session.Close(false), null,
                    TaskScheduler.FromCurrentSynchronizationContext());
        }
        /// <summary>
        /// 重新加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void reload_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CancelTips("确定要重新登录吗？", new Action<bool>(arg =>
            {
                if (arg)
                {
                    SettingModel json = SettingJsonConfig.readJson() ?? new SettingModel();
                    json.isAuto = false;
                    SettingJsonConfig.saveJson(json);
                    MainWindow main = new MainWindow();
                    main.Show();
                    MainWindow.indexoffline.Close();
                }
            }));

        }
        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void close_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CancelTips("确定要关闭程序吗?", new Action<bool>(arg =>
            {
                if (arg)
                {
                    Application.Current.Shutdown();
                }
            }));

        }
        /// <summary>
        /// 最小化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Min(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Click_Close(object sender, RoutedEventArgs e)
        {
            CancelTips("确定要关闭程序吗?", new Action<bool>(arg =>
            {
                if (arg)
                {
                    Application.Current.Shutdown();
                }
            }));
        }

        private void Click_Max(object sender, RoutedEventArgs e)
        {
            this.WindowState = this.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        }
    }
}
