using Acid.common.Library.config;
using Acid.http.Library.RequestModel;
using Acid.http.Library.ResponseModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using MaterialDesignThemes.Wpf;
using Nucleic_Acid.Model;
using Nucleic_Acid.View;
using System;
using System.Windows;
using System.Windows.Input;

namespace Nucleic_Acid.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        public RelayCommand<Window> Exit_Click { get; set; }
        public RelayCommand<Window> Offline_Click { get; set; }
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            Exit_Click = new RelayCommand<Window>(exit);
            Offline_Click = new RelayCommand<Window>(offline);
        }

        private void offline(Window obj)
        {
            if (obj != null)
            {
                obj.Hide();
            }
            MainWindow.index = new Index();
            MainWindow.index.Show();
        }

        private void exit(Window window) 
        {
            //MessageTips("ÍË³ö", null, null);
            //MessageBox.Show("ÍË³ö");
            if (window!=null)
            {
                window.Close();
            }
        }
    }
}