   M S B A M L   `   `   `  ����B  >Lab2wpf, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:Lab2wpfLab2wpf  5   	       Lab2wpf.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Lab2wpf     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � -   5         �Name$ DataBase��5	      .+�5
   
   �� -   5        $��Обновить��5
      $
��Left=�6$   $��
588,81,0,0q�6?   $	��Top=�6S   $	��123��6k   $��61��6w   6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 
ListMeance��5      $
��Left=�6'   $	��351��6B   $	��Top=�6O   $	��573��6g   $��	10,10,0,0q�6s      AutoGenerateColumns .     
IsReadOnly .    Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Id��6%   1  +System.Windows.Controls.DataGridBoundColumn   Binding 61   �� *Id+  5      $ Имя угрозы��6%    69   �� *Name+5      �� 5   
   -   5        $)��#Краткая информация��5      $
��Left=�6.   $��588,175,0,0q�6I   $	��Top=�6^   $	��123��6v   $��62��6�   6�   �� 5   
   -   5         ��Name$ NumberOfRecords��5      $
��Left=�6,   $��
63,366,0,0q�6G   $	��Top=�6[   $��79��6s   5      �� 5   
   -   5        	 ��Name$	 Button1��5      $��
Назад��6"   $
��Left=�62   $��160,366,0,0q�6M   $	��Top=�6b   $��75��6z   6�   �� 5   
   -   5        $	 Button2��5      $��Вперёд��6"   $
��Left=�63   $��257,366,0,0q�6N   $	��Top=�6c   $��75��6{   6�     5   
   -   5        $ 
ListUpdate��5      $
��Left=�6'   $	��227��6B   $��	10,10,0,0q�6O   $	��Top=�6b   $	��573��6z    .   .  5        5      $ Id��6%    61   �� *Id+  5      $ Поле��6%    63   �� *Pole+  5      $ Было��6%    63   �� *Was+  5      $ 
Стало��6%    64   �� *Bycame+5      5      5       