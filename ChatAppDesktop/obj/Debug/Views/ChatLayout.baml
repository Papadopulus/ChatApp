   M S B A M L   `   `   `  ����I  EChatAppDesktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:ChatAppDesktop.ViewModelsChatAppDesktop.ViewModels  5      %    ChatAppDesktop.Views.ChatLayout   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9local'clr-namespace:ChatAppDesktop.ViewModels     5        � DataContext  5	      0   *ChatAppDesktop.ViewModels.MessageViewModel  5
   
   .��5      � 
������� � RowDefinitions 5   
   �� 5      $��31 �6   6*   �� 5      $��* �6   6)   �� 5      $��70 �6   6*    � ColumnDefinitions 5   
   �� 5      $��* �6   6+   .+�5   
   � $��0��6   .+�5      �� -   5        $��Title��5      $��Connected Bogdan��6!   
���i��
�������$��12(�6f   $��Right=�6t   $��1q�6�   6�   5      � 5   
   $��1��6   .+�5      �� -   5        $��ContentScroller��5      $��Disabled=�62   $
��Auto=�6[   
�������.��5      �� -   5        $��MessageTemplate��5      ��66   �� *FilteredMessages+��5      �� 5      .R�5      � $��5q�6#    5   "   �� 5   &   $
��Auto �64   6D   �� 5    &   $
��Auto �64   6D   .+�5#   "   � $��0��6'   .+�5$   &   ��  �� Orientation$ 
Horizontal=�61   .+�5%   *   �� $��Right=�6N   ��61   �� *
FromUser+6l   �� 5&   *   $��:��61   $��Right=�6=   6[   5(   '   5)   #   � 5*   "   $��1��6'   .+�5+   &   �� $
��Left=�60   $
��Wrap=�6K   ��6_   �� *MessageSent+6~   5,   #   5-      5.      50      51      52      � 54   
   $��2��6   
���i��.+�56      �� $ 
Horizontal=�6   $��Bottom=�62   $��Center=�6M   .+�57      �� -   5        $��MessageContent��57      
�������$	��180��6C    �� TextWrapping$
 Wrap=�6O    �� AcceptsReturn . $��10q�6x   $��22��6�   6�   �� 58      -   5        $��
SendButton��58      $
��Send��6+   $��10q�6:   $��Right=�6F   
�������
���   �$��70��6�   6�   59      �� 5:      $ 
Horizontal=�6   $	��Top=�62   .+�5;      �� -   5        $��ShowCodedMessageCheckBox��5;      $��Coded Message��6;   $��8q�6~   $��10(�6�   �� 5=      -   5         ��Name$ CodedMessageTextBlock��5=       �� 
IsReadOnly . 
�������$	��174��6^   $
 Wrap=�6j   $��22��6~   $y�	Collapsed=�6�   5?      5@      5A      5B      