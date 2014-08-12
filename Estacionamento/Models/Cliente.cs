﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estacionamento.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string  Telefone { get; set; }
       
        public virtual ICollection<Contrato> ContratosClientes { get; set; }
        public virtual ICollection<Veiculo> Veiculos { get; set; }

    }
}