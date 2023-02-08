using Entidades;
using Microsoft.EntityFrameworkCore;
using System;

namespace Gastos
{
    public class GastosContext : DbContext
    {
        public DbSet<Gasto> Gastos { get; set; }

        public GastosContext(DbContextOptions<GastosContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            List<Gasto> gastos = new List<Gasto>();
            gastos.Add(new Gasto(){
                    idGasto = 1,
                    dblMontoGasto = 1000.00,
                    dtFechaGasto = new DateTime(),
                    strDescripcionGasto = "desc test",
                    strNombreGasto = "titulo test"
            });

            modelBuilder.Entity<Gasto>(
                entity => {
                    entity.ToTable("Gastos");
                    entity.HasKey(e => e.idGasto);
                    entity.Property(e => e.dtFechaGasto).IsRequired();
                    entity.Property(e => e.dblMontoGasto).IsRequired();
                    entity.Property(e => e.strNombreGasto).IsRequired();
                    entity.HasData(gastos);
                }

            );
        }
    }
}
