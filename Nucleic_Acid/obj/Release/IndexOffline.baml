   M S B A M L   `   `   `  ���� G  CNucleic_Acid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Nucleic_AcidNucleic_Acid      Nucleic_Acid.IndexOffline   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:Nucleic_Acid     b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -      MaterialDesignBody#���     MaterialDesignPaper#���  $��Medium"�$��14(�  � Title$!  核酸检测-(离线模式)��$	��818��$
��1280�� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � Icon$ logo.ico����:  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��)  #MaterialDesignThemes.Wpf.DialogHost     
Identifier$ 
ReadDialog��.��� -    �Name$ backGrid�����	#FFD3D1D1.+�� ���	#FF44B2F6$��60��$	��Top=�.+���  �� Orientation$ 
Horizontal=�$��0q�$��Center=�.+�	� -      MaterialDesignTabRadioButton#��[ $��0q� d� 	IsChecked$
 TrueY�$��用户信息��$��Microsoft YaHei)�$��18(�$
��Bold"����	#DDFFFFFF�� $ 
Horizontal=�$	��0,4q�$��Right=�$	��130��.+�'  !MaterialDesignThemes.Wpf.PopupBox  $��Right=�$��	10,10,0,0q����	#DDFFFFFF	   PopupContent.	 �� .@��� -   $��Microsoft YaHei)�$��12(�$
��Bold"�.��转到在线�� -   $��Microsoft YaHei)�$��12(�$
��Bold"�.��重新登录�� -   
 � ToolTip$
 	最小化��$��Center=�$
��0,10q�$��21����Z� ��Z� ����  �� ImageSource$ image/最小化.png�� j� Stretch$ Uniform=��� -   $
 	最大化��$��Center=�$��10,0,0,0q�
�������
�������$��16��$��16�� J� OpacityMask
 �������Z� �� -   $
 退出程序��$��Center=�$��10,0,0,0q�$��22��$��23����Z� ��Z� ���� $ image/退出.png���� $ 
Horizontal=�$��	0,3,123,5q�$��Right=�$
��auto��.+��� -	    ��Name$ Labelp��$y�Hidden=�$��自动打印��$
��Left=�$��Center=����	#DDFFFFFF$��Microsoft YaHei)�$��16(�$
��Bold"�d� -
    d�Name$ 	autoPrint��$y�Hidden=� $  MaterialDesignSwitchToggleButton#��[ $��Center=�$
 自动打印��
�������1  +MaterialDesignThemes.Wpf.ToggleButtonAssist   SwitchTrackOnBackground ��  �� Color$ Green��   SwitchTrackOffBackground �� $	 Red��#  MaterialDesignThemes.Wpf.Chip  $��30,0,0,0q�$��33��   IconBackground Z� ��Z�    IconForeground Z� ���� $" image/矢量智能对象.png���� -   $ 
Label_Name��$��管理员-离线��$
��Left=����	#DDFFFFFF$��Center=�$��Center=�$��Microsoft YaHei)�$
��Bold"��� $��0��$��1,60,1,1q����� � -    �Name$ 	lodingBar�� � IsIndeterminate . $��0,60,0,0q�$	��Top=����	#FF6EC6FF$y�Hidden=�'  !MaterialDesignThemes.Wpf.Snackbar  -     Name$ 
SnackbarOK��$��498,60,494,0q�   IsActive .  $	��Top=�$��Center=�   Message. .  (MaterialDesignThemes.Wpf.SnackbarMessage  -   $��数据导出完成��   ActionContent$ 确认��  -   $ SnackbarLoding��$��498,60,494,0q� .  $	��Top=�$��Center=�.   $-��'数据正在导出...请勿关闭电源��$	��237��$��Center=�  -   $ SnackbarWarn��$��455,60,470,0q� .  $	��Top=�$��Center=�.   $��	警告：��$��Center=�