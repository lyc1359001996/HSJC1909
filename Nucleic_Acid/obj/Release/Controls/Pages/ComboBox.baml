   M S B A M L   `   `   `  ���� G  CNucleic_Acid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DesignLibraryDesignLibrary  R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� 
TargetType   ComboBoxToggleButton �� Property �� Value �� 
TargetType ��Name �� CornerRadius J� SnapsToDevicePixels F�Name J� RenderTransformOrigin	 �� Stretch
 �� Angle �� Triggers P� Property P� Value N� EnterActions �� 
TargetName �� TargetProperty W� From	 W� To g� Duration N� ExitActions  ComboBoxClearButton  ComboBoxTextbox ��Name  ComboBoxBorderBrush �� Color  ComboBoxTemplate �Name � ColumnDefinitions �Name � VerticalOffset � AllowsTransparency �� Mode �� RelativeSource � ActualWidth J� Effect3   -System.Windows.Media.Effects.DropShadowEffect     Color!    Opacity"    
BlurRadius#    ShadowDepth$ �� ClearTypeHint% ��Name& ��Name' �� ElementName( ��Name-  'System.Windows.Input.KeyboardNavigation)   DirectionalNavigation* ��Name   DesignLibrary.Assists+   PlaceHolderBrush,   PlaceHolder- J� IsHitTestVisible. d�Name/ d� 	IsChecked0 ��Name1 �� SelectionBoxItemTemplate2 �� SelectionBoxItem#3 �� SelectionBoxItemStringFormat4 �� ContentStringFormat5 �� 
TargetName6 ]� 
Conditions7 �� Property8 �� Value9 P� 
SourceName  ComboBoxEditableTemplate: � Cursor; ��Name< �� SelectedValue= �� 
IsReadOnly> �� Value? �� Binding@ �� PathA �� 
SourceName  DefaultComboBoxItemB �� Padding!   DesignLibrary.ComboBoxAssitC �� ModeD �� Triggers�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     %localclr-namespace:DesignLibrary  %  &	        &	 �    &	 p    &	 �    &	 �    7   7  &	 2    7   7  7  &	     '�� �    7  7  7  �� "  d�.���  � �� �#c0c4cc��  � �� �� " d�.R��� $ border��$ 4��#��z��#��z��#��z�� . ._��� $ splitBorder��
������ $��1q�$��Right=�$��0q� . $��35��._�F� $ arrow��$ 0.5,0.5&�$��12��$��12��$	 Uniform=�#��z�������xZu!��m*�i��|?6�@ �+��v��À@���+�*��v+���/��@�
�+:��v�@`��*X����m*�0�	�N��������a@(*�������a@l����@u@0�	ȾG��=}
�J���J֣p=
wk@ȾG�V]�@� �rh݃@���^1��GS��[2�G�z.s@p>�3�G�z.s@g�4��GӠ��5`�D���5���(\�v@g�4�R��xZu!��m*	$��0q�$��Center=�z�V� .~��� �� �� $
 0Q�T�  P�  �@d�	IsChecked$
 TrueY� �� .���� $ arrow��$V P(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)�.��W� $ 0X�$
 -180X�$ 	0:0:0:0.3D� �� .���� $ arrow��$V P(UIElement.RenderTransform).(TransformGroup.Children)[2].(RotateTransform.Angle)�.��W� $
 -180X�$ 0X�$ 	0:0:0:0.3D��� "  ��.���  � �� �#c0c4cc��  � ��
 ���� ��  � ��
 ���� ��  � ��$ 0 0 11 0q���  � ��$ 15����  � ��$ 15����  � ��$ Right=���  � �� �� " ��.R��� $ border��$ 4��#��z��#��z��#��z�� . ._�F� $��12��$��12��#��z��$	 Uniform=�������_�$�z�� ֯$�z����$�_����$T�����$2H\�	%
j%\�	% �E�\�	%R-G��O
%�dN� %�.�fm�$�����,4������$x$�2��$��$$�%T@$
x%
�\$�(�%�)}$�	�$��$�$x��$�,��$��$��$\W�$��$>��$� �� R�$� ����� P��b�$Ѯ2���$�<��bT�$�m|�w�$��h�+�$�n�T��f$\�[4$
�z\�$�\m<\}��
�z����\�h x��^  e�^�zHӆ�]B�'Ԉj����6���d��x����*����H�� P��J�� �����Rݠ ���R=�H��Ӽ���Q�|HvF���ڛӧk�\ox,����P3��%�|:u g�,tz����0 �H�0� ��j�0��m�&�˯0�V�0�w�&���0�����0�Ȕv��&�R=�H��� �    S\Rs)\�%  �� �g5h�� �g5��� �g5Rs)\Rs)��g5 ���g5�2�)��g5�X��P0�ד(�(�.���4��.�����wד��Y���X�2�)\�%  �� �	 P�  �@J�IsMouseOver . . ���  � ��
 �   ���  �@J�Opacity$	 0.4Q��� "  ��.���  � �� �� " ��.R��� $ PART_ContentHost��
������ |�.  $��Hidden=�$��Hidden=��� $ #e4e7ed���� " ��.R�� $ templateRoot�� .  �� $��* ��� #��� r�$��0 �.+�� $ 
PART_Popup��$ 12�� . $��2��$��1q�#��� .�$��Bottom=����� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+.���� $ dropDownBorder��
�������$��0 5 5 5q�$ 4��#��z #���  $��1q�    $  Black��$	! 0.1Q�$" 12Q�$# 2Q�._��� $ DropDownScrollViewer��.��� $
 grid��$$ Enabled=�$	��0 4q�.+��� $% canvas��$
��Left=�$��0��$	��Top=�$��0��.+��� $& 
opaqueRect��
������ ���� *ActualHeight+$' dropDownBorder������ *ActualWidth+$' dropDownBorder���� $( ItemsPresenter��$) 	Contained=�# z �� $* placeholder��#��z+ $y�	Collapsed=�#��z, #��z��- .  #��z��# z #��z��d� $. toggleButton��|�.  8��  #��z��#��z��#��z��$��2��/ �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+�� $0 clearBtn��|�.  $��2��8�� �� $* contentPresenter��#��z1 #��z��#��z2 #4 z3 #��z��- .  #��z��# z #��z�� P�  �@��HasItems .  . ��� $5 dropDownBorder�� � ��$ 95��P�  �@��SelectedIndex$ -1��. ��� $5 placeholder�� � y�$ Visible=�P�  �@J�IsMouseOver . . ���  � �� �#C3CBD6P�  �@ 	Clearable .  . ��� $5 clearBtn�� � y�$ 	Collapsed=�P�  �@J�IsMouseOver .  . ��� $5 clearBtn�� � y�$ 	Collapsed=�P�  �@J�IsKeyboardFocusWithin . . ���  � �� �#409EFF]� 6 �� 7 �@��
IsGrouping8 . �� "7 �@%�IsVirtualizingWhenGrouping8 .  ./���  � �� .  ]� 6 �� 7 �@ 	Clearable8 . �� 7 �@��SelectedIndex$8 -1��./��� $5 clearBtn�� � y�$ 	Collapsed=�P� $9 clearBtn�� � y�$ Visible=�. ��� $5 toggleButton�� � ��
 ���� P� $9 clearBtn�� � �� . . ���  �@��SelectedIndex$ -1��P� $9 DropDownScrollViewer�� � �� .  . ��� $5 
opaqueRect�� �� *VerticalOffset+$' DropDownScrollViewer�� �@��Top�� $5 
opaqueRect�� �� *HorizontalOffset+$' DropDownScrollViewer�� �@��Left�� " ��.R�� $ templateRoot�� .  �� $��* ��� #��� r�$��0 �.+�� $ 
PART_Popup��$ 12�� . $��2��$��1q�#��� .�$��Bottom=����� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+.���� $ dropDownBorder��
�������$��0 5 5 5q�$ 4��#��z #���  $��1q�    $  Black��$	! 0.1Q�$" 12Q�$# 2Q�._��� $ DropDownScrollViewer��.��� $
 grid��$$ Enabled=�$	��0 4q�.+��� $% canvas��$
��Left=�$��0��$	��Top=�$��0��.+��� $& 
opaqueRect��
������ ���� *ActualHeight+$' dropDownBorder������ *ActualWidth+$' dropDownBorder���� $( ItemsPresenter��$) 	Contained=�# z �� $* placeholder��|�.  #��z+ $y�	Collapsed=�#��z, #��z��- .  #��z��# z #��z��� $��2��#: z: 
������ .+�d� $. toggleButton��|�.  - .  #��z��#��z��#��z��8��  / �� *IsDropDownOpen+$ TwoWay=� �� *TemplatedParent+�� $0 clearBtn��|�.  $��2��8�� �� $ border��
������ #��z��._��� $; PART_EditableTextBox��#��z< #��z��#��z��8�� #��z��= �� *
IsReadOnly+ �� *TemplatedParent+ P�  �@��HasItems .  . ��� $5 dropDownBorder�� � ��$ 95���� $> 0��? �� $' PART_EditableTextBox��$@ Text.Length�.a��� $5 placeholder�� � y�$ Visible=��� $5 clearBtn�� � y�$ 	Collapsed=��� ? �� $' PART_EditableTextBox��$
@ Text�> Z� .a��� $5 placeholder�� � y�$ Visible=��� $5 clearBtn�� � y�$ 	Collapsed=�P�  �@J�IsMouseOver . . ���  � �� �#C3CBD6P�  �@ 	Clearable .  . ��� $5 clearBtn�� � y�$ 	Collapsed=�P�  �@J�IsMouseOver .  . ��� $5 clearBtn�� � y�$ 	Collapsed=�P�  �@J�IsKeyboardFocusWithin . . ���  � �� �#409EFF]� 6 �� 7 �@��
IsGrouping8 . �� "7 �@%�IsVirtualizingWhenGrouping8 .  ./���  � �� .  ]� 6 �� 7 �@ 	Clearable8 . �� 7 �@��SelectedIndex$8 -1���� $A PART_EditableTextBox��8 Z� 7 � ��./��� $5 clearBtn�� � y�$ 	Collapsed=�P� $9 clearBtn�� � y�$ Visible=�. ��� $5 toggleButton�� � ��
 ���� P� $9 clearBtn�� � �� . . ��� $5 PART_EditableTextBox�� Z�  � ����  �@��SelectedIndex$ -1��P� $9 DropDownScrollViewer�� � �� .  . ��� $5 
opaqueRect�� �� *VerticalOffset+$' DropDownScrollViewer�� �@��Top�� $5 
opaqueRect�� �� *HorizontalOffset+$' DropDownScrollViewer�� �@��Left�� "  ��.���  � ��$ 34����  � ��$ 0q���  � ��$ 10,0,0,0q���  � ��$ 14(���  � �� �#606266��  �@�Cursor$
 Hand����  � �� �� " ��.R��� $ Bd��#��z��#��z��#��z��#B z�� . ._��� #��z��# z #��z�� P�  �@J�IsMouseOver . . ���  � �� �#F5F7FAP�  �@��
IsSelected . . ���  � �� �#409EFF��  � �� �#F5F7FA�� "  ��.���  �@�Cursor$
 Hand����  �@�ForceCursor . ��  � ��$	 240����  � ��$ 40����  � ��
 ���� ��  � ��8   ��  � �� �#DCDFE6��  � ��$ Center=���  �@ PlaceHolder$ 	请选择����  � ��$
 15 0q���  � ��$ Center=���  � ��8  ��  �@ 
IsEditable ��  �� $
C Self=�$@ 
IsEditable�D P�  �@��
IsEditable . . ���  � �� .  ��  � ��8  