using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Estacionamento.Models
{
    public class Veiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string  cor { get; set; }

       public int ClienteId { get; set; }

       public virtual Cliente Cliente { get; set; }

        public virtual ICollection<Registro> Registros { get; set; }
    }
}