using Acid.common.Library.config;
using Acid.http.Library.RequestModel;
using Acid.http.Library.ResponseModel;
using Acid.http.Library.Service;
using MaterialDesignThemes.Wpf;
using Nucleic_Acid.Util;
using Nucleic_Acid.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Nucleic_Acid
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        SettingModel settingModel = new SettingModel();
        public static Index index;
        public static IndexOffline indexoffline;
        public MainWindow()
        {
            InitializeComponent();
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

        private void backGrid_Loaded(object sender, RoutedEventArgs e)
        {
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Init()
        {
            Util.Logger.Default.Info("------------------------start------------------------------");
            SettingModel settingModel1 = SettingJsonConfig.readJson();
            settingModel = settingModel1 == null ? new SettingModel() : settingModel1;
            userNameBox.Text = settingModel.userName == null ? "" : settingModel.userName;
            passWordBox.Password = settingModel.passWord == null ? "" : settingModel.passWord;
            CheckBox_isRember.IsChecked = settingModel.isRember;
            CheckBox_isAuto.IsChecked = settingModel.isAuto;
            if (settingModel.isAuto)//自动登录
            {
                Login_Click(null, null);
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LogindTips();
            string username = userNameBox.Text;
            string password = passWordBox.Password;
            bool? isRember = CheckBox_isRember.IsChecked;
            bool? isAuto = CheckBox_isAuto.IsChecked;
            Task.Run(() =>
            {
                Thread.Sleep(500);//看效果
                ResultJson<LoginModel> dictionaries = LoginService.Login_Ex(username, password, isRember, isAuto);
                this.Dispatcher.Invoke(() =>
                {
                    DialogHost.Close("LoginDialog");
                    if (dictionaries.code == "20000")
                    {
                        index = new Index(username);
                        index.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageTips(dictionaries.message, sender, e);
                    }
                });
            });

        }


        public async void MessageTips(string message, object sender = null, RoutedEventArgs e = null)
        {
            var sampleMessageDialog = new MessageDialog()
            {
                Message = { Text = message }
            };
            await DialogHost.Show(sampleMessageDialog, "LoginDialog");
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
            await DialogHost.Show(sampleMessageDialog, "LoginDialog", e);
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

        public async void LogindTips()
        {
            var sampleMessageDialog = new SampleProgressDialog();
            await DialogHost.Show(sampleMessageDialog, "LoginDialog");
        }

        private void Wmain_Loaded(object sender, RoutedEventArgs e)
        {
            CheckForUpdates();
        }
        private void CheckForUpdates()
        {
            //dynamic temp = UpdateUtil.HasNewVersion();
            //if (Convert.ToBoolean(temp.result))
            //{
            //    //CancelTips("检测到有新版本可用，是否进行更新", CheckUpdate);
            //}
        }

        private void CheckUpdate(bool obj)
        {
            if (obj)
            {
                if (UpdateUtil.IsSystemPath() && !UpdateUtil.IsAdministrator())
                {
                    UpdateUtil.ReStartAppWithAdministrator();
                }
                else
                {
                    ProcessStartInfo p = new ProcessStartInfo();
                    p.FileName = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"\  \Nucleic_Acid.exe";
                    p.UseShellExecute = false;
                    p.WorkingDirectory = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
                    p.CreateNoWindow = true;
                    Process.Start(p);
                    Application.Current.Shutdown();
                    //TimeHelper.flagnotifyIcon1 = false;
                }
            }
        }
    }
}
