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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AdministradorTFG.vistas
{
    /// <summary>
    /// Lógica de interacción para CreaPeliVista.xaml
    /// </summary>
    public partial class CreaPeliVista : Window
    {
        public CreaPeliVista()
        {
            InitializeComponent();
            CargarDirectores();

        }


        private async void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            ConexionAPI conexionAPI = Application.Current.Properties["conexionAPI"] as ConexionAPI;
            try
            {
                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(txtTitulo.Text) ||
                    cbDirectores.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(txtDuracion.Text) ||
                    cbClasificacion.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(txtProductora.Text))
                {
                    MessageBox.Show("Por favor, completa todos los campos obligatorios.");
                    return;
                }

                // Verificar que la duración sea un número válido 
                string duracionTexto = txtDuracion.Text.Trim();
                int duracion;
                if (duracionTexto.All(char.IsDigit))
                {
                    duracion = Convert.ToInt32(duracionTexto);
                }
                else
                {
                    MessageBox.Show("Ups! La duración debe ser un número válido. Inténtalo otra vez.");
                    return;
                }




                // Crear el objeto Pelicula
                PeliculaPost nuevaPelicula = new PeliculaPost
                {
                    Titulo = txtTitulo.Text.Trim(),
                    IdDirector = ((Director)cbDirectores.SelectedItem).IdDirector,
                    Sinopsis = txtSinopsis.Text.Trim(),
                    Duracion = duracion,
                    Clasificacion = ((ComboBoxItem)cbClasificacion.SelectedItem).Content.ToString(),
                    Productora = txtProductora.Text.Trim()
                };
               
                


                using (HttpClient client = conexionAPI.GetHttpClient())
                {
                    // Serializar los datos de la película a JSON
                    string json = JsonConvert.SerializeObject(nuevaPelicula);
                   // MessageBox.Show($"JSON a enviar: {json}");
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Realizar la solicitud POST a la API
                    HttpResponseMessage response = await client.PostAsync("/api/peliculas", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Película añadida con éxito.");
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show($"Error al añadir película: {response.StatusCode}\nDetalles: {errorContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }


        private void BtnAñadirDirector_Click(object sender, RoutedEventArgs e)
        {
            CreaDirectorVista creaDirectorVista = new CreaDirectorVista();
            creaDirectorVista.ShowDialog();
            CargarDirectores();
        }



        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private async void CargarDirectores()
        {
            ConexionAPI conexionAPI = Application.Current.Properties["conexionAPI"] as ConexionAPI;

            try
            {
                using (HttpClient client = conexionAPI.GetHttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync("/api/directores");

                    if (response.IsSuccessStatusCode)
                    {
                        string json = await response.Content.ReadAsStringAsync();
                     //   MessageBox.Show(json);  // Revisa el JSON
                        var directores = JsonConvert.DeserializeObject<List<Director>>(json);
                    

                        // Depuración: mostrar los IDs de cada director
                        foreach (var director in directores)
                        {
                            MessageBox.Show($"Director: {director.NombreCompleto}, ID: {director.IdDirector}");
                        }

                        cbDirectores.ItemsSource = directores;
                        cbDirectores.DisplayMemberPath = "NombreCompleto";
                    }
                    else
                    {
                        MessageBox.Show("Error al cargar los directores: " + response.StatusCode);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al cargar los directores: " + ex.Message);
            }
        }
    }
}
