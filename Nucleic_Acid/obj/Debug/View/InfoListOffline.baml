   M S B A M L   `   `   `  ����G  CNucleic_Acid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Nucleic_Acid.ViewNucleic_Acid.View  5      %clr-namespace:WpfPaging	WpfPaging  5      -clr-namespace:DesignLibraryDesignLibrary  5   W   '    !Nucleic_Acid.View.InfoListOffline   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Nucleic_Acid.View     5      -	WpfPagingclr-namespace:WpfPaging     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      /designlibraryclr-namespace:DesignLibrary  6W   G CHandyControl, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null0hc'https://handyorg.github.io/handycontrol  5	      -   5           MaterialDesignBody#���   5
        MaterialDesignPaper#���  5      $��Medium"�5      $��14(�5      ��5      :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension    � DataContext  5      �� *
V_ReadCard+  Locator �� Source# [ .��5      � 
�������.+�5   
   � $��25,10q�6   
�������.+�5      �� $��	30,38,0,0q�6    �� Orientation$ 
Horizontal=�6,   $
��Left=�6E   $��55��6`   $	��Top=�6l   $	��739��6�   .+�5      #  HandyControl.Controls.TextBox  -   5          Name$ TextBox_Name��5      '  !HandyControl.Controls.InfoElement   Placeholder$ 请输入姓名��63   $��4,0,0,0q�6V   $��NSimSun)�6g   $��38��6|   $��12(�6�   $	��100��6�    � ToolTip$ 请输入姓名��6�     5      -   5        $ TextBox_CardNo��5      $ 请输入身份证号码��66   $��10,8,0,8q�6\   $��38��6n   $��NSimSun)�6z   $��12(�6�   $	��170��6�   $ 请输入身份证号码��6�   *  $HandyControl.Controls.DateTimePicker  5      -   5          Name$ dateTimeStart��5   $   $ 开始时间��6;   $��10,8,0,9q�6]   $��Stretch=�6o   $��Center=�6�   $	��195��6�      SelectionBrush �	#FF2196F3$��38��6�   $ 开始日期��6�     5      -   5        $ dateTimeEnd��5   $   $ 结束时间��69   $��10,8,0,9q�6[   $��Stretch=�6m   $��Center=�6�   $	��195��6�    �	#FF2196F3$��38��6�   $ 结束日期��6�   )  #HandyControl.Controls.LoadingCircle  5      -   5          Name$ loding��5   #   $y�Hidden=�63   5      �� 5      $��	0,40,30,0q�6   $ 
Horizontal=�6,   $��Right=�6E   $
��auto��6a   $��50��6n   $	��Top=�6z   .+�5      �� -   5         # MaterialDesignRaisedLightButton#��[ 5      +  %MaterialDesignThemes.Wpf.ButtonAssist	   CornerRadius$	 5��6S   $M G导入数据并合并数据至本地,导入文件为导出的json文件��6�   $��Center=�6�   ���	#FF44B2F6
�������$��NSimSun)�6�   $��12(�6   $��60��6  $��0,0,0,0q�6  .��5      �� $��导入��6    5      �� 5      -   5        #��[ 5      $	 5��6S   $R L按条件导出本地数据,如要导出所有数据请选择重置后导出��6�   $��Center=�6�   ���	#FF44B2F6
�������$��NSimSun)�6�   $��12(�6�   $��60��6  $��10,0,0,0q�6  .��5       �� $��导出��6    5!      �� 5"      -	   5        #��[ 5"      $	 5��6S   $ 新增数据��6�   $��Center=�6�   ���	#FF44B2F6
�������$��NSimSun)�6�   $��12(�6�   $��60��6�   $��10,0,0,0q�6  .��5#      �� $��新增��6    5$      �� 5%      -
   5        #��[ 5%      $	 5��6R   $ 重置查询��6   $��Center=�6�   ���	#FF44B2F6
�������$��NSimSun)�6�   $��12(�6�   $��60��6�   $��10,0,0,0q�6  .��5&      �� $��重置��6    5'      �� 5(      -   5        #��[ 5(      $	 5��6S   $ 查询��6�   $��Center=�6�   ���	#FF44B2F6
�������$��NSimSun)�6�   $��12(�6�   $��60��6�   $��10,0,0,0q�6   .��5)      �� $��查询��6    5*      5+      �� 5,      $��	0,108,0,0q�6   $
��Auto=�6.   .��5-      �� .+�5.      &   System.Windows.Controls.DataGrid  -   5        
  Name$
 dataGrid��5.         AutoGenerateColumns .     CanUserAddRows .     SelectionUnit$ FullRow=�6d      SelectionMode$ Single=�6|   $��	30,0,30,0q�6�   ���	#1F060000$��2q�6�   $	��Top=�6�   $��Center=�6�   $��Bottom=�6     AreRowDetailsFrozen . $
��auto��6K  $��NSimSun)�6Y  $��Normal"�6n  $��14(�6�     CanUserReorderColumns .     EnableRowVirtualization .     Columns 5/      4  .System.Windows.Controls.DataGridTemplateColumn  50      ,	  &System.Windows.Controls.DataGridColumn 	  Header$ 序号��65      CellTemplate 51   "   �� 52   &   .R�53   *   ��  �� TextWrapping$
 Wrap=�62   $	��250��6F    �� 
IsReadOnly . $��NSimSun)�6~    �� 	MaxLength$ 50��6�   $��Center=�6�   ��6U   �� *index+��6�   Z�  �� SelectionBrush 6�   Z� 6�   54   '   56        57      $ 编号��65    58   "   �� 59   &   .R�5:   *   �� $
 Wrap=�62   $	��250��6F    . $��NSimSun)�6   $ 50��6�   $��Center=�6�   ��6U   �� *acidNo+��6�   Z�  6�   Z� 6�   5;   '   5=        5>      $ 姓名��65    5?   "   �� 5@   &   .R�5A   *   �� $
 Wrap=�62   $	��250��6F    . $��NSimSun)�6�   $ 50��6�   $��Center=�6�   ��6U   �� *
userName+��6�   Z�  6�   Z� 6�   5B   '   5D        5E      $ 身份证号��65    5F   "   �� 5G   &   .R�5H   *   �� $
 Wrap=�62   $	��250��6F    . $��NSimSun)�6   $ 50��6�   $��Center=�6�   ��6U   �� *cardNo+��6�   Z�  6�   Z� 6�   5I   '   5K        5L      $ 性别��65    5M   "   �� 5N   &   .R�5O   *   �� $
 Wrap=�62   $	��250��6F    . $��NSimSun)�6|   $ 50��6�   $��Center=�6�   ��6U   �� *sex+��6�   Z�  6�   Z� 6�   5P   '   5R        5S      $ 工作单位��65    5T   "   �� 5U   &   .R�5V   *   �� $
 Wrap=�62   $	��250��6F   $��NSimSun)�6�   $ 50��6�   $��Center=�6�   ��6U   �� *	company+ �� UpdateSourceTrigger$ PropertyChanged=�{�6�   �� *Editor_company+6�   5W   '   5Y        5Z      $ 身份证地址��65    5[   "   �� 5\   &   .R�5]   *   �� $
 Wrap=�62   $	��250��6F    . $��NSimSun)�6�   $ 50��6�   $��Center=�6�   ��6U   �� *	address+��6�   Z�  6�   Z� 6�   5^   '   5`        5a      $ 现居住地址��65    5b   "   �� 5c   &   .R�5d   *   �� $
 Wrap=�62   $	��250��6F   $��NSimSun)�6�   $ 50��6�   $��Center=�6�   ��6U   �� *homeAddress+$ PropertyChanged=�{�6�   �� *Editor_homeAddress+6�   5e   '   5g        5i      $ 采样登记时间��65    5j   "   �� 5k   &   .R�5l   *   �� $
 Wrap=�62   $	��250��6F    . $��NSimSun)�6�   $ 50��6�   $��Center=�6�   ��6U   �� *
createTime+��6�   Z�  6�   Z� 6�   5m   '   5o        5p      $ 操作��65    5q   "   �� 5r   &   .R�5s   *   �� $ 
Horizontal=�65   .+�5t   .   �� -   5        #��[ 5t   5   $	 5��6o   $ 打印��6�   $��Center=�6�   ���	#FF44B2F6
�������$��NSimSun)�6�   $��12(�6  $��55��6  $��20,0,0,0q�6  $��22��6.  .��5u   2   �� $��打印��6<   5v   /   �� 5w   .    ��Name$ Button_update��65   -   5        #��[ 5w   k   $	 5��6�   $ 编辑��6�   $��Center=�6�   ���	#FF44B2F6
�������$��NSimSun)�6$  $��12(�69  $��55��6G  $��20,0,0,0q�6R  $��22��6d  ��6L   �� *
updateText+5y   /   5z   +   5{   '   5}      5      
   WpfPaging.PagingControl
  5�      -   5         
 Name$ pageControl��5�   .   $��44��6C   $��	30,20,0,0q�6O    
  PageSize$ 10��6b    
  DataTote$ 0��6p   $	��890��6}   $
��Left=�6�   5�      5�      �� 5�      $ 
Horizontal=�6   $��16��62   $��Bottom=�6>   $��Right=�6Y   $��60��6u   $��0,0,20,0q�6�   .+�5�      '  !MaterialDesignThemes.Wpf.PackIcon  -   5          Name$ disconn��5�   *      Kind$ LanDisconnect=�6;   $y�Visible=�6P   $
��Left=�6f   $
��Auto��6�   $��Stretch=�6�   ���	#DDEE46466�     5�      -   5        $
 conn��5�   *   $ 
LanConnect=�68   $y�Hidden=�6J   $��	-16,0,0,0q�6^   $
��Left=�6r   $
��Auto��6�   $��Stretch=�6�   ���	#DD52DE116�   �� 5�      -   5          ��Name$  connect_Text��5�      $��	未连接��62   $��6,0,0,0q�6=   $��NSimSun)�6N   $��12(�6c   $��Center=�6q   5�      5�      5�      5�      