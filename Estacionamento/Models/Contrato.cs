using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Estacionamento.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estacionamento.Models
{
    public class Contrato
    {
        public int Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime Datafim { get; set; }

        public float Valor { get; set; }

        public int TurnoId { set; get; }

        public int ClienteId { set; get; }

        public virtual Turno Turno { set; get; }
        public virtual Cliente Cliente { set; get; }
    }
}