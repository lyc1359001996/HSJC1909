   M S B A M L   `   `   `  ���� G  CNucleic_Acid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Nucleic_Acid.ViewNucleic_Acid.View  %clr-namespace:WpfPaging	WpfPaging       Nucleic_Acid.View.InfoList   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:Nucleic_Acid.View     G CHandyControl, Version=3.0.0.0, Culture=neutral, PublicKeyToken=null0hc'https://handyorg.github.io/handycontrol  '	WpfPagingclr-namespace:WpfPaging  b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -      MaterialDesignBody#���     MaterialDesignPaper#���  $��Medium"�$��14(���:  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension    � DataContext  ��   Locator �� Source# [  �� Path$ 
V_ReadCard�.��� 
�������.+�� $��25,10q�
�������.+��� $��	30,38,0,0q� �� Orientation$ 
Horizontal=�$
��Left=�$��55��$	��Top=�.+�#  HandyControl.Controls.TextBox  -     Name$ TextBox_Name��'  !HandyControl.Controls.InfoElement   Placeholder$ 请输入姓名��$��4,0,0,0q�$��NSimSun)�$��38��$��12(�$	��100�� � ToolTip$ 请输入姓名��  -   $ TextBox_CardNo��$ 请输入身份证号码��$��10,8,0,8q�$��38��$��NSimSun)�$��12(�$	��170��$ 请输入身份证号码��*  $HandyControl.Controls.DateTimePicker  -     Name$ dateTimeStart��$ 开始时间��$��10,8,0,9q�$��Stretch=�$��Center=�$	��195��   SelectionBrush �	#FF2196F3$��38��$ 开始日期��  -   $ dateTimeEnd��$ 结束时间��$��10,8,0,9q�$��Stretch=�$��Center=�$	��195�� �	#FF2196F3$��38��$ 结束日期��)  #HandyControl.Controls.LoadingCircle  -   	  Name$	 loding��$y�Hidden=��� $��	0,40,44,0q�$ 
Horizontal=�$��Right=�$
��auto��$��50��$	��Top=�.+��� -    # MaterialDesignRaisedLightButton#��[ +  %MaterialDesignThemes.Wpf.ButtonAssist
   CornerRadius$
 5��$M G导入数据并合并数据至本地,导入文件为导出的json文件��$��Center=����	#FF44B2F6
�������$��NSimSun)�$��12(�$��60��$��0,0,0,0q�.���� $��导入���� -   #��[ $
 5��$R L按条件导出本地数据,如要导出所有数据请选择重置后导出��$��Center=����	#FF44B2F6
�������$��NSimSun)�$��12(�$��60��$��10,0,0,0q�.���� $��导出���� -	   #��[ $
 5��$ 数据回传到本地��$��Center=����	#FF44B2F6
�������$��NSimSun)�$��12(�$��60��$��10,0,0,0q�.���� $��回传���� -
   #��[ $
 5��$ 新增��$��Center=����	#FF44B2F6
�������$��NSimSun)�$��12(�$��60��$��10,0,0,0q�.���� $��新增���� -   #��[ $
 5��$ 重置查询��$��Center=����	#FF44B2F6
�������$��NSimSun)�$��12(�$��60��$��10,0,0,0q�.���� $��重置���� -   #��[ $
 5��$ 查询��$��Center=����	#FF44B2F6
�������$��NSimSun)�$��12(�$��60��$��10,0,0,0q�.���� $��查询���� $��	0,108,0,0q�.���� .+�&   System.Windows.Controls.DataGrid  -     Name$ dataGrid��   AutoGenerateColumns .     CanUserAddRows .     SelectionUnit$ FullRow=�   SelectionMode$ Single=�$��	30,0,30,0q����	#1F060000$��2q�$	��Top=�$��Center=�$��Bottom=�   AreRowDetailsFrozen . $
��auto��$��NSimSun)�$��Normal"�$��14(�   CanUserReorderColumns .     Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,	  &System.Windows.Controls.DataGridColumn 	  Header$ 序号��   CellTemplate �� .R���  �� TextWrapping$
 Wrap=�$	��250�� �� 
IsReadOnly . $��NSimSun)� �� 	MaxLength$ 50��$��Center=����� $ index���Z�  �� SelectionBrush Z�   $ 编号�� �� .R��� $
 Wrap=�$	��250�� . $��NSimSun)�$ 50��$��Center=����� $ acidNo���Z�  Z�   $ 姓名�� �� .R��� $
 Wrap=�$	��350�� . $��NSimSun)�$ 50��$��Center=����� $ userName���Z�  Z�   $ 身份证号�� �� .R��� $
 Wrap=�$	��250�� . $��NSimSun)�$ 50��$��Center=����� $ cardNo���Z�  Z�   $ 性别�� �� .R��� $
 Wrap=�$	��250�� . $��NSimSun)�$ 50��$��Center=����� $	 sex���Z�  Z�   $ 工作单位�� �� .R��� $
 Wrap=�$	��250��$��NSimSun)�$ 50��$��Center=����� $ company� �� UpdateSourceTrigger$ PropertyChanged=�{��� $ Editor_company�  $ 身份证地址�� �� .R��� $
 Wrap=�$	��250�� . $��NSimSun)�$ 50��$��Center=����� $ address���Z�  Z�   $ 现居住地址�� �� .R��� $
 Wrap=�$	��250��$��NSimSun)�$ 50��$��Center=����� $ homeAddress�$ PropertyChanged=�{��� $ Editor_homeAddress�  $ 采样登记时间�� �� .R��� $
 Wrap=�$	��250�� . $��NSimSun)�$ 50��$��Center=����� $ 
createTime���Z�  Z�   $ 操作�� �� .R��� $ 
Horizontal=�.+��� -   #��[ $
 5��$ 打印��$��Center=����	#FF44B2F6
�������$��NSimSun)�$��12(�$��55��$��20,0,0,0q�$��22��.���� $��打印����  ��Name$ Button_update��-   #��[ $
 5��$ 编辑��$��Center=����	#FF44B2F6
�������$��NSimSun)�$��12(�$��55��$��20,0,0,0q�$��22������ *
updateText+�� -   #��[ $
 5��$ 打印��$��Center=����	#FF44B2F6
�������$��NSimSun)�$��12(�$��55��$��20,0,0,0q�$��22��.���� $��删除��
   WpfPaging.PagingControl
  -    
 Name$ pageControl��$��44��$��	30,20,0,0q� 
  PageSize$ 10�� 
  DataTote$ 0��$	��890��$
��Left=��� $ 
Horizontal=�$��16��$��Bottom=�$��Right=�$��60��$��0,0,20,0q�.+�'  !MaterialDesignThemes.Wpf.PackIcon  -     Name$ disconn��   Kind$ LanDisconnect=�$y�Visible=�$
��Left=�$
��Auto��$��Stretch=����	#DDEE4646  -   $
 conn��$ 
LanConnect=�$y�Hidden=�$��	-16,0,0,0q�$
��Left=�$
��Auto��$��Stretch=����	#DD52DE11�� -     ��Name$  connect_Text��$��	未连接��$��6,0,0,0q�$��NSimSun)�$��12(�$��Center=�