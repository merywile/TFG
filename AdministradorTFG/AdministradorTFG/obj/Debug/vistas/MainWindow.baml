   M S B A M L   `   `   `  ����  AdministradorTFG!    AdministradorTFG.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$%  Panel de Administración - Cine��5   	    � WindowState$ 	Maximized=�5   	   ���#2C2F33
�������$��Segoe UI)�5   	   $��14(�6   .��5	      ]� ��. .+�5   
   o� $	��Top=�6   ���#23272A
�������$��30��6L   .@�5      m� $��
Películas��6   $��18(�6*   .@�5      m� -   5        $��Listar Películas��5      
���   �6H   m� 5      -   5        $��Añadir Película��5      
���   �6G   m� 5      -   5        $��Añadir Imágenes��5      
���   �6G   5      m� 5      $��Cines��6   $��18(�6&   .@�5      m� -   5        $��Listar Cines��5      
���   �6D   m� 5      -   5        $��Añadir Cine��5      
���   �6C   5      m� 5      $��Sesiones��6   $��18(�6)   .@�5      m� $��Listar Sesiones��6   
���   �64   m� 5      $��Añadir Sesión��6   
���   �62   5      m� 5      $��Usuarios��6   $��18(�6)   .@�5      m� $��Listar Usuarios��6   
���   �64   m� 5      $��Añadir Usuario��6   
���   �63   5      m� 5      $��Ventas��6   $��18(�6'   .@�5      m� $��Listado de Ventas��6   
���   �66   m� 5      $��Facturas��6   
���   �6-   5       m� 5!      -   5        $��Estadísticas��5!      $��18(�6L   m� 5"      $��Admin��6   $��18(�6&   .@�5#      m� -   5        $��Cambiar contraseña��5#      
���   �6K   m� 5$      -   5        $��Salir��5$      
���   �6?   5%      5&      � 5)   
    � RowDefinitions 5*      �� 5+      $��* �6    �� 5,      $
��Auto �6    .+�50      &   System.Windows.Controls.DataGrid  -	   5          Name$ dgPeliculas��50         AutoGenerateColumns . ���#23272A
���   ����#99AAB5$��Stretch=�56      $��Stretch=�57      $��0q�58         
IsReadOnly .    ColumnWidth5  /System.Windows.Controls.DataGridLengthConverter$ * 5:         RowHeaderWidth$ 0Q�5;      $
��Auto=�5<      $
��Auto=�5=      �� 5@      $��1��6    �� Orientation$ 
Horizontal=�6&   $��Center=�5A      $��10q�66   .+�5C      �� -
   5        	 ��Name$	 btnModificar��5C      $��	Modificar��6/   $	��140��6C   $��50��6O   $��10q�5D      ���#43B581
�������5E      �� 5F      -   5        $	 btnEliminar��5F      $��Eliminar��6.   $	��140��6A   $��50��6M   $��10q�5G      ���#F04747
�������5H      5I      5J      5K      5L      