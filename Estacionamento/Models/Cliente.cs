using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Estacionamento.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public string Telefone { get; set; }

        public virtual ICollection<Contrato> ContratosClientes { get; set; }
        public virtual ICollection<Veiculo> Veiculos { get; set; }

    }
}