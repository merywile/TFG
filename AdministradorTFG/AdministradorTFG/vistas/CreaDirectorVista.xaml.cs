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
    /// Lógica de interacción para CreaDirectorVista.xaml
    /// </summary>
    public partial class CreaDirectorVista : Window
    {
        public CreaDirectorVista()
        {
            InitializeComponent();
        }

        private async void BtnGuardar_Click(object sender, RoutedEventArgs e)
        {
            ConexionAPI conexionAPI = Application.Current.Properties["conexionAPI"] as ConexionAPI;

            try
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtApellidos.Text))
                {
                    MessageBox.Show("Por favor, completa todos los campos.");
                    return;
                }

                // Verificar si el director ya está en la API
                bool existe = await VerificarDirectorExistente(txtNombre.Text.Trim(), txtApellidos.Text.Trim());
                if (existe)
                {
                    MessageBox.Show("Este director ya está registrado en la API.");
                    return;
                }

                using (HttpClient client = conexionAPI.GetHttpClient())
                {
                    MessageBox.Show("Nombre: " + txtNombre.Text + "\nApellidos: " + txtApellidos.Text);

                    // Crear el objeto Director
                    Director nuevoDirector = new Director
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Apellidos = txtApellidos.Text.Trim()
                    };

                    string json = JsonConvert.SerializeObject(nuevoDirector);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("/api/directores", content);

                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Director añadido con éxito.");
                    }
                    else
                    {
                        string errorContent = await response.Content.ReadAsStringAsync();
                        MessageBox.Show("Error al añadir director: " + response.StatusCode + "\nDetalles: " + errorContent);

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
            }
        }



        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private async Task<bool> VerificarDirectorExistente(string nombre, string apellidos)
        {
            ConexionAPI conexionAPI = Application.Current.Properties["conexionAPI"] as ConexionAPI; // Este tiene el token

            try
            {
                using (HttpClient cliente = conexionAPI.GetHttpClient())
                {
                    HttpResponseMessage respuestaDirectores = await cliente.GetAsync("/api/directores");


                    if (respuestaDirectores.IsSuccessStatusCode)
                    {
                        string json = await respuestaDirectores.Content.ReadAsStringAsync();
                        var directores = JsonConvert.DeserializeObject<List<Director>>(json);

                        // Verificar si hay un director con el mismo nombre y apellidos
                        return directores.Any(d => d.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase) &&
                                                   d.Apellidos.Equals(apellidos, StringComparison.OrdinalIgnoreCase));
                    }
                    else
                    {
                        MessageBox.Show("Error al consultar los directores: " + respuestaDirectores.StatusCode);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error: " + ex.Message);
                return false;
            }
        }


    }
}
