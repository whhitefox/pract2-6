   M S B A M L   `   `   `  ����T  PПрактическая6, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:Практическая6Практическая6  5   	   )    #Практическая6.AdminChat   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:Практическая6     5   	   -   5          � Title$  	AdminChat��5   	   $	��450��6   $	��800��6(   .��5	      �  � RowDefinitions 5
   
   �� 5      �� 5      $��8* �6   �� 5       � ColumnDefinitions 5   
   �� 5      �� 5      $
��0.5* �6   �� 5      $
��1.4* �6   �� 5      $
��0.6* �6   .+�5   
   �� -   5         ��Name$ 	LeftLabel��5      $��0��6#   $��0��60   $��Пользователи��6@   $��Center=�6W   $��Center=�6t   $��16(�6�   �� 5   
   -   5         ��Name$ 
LogsButton��5      $��0��6%   $��1��62   $��3��6B   $(��"Показать логи чата��6V   $��16(�6s   $��5,5,5,5q�6�   6�   �� 5   
   -   5         ��Name$ LeftListBox��5      $��1��6'   $��2��64   $��0��6E   �� 5   
   -   5        $ MessagesListBox��5      $��1��6+   $��1��68   $��3��6H   �� 5   
   -   5        $ 
ExitButton��5      $��2��6%   $��1��62   $��5,5,5,5q�6B   $��
Выйти��6S   $��16(�6c   6q   �� 5   
   -   5         ��Name$ MessageTextBox��5      $��2��6*   $��2��67   $��16(�6G   $��5,5,5,5q�6U   �� 5   
   -   5        $ 
SendButton��5      $��2��6%   $��3��62   $��5,5,5,5q�6B   $��16(�6S   $��Отправить��6a   6u   5      5      