using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorTFG.modelos
{
    public class Cine
    {
        [JsonProperty("id_cine", NullValueHandling = NullValueHandling.Ignore)]
        public int Id_cine {  get; set; }

        [JsonProperty("nombre")]
        public String Nombre { get; set; }

        [JsonProperty("direccion")]
        public String Direccion {  get; set; }

        [JsonProperty("telefono")]
        public String Telefono { get; set; }

        public Cine(int idCine, string nombre, string direccion, string telefono)
        {
            this.Id_cine = idCine;
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Telefono = telefono;
        }

        public Cine(string nombre, string direccion, string telefono)
        {
            this.Nombre = nombre;
            this.Direccion = direccion;
            this.Telefono = telefono;
        }

        public Cine()
        {
        }
    }
}
