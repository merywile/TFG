using Newtonsoft.Json;
using System;

namespace AdministradorTFG.modelos
{
    public class Pelicula
    {
        [JsonProperty("id_pelicula")]
        public int IdPelicula { get; set; }

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

        // Ahora se elimina la inicialización para que quede null si no se asigna
        [JsonProperty("director")]
        public Director Director { get; set; }

        // Propiedad calculada para Nombre Completo del Director
        public string Nombre_Director => Director != null ? $"{Director.Nombre} {Director.Apellidos}" : "Director Desconocido";

        public Pelicula(int idPelicula, string titulo, int idDirector, string sinopsis, int duracion, string clasificacion, string productora)
        {
            IdPelicula = idPelicula;
            Titulo = titulo;
            IdDirector = idDirector;
            Sinopsis = sinopsis;
            Duracion = duracion;
            Clasificacion = clasificacion;
            Productora = productora;
        }

        public Pelicula(string titulo, int idDirector, string sinopsis, int duracion, string clasificacion, string productora)
        {
            Titulo = titulo;
            IdDirector = idDirector;
            Sinopsis = sinopsis;
            Duracion = duracion;
            Clasificacion = clasificacion;
            Productora = productora;
        }

        public Pelicula(int idPelicula, string titulo, int idDirector, string sinopsis, int duracion, string clasificacion, string productora, Director director) : this(idPelicula, titulo, idDirector, sinopsis, duracion, clasificacion, productora)
        {
            Director = director;
        }

        public Pelicula()
        {
        }
    }
}
