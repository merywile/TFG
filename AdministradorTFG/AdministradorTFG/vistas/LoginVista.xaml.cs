using AdministradorTFG.conexionApi;
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
    /// Lógica de interacción para LoginVista.xaml
    /// </summary>
    public partial class LoginVista : Window
    {
        public LoginVista()
        {
            InitializeComponent();
        }
        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            ConexionAPI api = new ConexionAPI();
            try
            {
                using (HttpClient client = api.GetHttpClient())
                {
                    var loginData = new
                    {
                        email = txtEmail.Text.Trim(),
                        password = txtPassword.Password.Trim()
                    };

                    string json = JsonConvert.SerializeObject(loginData);
                    HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response = await client.PostAsync("/api/login", content);

                    if (response.IsSuccessStatusCode)
                    {
                        string responseJson = await response.Content.ReadAsStringAsync();
                        dynamic result = JsonConvert.DeserializeObject(responseJson);
                        string token = result.token;

                        // Guardar el token para futuras solicitudes
                        api.SetToken(token);

                        Application.Current.Properties["conexionAPI"] = api;

                        MessageBox.Show("Inicio de sesión exitoso.");
                        Console.Write("Token obtenido: " + token);

                        // Abrir la ventana
                        MainWindow ventanaPrincipal = new MainWindow();
                        ventanaPrincipal.Show();

                        // Cerrar la ventana de login
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Error en el inicio de sesión: " + response.StatusCode);
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
            this.Close(); // Cerrar la ventana
        }
    }
}
