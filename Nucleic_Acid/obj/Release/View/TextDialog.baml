   M S B A M L   `   `   `  ���� G  CNucleic_Acid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Nucleic_Acid.ViewNucleic_Acid.View  "    Nucleic_Acid.View.TextDialog   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     1localclr-namespace:Nucleic_Acid.View     b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -      MaterialDesignBody#���     MaterialDesignPaper#���  $��Medium"�$��14(�$	��460��$	��665����:  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .��� .+��� $��NSimSun)�$��30,20,-10,0q�$��20��$	��Top=�用户信息�   � Value$  0Q�;  5MaterialDesignThemes.Wpf.Transitions.TransitionAssist   DisableTransitions . $��2�����	#FFF1F1F1���	#FFF1F1F1$��	20,42,0,0q�$	��Top=��� ���	#FF0099FF$
��Left=�$��12��$��	20,23,0,0q����	#FF0099FF$	��Top=�$��3���� $��	61,60,0,0q� �� Orientation$ 
Horizontal=�$��45��$	��Top=�.+��� $��Microsoft YaHei)�$��Center=�$��	0,14,0,15q�$	��104�� �� TextAlignment$ Right=�$
��Bold"�	姓名：�� -    ��Name$ 	Text_Name��)  #MaterialDesignThemes.Wpf.HintAssist   Hint$ 请输入姓名��$�� ��$��NSimSun)�$	��405��$��Center=����	#89000000{�.  �� $��
61,110,0,0q�$ 
Horizontal=�$��45��$	��Top=�.+��� $��Microsoft YaHei)�$��Center=�$��	0,14,0,15q�$	��104��$ Right=�$
��Bold"�身份证号：�� -   $ 	Text_Card��$ 请输入身份证号��$�� ��$��NSimSun)�$	��405��$��Center=�{�.  �� $��
61,160,0,0q�$ 
Horizontal=�$��45��$	��Top=�.+��� $��Microsoft YaHei)�$��Center=�$��	0,14,0,15q�$	��104��$ Right=�$
��Bold"�	性别：�� -   $ Text_Sex��$�� ��$ 请输入性别��$��NSimSun)�$	��405��$��Center=�{�.  �� $��
61,210,0,0q�$ 
Horizontal=�$��45��$	��Top=�.+��� $��Microsoft YaHei)�$��Center=�$��	0,14,0,15q�$	��104��$ Right=�$
��Bold"�身份证地址：�� -   $ Text_CardAddress��$�� ��$ 请输入身份证地址��$��NSimSun)�$	��405��$��Center=�{�.  �� $��
61,260,0,0q�$ 
Horizontal=�$��45��$	��Top=�.+��� $��Microsoft YaHei)�$��Center=�$��	0,14,0,15q�$	��104��$ Right=�$
��Bold"�现居住地址：�� -   $ Text_homeAddress��$�� ��$ 请输入现居住地址��$��NSimSun)�$	��405��$��Center=��� $��
61,310,0,0q�$ 
Horizontal=�$��45��$	��Top=�.+��� $��Microsoft YaHei)�$��Center=�$��	0,14,0,15q�$	��104��$ Right=�$
��Bold"�工作单位：�� -   $ Text_company��$�� ��$ 请输入工作单位��$��NSimSun)�$	��405��$��Center=��� $ 
Horizontal=�$��Bottom=�$��Center=�$��224,0,195,26q�$	��246��.+��� -    ��Name$ aceept��  MaterialDesignRaisedButton#��[  � ToolTip$ 确认��3  -MaterialDesignThemes.Wpf.ButtonProgressAssist   Value$ -1Q�	   IsIndicatorVisible	 . 
   IsIndeterminate
 . )  #MaterialDesignThemes.Wpf.DialogHost   CloseDialogCommand#��Z $��确认��$��2,8,0,8q�
�������$
��Left=�$	��102��$��NSimSun)�$��12(�$
��Bold"����	#FF44B2F6�� -	   $��50,0,0,0q�  MaterialDesignFlatButton#��[ $ 取消��#��Z .��取消