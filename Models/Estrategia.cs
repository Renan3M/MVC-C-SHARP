using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MvcMovie.Models
{
    public class Estrategia
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string side { get; set; } // Só pode ser "buy" or "sell" ==> Lembrar de validar no formulario.
        [Required]
        public decimal price { get; set; } // Tem que ser positivo
        [Required]
        public int quantity { get; set; } // Tem que ser positivo
        [Required]
        public string symbol { get; set; }

    }

    public class EstrategiaDBContext : DbContext
    {
        public DbSet<Estrategia> Estrategias { get; set; } // Coleção de Estrategias.

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<EstrategiaDBContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}