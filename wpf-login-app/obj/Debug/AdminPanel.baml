   M S B A M L   `   `   `  ����D  @wpf_login, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null%clr-namespace:wpf_login	wpf_login  5   	       wpf_login.AdminPanel   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   )localclr-namespace:wpf_login     5   	   -   5          � Title$  
AdminPanel��5   	   $	��450��6   $	��800��6)   $	��450��65   $	��800��6E    � WindowStartupLocation$ CenterScreen=�5	   	   ���#E0F2F1.��5      � .+�5   
   �� -   5         ��Name$ Login��5      $y�	Collapsed=�6!   6:   �� 5   
   ���#26A69A$	��640��6#   $	��220��6/   $��Center=�6<   $��Center=�6Y    �� RadiusX$ 10��6t    �� RadiusY$ 10��6�   � 5   
   -   5         �Name$ viewForm��5      $y�Visible=�6&   $��Center=�6;   $��Center=�6X   .+�5      �� -   5        
�������
�������
������ $ ��Редактировать��5   _   $��14(�6w   $��25��6�   $	��140��6�   $��10q�6�    � 	Resources 5       �� 
TargetType �� Property	 �� Value��%^   '��       �� 5      " ��6   .�5      ��  �@��CornerRadius$	 5��69   5      5      5      � 5      
 � Orientation$
 Vertical=�6   .+�5      � .+�5      �� ���#263238$
��Bold"�61   $��Center=�6C   $��70��6g   $��14(�6r   $��10q�6�   .��6�   
Логин6�   �� 5      -   5        $ 
sLoginView��5      $��Center=�62   $��14(�6M   
�������$	��100��6n   $��10q�6z   .��6�    5      ��%^   '��       �� 5      " ��6$   .�5   "   ��  �@��CornerRadius$	 5��6A   5      5      .��6-   5       5!      �� 5"      -   5        
�������
����������#90A4AE$#��Показать данные��5"   _   $��14(�6y   $	��140��6�   $��10q�6�    5#      ��%^   '��       �� 5$      " ��6    .�5%      ��  �@��CornerRadius$	 5��6=   5&      5'      5(      5)      � 5*      $
 Vertical=�6   .+�5+      �� -   5         ��Name$ 	sNameView��5+      ���#263238$
��Bold"�6@   $��Center=�6R   $��14(�6v   $��10q�6�   6�   �� 5,      -   5        $ 
sPhoneView��5,      ���#263238$
��Bold"�6A   $��Center=�6S   $��14(�6w   $��10q�6�   6�   �� 5-      -	   5        $ 	sRoleView��5-      ���#263238$
��Bold"�6@   $��Center=�6R   $��14(�6v   $��10q�6�   6�   5.      5/      �� 50   
   -
   5        $ sLastSession��50   %   ���#263238$��Center=�6;   $��30��6_   $��14(�6k   $	��100��6y   $��	0,180,0,0q�6�   6�   � 52   
   -   5        $ editForm��52      $y�Hidden=�6&   $
 Vertical=�6:   $��Center=�6Q   $��Center=�6n   .+�53      � .+�54      � $
 Vertical=�6   .+�55      � .+�56      �� ���#263238$
��Bold"�65   $��Center=�6G   $��65��6k   $��14(�6v   $��10q�6�   .��6�   
Логин6�   �� 57      -   5        $ sLogin��57   "   $��Center=�62   $��14(�6M   
�������$	��100��6n   $��10q�6z   .��6�    58      ��%^   '��       �� 59   "   " ��6(   .�5:   &   ��  �@��CornerRadius$	 5��6E   5;   #   5<      .��61   5=      5>      �� 5?      -   5        
�������
����������#90A4AE$#��Изменить данные��5?   c   $��14(�6}   $	��140��6�   $��0,0,0,10q�6�    5@      ��%^   '��       �� 5A      " ��6$   .�5B   "   ��  �@��CornerRadius$	 5��6A   5C      5D      5E      �� 5F      -   5         ��Name$ btnBack��5F      
�������
�������
������ $��
Назад��6�   $��14(�6�   $	��140��6�    5G      ��%^   '��       �� 5H      " ��6$   .�5I   "   ��  �@��CornerRadius$	 5��6A   5J      5K      5L      5M      � 5N      $
 Vertical=�6   .+�5O      � .+�5P      �� ���#263238$
��Bold"�65   $
��Left=�6G   $��65��6i   $��14(�6t   $��10q�6�   .��6�   Пароль6�   �� 5Q      -   5        $ sPass��5Q   "   $��Center=�61   $��14(�6L   
�������$	��100��6m   $��10q�6y   .��6�    5R      ��%^   '��       �� 5S   "   " ��6(   .�5T   &   ��  �@��CornerRadius$	 5��6E   5U   #   5V      .��61   5W      5X      � 5Y      .+�5Z      �� ���#263238$
��Bold"�65   $
��Left=�6G   $��65��6i   $��14(�6t   $��10q�6�   .��6�   
Роль6�   �� 5[      -   5        $ sRole��5[   "   $��Center=�61   $��14(�6L   
�������$	��100��6m   $��10q�6y   .��6�    5\      ��%^   '��       �� 5]   "   " ��6(   .�5^   &   ��  �@��CornerRadius$	 5��6E   5_   #   5`      .��61   5a      5b      5c      � 5d      $
 Vertical=�6   .+�5e      � .+�5f      �� ���#263238$
��Bold"�65   $
��Left=�6G   $��70��6i   $��14(�6t   $��10q�6�   .��6�   Имя6�   �� 5g      -   5        $ sName��5g   "   $��Center=�61   $��14(�6L   
�������$	��100��6m   $��10q�6y   .��6�    5h      ��%^   '��       �� 5i   "   " ��6(   .�5j   &   ��  �@��CornerRadius$	 5��6E   5k   #   5l      .��61   5m      5n      � 5o      .+�5p      �� ���#263238$
��Bold"�65   $
��Left=�6G   $��70��6i   $��14(�6t   $��10q�6�   .��6�   Телефон6�   �� 5q      -   5        $ sPhone��5q   "   $��Center=�62   $��14(�6M   
�������$	��100��6n   $��10q�6z   .��6�    5r      ��%^   '��       �� 5s   "   " ��6(   .�5t   &   ��  �@��CornerRadius$	 5��6E   5u   #   5v      .��61   5w      5x      5y      5z      5{      �� 5}   
   -   5        $ sStatus��5}      ���#263238$��18(�66   $��Center=�6D   $��Bottom=�6a   6~   5~      5      