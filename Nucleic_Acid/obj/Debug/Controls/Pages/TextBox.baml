   M S B A M L   `   `   `  ����G  CNucleic_Acid, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:DesignLibraryDesignLibrary  5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089  �� MergedDictionaries �� Source �� 
TargetType �� Property   ControlWidth �� Value  ControlHeight    DesignLibrary.Assists  LargeCorner  BorderBrush-  'System.Windows.Input.KeyboardNavigation  FontSize.Base �� RelativeSource �� Mode �� Path �� Mode	 �� 
TargetType
 ��Name    CornerRadius �� CornerRadius J� SnapsToDevicePixels � ColumnDefinitions �Name  	Icon.Size �� Stretch    	IconBrush    Icon ��Name J� IsHitTestVisible    PlaceHolderBrush    PlaceHolder ��Name ��Name  ClearButton �� Triggers ]� 
Conditions �� Property �� Value �� 
TargetName �� Value �� Binding  P� 
SourceName! P� Property" P� Value# �� Triggers  Control.Unable.Background  MouseOverBorderBrush 	 FocusedBorderBrush�� 5      D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     5      #libclr-namespace:DesignLibrary  5        5      �� 5   
   $F @pack://application:,,,/DesignLibrary;component/Themes/Theme.xaml?�6   6i   %�  '��       7  �� 5      " ��6   .�5	   
   ��  � ��# �   6"   6K   �� 5
   
    � ��# �  6#   6M   �� 5   
    �@  CornerRadius# �  65   6]   �� 5   
    �@�Cursor$
 Hand��6#   62   �� 5   
    � ��$ Center=�65   6F   �� 5   
    � ��$ 0q�6$   60   �� 5   
    � ��# �  6(   6P   �� 5   
    � ��$ 1q�6,   �� 5   
    �@ TabNavigation$
 None=�6=   �� 5   
    � ��$
 Left=�67   �� 5   
    �@J�	AllowDrop . 6&   �� 5   
    �@��PanningMode$ VerticalFirst=�65   �� 5   
    �@��IsFlicksEnabled .  63   �� 5   
    � ��# �  6%   6O   �� 5   
    �@  PlaceHolder$ 请输入内容��64   6D   �� 5   
    �@  IsEnable 61   ��  �� $
 Self=�$ 	IsEnabled�$ TwoWay=�6�   �� 5   
    � �� 5      �� 5      "	 ��6"   .R�5      �� $
 border��6W   # z 6   #��z��6g   #��z��6�   #��z��6�    . ._�5      �  5      �� 5   "   $��10 �63   �� 5    "   $
��auto �63   �� 5!   "   �� 5"   "   $
��auto �63   �� 5#   "   $
��auto �63   .+�5%      � $ 	iconPanel��6D   $��1��6#   $��0 0 5 0q�63   #���  6W   #���  6{   .+�5&   "   F� $��Center=�6'   $ Uniform=�6D   #��z 6V   # z 6�   6�   �� 5'   "   65   5(      �� 5)      $ placeHolder��6=   $��2��6-   $y�	Collapsed=�6R    .  #��z 6�   #��z��5*   -   #��z��5+   -   #��z��5,   -   #��z 6P   6�   �� 5-      $ PART_ContentHost��6<   $��2��6+   |�.  $��Hidden=�6h   $��Hidden=�6�   �� 5.      $ clearButton��50   %   $��4��5.   %   $��30��65   $��0q�6@   $y�	Collapsed=�5/   %   8��  51   L   52      53       54      ]� 55       56      �� 57   "    �@  	Clearable . 6\   �� 58   "    �@J�IsMouseOver . 6R   ./�5:      �� $ clearButton��6%    � y�$ Visible=�6T   6f   5;      ]� 5<       5=      �� 5>   "    �@  	Clearable . 6\   �� 5?   "    �@J�	IsFocused . 6P   ./�5A      �� $ clearButton��6%    � y�$ Visible=�6T   6f   5B      �� 5C      $ 0��6u    6&   ��  �� $
 Self=�$ Text.Length�.a�5D      �� $ placeHolder��6%    � y�$ Visible=�6T   6f   �� 5E      $ clearButton��6%    � y�$ 	Collapsed=�6T   6h   5F      �� 5G       6&   ��  �� $
 Self=�$
 Text� 6n   Z� .a�5H      �� $ placeHolder��6%    � y�$ Visible=�6T   6f   �� 5I      $ clearButton��6%    � y�$ 	Collapsed=�6T   6h   5J      P� 5K      $  clearButton��6"   ! � ��" . . �5L      ��  65   Z�  � ��6H   5M      P� 5N      ! �@  Icon$" Default=�6>   . �5O      �� $ 	iconPanel��6%    � y�$ 	Collapsed=�6S   6g   5P      P� 5Q      ! �@  IconPosition$" Right=�6F   . �5R      �� $ 	iconPanel��6%    � ��$ 3��6S   6_   5S      P� 5T      ! �@  IconPosition$" Strech=�6F   . �5U      �� $ 	iconPanel��6%    � ��$ 2��6S   6_   5V      5X      5Z      # 5[   
   P� 5\      ! � {�" .  . �5]      ��  � ��# �  6/   6e   5^      P� 5_      ! �@J�IsMouseOver" . . �5`      ��  � ��# �  60   6a   5a      P� 5b      ! �@J�	IsFocused" . . �5c      ��  � ��# � 	 60   6_   5d      P� 5e      ! � ��$	" Top=�6:   . �5f      ��  � ��$
 0 10q�6,   6;   5g      5i      5j      