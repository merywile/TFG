using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdministradorTFG.modelos
{
    public class Butaca
    {
        [JsonProperty("id_butaca")]
        public int Id_butaca {  get; set; }

        [JsonProperty("id_sala")]
        public int Id_sala { get; set; }

        [JsonProperty("fila")]
        public string Fila {  get; set; }

        [JsonProperty("numero")]
        public int Numero { get; set; }

        [JsonProperty("estado")]
        public string Estado { get; set; }

        public Butaca() { }

        public Butaca(int idButaca, int idSala, string fila, int numero, string estado)
        {
            Id_butaca = idButaca;
            Id_sala = idSala;
            Fila = fila;
            Numero = numero;
            Estado = estado;
        }

        public Butaca(int idSala, string fila, int numero, string estado)
        {
            Id_sala = idSala;
            Fila = fila;
            Numero = numero;
            Estado = estado;
        }
    }
}
