   M S B A M L   `   `   `  ����K  GAdministradorTFG, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:AdministradorTFG.vistasAdministradorTFG.vistas  5   	   /    )AdministradorTFG.vistas.ImagenesPeliVista   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   7local%clr-namespace:AdministradorTFG.vistas     5   	   $	��500��5   	   $
�� 300��6   ���#2C2F33
�������$��Segoe UI)�5
   	   $��14(�6     � Title$  Imagenes Película��5   	    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�5   	   .��5      � $��20,38,20,22q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $	��97* �6   �� 5       $	��59* �6   �� 5!      $
��313* �6   .+�5%   
   �� $��0��6   $��Center=�6    $
��Left=�6;   
�������.��6i   
Película:6t   �� 5&   
   -   5         ��Name$ cbPeliculas��5&      $��2��6(   $��Center=�68   ���#23272A
���   ����#99AAB55(      �� 5+   
   $��1��6   $��0��6   $	��Top=�6-   $
��Left=�6E   $��0,10,0,0q�6`   
�������.��6�   
Portada:6�   �� 5,   
   -   5         ��Name$ 
txtPortada��5,      $��1��6&   $��2��63   $	��Top=�6C   ���#23272A
����������#99AAB5$��0,14,0,0q�5-   P   �� 50   
   $��2��6   $��0��6   $	��Top=�6-   $
��Left=�6E   $��0,10,0,0q�6`   
�������$��2��6�   .��6�   	Banner:6�   �� 51   
   -   5        $ 	txtBanner��51      $��2��6%   $��2��62   $	��Top=�6B   ���#23272A
����������#99AAB5$��0,14,0,0q�52   P   �� 55   
   $��3��6   $��0��6   $	��Top=�6-   $
��Left=�6E   $��0,10,0,0q�6`   
�������.��6�   
Trailer:6�   �� 56   
   -   5        $ 
txtTrailer��56      $��3��6&   $��2��63   $	��Top=�6C   ���#23272A
����������#99AAB5$��0,14,0,0q�57   P   �� 5:   
   $��5��6   $��2��6"    �� Orientation$ 
Horizontal=�66   $
��Left=�6O   $��	18,20,0,0q�6j   $��1��6}   .+�5;      �� -   5         ��Name$ 
btnAñadir��5;      $��Añadir��6(   $	��100��69   $��	10,0,10,0q�6E   ���#99AAB5
���   ����#23272A5<   K   �� 5=      -   5        $ btnCancelar��5=      $��Cancelar��6*   $	��100��6=   $��	10,0,10,0q�6I   ���#99AAB5
���   ����#23272A5>   K   5?      5@      5A      