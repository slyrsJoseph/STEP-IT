   M S B A M L   `   `   `  ����  LogAnalizerWpfClient%    LogAnalizerWpfClient.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Log Analyzer WPF��5   	   $	��500��6"   $	��700��6/   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $��* �6   �� 5
      $
��Auto �6   .+�5   
   �� $��0��6    �� Orientation$ 
Horizontal=�6"   $��0,0,0,10q�6;   .+�5      �� -   5         ��Name$ textBoxCurrentWeekFile��5      $	��250��66   $��0,0,5,0q�6B   �� 5      -   5        $��Browse Current��5      $	��100��6.   $��0,0,10,0q�6\   �� 5      -   5        $ textBoxPreviousWeekFile��5      $	��250��67   $��0,0,5,0q�6C   �� 5      -   5        $��Browse Previous��5      $	��100��6/   6;   5      �� 5   
   $��1��6   $ 
Horizontal=�6"   $��0,0,0,10q�6;   .+�5      �� -   5         ��Name$ comboBoxWeekType��5      $	��150��61   $��0,0,10,0q�6=   �� 5      -   5        $ comboBoxWeekType2��5      $	��150��62   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ dataGridResults��5       $��2��6      AutoGenerateColumns$
 True��69   $��0,0,0,10q�6T   �� 5   
   $��3��6   $ 
Horizontal=�6"   $
��Left=�6;   .+�5       �� -   5        $��Import��5       $	��100��6&   $��0,0,10,0q�62   6D   �� 5!      -	   5        $��Compare��5!      $	��100��6'   63   5"      5#      5$      