   M S B A M L   `   `   `  ����P  LRestaurant-Management, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null=#clr-namespace:Restaurant_ManagementRestaurant_Management  5   	   *    $Restaurant_Management.ProductsWindow   5   	   W SPresentationCore, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=5.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=5.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   5local#clr-namespace:Restaurant_Management     5   	   -   5          � Title$  Restaurant-Management��5      $	��0pxq�6&   $	��720��6<   $
��1280��6b   $��Center=�6o   $��Center=�6�    � WindowStartupLocation$ CenterScreen=�6�   $��Normal&�6�    � WindowStyle$
 None=�6�   $��40(�6�    � 
ResizeMode$ NoResize=�6  .��5	      � .+�5
   
   A� 
���� ��$
��Left=�6    $
��1008��6;   $��
-372,0,0,0q�6I   ���	#FF610606$��Center=�6p   $	��656��6�   �� 5   
   -   5         ��Name$ categoriesButton��5      $��0q�6+   
�������$��Times New Roman)�6R   $
��Bold"�6o   $��
Categories��6�   $
��Left=�6�   $��
10,325,0,0q�6�   $	��Top=�6�   $	��220��6�   ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value
 �� ��63   �� 5       � �� 5      �� 5       �� 
TargetType" ��6.   .R�5   "   �� 	 �� CornerRadius$	 30��6)   #��z��6;   ._�5   &   �� $��Center=�67   $��Center=�6T   5   #   5      5      
 �� Triggers
 5      P� 5       P� Property �@J�IsMouseOver P� Value$
 True��69   . �5      ��  � �� �	#FFEB13F56;   5      5      5       �� 5!   
   -   5        $ productsButton��5!      $��0q�6)   
�������$��Times New Roman)�6P   $
��Bold"�6m   $��Products��6   $
��Left=�6�   $��
10,193,0,0q�6�   $	��Top=�6�   $	��220��6�   ��5"      �� 5#      " ��6   .�5$      ��  � ��
 �� ��63   �� 5%       � �� 5&      �� 5'      " ��6.   .R�5(   "   �� $	 30��6)   #��z��6;   ._�5)   &   �� $��Center=�67   $��Center=�6T   5*   #   5+      5-      
 5.      P� 5/       �@J�IsMouseOver$
 True��69   . �50      ��  � �� �	#FFEB13F56;   51      53      55      �� 56   
   -   5        $ logoutButton��56      $��0q�6'   
���� ��$��Times New Roman)�6O   $
��Bold"�6l   $��Logout��6~   $
��Left=�6�   $��
37,621,0,0q�6�   $	��Top=�6�   $	��152��6�   ��57      �� 58      " ��6   .�59      ��  � ��
 �����63   �� 5:       � �� 5;      �� 5<      " ��6.   .R�5=   "   �� $	 30��6)   #��z��6;   ._�5>   &   �� $��Center=�67   $��Center=�6T   5?   #   5@      5B      
 5C      P� 5D       �@J�IsMouseOver$
 True��69   . �5E      ��  � ��
 �����6;   5F      5H      5J      �� 5K   
   -   5        $ historyButton��5K      $��0q�6(   
�������$��Times New Roman)�6O   $
��Bold"�6l   $��Sells��6~   $
��Left=�6�   $��
10,469,0,0q�6�   $	��Top=�6�   $	��220��6�   ��5L      �� 5M      " ��6   .�5N      ��  � ��
 �� ��63   �� 5O       � �� 5P      �� 5Q      " ��6.   .R�5R   "   �� $	 30��6)   #��z��6;   ._�5S   &   �� $��Center=�67   $��Center=�6T   5T   #   5U      5W      
 5X      P� 5Y       �@J�IsMouseOver$
 True��69   . �5Z      ��  � �� �	#FFEB13F56;   5[      5]      5_      �� 5`   
   -   5        $��X��5`      $��0q�6   
�������$
��Left=�6D   $��
1181,0,0,0q�6_   $	��Top=�6s   $��58��6�   $��76��6�   $��Times New Roman)�6�   $
��Bold"�6�   ��5a      �� 5b      " ��6   .�5c      ��  � ��
 �� ��63   �� 5d       � �� 5e      �� 5f      " ��6.   .R�5g   "   �� #��z��6)   ._�5h   &   �� $��Center=�67   $��Center=�6T   5i   #   5j      5l      
 5m      P� 5n       �@J�IsMouseOver$
 True��69   . �5o      ��  � �� �	#FFEB13F56;   5p      5r      5t      �� 5u   
   -   5         ��Name$ 	idTextBox��5u      
������� �� 	MaxLength$ 17��6k   $��3q�6z   $��30��6�   $	��204��6�   $	��Top=�6�   $
��Left=�6�   $��330,199,0,0q�6�   $��Times New Roman)�6�   $��20(�6  $��Center=�6  $��Center=�6>   �� 	UndoLimit$	 200��6`   J� RenderTransformOrigin$ -0.62,-2.98&�6p  .��6�   � 	Resources 5v      ��%^   '��       �� 5w      " ��6   .�5x      ��  �@��CornerRadius$ 3��65   5y      5z      .��6!   ��5{      �� 5|       �� EndPoint$	 1,1&�6&    �� 
StartPoint$	 1,0&�65   .��5}      �  � Color$ White��6#    � Offset$ 0Q�61   � 5~      $ White��6#   $	 0.9Q�61   � 5      $ Purple��6#   $ 1Q�62   � 5�      $ Purple��6#   $ 1Q�62   5�      .��5�   #   5�      &   System.Windows.Controls.DataGrid  5�   
   -   5          Name$ dataTableChosed��5�         
IsReadOnly$
 True��6,      RowBackground
 �����
���� ��$��3q�6n   
�������
���� ��$��598,126,23,10q�6�   $��Times New Roman)�6�   $��25(�6�   $��Center=�6�   $��Center=�6  $��Stretch=�66  5�      �� 5�   
   $��ID��6   
���� ��$��Times New Roman)�62   $
��Bold"�6O   $
��Left=�6a   $��337,170,0,0q�6|   $	��Top=�6�   $	��177��6�   $��41��6�   $��25(�6�   $��Center=�6�   $��Center=�6�   5�      �� 5�   
   -	   5        $ productNameTextBox��5�      
�������$ 50��6G   $��3q�6V   $��30��6j   $	��204��6w   $	��Top=�6�   $
��Left=�6�   $��330,281,0,0q�6�   $��Times New Roman)�6�   $��20(�6�   $��Center=�6�   $��Center=�6  $	 200��6<  $ -0.62,-2.98&�6L  .��6q   5�      ��%^   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 3��65   5�      5�      .��6!   ��5�      �� 5�      $	 1,1&�6&   $	 1,0&�65   .��5�      � $ White��6#   $ 0Q�61   � 5�      $ White��6#   $	 0.9Q�61   � 5�      $ Purple��6#   $ 1Q�62   � 5�      $ Purple��6#   $ 1Q�62   5�      .��5�   #   5�      �� 5�   
   $��Product Name��6   
���� ��$��Times New Roman)�6<   $
��Bold"�6Y   $
��Left=�6k   $��337,247,0,0q�6�   $	��Top=�6�   $	��177��6�   $��41��6�   $��25(�6�   $��Center=�6�   $��Center=�6�   �� 5�   
   -
   5        $ categoryIDTextBox��5�      
�������$ 15��6z   $��3q�6�   $��30��6�   $	��204��6�   $	��Top=�6�   $
��Left=�6�   $��330,361,0,0q�6�   $��Times New Roman)�6�   $��20(�6  $��Center=�6)  $��Center=�6M  $	 200��6o  $ -0.62,-2.98&�6  .��6�   5�      ��%^   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 3��65   5�      5�      .��6!   ��5�      �� 5�      $	 1,1&�6&   $	 1,0&�65   .��5�      � $ White��6#   $ 0Q�61   � 5�      $ White��6#   $	 0.9Q�61   � 5�      $ Purple��6#   $ 1Q�62   � 5�      $ Purple��6#   $ 1Q�62   5�      .��5�   #   5�      �� 5�   
   $��Category ID��6   
���� ��$��Times New Roman)�6;   $
��Bold"�6X   $
��Left=�6j   $��337,328,0,0q�6�   $	��Top=�6�   $	��177��6�   $��41��6�   $��25(�6�   $��Center=�6�   $��Center=�6�   �� 5�   
   -   5        $ priceTextBox��5�      
�������$ 20��6s   $��3q�6�   $��30��6�   $	��204��6�   $	��Top=�6�   $
��Left=�6�   $��330,436,0,0q�6�   $��Times New Roman)�6�   $��20(�6  $��Center=�6"  $��Center=�6F  $	 200��6h  $ -0.62,-2.98&�6x  .��6�   5�      ��%^   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 3��65   5�      5�      .��6!   ��5�      �� 5�      $	 1,1&�6&   $	 1,0&�65   .��5�      � $ White��6#   $ 0Q�61   � 5�      $ White��6#   $	 0.9Q�61   � 5�      $ Purple��6#   $ 1Q�62   � 5�      $ Purple��6#   $ 1Q�62   5�      .��5�   #   5�      �� 5�   
   -   5        $ ingredientsTextBox��5�      
�������$ 50��6G   $��3q�6V   $��30��6j   $	��204��6w   $	��Top=�6�   $
��Left=�6�   $��330,520,0,0q�6�   $��Times New Roman)�6�   $��20(�6�   $��Center=�6�   $��Center=�6  $	 200��6<  $ -0.62,-2.98&�6L  .��6q   5�      ��%^   '��       �� 5�      " ��6   .�5�      ��  �@��CornerRadius$ 3��65   5�      5�      .��6!   ��5�      �� 5�      $	 1,1&�6&   $	 1,0&�65   .��5�      � $ White��6#   $ 0Q�61   � 5�      $ White��6#   $	 0.9Q�61   � 5�      $ Purple��6#   $ 1Q�62   � 5�      $ Purple��6#   $ 1Q�62   5�      .��5�   #   5�      �� 5�   
   $��Price��6   
���� ��$��Times New Roman)�65   $
��Bold"�6R   $
��Left=�6d   $��337,400,0,0q�6   $	��Top=�6�   $	��177��6�   $��41��6�   $��25(�6�   $��Center=�6�   $��Center=�6�   �� 5�   
   $��Ingredients��6   
���� ��$��Times New Roman)�6;   $
��Bold"�6X   $
��Left=�6j   $��337,484,0,0q�6�   $	��Top=�6�   $	��177��6�   $��41��6�   $��25(�6�   $��Center=�6�   $��Center=�6�   �� 5�   
   -   5        $ 	addButton��5�      $	��ADD��6$   $
��Left=�62   
���� ��$��2q�6b   
���� ��$��289,575,0,0q�6�   $	��Top=�6�   $��45��6�   $��79��6�   $��25(�6�   $��Times New Roman)�6�   $
��Bold"�6�   ��5�      �� 5�      " ��6   .�5�      ��  � ��
 �����63   
 5�      P� 5�       �@J�IsMouseOver$
 True��69   . �5�      ��  � �� �	#FFEB13F56;   5�      5�      5�      �� 5�   
   -   5        $ 
editButton��5�      $
��EDIT��6%   
���� ��$��2q�6I   
���� ��$
��Left=�6q   $��383,575,0,0q�6�   $	��Top=�6�   $��45��6�   $��79��6�   $��25(�6�   $��Times New Roman)�6�   $
��Bold"�6�   ��5�      �� 5�      " ��6   .�5�      ��  � ��
 �����63   
 5�      P� 5�       �@J�IsMouseOver$
 True��69   . �5�      ��  � �� �	#FFEB13F56;   5�      5�      5�      �� 5�   
   -   5        $ deleteButton��5�      $��DELETE��6'   
���� ��$��2q�6M   
���� ��$
��Left=�6u   $��477,575,0,0q�6�   $	��Top=�6�   $��45��6�   $	��112��6�   $��25(�6�   $��Times New Roman)�6�   $
��Bold"�6   ��5�      �� 5�      " ��6   .�5�      ��  � ��
 �����63   
 5�      P� 5�       �@J�IsMouseOver$
 True��69   . �5�      ��  � �� �	#FFEB13F56;   5�      5�      5�      �� 5�   
   $��MANAGE PRODUCTS��6   
���� ��$
��Bold"�6>   $��Times New Roman)�6P   $��45(�6m   $
��Left=�6{   $��
465,29,0,0q�6�   $	��Top=�6�   5�      5�      5�      