using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorTFG.modelos
{
    public class Sala
    {


        [JsonProperty("id_sala")]
        public int Id_sala { get; set; }

        [JsonProperty("id_cine")]
        public int Id_cine { get; set; }

        [JsonProperty("nombre")]
        public string Nombre {  get; set; }

        [JsonProperty("capacidad")]
        public int Capacidad { get; set; }

        public Sala() { }

        public Sala(int idSala, int idCine, string nombre, int capacidad)
        {
            Id_sala = idSala;
            Id_cine = idCine;
            Nombre = nombre;
            Capacidad = capacidad;
        }

        public Sala(int idCine, string nombre, int capacidad)
        {
            Id_cine = idCine;
            Nombre = nombre;
            Capacidad = capacidad;
        }
    }
}
