   M S B A M L   `   `   `  ����G  CNucleic_Acid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Nucleic_Acid.ViewNucleic_Acid.View  5      "    Nucleic_Acid.View.TextDialog   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:Nucleic_Acid.View     5      b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5
      -   5           MaterialDesignBody#���   5        MaterialDesignPaper#���  5	      $��Medium"�5      $��14(�5      $	��460��5   ?   $	��665��6L   ��6   :  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��5      � .+�5   
   �� $��NSimSun)�6   $��30,20,-10,0q�6)   $��20��6>   $	��Top=�6J   用户信息6h   � 5   
     � Value$  0Q�6   ;  5MaterialDesignThemes.Wpf.Transitions.TransitionAssist   DisableTransitions . $��2��6Z   ���	#FFF1F1F1���	#FFF1F1F1$��	20,42,0,0q�6�   $	��Top=�6�   �� 5   
   ���	#FF0099FF$
��Left=�6%   $��12��6@   $��	20,23,0,0q�6L   ���	#FF0099FF$	��Top=�6r   $��3��6�   �� 5   
   $��	61,60,0,0q�6    �� Orientation$ 
Horizontal=�6(   $��45��6A   $	��Top=�6M   .+�5      �� $��Microsoft YaHei)�6   $��Center=�65   $��	0,14,0,15q�6P   $	��104��6c    �� TextAlignment$ Right=�6o   $
��Bold"�6�   	姓名：6�   �� 5      -   5         ��Name$ 	Text_Name��5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 请输入姓名��6)   $�� ��6P   $��NSimSun)�6Y   $	��405��6n   $��Center=�6z   ���	#89000000{�.  6�   5      �� 5   
   $��
61,110,0,0q�6   $ 
Horizontal=�6)   $��45��6B   $	��Top=�6N   .+�5      �� $��Microsoft YaHei)�6   $��Center=�65   $��	0,14,0,15q�6P   $	��104��6c   $ Right=�6o   $
��Bold"�6�   身份证号：6�   �� 5      -   5        $ 	Text_Card��5      $ 请输入身份证号��6)   $�� ��6R   $��NSimSun)�6[   $	��405��6p   $��Center=�6|   {�.  6�   5      �� 5   
   $��
61,160,0,0q�6   $ 
Horizontal=�6)   $��45��6B   $	��Top=�6N   .+�5      �� $��Microsoft YaHei)�6   $��Center=�65   $��	0,14,0,15q�6P   $	��104��6c   $ Right=�6o   $
��Bold"�6�   	性别：6�   �� 5      -   5        $ Text_Sex��5      $�� ��6(   $ 请输入性别��60   $��NSimSun)�6W   $	��405��6l   $��Center=�6x   {�.  6�   5      �� 5   
   $��
61,210,0,0q�6   $ 
Horizontal=�6)   $��45��6B   $	��Top=�6N   .+�5      �� $��Microsoft YaHei)�6   $��Center=�65   $��	0,14,0,15q�6P   $	��104��6c   $ Right=�6o   $
��Bold"�6�   身份证地址：6�   �� 5       -   5        $ Text_CardAddress��5       $�� ��60   $ 请输入身份证地址��68   $��NSimSun)�6b   $	��405��6w   $��Center=�6�   {�.  6�   5!      �� 5"   
   $��
61,260,0,0q�6   $ 
Horizontal=�6)   $��45��6B   $	��Top=�6N   .+�5#      �� $��Microsoft YaHei)�6   $��Center=�65   $��	0,14,0,15q�6P   $	��104��6c   $ Right=�6o   $
��Bold"�6�   现居住地址：6�   �� 5$      -   5        $ Text_homeAddress��5$      $�� ��60   $ 请输入现居住地址��68   $��NSimSun)�6b   $	��405��6w   $��Center=�6�   5%      �� 5&   
   $��
61,310,0,0q�6   $ 
Horizontal=�6)   $��45��6B   $	��Top=�6N   .+�5'      �� $��Microsoft YaHei)�6   $��Center=�65   $��	0,14,0,15q�6P   $	��104��6c   $ Right=�6o   $
��Bold"�6�   工作单位：6�   �� 5(      -   5        $ Text_company��5(      $�� ��6,   $ 请输入工作单位��64   $��NSimSun)�6]   $	��405��6r   $��Center=�6~   5)      �� 5*   
   $ 
Horizontal=�6   $��Bottom=�6.   $��Center=�6I   $��224,0,195,26q�6f   $	��246��6|   .+�5+      �� -   5         ��Name$ aceept��5+        MaterialDesignRaisedButton#��[ 6%    � ToolTip$ 确认��6Y   3  -MaterialDesignThemes.Wpf.ButtonProgressAssist   Value$ -1Q�6f   	   IsIndicatorVisible	 . 
   IsIndeterminate
 . )  #MaterialDesignThemes.Wpf.DialogHost   CloseDialogCommand#��Z 6  $��确认��6f  $��2,8,0,8q�6s  
�������$
��Left=�6�  $	��102��6�  $��NSimSun)�6�  $��12(�6�  $
��Bold"�6�  ���	#FF44B2F66�  �� 5,      -	   5        $��50,0,0,0q�5,        MaterialDesignFlatButton#��[ 6'   $ 取消��6Y   #��Z 6f   .��6�   取消5.      5/      51      52      