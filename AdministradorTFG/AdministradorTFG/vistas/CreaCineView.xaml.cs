using AdministradorTFG.conexionApi;
using AdministradorTFG.modelos;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AdministradorTFG.vistas
{
    public partial class CreaCineView : Window
    {
        public CreaCineView()
        {
            InitializeComponent();
        }

        private async void BtnGuardarCine_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text) || string.IsNullOrWhiteSpace(txtDireccion.Text))
            {
                MessageBox.Show("Por favor, completa todos los campos del cine.");
                return;
            }

            try
            {
                ConexionAPI conexionAPI = Application.Current.Properties["conexionAPI"] as ConexionAPI;

                var cineNuevo = new Cine
                {
                    Nombre = txtNombre.Text.Trim(),
                    Direccion = txtDireccion.Text.Trim(),
                    Telefono = string.IsNullOrWhiteSpace(txtTelefono.Text) ? null : txtTelefono.Text.Trim()
                };

                using (HttpClient cliente = conexionAPI.GetHttpClient())
                {
                    string jsonCine = JsonConvert.SerializeObject(cineNuevo);
                    HttpContent contenido = new StringContent(jsonCine, Encoding.UTF8, "application/json");

                    HttpResponseMessage respuesta = await cliente.PostAsync("/api/cines", contenido);

                    if (respuesta.IsSuccessStatusCode)
                    {
                        string resultado = await respuesta.Content.ReadAsStringAsync();
                        var cineRespuesta = JsonConvert.DeserializeObject<RespuestaCine>(resultado);
                        int idCine = cineRespuesta.Id;

                        MessageBox.Show("Cine creado correctamente con ID: " + idCine);

                        var idsSalas = await CrearSalas(idCine);

                        if (idsSalas != null && idsSalas.Count > 0)
                        {
                            await CrearButacas(idsSalas);
                        }
                    }
                    else
                    {
                        string mensajeError = await respuesta.Content.ReadAsStringAsync();
                        MessageBox.Show("Error al guardar el cine: " + mensajeError);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un problema al crear el cine: " + ex.Message);
            }
        }

        private async Task<List<int>> CrearSalas(int idCine)
        {
            List<int> idsSalas = new List<int>();

            try
            {
                ConexionAPI conexionAPI = Application.Current.Properties["conexionAPI"] as ConexionAPI;

                using (HttpClient cliente = conexionAPI.GetHttpClient())
                {
                    for (int sala = 1; sala <= 5; sala++)
                    {
                        var salaNueva = new Sala
                        {
                            Id_cine = idCine,
                            Nombre = "Sala " + sala,
                            Capacidad = 50
                        };

                        string jsonSala = JsonConvert.SerializeObject(salaNueva);
                        HttpContent contenidoSala = new StringContent(jsonSala, Encoding.UTF8, "application/json");

                        HttpResponseMessage respuestaSala = await cliente.PostAsync("/api/salas", contenidoSala);

                        if (respuestaSala.IsSuccessStatusCode)
                        {
                            string resultadoSala = await respuestaSala.Content.ReadAsStringAsync();
                            var salaRespuesta = JsonConvert.DeserializeObject<RespuestaSala>(resultadoSala);
                            int idSala = salaRespuesta.Id;
                            idsSalas.Add(idSala);
                        }
                        else
                        {
                            string mensajeError = await respuestaSala.Content.ReadAsStringAsync();
                            MessageBox.Show("Error al crear la Sala " + sala + ": " + mensajeError);
                            return null;
                        }
                    }

                    MessageBox.Show("¡Todas las salas creadas correctamente!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un problema al crear las salas: " + ex.Message);
            }

            return idsSalas;
        }

        private async Task CrearButacas(List<int> idsSalas)
        {
            try
            {
                ConexionAPI conexionAPI = Application.Current.Properties["conexionAPI"] as ConexionAPI;

                using (HttpClient cliente = conexionAPI.GetHttpClient())
                {
                    foreach (int idSala in idsSalas)
                    {
                        for (char fila = 'A'; fila <= 'E'; fila++)
                        {
                            for (int numero = 1; numero <= 10; numero++)
                            {
                                var butacaNueva = new Butaca
                                {
                                    Id_sala = idSala,
                                    Fila = fila.ToString(),
                                    Numero = numero,
                                    Estado = "Disponible"
                                };

                                string jsonButaca = JsonConvert.SerializeObject(butacaNueva);
                                HttpContent contenidoButaca = new StringContent(jsonButaca, Encoding.UTF8, "application/json");

                                HttpResponseMessage respuestaButaca = await cliente.PostAsync("/api/butacas", contenidoButaca);

                                if (!respuestaButaca.IsSuccessStatusCode)
                                {
                                    string mensajeError = await respuestaButaca.Content.ReadAsStringAsync();
                                    MessageBox.Show($"Error al crear la butaca {fila}-{numero} en sala {idSala}: {mensajeError}");
                                    return;
                                }
                            }
                        }
                    }

                    MessageBox.Show("¡Todas las butacas creadas correctamente!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un problema al crear las butacas: " + ex.Message);
            }
        }

        private void BtnCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public class RespuestaCine
        {
            [JsonProperty("id_cine")]
            public int Id { get; set; }
        }

        public class RespuestaSala
        {
            [JsonProperty("id_sala")]
            public int Id { get; set; }
        }
    }
}
