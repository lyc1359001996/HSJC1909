   M S B A M L   `   `   `  ����G  CNucleic_Acid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Nucleic_AcidNucleic_Acid  5   	       Nucleic_Acid.IndexOffline   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Nucleic_Acid     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
   	   -   5           MaterialDesignBody#���   5   	     MaterialDesignPaper#���  5	   	   $��Medium"�5   	   $��14(�5   	     � Title$!  核酸检测-(离线模式)��5   	   $	��818��6   $
��1280��6*    � WindowStyle$
 None=�67    � WindowStartupLocation$ CenterScreen=�6J    � 
ResizeMode$ NoResize=�6o    � Icon$ logo.ico��6�   ��5   	   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��5      )  #MaterialDesignThemes.Wpf.DialogHost     
Identifier$ 
ReadDialog��6    .��5   
   � -   5         �Name$ backGrid��5      ���	#FFD3D1D1.+�5      � ���	#FF44B2F6$��60��6*   $	��Top=�66   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��0q�66   $��Center=�6A   .+�5      	� -   5           MaterialDesignTabRadioButton#��[ 5   "   $��0q�6X    d� 	IsChecked$
 TrueY�6c   $��用户信息��6t   $��Microsoft YaHei)�6�   $��18(�6�   $
��Bold"�6�   ���	#DDFFFFFF6�   5      �� 5      $ 
Horizontal=�6   $	��0,4q�66   $��Right=�6C   $	��130��6_   .+�5      '  !MaterialDesignThemes.Wpf.PopupBox  $��Right=�6.   $��	10,10,0,0q�6J   ���	#DDFFFFFF	   PopupContent.	 5      �� .@�5      �� -   5        $��Microsoft YaHei)�5   *   $��12(�6G   $
��Bold"�6U   .��6~   转到在线5      �� 5      -   5        $��Microsoft YaHei)�5   *   $��12(�6G   $
��Bold"�6U   .��6�   重新登录5      5      5       �� 5!      -   5        
 � ToolTip$
 	最小化��5!      $��Center=�6+   $
��0,10q�6s   $��21��6�   ��6F   Z� ��6]   Z� ��5"      �� 5#       �� ImageSource$ image/最小化.png��6)    j� Stretch$ Uniform=�6E   5%      �� 5&      -   5        $
 	最大化��5&      $��Center=�6+   $��10,0,0,0q�6F   
�������
�������$��16��6   $��16��6�    J� OpacityMask
 �������6�   Z� 5'      �� 5(      -   5        $
 退出程序��5(      $��Center=�6,   $��10,0,0,0q�6G   $��22��6�   $��23��6�   ��6Y   Z� ��6p   Z� ��5)      �� 5*      $ image/退出.png��6)   5,      5-      �� 5.      $ 
Horizontal=�6   $��	0,3,123,5q�66   $��Right=�6I   $
��auto��6e   .+�5/      �� -	   5         ��Name$ Labelp��5/   0   $y�Hidden=�6   $��自动打印��6@   $
��Left=�6O   $��Center=�6j   ���	#DDFFFFFF$��Microsoft YaHei)�6�   $��16(�6�   $
��Bold"�6�   d� 50      -
   5         d�Name$ 	autoPrint��50   7   $y�Hidden=�6#    $  MaterialDesignSwitchToggleButton#��[ 6J   $��Center=�6�   $
 自动打印��6�   
�������1  +MaterialDesignThemes.Wpf.ToggleButtonAssist   SwitchTrackOnBackground 51      �� 52       �� Color$ Green��6.      SwitchTrackOffBackground 54      �� 55      $	 Red��6.   57      #  MaterialDesignThemes.Wpf.Chip  58      $��30,0,0,0q�6*   $��33��6=      IconBackground 6H   Z� ��6b   Z�    IconForeground 6x   Z� ��59      �� 5:      $" image/矢量智能对象.png��6)   5<      �� 5=      -   5        $ 
Label_Name��5=      $��管理员-离线��60   $
��Left=�6A   ���	#DDFFFFFF$��Center=�6s   $��Center=�6�   $��Microsoft YaHei)�6�   $
��Bold"�6�   5>      5?      �� 5@      $��0��6   $��1,60,1,1q�6>   ��6*   �� 6>   � 5A      -   5         �Name$ 	lodingBar��5A       � IsIndeterminate . $��0,60,0,0q�6D   $	��Top=�6V   ���	#FF6EC6FF$y�Hidden=�6�   '  !MaterialDesignThemes.Wpf.Snackbar  5B      -   5          Name$ 
SnackbarOK��5B   M   $��498,60,494,0q�6&      IsActive .  $	��Top=�6a   $��Center=�6y      Message. 5C      .  (MaterialDesignThemes.Wpf.SnackbarMessage  -   5        $��数据导出完成��5C   1      ActionContent$ 确认��6B   6U   5D        5E      -   5        $ SnackbarLoding��5E   M   $��498,60,494,0q�6&    .  $	��Top=�6e   $��Center=�6}   . 5F        $-��'数据正在导出...请勿关闭电源��61   $	��237��6L   $��Center=�6X   5G        5H      -   5        $ SnackbarWarn��5H   M   $��455,60,470,0q�6&    .  $	��Top=�6c   $��Center=�6{   . 5I        $��	警告：��61   $��Center=�6?   5J      5K      5L      5M      