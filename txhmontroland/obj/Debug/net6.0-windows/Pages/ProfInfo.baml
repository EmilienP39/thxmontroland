   M S B A M L   `   `   `  ����H  Dtxhmontroland, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null9!clr-namespace:txhmontroland.Pagestxhmontroland.Pages  5	      "    txhmontroland.Pages.ProfInfo   5      W SPresentationCore, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      3local!clr-namespace:txhmontroland.Pages     5	      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5
      R NSystem.Xaml, Version=6.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  ProfInfo��5      .��5      � .+�5   
   �� $��Prof principal :��5      $��25(�5      $
��Left=�5      $��740,119,0,0q�5      $	��Top=�5      $��50��61   $	��172��6=   �� 5   
   -   5         ��Name$ lbl_pp��5      $	��Non��5      $��25(�5      $��47��5      $
��Left=�5      $��909,121,0,0q�5      $	��Top=�5      $��69��5       J� RenderTransformOrigin$ 	0.464,0.5&�5   )   �� 5   
   $��Heures Total : ��5      $��25(�5      $��44��5       $
��Left=�5!      $��
750,51,0,0q�5"      $	��Top=�5#      $	��165��5$      �� 5%   
   $��Heures ORS : ��5&      $��25(�5'      $��42��5(      $
��Left=�5)      $��
756,86,0,0q�5*      $	��Top=�5+      $	��163��5,      �� 5-   
   $��HSA Totales :��5.      $��25(�5/      $��44��50      $
��Left=�51      $��
756,16,0,0q�52      $	��Top=�53      $	��155��54      �� 55   
   -   5        $ lbl_heure_ors��5=      $��Label��56      $��25(�57      $��47��58      $
��Left=�59      $��
909,87,0,0q�5:      $	��Top=�5;      $��73��5<      5=      �� 5>   
   -   5        $ lbl_heures_totales��5F      $��Label��5?      $��25(�5@      $��47��5A      $
��Left=�5B      $��
909,52,0,0q�5C      $	��Top=�5D      $��73��5E      5F      �� 5G   
   -   5        $ lbl_HSA_totales��5O      $��Label��5H      $��25(�5I      $��52��5J      $
��Left=�5K      $��
909,16,0,0q�5L      $	��Top=�5M      $��73��5N      5O      &   System.Windows.Controls.DataGrid  5P   
   -   5          Name$ 
dg_joueurs��5T         AutoGenerateColumns$ False��5Q      $��15(�5R         
IsReadOnly$
 True��5S      $��
0,206,-1,0q�5T   )      Columns 5U      0  *System.Windows.Controls.DataGridTextColumn  5V      ,  &System.Windows.Controls.DataGridColumn   Header$ Classe��5X         Width5  /System.Windows.Controls.DataGridLengthConverter$ * 5Y      1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 5W      �� *afficherNomClasse+
   HeaderStyle
 5Z      �� 5[       �� 
TargetType=  7System.Windows.Controls.Primitives.DataGridColumnHeader"  6    .�5\      ��  �� Property � �� �� Value$ Center=�6K   5]         ElementStyle 5`      �� 5a      " ��6    .�5b      ��  � ��$ Center=�6D   5c         	CellStyle 5e      �� 5f      *  $System.Windows.Controls.DataGridCell"  6    .�5g      ��  � ��
 �   �6<   5h      5j        5k      $ Matière��5m      $ * 5n      	 5l      �� *afficherNomMatiere+
 5o      �� 5p      "  6    .�5q      ��  � ��$ Center=�6K   5r       5t      �� 5u      " ��6    .�5v      ��  � ��$ Center=�6D   5w       5y      �� 5z      "  6    .�5{      ��  � ��
 �   �6<   5|      5~        5      $	 LEP��5�      $ * 5�      	 5�      �� *afficherHLEP+
 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�       5�      �� 5�      " ��6    .�5�      ��  � ��$ Center=�6D   5�       5�      �� 5�      "  6    .�5�      ��  � ��
 �   �6<   5�      5�        5�      $ LEGT 1°T°��5�      $ * 5�      	 5�      �� *afficherHLegt1T+
 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�       5�      �� 5�      " ��6    .�5�      ��  � ��$ Center=�6D   5�       5�      �� 5�      "  6    .�5�      ��  � ��
 �   �6<   5�      5�        5�      $ LEGT 2°��5�      $ * 5�      	 5�      �� *afficherHLegt2+
 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�       5�      �� 5�      " ��6    .�5�      ��  � ��$ Center=�6D   5�       5�      �� 5�      "  6    .�5�      ��  � ��
 �   �6<   5�      5�        5�      $	 STS��5�      $ * 5�      	 5�      �� *afficherHBts+
 5�      �� 5�      "  6    .�5�      ��  � ��$ Center=�6K   5�       5�      �� 5�      " ��6    .�5�      ��  � ��$ Center=�6D   5�       5�      �� 5�      "  6    .�5�      ��  � ��
 �   �6<   5�      5�      5�      �� 5�   
   -   5         ��Name$ btn_back��5�      ���	#FFFFCF00$��Retour��5�      
�������$��0��5�      $��28,22,1750,938q�5�   '   $��25(�6?   ��5�      Z� 5�   ?   �� 5�   
   -   5        $ btn_pdf��5�      ���	#FFD2007B$	��PDF��5�      
�������$
��Left=�5�      $��1594,34,0,930q�5�      $	��152��5�      $��25(�5�   &   ��5�      Z� 5�   &   �� 5�   
   -   5        $ lbl_initial��5�      $��Center=�6%   $��Label��6I   $
��Left=�6Y   $	��102��6t   $��
295,46,0,0q�6�   $	��Top=�6�   $	��290��6�   $��30(�6�   $ 0.503,0.549&�6�   $
��Bold"�6�   5�      5�      