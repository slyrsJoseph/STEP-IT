   M S B A M L   `   `   `  ����  LogAnalizerWpfClient*    $LogAnalizerWpfClient.TableViewWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Alarm Log Table View��5   	   $	��600��6&   $
��1000��63    � WindowStyle$
 None=�5   	    � 
ResizeMode$ 	CanResize=�6   ���#1A1A1A.��5      �� ���#00FFFF$��2q�6#    �� CornerRadius$ 12��67   ._�5   
   �  � RowDefinitions 5      �� 5	      $��40 �6    �� 5
      $��* �6    .+�5      ]� -   5        $��0��5      ���#1A1A1A.+�5      �� $��📂 Alarm Log Table View��6   ���#00FFFF$��Center=�5      $
��Bold"�5      $��10,0,0,0q�5      �� 5       �� Orientation$ 
Horizontal=�6   $��Right=�66   .+�5      �� -   5        $��_��5      $��40��6)   $��30��64   ���#1A1A1A���#00FFFF���#00FFFF5   G   �� 5      -   5        $	��⬜��5      $��40��6)   $��30��64   ���#1A1A1A���#00FFFF���#00FFFF5   G   �� 5      -   5        $	��✖��5      $��40��6)   $��30��64   ���#1A1A1A���#FF4B4B���#FF4B4B5   G   5      5      &   System.Windows.Controls.DataGrid  5      -   5          Name$ dataGrid��5      $��1��5          AutoGenerateColumns$
 True��5!         HeadersVisibility$ Column=�5"      ���#1A1A1A
�������	   GridLinesVisibility$	 
Horizontal=�5%      ���#00FFFF$��1q�5'      
   ColumnHeaderHeight$
 35Q�5(         RowHeaderWidth$ 0Q�5)      $��12(�5*      $��Medium"�5+      $   HorizontalScrollBarVisibility$
 Auto=�5,       � 	Resources 5-      =  7System.Windows.Controls.Primitives.DataGridColumnHeader �� 
TargetType �� Property �� Value)  #System.Windows.Controls.DataGridRow��%t  '        '  �     �� 5.      "  6   .�5/      ��  � �� �#00FFFF67   �� 50       � ��$
 Bold"�67   �� 51       � �� �#11111167   52      �� 53      "  6   .�54      ��  � �� �#1A1A1A67   �� 55       � ��
 �����67   �� 56       � ��$ 12(�65   �� 57       � �� �#00FFFF68   58      59      5:      5;      5<      5=      