   M S B A M L   `   `   `  ���� G  CNucleic_Acid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Nucleic_Acid.ViewNucleic_Acid.View  '    !Nucleic_Acid.View.ReadCardOffline   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:Nucleic_Acid.View     U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -      MaterialDesignBody#���     MaterialDesignPaper#���  $��Medium"�$��14(���:  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension    � DataContext  ��   Locator �� Source# [  �� Path$ 
V_ReadCard�.��� ����  �� ImageSource$- '/Nucleic_Acid;component/image/read1.png��.+�� $��25,10q�.+�� $��
0,10,0,138q�.+�� $��175,106,160,87q�$��Center=����� $1 +/Nucleic_Acid;component/image/cardscan1.png�� j� Stretch$ Uniform=�.+�� $��160,-62,170,97q�$��Center=�$	��362������ $0 */Nucleic_Acid;component/image/cardscan.png��$ Uniform=�.+��� -    ��Name$ scanR�� J� Opacity$
 0.15Q�$��Center=�$��12��$��90,118,80,232q�$��Center=�$	��315������  �� EndPoint$ 0.5,1&� � MappingMode$ RelativeToBoundingBox=�	 �� 
StartPoint$	 0.5,0&�.��� -   of1��
 � Color$
 	#FFC9C6C6�� � Offset$ 0.807Q�� -   of2��$
 	#FF0099FF��� $
 White��$ 0.503Q��� $��0,0,0,21q�$	��100��$��Bottom=�.+�&   System.Windows.Controls.DataGrid  -     Name$ datagrid��   AutoGenerateColumns .     SelectionUnit$ FullRow=�   SelectionMode$ Single=����	#1F060000$��2q�$��Bottom=�   CanUserResizeColumns .     CanUserResizeRows .  $��88��$��Center=�$��Center=�   AreRowDetailsFrozen .    CanUserReorderColumns .     GridLinesVisibility$ 
Horizontal=���.     
IsReadOnly .     VerticalGridLinesBrush
 �   �$��NSimSun)�$��14(�$��	30,0,30,0q�   CanUserAddRows .  ���� *Items2+   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ 序号�� ) %MaterialDesignDataGridTextColumnStyle1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [  0 ,MaterialDesignDataGridTextColumnEditingStyle   EditingElementStyle# [    
IsReadOnly .    
FontWeight$
 Bold"�   
FontFamily$ NSimSun)�   Binding �� $ gIndex�  $ 姓名��# [ # [  . $
 Bold"�$ NSimSun)� �� $ SName�  $ 身份证号��# [ # [  . $
 Bold"�$ NSimSun)� �� $
 temp�  $ 性别��# [ # [  . $
 Bold"�$ NSimSun)� �� $	 Sex�  $ 身份证地址��# [ # [  .     MaxWidth$	  400Q�$
 Bold"�$ NSimSun)� �� $
 home�  $ 现居住地址��# [ # [  .  $	  400Q�$
 Bold"�$ NSimSun)� �� $ homeAddress�  $ 刷卡时间�� . $
 Bold"�$ NSimSun)� �� $ 
Sbirthdate�4  .System.Windows.Controls.DataGridTemplateColumn  $ 操作��!   CellTemplate! �� .R��� " �� Orientation$" 
Horizontal=�.+��� -    # MaterialDesignRaisedLightButton#��[ +  %MaterialDesignThemes.Wpf.ButtonAssist#   CornerRadius$# 5��$ � ToolTip$$ 删除��$��Center=����	#FF44B2F6
�������$��NSimSun)�$��12(�$��55��$��22��.���� $��删除���� -   #��[ $# 5��$$ 打印��$��Center=����	#FF44B2F6
�������$��NSimSun)�$��12(�$��55��$��20,0,0,0q�$��22��.���� $��打印���� $" 
Horizontal=�$��16��$��Bottom=�$��Right=�$��60��.+�'  !MaterialDesignThemes.Wpf.PackIcon  -   %  Name$% disconn��&   Kind$& LanDisconnect=�$y�Visible=�$
��Left=�$
��Auto��$��Stretch=����	#DDEE4646  -	   $
% conn��$& 
LanConnect=�$y�Hidden=�$��	-16,0,0,0q�$
��Left=�$
��Auto��$��Stretch=����	#DD52DE11�� -
   ' ��Name$' connect_Text��$��	未连接��$��6,0,0,0q�$��NSimSun)�$��12(�$��Center=�