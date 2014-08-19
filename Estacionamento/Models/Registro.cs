using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Estacionamento.Models
{
    public class Registro
    {
        public int Id { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime? Saida { get; set; }
        public float? ValorDevido { get; set; }
        public float? ValorPago { get; set; }
        public string Placa { get; set; }

        public bool Rotativo { get; set; }

        public int VeiculoId { get; set; }
        public virtual Veiculo Veiculo { get; set; }

    }
}