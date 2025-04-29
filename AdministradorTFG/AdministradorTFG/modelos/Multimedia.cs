using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorTFG.modelos
{
    public class Multimedia
    {

        [JsonProperty("id_multimedia")]
        public int IdMultimedia { get; set; }

        [JsonProperty("id_pelicula")]
        public int IdPelicula { get; set; }

        [JsonProperty("portada")]
        public string Portada { get; set; }

        [JsonProperty("banner")]
        public string Banner { get; set; }

        [JsonProperty("trailer")]
        public string Trailer { get; set; }

        // Constructor vacío
        public Multimedia() { }

        // Constructor para inicializar con valores
        public Multimedia(int idMultimedia, int idPelicula, string portada, string banner, string trailer)
        {
            IdMultimedia = idMultimedia;
            IdPelicula = idPelicula;
            Portada = portada;
            Banner = banner;
            Trailer = trailer;
        }

        public Multimedia(int idPelicula, string portada, string banner, string trailer)
        {
            IdPelicula = idPelicula;
            Portada = portada;
            Banner = banner;
            Trailer = trailer;
        }
    }
}
