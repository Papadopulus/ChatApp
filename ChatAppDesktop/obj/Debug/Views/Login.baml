   M S B A M L   `   `   `  ����I  EChatAppDesktop, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:ChatAppDesktop.ViewsChatAppDesktop.Views  5           ChatAppDesktop.Views.Login   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      X TXceed.Wpf.Toolkit, Version=4.5.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:ChatAppDesktop.Views     5      $	��420��5	      $	��540��6   $	��388��6-   .��5
      � 
�������.+�5   
   �� $��Center=�6   $	��Top=�62     �� Orientation$  Vertical=�6J   $��0,94,0,0q�6a   $	��230��6s   $��67��6   .+�5      �� $!��Please enter your username:��6   $��Center=�6:   $
��Bold"�6W   $��14(�6i   $��30��6w   $��17,0,17.038,0q�6�   6�   �� 5      -   5         ��Name$ Username��5      $��Center=�6(   $��23��6E    �� TextAlignment$
 Left=�6Q    �� TextWrapping$
 Wrap=�6f   $��Center=�6z   $	��210��6�   $��10,0,10.038,0q�6�   6�   5      �� 5   
   $  Vertical=�6   $��
0,190,0,31q�6,   .+�5      �� $+��%Select the algorithm you want to use:��6   $
��Bold"�6D   $��14(�6V   $��Center=�6d   $	��Top=�6�   $	��257��6�   �� 5      $  
Horizontal=�6   $��Center=�62   .+�5       	� -   5        $��Double Transposition��5       $
��Bold"�6=   $��12(�6O    	� 	GroupName$ AlgorithmGroup��6]   $��5q�6x   6�   	� 5!      -   5        $
��A5/2��5!      $ AlgorithmGroup��6-   $
��Bold"�6H   $��12(�6Z   $��5q�6h   6s   5#      �� 5%      -   5         ��Name$ DoubleTranspositionPanel��5%      $y�	Collapsed=�6;   .+�5&      �� $��Please enter your key:��6   $��Center=�69   $
��Bold"�6V   $��14(�6h   %  Xceed.Wpf.Toolkit.IntegerUpDown  5'      -   5          Name$	 Key��5'   %   $��Center=�5(      $��Center=�5)      $	��210��5*      $��25��5+      $��10,10,10.038,0q�5,      �  �Xceed.Wpf.Toolkit.Primitives.UpDownBase`1[[System.Nullable`1[[System.Int32, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   	AllowSpin . ,  &Xceed.Wpf.Toolkit.Primitives.InputBase   	Watermark$ Enter your key��5.      5/      �� 52      -   5        $ A52Panel��52      $y�	Collapsed=�6+   .+�53      �� $*��$Please enter 8 caracter as your key:��6   $��Center=�6G   $
��Bold"�6d   $��14(�6v   �� 54      -   5        $ A52Key��54      	 �� 	MaxLength$	 8��6*   $��Center=�68   $��23��6U   $
 Left=�6a   $
 Wrap=�6v   $��Center=�6�   $	��210��6�   $��10,0,10.038,0q�6�   55      57      �� 58   
   -   5        
 ��Name$
 LoginButton��58      $��Login��6'   $y�	Collapsed=�67   $	��100��6N   $��25��6Z   
���i��
�������$��285,340,155,55q�6�   59      5:      