using Acid.common.Library.config;
using Acid.http.Library.ResponseModel;
using Acid.http.Library.Service;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using Nucleic_Acid.Util;
using Nucleic_Acid.View;
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
using System.Windows.Shapes;

namespace Nucleic_Acid
{
    /// <summary>
    /// Index.xaml 的交互逻辑
    /// </summary>
    public partial class Index : Window
    {
        //ReadCard V_readCard;
        InfoList V_infoList;
        public Index(string name)
        {
            InitializeComponent();
            Label_Name.Content = name + "-在线";
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
            //if (V_readCard == null)
            //{
            //    V_readCard = new ReadCard();
            //}
            //DataContext = V_readCard;
        }

        private void RadioButton_Click_1(object sender, RoutedEventArgs e)
        {
            if (V_infoList == null)
            {
                V_infoList = new InfoList();
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
        public async void TextTips(InfoListModel infoListModel, Action<InfoListModel> action, DialogClosingEventHandler e = null)
        {
            if (e == null)
                e = closingEventHandler;
            var textDialog = new TextDialog()
            {
                Text_CardAddress = { Text = infoListModel.address },
                Text_Name = { Text = infoListModel.userName },
                Text_Card = { Text = infoListModel.cardNo },
                Text_Sex = { Text = infoListModel.sex },
                Text_homeAddress = { Text = infoListModel.address }
            };
            await DialogHost.Show(textDialog, "ReadDialog");
            action(textDialog.InfoListModel);
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

        public async void LoadingTips(string message)
        {
            var sampleMessageDialog = new LodingText()
            {
                Message = { Text = message }
            };
            await DialogHost.Show(sampleMessageDialog, "ReadDialog");
        }
        public void ShowWarn(string name,string datetime)
        {
            SnackbarWarn.Message.Content = "提示："+ name + "已于"+ datetime + "进行核酸检测";
            Task.Factory.StartNew(showWarn);
        }
        private void showWarn()
        {
            this.Dispatcher.Invoke(() => { SnackbarWarn.IsActive = true; });
            Thread.Sleep(10000);
            this.Dispatcher.Invoke(() => { SnackbarWarn.IsActive = false; });
        }
        public async void Loding()
        {
            var sampleMessageDialog = new SampleProgressDialog();
            await DialogHost.Show(sampleMessageDialog, "ReadDialog_new");
        }
        public void Loding_close()
        {
            try
            {
                DialogHost.Close("ReadDialog_new");
            }
            catch (Exception ex)
            {
                MessageTips(ex.Message);
            }
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
                     SettingJsonConfig.saveJson(json);
                     MainWindow main = new MainWindow();
                     main.Show();
                     this.Close();
                 }
             }));

        }
        /// <summary>
        /// 退出程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void about_MouseUp(object sender, MouseButtonEventArgs e)
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

        /// <summary>
        /// 同步本地线上
        /// </summary>
        private void synchronization()
        {
            //LoadingTips("正在同步数据...");
            Task.Factory.StartNew(synchronousData);
        }
        private void synchronousData()
        {
            try
            {
                List<InfoListModel> lists = SettingJsonConfig.readData() ?? new List<InfoListModel>();

                if (lists.Count > 0)
                {
                    synchronousAdd(lists);//同步新增
                    synchronousUpdate(lists);//同步修改的
                }
                this.Dispatcher.Invoke(() =>
                {
                    SnackbarOK.IsActive = true;
                    SnackbarLoding.IsActive = false;
                    lodingBar.Visibility = Visibility.Hidden;
                });
                Thread.Sleep(5000);//5秒后自动关闭
                this.Dispatcher.Invoke(() =>
                {
                    SnackbarOK.IsActive = false;
                });
            }
            catch (Exception ex)
            {
                Util.Logger.Default.Error(ex.Message);
            }
        }

        private void synchronousAdd(List<InfoListModel> lists)
        {
            List<InfoListModel> newlist = lists.Where(u => u.versions == 0).ToList();
            string str = JsonConvert.SerializeObject(newlist);
            List<InfoListModel> lists1 = JsonConvert.DeserializeObject<List<InfoListModel>>(str);
            int count = lists1.Count();
            int page = count / 1000 + 1;
            ResultJson<string> resultJson = new ResultJson<string>() { code = "1" };
            for (int i = 1; i <= page; i++)
            {
                List<InfoListModel> data = lists1.Skip((i - 1) * 1000).Take(1000).ToList();
                resultJson = InfoListService.addNucleic(data);
            }
            if (resultJson.code == "20000")
            {
                foreach (var item in lists.Where(u => u.versions == 0).ToList())
                {
                    item.versions = 1;
                }
                SettingJsonConfig.saveData(lists);
                Console.WriteLine("新增:" + newlist.Count + "条数据");
            }
        }
        private void synchronousUpdate(List<InfoListModel> lists)
        {
            List<InfoListModel> newlist = lists.Where(u => u.versions == 3).ToList();
            string str = JsonConvert.SerializeObject(newlist);
            List<InfoListModel> lists1 = JsonConvert.DeserializeObject<List<InfoListModel>>(str);
            int count = lists1.Count();
            int page = count / 1000 + 1;
            ResultJson<string> resultJson = new ResultJson<string>() { code = "1" };
            for (int i = 1; i <= page; i++)
            {
                List<InfoListModel> data = lists1.Skip((i - 1) * 1000).Take(1000).ToList();
                resultJson = InfoListService.updateNucleic(lists1);
            }
            if (resultJson.code == "20000")
            {
                foreach (var item in lists.Where(u => u.versions == 3).ToList())
                {
                    item.versions = 1;
                }
                SettingJsonConfig.saveData(lists);
                Console.WriteLine("更新:" + newlist.Count + "条数据");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //SettingModel settingModel = SettingJsonConfig.readJson() ?? new SettingModel();
            //autoPrint.IsChecked = settingModel.AutoPrint;
            UrlModel.autoPrint = true;
            synchronization();
            if (V_infoList == null)
            {
                V_infoList = new InfoList();
            }
            DataContext = V_infoList;
        }

        private void SnackbarMessage_ActionClick(object sender, RoutedEventArgs e)
        {
            SnackbarOK.IsActive = false;
        }

        private void autoPrint_Checked(object sender, RoutedEventArgs e)
        {
            //SettingModel settingModel = SettingJsonConfig.readJson() ?? new SettingModel();
            //settingModel.AutoPrint = autoPrint.IsChecked ?? false;
            //UrlModel.autoPrint = settingModel.AutoPrint;
            //SettingJsonConfig.saveJson(settingModel);
        }
        /// <summary>
        /// 转到离线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void turn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            MainWindow.indexoffline = new IndexOffline(CommonHelper.detectionName);
            MainWindow.indexoffline.Show();
            this.Close();
        }
    }
}
