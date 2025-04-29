using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorTFG.modelos
{
    public class PeliculaPost
    {
        [JsonProperty("titulo")]
        public string Titulo { get; set; }

        [JsonProperty("id_director")]
        public int IdDirector { get; set; }

        [JsonProperty("sinopsis")]
        public string Sinopsis { get; set; }

        [JsonProperty("duracion")]
        public int Duracion { get; set; }

        [JsonProperty("clasificacion")]
        public string Clasificacion { get; set; }

        [JsonProperty("productora")]
        public string Productora { get; set; }
    }

}
