   M S B A M L   `   `   `  ����H  Dtxhmontroland, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:txhmontroland.Pagestxhmontroland.Pages  5      '    !txhmontroland.Pages.ListeConcours   5      W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:txhmontroland.Pages     5        N� Title$  ListeConcours��5	      .��5      �  � RowDefinitions 5   
   �� 5      $
��80px �6   �� 5      $��* �6   .+�5   
   � $��0��6    5      �� 5      $��* �6     � ColumnDefinitions 5      �� 5      .+�5      � $��0��6   .+�5      �� $��0��6   $��Concours��6(   $��0��6;   $
��Bold"�6H   $��40(�6Z   $��857,9,856,11q�6h   �� 5      -   5         ��Name$ 
btn_retour��5      $��0��6-   $��Retour��6=   $��47��6N   $	��148��6Z   
����������	#FFFFCF00$��23,0,0,0q�6�   $
��Left=�6�   $��Center=�6�   $��25(�6  ��6y   Z� 6  5      5      � 5   
   $��1��6    5      �� 5       $��15 �6#   �� 5!      $	��37* �6#   �� 5"      $
��117* �6#   �� 5#      $��15 �6#    5%      �� 5&      $��10 �6    �� 5'      $��* �6    �� 5(      $��10 �6    .+�5*      &   System.Windows.Controls.DataGrid  -   5          Name$ dg_concours��5*   Q   $��1��6      AutoGenerateColumns$ False��6(   $��1��6D      
IsReadOnly$
 True��6f   $��25(�6x   $��2��6�      Columns 5+      0  *System.Windows.Controls.DataGridTextColumn  5,      ,  &System.Windows.Controls.DataGridColumn   Header$	 Nom��6)   	   Width5  /System.Windows.Controls.DataGridLengthConverter$	 * 6N   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 66   �� *nom+   HeaderStyle 5-      �� 5.       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6$   .�5/   "   ��  �� Property � �� �� Value$ Center=�6O   50         ElementStyle 52      �� 53      " ��6$   .�54   "   ��  � ��$ Center=�6H   55      57        58      $ Code matière��6)   $	 * 6_   
 6?   �� *codeconcour+ 59      �� 5:      "  6$   .�5;   "   ��  � ��$ Center=�6O   5<       5>      �� 5?      " ��6$   .�5@   "   ��  � ��$ Center=�6H   5A      5C      4  .System.Windows.Controls.DataGridTemplateColumn  5D      $
 Voir��6-   $	 * 6;    5E      �� 5F      "  6$   .�5G   "   ��  � ��$ Center=�6O   5H         CellTemplate 5J      �� 5K      .R�5L   "   �� $��btnView��6<   -   5        $
��Voir��5L   K   
����������	#FFFFCF00$��4q�6�   
 � Tag 6)   �� *id+��6m   Z� 6�   5M      5O      5Q      5R      5S      5T      