   M S B A M L   `   `   `  ����A  =Cinema, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:Cinema.ViewModelsCinema.ViewModels  5      +clr-namespace:Cinema.ViewsCinema.Views  5      "    Cinema.Views.ReservesControl   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      .vmclr-namespace:Cinema.ViewModels     5      ,localclr-namespace:Cinema.Views     5        � DataContext  5
      )  #Cinema.ViewModels.ReservesViewModel  5   
    � 	Resources 5       �� 
TargetType �� Property �� Value � Orientation��%�   '��       �� 5   
   " ��6   .�5      ��  � �� 5      �� 5      .R�5      � $ 
Horizontal=�6$   $��Center=�6=   5      5      5      5      .��5      �  � ColumnDefinitions 5   
   �� 5      $��90 �6   �� 5      $��* �6   .+�5   
   � $��1��6    � RowDefinitions 5      �� 5      $	��10* �6    �� 5       $	��40* �6    .+�5"      � $��0��6    5#      �� 5$      $
��0.4* �6'   �� 5%      $��* �6'   .+�5'      �� $	��175��6   $��Right=�6)   ��5(      �� 5)       �� Color$ 
LightCoral��6*   	 �� Opacity$		 0.3Q�6=   .+�5+      �� 
 �� Orientation$
 
Horizontal=�6!   $��5q�6:   .+�5,      �� $��User: ��6    �� 5-      $��Center=�6H   ��6$   �� *SelectedUser.Login+6H   5.      �� 5/      $
 
Horizontal=�6!   $��5q�6:   .+�50      �� $��Seats: ��6    �� 51      $
��Auto=�6'   $��Disabled=�6L   .��52      �� $
��Auto=�6I   ��6&   �� *
UsersSeats+��53   "   �� 54   &   $	 0Q�66   ��56   "   �� 57   &   .R�58   *   �� ��64   �� *Number+59   '   5;      5<      5=      5>      �� 5?      $��1��6   $
��Left=�6n   $	��475��6�   ��6*   �� *Users+ �� SelectedItem 6H   �� *SelectedUser+��5@      �� 5A      $		 0.5Q�6*   $ Yellow��68   ��5C      �� 5D      $	 0Q�6*   ��5F      �� 5G      .R�5H      �� $��1q�6%   $��5q�69   ._�5I   "   �� ��6,   �� *Login+5J      5K      5M      5N      �� 5O      -   5         ��Name$ lb��5O      $��1��6@   $	��650��6M   $��Disabled=�6Y   ��.  ��6"   �� *Seats+��5P      �� 5Q      $		 0.5Q�6&   $ 	LightBlue��64   ��5S      �� 5T      $	 0Q�6&   ��5V      �� 5W      .R�5X      �� $
 
Horizontal=�6%   .+�5Y      �� 
���   �$	��0.5q�69   $��10q�6O   ._�5Z   "   �� $��30��6(   $��30��63   $��Center=�6?   $��Center=�6Z   ��6w   �� *Number+5[      5\      5]      ��5_      �� 5`      " ��6   .�5a      ��  � {� 66   �� *IsFree+5b      5d      5e      �� 5f   
   $��20 0 0 0q�6   .+�5g      �� ��6   �� *CencellationCommand+.��6=   Cencellation6K   5h      5i      5j      