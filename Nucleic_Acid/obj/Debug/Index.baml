   M S B A M L   `   `   `  ����G  CNucleic_Acid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Nucleic_AcidNucleic_Acid  5   	       Nucleic_Acid.Index   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Nucleic_Acid     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	   -   5           MaterialDesignBody#���   5   	     MaterialDesignPaper#���  5	   	   $��Medium"�5   	   $��14(�5   	     � Title$!  核酸检测-(在线模式)��5   	   $	��818��6   $
��1280��6*    � WindowStyle$
 None=�67    � WindowStartupLocation$ CenterScreen=�6J    � 
ResizeMode$ NoResize=�6o    � Icon$ logo.ico��6�   ��5   	   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��5      )  #MaterialDesignThemes.Wpf.DialogHost     
Identifier$ 
ReadDialog��6    .��5   
     $ ReadDialog_new��6$   .��5      � -   5         �Name$ backGrid��5      ���	#FFD3D1D1.+�5      � ���	#FF44B2F6$��60��6.   $	��Top=�6:   .+�5      ��  �� Orientation$ 
Horizontal=�6!   $��0q�6:   $��Center=�6E   .+�5      	� -   5           MaterialDesignTabRadioButton#��[ 5   &   $��0q�6\    d� 	IsChecked$
 TrueY�6g   $��用户信息��6x   $��Microsoft YaHei)�6�   $��18(�6�   $
��Bold"�6�   ���	#DDFFFFFF6�   5      �� 5      $ 
Horizontal=�6!   $	��0,4q�6:   $��Right=�6G   $	��130��6c   .+�5      '  !MaterialDesignThemes.Wpf.PopupBox  $��Right=�62   $��	10,10,0,0q�6N   ���	#DDFFFFFF	   PopupContent.	 5      �� .@�5   "   �� -   5        $��Microsoft YaHei)�5   .   $��12(�6K   $
��Bold"�6Y   .��6�   转到离线5   #   �� 5   "   -   5        $��Microsoft YaHei)�5   .   $��12(�6K   $
��Bold"�6Y   .��6�   退出登录5   #   5       5!      �� 5"      -   5        
 � ToolTip$
 	最小化��5"   !   $��Center=�6/   $
��0,10q�6w   $��21��6�   ��6J   Z� ��6a   Z� ��5#      �� 5$   "    �� ImageSource$ image/最小化.png��6-    j� Stretch$ Uniform=�6I   5&      �� 5'      -   5        $
 	最大化��5'   !   $��Center=�6/   $��10,0,0,0q�6J   
�������
�������$��16��6�   $��16��6�    J� OpacityMask
 �������6�   Z� 5(      �� 5)      -   5        $
 退出程序��5)   !   $��Center=�60   $��10,0,0,0q�6K   $��22��6�   $��23��6�   ��6]   Z� ��6t   Z� ��5*      �� 5+   "   $ image/退出.png��6-   5-      5.      �� 5/      $ 
Horizontal=�6!   $��	0,3,123,5q�6:   $��Right=�6M   $
��auto��6i   .+�50      �� -	   5         ��Name$ Labelp��50   4   $y�Hidden=�6    $��自动打印��6D   $
��Left=�6S   $��Center=�6n   ���	#DDFFFFFF$��Microsoft YaHei)�6�   $��16(�6�   $
��Bold"�6�   d� 51      -
   5         d�Name$ 	autoPrint��51   ;   $y�Hidden=�6'    $  MaterialDesignSwitchToggleButton#��[ 6N   $��Center=�6�   $
 自动打印��6�   
�������1  +MaterialDesignThemes.Wpf.ToggleButtonAssist   SwitchTrackOnBackground 52      �� 53   "    �� Color$ Green��62      SwitchTrackOffBackground 55      �� 56   "   $	 Red��62   58      #  MaterialDesignThemes.Wpf.Chip  59      $��30,0,0,0q�6.   $��33��6A      IconBackground 6L   Z� ��6f   Z�    IconForeground 6|   Z� ��5:      �� 5;   "   $" image/矢量智能对象.png��6-   5=      �� 5>      -   5        $ 
Label_Name��5>       $��管理员-在线��64   $
��Left=�6E   ���	#DDFFFFFF$��Center=�6w   $��Center=�6�   $��Microsoft YaHei)�6�   $
��Bold"�6�   5?      5@      �� 5A      $��0��6!   $��1,60,1,1q�6B   ��6.   �� 6B   � 5B      -   5         �Name$ 	lodingBar��5B       � IsIndeterminate . $��0,60,0,0q�6H   $	��Top=�6Z   ���	#FF6EC6FF6r   '  !MaterialDesignThemes.Wpf.Snackbar  5C      -   5          Name$ 
SnackbarOK��5C   Q   $��498,60,494,0q�6*      IsActive .  $	��Top=�6e   $��Center=�6}      Message. 5D      .  (MaterialDesignThemes.Wpf.SnackbarMessage  -   5        $��数据同步完成��5D   5      ActionContent$ 确认��6F   6Y   5E        5F      -   5        $ SnackbarLoding��5F   P   $��498,60,494,0q�6*    . $	��Top=�6h   $��Center=�6�   . 5G        $-��'数据正在同步...请勿关闭电源��65   $	��237��6P   $��Center=�6\   5H        5I      -   5        $ SnackbarWarn��5I   Q   $��455,60,470,0q�6*    .  $	��Top=�6g   $��Center=�6   . 5J        $C��=提示：王大大已于2020-10-25 12:00:00进行核酸检测��65   $��Center=�6a   5K      5L      5M      5N      5O      