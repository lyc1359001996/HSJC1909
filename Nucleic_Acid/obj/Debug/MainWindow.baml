   M S B A M L   `   `   `  ����G  CNucleic_Acid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:Nucleic_AcidNucleic_Acid  5   	       Nucleic_Acid.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:Nucleic_Acid     5   	   b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          �Name$  Wmain��5   	      MaterialDesignBody#���   5	   	   $��Medium"�5   	   $��14(�5   	    � Title$ 核酸检测-(登录)��5   	   $	��818��6   $	��960��6(    � WindowStyle$
 None=�64    � WindowStartupLocation$ CenterScreen=�6G    � 
ResizeMode$ NoResize=�6l    � Topmost .  � Icon$ logo.ico��6�   ��5
   	   Z� ��5   	   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension   � DataContext 5   	   ��   Locator �� Source# [ 	 �� Path$
	 Main�.��5      )  #MaterialDesignThemes.Wpf.DialogHost  
   
Identifier$
 LoginDialog��6       CloseOnClickAway . .��5   
   � -   5         �Name$ backGrid��5      ��5      �� 5       �� ImageSource$ image/背景.png��6    j� Stretch$
 None=�68   .+�5      � .+�5      �� $��80,163,80,0q�6    �� RadiusY$ 27.051��61    �� RadiusX$ 27.051��6B   $	��493��6S   $	��Top=�6`   ��5      �� 5      $ image/组 15.png��6%   5      � 5      $��566,354,195,370q�6   .+�5      '  !MaterialDesignThemes.Wpf.PackIcon     Kind$ Account=�6.   ���	#FFAAAAAA$��
-30,26,0,0q�6T   $��24��6h   $��24��6t   �� 5      -   5         ��Name$ userNameBox��5       % !MaterialDesignFloatingHintTextBox#��[ 63   $��Center=�6n   .  (MaterialDesignThemes.Wpf.TextFieldAssist   HasClearButton .    
PrefixText$  ��6�      
SuffixText$  ��6�   ���	#FFAAAAAA$��16(�61  $��0,7,0,41q�6?  .��6Q  )  #MaterialDesignThemes.Wpf.HintAssist   Hint 5      �� 5        �� Orientation$ 
Horizontal=�6)   $��-2 0 0 0q�6B   .+�5"   "   �� $
��Bold"�6,   Username账号5$   #   5%      .��5&   :   5'      5(      � 5)      $��566,410,195,314q�6   .+�5*        $ LockOutline=�6.   ���	#FFAAAAAA$��
-30,26,0,0q�6X   $��24��6l   $��24��6x   G� 5+      -   5         G�Name$ passWordBox��5+   "    ) %MaterialDesignFloatingHintPasswordBox#��[ 67   $��Center=�6v    . $  ��6�   $  ��6�   ���	#FFAAAAAA$��16(�69  $��	0,10,0,44q�6G   5,      �� 5-      $ 
Horizontal=�6)   $��-2 0 0 0q�6B   .+�5/   "   �� �� 6-   $� Password密码��61   $
��Bold"�6D   6Y   50      52      53      � 54      $y�Hidden=�6   $��566,430,195,294q�6,   .+�55        $ CreditCardScan=�6.   ���	#FFAAAAAA$��
-30,26,0,0q�6[   $��24��6o   $��24��6{   �� 56      -   5        $ nameBox��56      #��[ 6/   $��Center=�6j    . $  ��6�   $  ��6�   
���   �$��16(�6)  $��0,7,0,41q�67  {�.  $��Roboto)�6[  $
��Bold"�6o  .��6�   57      �� 58      $ 
Horizontal=�6)   $��-2 0 0 0q�6B   .+�5:   "   �� $
��Bold"�6,   刷卡验证5<   #   5=      .��5>   :   5?      5H      � 5I      $��562,471,195,321q�6   .+�5J      �� -   5         ��Name$ CheckBox_isRember��5J       d� 	IsChecked$
 TrueY�6:     MaterialDesignLightCheckBox#��[ 6K   $��记住密码��6�   $��Magneto)�6�   $��Normal"�6�   $��12(�6�   ���	#DDCFC2C2$
��Left=�6�   $��73��6�   5K      � 5L      $��580,524,136,246q�6   $��Center=�60   $��Center=�6K   $	��244��6h   .+�5M      �� -   5          MaterialDesignRaisedButton#��[ 5M      3  -MaterialDesignThemes.Wpf.ButtonProgressAssist   Value$ -1Q�6Q      IsIndicatorVisible .    IsIndeterminate . $��登录��6�   $��2,8,0,8q�6  
�������$
��Left=�6>  $	��158��6Y  $��NSimSun)�6e  $��12(�6z  $
��Bold"�6�  ���	#FF44B2F66�  5N      �� 5O      -   5         � ToolTip$ 	最小化��5O      $��0,3,50,0q�6U   $��21��6g   $��Right=�6�   $	��Top=�6�   ��6(   Z� ��6?   Z� ��5P      �� 5Q      $ image/最小化.png��6%   $ Uniform=�6A   5S      �� 5T       , (MaterialDesignFloatingActionAccentButton#��[ 6    . $ 50Q�6�   $ 退出程序��6�    . $��退出��6  $��0,3,8,0q�6  $��Right=�6�  $��22��6�  $��23��6�  $	��Top=�6�  ��60  Z� ��6F  �� *
Exit_Click+��6e  ��  �� ElementName$ Wmain����6�  Z� ��5U      �� 5V      $ image/退出.png��6%   5X      �� 5Y      -	   5          ��Name$  label��5Y      $��核酸检测��6'   $
��Left=�66   $��574,233,0,0q�6Q   $	��Top=�6f   $��Microsoft YaHei)�6~   $
��45.7(�6�   ���	#FF0099FF6�   �� 5Z      -
   5        $  
label_Copy��5Z      $��HESUANJIANCE��6,   $
��Left=�6C   $��574,283,0,0q�6^   $	��Top=�6s   ���	#FF0099FF$
��25.8(�6�   $��Microsoft YaHei)�6�   5[      5\      5]      5^      