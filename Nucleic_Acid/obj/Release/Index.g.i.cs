﻿#pragma checksum "..\..\Index.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "6234F51EB91BE3294F6642EAAE8EC685D379C7B95FF4361DEBC4D262C0A0ACDF"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using Nucleic_Acid;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Nucleic_Acid {
    
    
    /// <summary>
    /// Index
    /// </summary>
    public partial class Index : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\Index.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid backGrid;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\Index.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Labelp;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\Index.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton autoPrint;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\Index.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label Label_Name;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\Index.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar lodingBar;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\Index.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Snackbar SnackbarOK;
        
        #line default
        #line hidden
        
        
        #line 70 "..\..\Index.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Snackbar SnackbarLoding;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\Index.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal MaterialDesignThemes.Wpf.Snackbar SnackbarWarn;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Nucleic_Acid;component/index.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Index.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 14 "..\..\Index.xaml"
            ((Nucleic_Acid.Index)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.backGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            
            #line 21 "..\..\Index.xaml"
            ((System.Windows.Controls.RadioButton)(target)).Click += new System.Windows.RoutedEventHandler(this.RadioButton_Click_1);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 26 "..\..\Index.xaml"
            ((System.Windows.Controls.ListBoxItem)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.turn_MouseUp);
            
            #line default
            #line hidden
            return;
            case 5:
            
            #line 29 "..\..\Index.xaml"
            ((System.Windows.Controls.ListBoxItem)(target)).MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.reload_MouseUp);
            
            #line default
            #line hidden
            return;
            case 6:
            
            #line 34 "..\..\Index.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Click_Min);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 39 "..\..\Index.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Click_Max);
            
            #line default
            #line hidden
            return;
            case 8:
            
            #line 41 "..\..\Index.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Click_Close);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Labelp = ((System.Windows.Controls.Label)(target));
            return;
            case 10:
            this.autoPrint = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 49 "..\..\Index.xaml"
            this.autoPrint.Unchecked += new System.Windows.RoutedEventHandler(this.autoPrint_Checked);
            
            #line default
            #line hidden
            
            #line 49 "..\..\Index.xaml"
            this.autoPrint.Checked += new System.Windows.RoutedEventHandler(this.autoPrint_Checked);
            
            #line default
            #line hidden
            return;
            case 11:
            this.Label_Name = ((System.Windows.Controls.Label)(target));
            return;
            case 12:
            this.lodingBar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 13:
            this.SnackbarOK = ((MaterialDesignThemes.Wpf.Snackbar)(target));
            return;
            case 14:
            
            #line 68 "..\..\Index.xaml"
            ((MaterialDesignThemes.Wpf.SnackbarMessage)(target)).ActionClick += new System.Windows.RoutedEventHandler(this.SnackbarMessage_ActionClick);
            
            #line default
            #line hidden
            return;
            case 15:
            this.SnackbarLoding = ((MaterialDesignThemes.Wpf.Snackbar)(target));
            return;
            case 16:
            this.SnackbarWarn = ((MaterialDesignThemes.Wpf.Snackbar)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

