using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class VetStanicaContext : DbContext
    {
        public DbSet<Zivotinja> Zivotinje { get; set; }
        public DbSet<Veterinar> Veterinari { get; set; }
        public DbSet<Pregled> Pregledi { get; set; }
        public DbSet<TipStruke> TipoviStruke { get; set; }
        public DbSet<VrstaZivotinje> VrsteZivotinja { get; set; }
        public DbSet<Veza> ZivotinjeVeterinari { get; set; }

        public VetStanicaContext(DbContextOptions options) : base(options)
        {

        }
    }
}