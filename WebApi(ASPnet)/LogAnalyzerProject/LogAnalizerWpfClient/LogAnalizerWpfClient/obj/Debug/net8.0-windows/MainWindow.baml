   M S B A M L   `   `   `  ����  LogAnalizerWpfClient%    LogAnalizerWpfClient.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Alarm Log Analyzer��5   	   $	��450��6$   $	��800��61    � 
ResizeMode$ NoResize=�5   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   .+�5   
   �� $"��Import Logs with Week label:��6   $��Center=�68   $��0��6S   $��0��6`   $��0,0,10,0q�6p   �� 5   
   -   5         ��Name$ comboImportWeek��5      $	��150��6,   $��0��68   $��0��6E   $��Right=�6U   $��0,0,10,0q�6q   �� 5   
   -   5         ��Name$ 	btnImport��5      $��
Import Log��6$   $	��100��69   $��25��6E   $��0��6Q   $��1��6^   $��0,0,10,0q�6n   6�   �� 5   
   -   5        $ btnResetLogs��5      $��
Reset Logs��6'   $	��100��6<   $��25��6H   $��0��6T   $��2��6a   6q   �� 5   
   $��Loaded logs:��6   $��1��6(   $��0��65   $��0,10,0,5q�6E   $��3��6W   �� 5   
   -   5         ��Name$ listBoxLogs��5      $��2��6'   $��0��64   $��3��6D   $	��150��6X   $��0,0,0,10q�6e   �� 5   
   $��3��6   $��0��6!   $��3��61   $
��0,10q�6E   �� 5"   
    �� Orientation$ 
Horizontal=�6   $��4��6.   $��0��6;   $��3��6K   $
��Left=�6_   .+�5#      �� -   5        $ 
btnCompare��5#      $��Compare Logs��6)   $	��120��6@   $��30��6L   $��	0,10,10,0q�6X   ${�False��6�   5%      5&      5'      