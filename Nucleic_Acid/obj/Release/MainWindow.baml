   M S B A M L   `   `   `  ���� G  CNucleic_Acid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Nucleic_AcidNucleic_Acid      Nucleic_Acid.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     ,localclr-namespace:Nucleic_Acid     b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -     �Name$  Wmain��   MaterialDesignBody#���   $��Medium"�$��14(� � Title$ 核酸检测-(登录)��$	��818��$	��960�� � WindowStyle$
 None=� � WindowStartupLocation$ CenterScreen=� � 
ResizeMode$ NoResize=� � Topmost .  � Icon$ logo.ico����Z� ��:  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension   � DataContext ��   Locator �� Source# [ 	 �� Path$
	 Main�.��)  #MaterialDesignThemes.Wpf.DialogHost  
   
Identifier$
 LoginDialog��   CloseOnClickAway . .��� -    �Name$ backGrid������  �� ImageSource$ image/背景.png�� j� Stretch$
 None=�.+�� .+��� $��80,163,80,0q� �� RadiusY$ 27.051�� �� RadiusX$ 27.051��$	��493��$	��Top=����� $ image/组 15.png��� $��
0,0,80,370q�$��94��$��Bottom=�$��Right=�$	��800��.+�'  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Account=����	#FFAAAAAA$��
460,26,0,0q�$��24��$��24���� -    ��Name$ userNameBox�� % !MaterialDesignFloatingHintTextBox#��[ $��Center=�.  (MaterialDesignThemes.Wpf.TextFieldAssist   HasClearButton .    
PrefixText$  ��   
SuffixText$  �����	#FFAAAAAA$��16(�$��490,7,110,41q�.��)  #MaterialDesignThemes.Wpf.HintAssist   Hint ��  �� Orientation$ 
Horizontal=�$��-2 0 0 0q�.+��� $
��Bold"�Username账号.��� $��
0,0,80,314q�$��Right=�$	��800��$��94��$��Bottom=�.+�  $ LockOutline=����	#FFAAAAAA$��
460,26,0,0q�$��24��$��24��G� -    G�Name$ passWordBox�� ) %MaterialDesignFloatingHintPasswordBox#��[ $��Center=� . $  ��$  �����	#FFAAAAAA$��16(�$��490,10,110,44q� �� $ 
Horizontal=�$��-2 0 0 0q�.+��� �� $� Password密码��$
��Bold"�� $y�Hidden=�$��566,430,195,294q�.+�  $ CreditCardScan=����	#FFAAAAAA$��
-30,26,0,0q�$��24��$��24���� -   $ nameBox��#��[ $��Center=� . $  ��$  ��
���   �$��16(�$��0,7,0,41q�{�.  $��Roboto)�$
��Bold"�.�� �� $ 
Horizontal=�$��-2 0 0 0q�.+��� $
��Bold"�刷卡验证.��� $��
0,0,80,321q�$��26��$��Bottom=�$��Right=�$	��800��.+��� -    ��Name$ CheckBox_isRember�� d� 	IsChecked$
 TrueY�  MaterialDesignLightCheckBox#��[ $��记住密码��$��Magneto)�$��Normal"�$��12(����	#DDCFC2C2$
��Left=�$��73��$��490,0,110,0q�� $��
0,0,80,246q�$��48��$��Bottom=�$��Right=�$	��800��.+��� -     MaterialDesignRaisedButton#��[ 3  -MaterialDesignThemes.Wpf.ButtonProgressAssist   Value$ -1Q�   IsIndicatorVisible .    IsIndeterminate . $��登录��$��500,8,110,8q�
�������$
��Left=�$	��158��$��NSimSun)�$��12(�$
��Bold"����	#FF44B2F6�� -    � ToolTip$ 	最小化��$��0,3,50,0q�$��21��$��Right=�$	��Top=���Z� ��Z� ���� $ image/最小化.png��$ Uniform=���  , (MaterialDesignFloatingActionAccentButton#��[  . $ 50Q�$ 退出程序�� . $��退出��$��0,3,8,0q�$��Right=�$��22��$��23��$	��Top=���Z� ���� *
Exit_Click+����  �� ElementName$ Wmain����Z� ���� $ image/退出.png���� -	     ��Name$  label��$��核酸检测��$��0,0,195,517q�$��Microsoft YaHei)�$
��45.7(����	#FF0099FF$��68��$��Bottom=�$��Right=�$	��191���� -
   $  
label_Copy��$��HESUANJIANCE��$��0,0,182,493q����	#FF0099FF$
��25.8(�$��Microsoft YaHei)�$��42��$��Bottom=�$��Right=�$	��204��