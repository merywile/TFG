using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorTFG.conexionApi
{
    public class ConexionAPI
    {
        // URL base de la API
        public readonly string BaseUrl = "https://laravelcine-cine-zeocca.laravel.cloud";

        // Token de autenticación 
        public string Token;

        // Método para configurar el token
        public void SetToken(string token)
        {
            Token = token;
        }


        // Método para obtener un cliente HTTP configurado
        public HttpClient GetHttpClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(BaseUrl) // Configura la URL base
            };

            // Configurar los headers básicos
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Si hay un token, incluirlo en la cabecera de autorización
            if (!string.IsNullOrEmpty(Token))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Token);
            }

            return client;
        }
    }
        
}
