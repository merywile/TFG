using AdministradorTFG.conexionApi;
using AdministradorTFG.modelos;
using AdministradorTFG.vistas;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Collections.Specialized.BitVector32;

namespace AdministradorTFG
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        //-----------PELICULAS---------------

        private async void ListarPeliculas_Click(object sender, RoutedEventArgs e)
        {
            ConexionAPI conexionAPI = Application.Current.Properties["conexionAPI"] as ConexionAPI;

            try
            {
                using (HttpClient client = conexionAPI.GetHttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("/api/peliculas");

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                        var peliculas = JsonConvert.DeserializeObject<List<Pelicula>>(json);

                        foreach (var pelicula in peliculas)
                        {
                            if (pelicula.Director == null)
                            {
                                pelicula.Director = new Director
                                {
                                    Nombre = "Director",
                                    Apellidos = "Desconocido"
                                };
                            }
                        }
                        dgPeliculas.ItemsSource = peliculas;

                    }
                    else
                    {
                        MessageBox.Show("Error al cargar las películas: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar las películas: " + ex.Message);
            }
        }
        private  void dgPeliculas_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            // Ocultar la columna que muestra la propiedad "Director"
            if (e.PropertyName == "Director")
            {
                e.Cancel = true; // Cancela la generación de esta columna
            }

            if (e.PropertyName == "Sinopsis")
            {
                var column = (DataGridTextColumn)e.Column;
                column.Width = new DataGridLength(200); // Ajusta el ancho deseado (200 píxeles en este caso)
                column.ElementStyle = new Style(typeof(TextBlock))
                {
                    Setters =
                    {
                        new Setter(TextBlock.TextTrimmingProperty, TextTrimming.CharacterEllipsis), // Corta el texto con "..."
                        new Setter(TextBlock.TextWrappingProperty, TextWrapping.NoWrap) // No permite saltos de línea
                    }
                };
            }
        }


        private async void btnAnadirImagenesPeli_Click(object sender, RoutedEventArgs e)
        {
            ImagenesPeliVista imag = new ImagenesPeliVista();
            imag.ShowDialog();

        }




        private void btnAgregarPelicula_Click(object sender, RoutedEventArgs e)
        {
            CreaPeliVista creaPeliVista = new CreaPeliVista();
            
            creaPeliVista.ShowDialog();
            
        }



        //_------------------CINES--------------

        private async void ListarCines_Click(object sender, RoutedEventArgs e)
        {
            ConexionAPI conexionAPI = Application.Current.Properties["conexionAPI"] as ConexionAPI;

            // Validar que la conexión no sea nula
            if (conexionAPI == null)
            {
                MessageBox.Show("Error: No se pudo establecer la conexión con la API.");
                return;
            }

            try
            {
                HttpClient clienteHttp = conexionAPI.GetHttpClient();
                HttpResponseMessage respuestaCines = await clienteHttp.GetAsync("/api/cines");

                if (respuestaCines.IsSuccessStatusCode)
                {
                    string json = await respuestaCines.Content.ReadAsStringAsync();
                    var cines = JsonConvert.DeserializeObject<List<Cine>>(json);

                    // Asignar la lista de cines directamente a ItemsSource
                    dgPeliculas.ItemsSource = cines;
                }
                else
                {
                    MessageBox.Show("Error al cargar los cines: " + respuestaCines.StatusCode);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message + "\nDetalles: " + ex.StackTrace);
            }
        }


        private void btnAgregarCine_Click(object sender, RoutedEventArgs e)
        {
            CreaCineView creaCineView = new CreaCineView();
            creaCineView.ShowDialog();
        }




        //***********BOTONES DE ABAJO************************

        private void btnModificar_Click(object sender, RoutedEventArgs e)
        {
            /*
            var seleccionado = dgPeliculas.SelectedItem;

            if (seleccionado is Pelicula pelicula)
            {
                CreaPeliVista ventanaModificar = new CreaPeliVista(pelicula); // supondremos que acepta el objeto como parámetro
                ventanaModificar.ShowDialog();
            }
            else if (seleccionado is Cine cine)
            {
                CreaCineView ventanaModificar = new CreaCineView(cine); // idem
                ventanaModificar.ShowDialog();
            }
            else if (seleccionado is Sesion sesion)
            {
                // Aquí abrirías una ventana para editar la sesión
                EditarSesionVista editar = new EditarSesionVista(sesion);
                editar.ShowDialog();
            }
            else
            {
                MessageBox.Show("Selecciona una fila primero.");
            }
            /*/
        }


        private async void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            /*
            var seleccionado = dgPeliculas.SelectedItem;

            if (seleccionado == null)
            {
                MessageBox.Show("Selecciona una fila para eliminar.");
                return;
            }

            if (MessageBox.Show("¿Estás seguro de que quieres eliminar este elemento?", "Confirmar eliminación", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            ConexionAPI conexionAPI = Application.Current.Properties["conexionAPI"] as ConexionAPI;
            using (HttpClient client = conexionAPI.GetHttpClient())
            {
                HttpResponseMessage response = null;

                if (seleccionado is Pelicula pelicula)
                {
                    response = await client.DeleteAsync($"/api/peliculas/{pelicula.Id}");
                }
                else if (seleccionado is Cine cine)
                {
                    response = await client.DeleteAsync($"/api/cines/{cine.Id}");
                }
                else if (seleccionado is Sesion sesion)
                {
                    response = await client.DeleteAsync($"/api/sesiones/{sesion.Id}");
                }

                if (response != null && response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Elemento eliminado correctamente.");
                     dgPeliculas.ItemsSource = null; // podrías recargar la lista si quieres
                }
                else
                {
                    MessageBox.Show("Error al eliminar el elemento.");
                }
            }
            */
        }


        //-----------------ADMIN------------------
        private void CerrarSesion_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LoginVista loginVista = new LoginVista();
            loginVista.Show();
        }

        private void CambiaContraseña_Click(object sender, RoutedEventArgs e)
        {

        }


        //--------------------ESTADÍSTICAS-----------------------------

        private void VerEstadisticas_Click(object sender, RoutedEventArgs e)
        {
        }

       
    }
}
