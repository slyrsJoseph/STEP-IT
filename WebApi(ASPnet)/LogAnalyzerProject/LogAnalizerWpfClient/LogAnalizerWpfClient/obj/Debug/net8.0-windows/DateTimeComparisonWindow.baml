   M S B A M L   `   `   `  ����  LogAnalizerWpfClient3    -LogAnalizerWpfClient.DateTimeComparisonWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Date/Time Comparison��5   	   $	��400��6&   $	��500��63    � WindowStartupLocation$ CenterScreen=�5   	   ���#1A1A1A � WindowStyle$
 None=�5   	    � AllowsTransparency$
 True��6    � 	Resources 5	         TechnoButtonStyle �� 
TargetType �� Property �� Value �� 
TargetType	 �� CornerRadius
 �� Triggers P� Property P� Value��%�  &	        �� 5
   
   " ��6*   .�5      ��  � �� �#0000006+   �� 5       � ��
 �����6+   �� 5       � ��$	 6,2q�6(   �� 5       � ��$ 4q�6'   �� 5       � ��$
 Bold"�6+   �� 5       � ��$ 12(�6)   �� 5       � �� �#00FFFF6,   �� 5       � ��$ 2q�60   �� 5       �@�Cursor$
 Hand��6'   �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $	 6��6!   #��z��62   #��z��6\   #��z��6�   ._�5      �� $��Center=�6/   $��Center=�6L   5      5      5      
 5      P� 5       �@J�IsMouseOver$
 True��61   . �5      ��  � �� �#00FFFF63   �� 5        � �� �#00000063   5!      5#      5$      .��5&      �� ���#1A1A1A$	 10��6"   ���#00FFFF$��2q�6J    �� Padding$ 10q�6^   ._�5'   
   �� .+�5(      �� $��Date/time range compare��6   ���#00FFFF$��16(�6L   $
��Bold"�6Z   $��0,0,0,10q�6l   $��Center=�6~   �� 5+       �� Orientation$ Vertical=�6   $	��0,5q�60   .+�5,      �� $��Select first week:��6   ���#00FFFF66   �� 5-      -   5         ��Name$ 
comboWeek1��5-      6/   5.      �� 50      $ 
Horizontal=�6   $	��0,5q�62   .+�51      (  "System.Windows.Controls.DatePicker  -   5          Name$ dateRange1Start��51      $	��200��66   $��0,0,10,0q�6B     52      -   5        $ dateRange1End��52      $	��200��64   53      �� 55      $ Vertical=�6   $
��0,10q�60   .+�56      �� $��Select second week:��6   ���#00FFFF67   �� 57      -   5        $ 
comboWeek2��57      6/   58      �� 5:      $ 
Horizontal=�6   $	��0,5q�62   .+�5;        -   5        $ dateRange2Start��5;      $	��200��66   $��0,0,10,0q�6B     5<      -   5        $ dateRange2End��5<      $	��200��64   5=      �� 5?      $ 
Horizontal=�6   $��Center=�62   $��0,20,0,0q�6O   .+�5@      �� -   5        $��Compare��5@      $	��100��6+   #��[  6P   �� 5A      -   5        $
��Back��5A      $	��100��6(   #��[  6J   5B      5C      5D      5E      