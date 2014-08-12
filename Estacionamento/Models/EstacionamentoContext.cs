using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Estacionamento.Models
{
    public class EstacionamentoContext : DbContext
    {

        
        public EstacionamentoContext() : base("name=EstacionamentoContext")
        {
        }

        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Contrato> Contratos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Veiculo> Veiculos { get; set; }
        public DbSet<Registro> Registros { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            modelBuilder.Entity<Contrato>().HasRequired(t => t.Turno).WithMany(c => c.Contratos).HasForeignKey(t => t.TurnoId);
            modelBuilder.Entity<Contrato>().HasRequired(c => c.Cliente).WithMany(c => c.ContratosClientes).HasForeignKey(c => c.ClienteId);
            
            modelBuilder.Entity<Veiculo>().HasRequired(c => c.Cliente).WithMany(v => v.Veiculos).HasForeignKey(c => c.ClienteId);
            modelBuilder.Entity<Registro>().HasOptional(v => v.Veiculo).WithMany(r => r.Registros).HasForeignKey(v => v.VeiculoId);

            
            
            base.OnModelCreating(modelBuilder);
        }
        

            
    }
    
    

}
