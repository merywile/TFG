using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorTFG.modelos
{
    public class Director
    {
        [JsonProperty("id_director")]
        public int IdDirector {  get; set; }

        [JsonProperty("nombre")]
        public string Nombre { get; set; }

        [JsonProperty("apellidos")]
        public string Apellidos { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellidos}";


        public Director(int idDirector, string nombre, string apellidos)
        {
            IdDirector = idDirector;
            Nombre = nombre;
            Apellidos = apellidos;
        }

        public Director(string nombre, string apellidos)
        {
            Nombre = nombre;
            Apellidos = apellidos;
        }

        public Director()
        {
        }


    }
}
