   M S B A M L   `   `   `  ����G  CNucleic_Acid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Nucleic_Acid.ViewNucleic_Acid.View  5           Nucleic_Acid.View.ReadCard   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Nucleic_Acid.View     5      U QWpfAnimatedGif, Version=2.0.0.0, Culture=neutral, PublicKeyToken=9e7cd3b544a090dc,gif"http://wpfanimatedgif.codeplex.com  5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5      -   5          >�Name$  user1��5         MaterialDesignBody#���   5	        MaterialDesignPaper#���  5
      $��Medium"�5      $��14(�5      ��5      :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension   � DataContext 5      ��   Locator �� Source# [  �� Path$ 
V_ReadCard�.��5      � ��5   
   �� 5       �� ImageSource$- '/Nucleic_Acid;component/image/read1.png��6   .+�5   
   � $��25,10q�6   .+�5      � $��
0,10,0,138q�6   .+�5      � $��175,106,160,87q�6   $��Center=�6/   ��5      �� 5      $1 +/Nucleic_Acid;component/image/cardscan1.png��6%    j� Stretch$ Uniform=�6_   .+�5      � $��160,-62,170,97q�6   $��Center=�63   $	��362��6N   ��5      �� 5      $0 */Nucleic_Acid;component/image/cardscan.png��6)   $ Uniform=�6b   .+�5!      �� -   5         ��Name$ scanR��5!   $    J� Opacity$
 0.15Q�63   $��Center=�6B   $��12��6]   $��90,118,80,232q�6i   $��Center=�6�   $	��315��6�   ��5"      �� 5#   "    �� EndPoint$ 0.5,1&�66   	 � MappingMode$	 RelativeToBoundingBox=�6G   
 �� 
StartPoint$
 0.5,0&�6k   .��5$   &   � -   5        of1�� � Color$ 	#FFC9C6C6��5$   @    � Offset$ 0.807Q�6R   � 5%   &   -   5        of2��$ 	#FF0099FF��5%   @   � 5&   &   $ White��63   $ 0.503Q�6A   5'   #   5)      5*      5+      5,      �� 5-      $��0,0,0,21q�6   $	��100��6+   $��Bottom=�68   .+�5.      &   System.Windows.Controls.DataGrid  -   5          Name$ datagrid��5.         AutoGenerateColumns .     SelectionUnit$ FullRow=�6h      SelectionMode$ Single=�6�   ���	#1F060000$��2q�6�   $��Bottom=�6�      CanUserResizeColumns .     CanUserResizeRows .  $��88��6  $��Center=�6!  $��Center=�6E     AreRowDetailsFrozen .    CanUserReorderColumns .     GridLinesVisibility$ 
Horizontal=�6�  ��.     
IsReadOnly .     VerticalGridLinesBrush
 �   �$��NSimSun)�6  $��14(�6  $��	30,0,30,0q�6(     CanUserAddRows .  ��6-   �� *Items2+   Columns 5/      0  *System.Windows.Controls.DataGridTextColumn  50      ,  &System.Windows.Controls.DataGridColumn   Header$ 序号��6-    ) %MaterialDesignDataGridTextColumnStyle1  +System.Windows.Controls.DataGridBoundColumn   ElementStyle# [ 69    0 ,MaterialDesignDataGridTextColumnEditingStyle   EditingElementStyle# [ 6      
IsReadOnly .    
FontWeight$
 Bold"�6�      
FontFamily$ NSimSun)�6�       Binding  51      �� 52   "   $ gIndex�6*   54        55      $ 姓名��6-   # [ 69   # [ 6    . $
 Bold"�6�   $ NSimSun)�6�     56      �� 57   "   $ SName�6*   59        5:      $ 身份证号��6-   # [ 6;   # [ 6�    . $
 Bold"�6�   $ NSimSun)�6�     5;      �� 5<   "   $
 temp�6*   5>        5?      $ 性别��6-   # [ 69   # [ 6    . $
 Bold"�6�   $ NSimSun)�6�     5@      �� 5A   "   $	 Sex�6*   5C        5D      $ 身份证地址��6-   # [ 6<   # [ 6�    . !   MaxWidth$	! 400Q�6�   $
 Bold"�6�   $ NSimSun)�6	    5E      �� 5F   "   $
 home�6*   5H        5I      $ 现居住地址��6-   # [ 6<   # [ 6�    .  $	! 400Q�6�   $
 Bold"�6�   $ NSimSun)�6
    5J      �� 5K   "   $ homeAddress�6*   5M        5N      $ 刷卡时间��6-    . $
 Bold"�6M   $ NSimSun)�6_     5O      �� 5P   "   $ 
Sbirthdate�6*   5R      4  .System.Windows.Controls.DataGridTemplateColumn  5S      $ 操作��61   "   CellTemplate" 5T      �� 5U   "   .R�5V   &   �� # �� Orientation$# 
Horizontal=�61   .+�5W   *   �� -   5         # MaterialDesignRaisedLightButton#��[ 5W   1   +  %MaterialDesignThemes.Wpf.ButtonAssist$   CornerRadius$$ 5��6j   % � ToolTip$% 删除��6�   $��Center=�6�   ���	#FF44B2F6
�������$��NSimSun)�6�   $��12(�6  $��55��6!  $��22��6,  .��5X   .   �� $��删除��68   5Y   +   �� 5Z   *   -   5        #��[ 5Z   1   $$ 5��6k   $% 打印��6�   $��Center=�6�   ���	#FF44B2F6
�������$��NSimSun)�6�   $��12(�6  $��55��6!  $��20,0,0,0q�6,  $��22��6>  .��5[   .   �� $��打印��68   5\   +   5]   '   5^   #   5`      5b      5c      �� 5d      $# 
Horizontal=�6   $��16��62   $��Bottom=�6>   $��Right=�6Y   $��60��6u   .+�5e      '  !MaterialDesignThemes.Wpf.PackIcon  -   5        &  Name$& disconn��5e   *   '   Kind$' LanDisconnect=�6;   $y�Visible=�6P   $
��Left=�6f   $
��Auto��6�   $��Stretch=�6�   ���	#DDEE46466�     5f      -	   5        $
& conn��5f   *   $' 
LanConnect=�68   $y�Hidden=�6J   $��	-16,0,0,0q�6^   $
��Left=�6r   $
��Auto��6�   $��Stretch=�6�   ���	#DD52DE116�   �� 5g      -
   5        ( ��Name$( connect_Text��5g      $��	未连接��62   $��6,0,0,0q�6=   $��NSimSun)�6N   $��12(�6c   $��Center=�6q   5h      5i      5j      5k      