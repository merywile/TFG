using AdministradorTFG.conexionApi;
using AdministradorTFG.modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AdministradorTFG.vistas
{
    /// <summary>
    /// Lógica de interacción para ImagenesPeliVista.xaml
    /// </summary>
    public partial class ImagenesPeliVista : Window
    {
        public ImagenesPeliVista()
        {
            InitializeComponent();
            CargarPelis();
        }

        private async void btnAñadir_Click(object sender, RoutedEventArgs e)
        {
            // Validar que se haya seleccionado una película
            if (cbPeliculas.SelectedItem == null || !(cbPeliculas.SelectedItem is Pelicula peliculaSeleccionada))
            {
                MessageBox.Show("Por favor, selecciona una película.");
                return;
            }

            // Crear el objeto Multimedia con los campos requeridos
            var multimedia = new
            {
                id_pelicula = peliculaSeleccionada.IdPelicula, // Campo exacto esperado por el servidor
                portada = string.IsNullOrWhiteSpace(txtPortada.Text) ? null : txtPortada.Text.Trim(),
                banner = string.IsNullOrWhiteSpace(txtBanner.Text) ? null : txtBanner.Text.Trim(),
                trailer = string.IsNullOrWhiteSpace(txtTrailer.Text) ? null : txtTrailer.Text.Trim()
            };

            try
            {
                ConexionAPI conexionAPI = Application.Current.Properties["conexionAPI"] as ConexionAPI;

                using (HttpClient client = conexionAPI.GetHttpClient())
                {
                    // Serializar el objeto a JSON
                    string json = JsonConvert.SerializeObject(multimedia, new JsonSerializerSettings
                    {
                        NullValueHandling = NullValueHandling.Ignore // Excluye valores nulos
                    });

                    MessageBox.Show("JSON enviado: " + json); // Depuración para verificar el JSON
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Realizar la solicitud POST para crear un registro multimedia
                    HttpResponseMessage response = await client.PostAsync("/api/multimedia", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("¡Multimedia añadido con éxito!");
                        this.Close();
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Error al añadir multimedia: " + response.StatusCode + "\nDetalles: " + errorContent);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void CargarPelis()
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

                        // Depuración: mostrar los IDs de cada película

                        /*
                        foreach (var pelicula in peliculas)
                        {
                            MessageBox.Show("Película: " + pelicula.Titulo + ", ID: " + pelicula.IdPelicula);

                        }
                        */
                        cbPeliculas.ItemsSource = peliculas;
                        cbPeliculas.DisplayMemberPath = "Titulo"; // Mostrar solo el título
                        cbPeliculas.SelectedValuePath = "IdPelicula"; // Usar el ID como valor seleccionado
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

        private void CbPeliculas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Obtener la película seleccionada como objeto completo
            if (cbPeliculas.SelectedItem is Pelicula peliculaSeleccionada)
            {
                MessageBox.Show("Seleccionaste la película: " + peliculaSeleccionada.Titulo + " con ID " + peliculaSeleccionada.IdPelicula);

            }
        }
    }
}
