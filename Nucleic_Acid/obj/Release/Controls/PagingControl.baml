   M S B A M L   `   `   `  ����   Nucleic_Acid    WpfPaging.PagingControl   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     b ^MaterialDesignThemes.Wpf, Version=3.2.0.1979, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes     MaterialDesignBody#���   $��Medium"�$��14(�$��55��$	��850����:  4MaterialDesignThemes.Wpf.MaterialDesignFontExtension  .���   � ColumnDefinitions  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+���  �� Orientation$ 
Horizontal=�.+��� $	��共��$��Center=�$��10,0,0,0q��� -    ��Name$ 
Text_Total��$��0��$��Center=��� $	��条��$��Center=��� -    ��Name$ btnBackPage��$��10,0,0,0q�$��	上一页��$��40��$��80��$��0��
��������� -    ��Name$ scrollViewer��$��Disabled=�$��Hidden=�$	��500��$��1��$
��Left=�$
��Auto��.���� -    ��Name$ 	pagePanel��$ 
Horizontal=�.+��� $��1��$��40��$��40��$��5,0,0,0q��� $��2��$��40��$��40��$��5,0,0,0q��� $��3��$��40��$��40��$��5,0,0,0q��� $��4��$��40��$��40��$��5,0,0,0q��� $��5��$��40��$��40��$��5,0,0,0q��� $��6��$��40��$��40��$��5,0,0,0q��� $��7��$��40��$��40��$��5,0,0,0q��� $��8��$��40��$��40��$��5,0,0,0q��� $��9��$��40��$��40��$��5,0,0,0q��� -   $ btnNextPage��$��	下一页��$��40��$��80��$��2��$��5,0,0,0q�
��������� $��
1,0,-196,0q�$��3��$ 
Horizontal=�$��Center=�.+��� $��跳至��$��Center=��� -    ��Name$ TextBox_page�� * &MaterialDesignOutlinedTextFieldTextBox#��[ $	��Top=� �� AcceptsReturn .   �� TextWrapping$
 Wrap=�"	 �� VerticalScrollBarVisibility$
	 Auto=�)  #MaterialDesignThemes.Wpf.HintAssist
   Hint$
 页数�� �� 	MaxLength$ 9���� $	��页��$��Center=��� -   $��10,0,0,0q�$
��Bold"�.@���  �� 
IsSelected . .��	10条/页�� .��	20条/页�� .��	30条/页�� .��	50条/页