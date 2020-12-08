using Acid.common.Library.config;
using Acid.http.Library.RequestModel;
using Acid.http.Library.ResponseModel;
using MaterialDesignThemes.Wpf;
using Nucleic_Acid.View;
using System;
using System.Collections.Generic;
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
;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            ResultJson<LoginModel> dictionaries = Login_Ex(userNameBox.Text, passWordBox.Password, CheckBox_isRember.IsChecked, CheckBox_isAuto.IsChecked);
            if (dictionaries.code == "20000")
            {
                this.Hide();
                index = new Index();
                index.Show();
            }
            else
            {
                MessageTips(dictionaries.message, sender, e);
            }
        }

        private ResultJson<LoginModel> Login(string username, string password)
        {
            try
            {
                string url = UrlModel.ip + UrlModel.login;
                RequestLoginModel requestLoginModel = new RequestLoginModel(username, password);
                string result = HttpUrlConfig.PostBody(url, requestLoginModel);
                ResultJson<LoginModel> retStu = Newtonsoft.Json.JsonConvert.DeserializeObject<ResultJson<LoginModel>>(result);
                Console.WriteLine(result);
                return retStu;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
        private ResultJson<LoginModel> Login_Ex(string username, string password, bool? rem = false, bool? auto = false)
        {
            ResultJson<LoginModel> resultJson = Login(username, password);
            if (resultJson == null)
            {
                return new ResultJson<LoginModel>() { code = "1", message = "网络连接异常" };
            }
            else if (resultJson.code == "20000")
            {
                SettingModel json = SettingJsonConfig.readJson();
                if ((bool)rem)//保存
                {
                    json.userName = username;
                    json.passWord = password;
                    json.isRember = (bool)rem;
                    json.isAuto = (bool)auto;
                    SettingJsonConfig.saveJson(json);
                }
                else
                {
                    json.userName = "";
                    json.passWord = "";
                    json.isRember = false;
                    json.isAuto = false;
                    SettingJsonConfig.saveJson(json);
                }
                return resultJson;
            }
            else
            {
                return resultJson;
            }
        }

        public async void MessageTips(string message, object sender = null, RoutedEventArgs e = null)
        {
            var sampleMessageDialog = new MessageDialog()
            {
                Message = { Text = message }
            };
            await DialogHost.Show(sampleMessageDialog, "LoginDialog");
        }
    }
}
