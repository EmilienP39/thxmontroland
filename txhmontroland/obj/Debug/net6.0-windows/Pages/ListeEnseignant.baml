   M S B A M L   `   `   `  ����H  Dtxhmontroland, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:txhmontroland.Pagestxhmontroland.Pages  5      )    #txhmontroland.Pages.ListeEnseignant   5      W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:txhmontroland.Pages     5        N� Title$  ListeEnseignant��5	      .��5      �  � RowDefinitions 5   
   �� 5      $
��80px �6   �� 5      $��* �6   .+�5   
   � $��0��6    5      �� 5      $��* �6     � ColumnDefinitions 5      �� 5      .+�5      � $��0��6   .+�5      �� $��Enseignants��6   $��0��6.   $
��Bold"�6;   $��40(�6M   $��835,6,836,10q�6[   �� 5      -   5         ��Name$ 
btn_retour��5      $��0��6-   $��Retour��6=   
����������	#FFFFCF00$��30,19,1755,19q�6�   $��25(�6�   ��6a   Z� 6�   5      5      � 5   
   $��1��6    5      �� 5       $��15 �6#   �� 5!      $	��37* �6#   �� 5"      $
��117* �6#   �� 5#      $��15 �6#    5%      �� 5&      $��10 �6    �� 5'      $��* �6    �� 5(      $��10 �6    .+�5*      &   System.Windows.Controls.DataGrid  -   5          Name$ 
dg_joueurs��5*   Q   $��1��6      AutoGenerateColumns$ False��6(   $��1��6D      
IsReadOnly$
 True��6e   $��25(�6w   $��2��6�      Columns 5+      0  *System.Windows.Controls.DataGridTextColumn  5,      ,  &System.Windows.Controls.DataGridColumn   Header$	 Nom��6)   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6N   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 66   �� *nom+   HeaderStyle 5-      �� 5.       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6$   .�5/   "   ��  �� Property � �� �� Value$ Center=�6O   50         ElementStyle 52      �� 53      " ��6$   .�54   "   ��  � ��$ Center=�6H   55      57        58      $ Prénom��6)   $	 * 6T   
 69   �� *	prénom+ 59      �� 5:      "  6$   .�5;   "   ��  � ��$ Center=�6O   5<       5>      �� 5?      " ��6$   .�5@   "   ��  � ��$ Center=�6H   5A      5C        5D      $ Contract��6)   $	 * 6X   
 6;   �� *
contract+ 5E      �� 5F      "  6$   .�5G   "   ��  � ��$ Center=�6O   5H       5J      �� 5K      " ��6$   .�5L   "   ��  � ��$ Center=�6H   5M      5O      4  .System.Windows.Controls.DataGridTemplateColumn  5P      $
 Voir��6-   $	 * 6;    5Q      �� 5R      "  6$   .�5S   "   ��  � ��$ Center=�6O   5T         CellTemplate 5V      �� 5W      .R�5X   "   �� $��btnView��6<   -   5        $
��Voir��5X   K   
����������	#FFFFCF00$��4q�6�   
 � Tag 6)   �� *id+��6m   Z� 6�   5Y      5[      5]      5^      5_      5`      