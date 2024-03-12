using Microsoft.EntityFrameworkCore;


namespace ApiNotas.Models
{
    public partial class NotasContext: DbContext
    {
        public NotasContext() { }

        public NotasContext(DbContextOptions<NotasContext> options)
           : base(options)
        {
        }

        public virtual DbSet<Nota> Notas { get; set; } = null!;
        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.ToTable("Categorias");
                entity.Property(e => e.Nombre).HasMaxLength(50);
                entity.Property(e => e.FechaReg).HasColumnType("datetime");
                entity.HasMany(e => e.Notas);
            });

            //modelBuilder.Entity<Nota>(entity =>
            //{
            //    entity.HasKey(e => e.Id);
            //    entity.ToTable("Notas");
            //    entity.Property(e => e.Nombre).HasMaxLength(50);
            //    entity.Property(e => e.Descripcion).HasColumnType("varchar(max)");
            //    entity.Property(e => e.FechaReg).HasColumnType("datetime");

            //    // Configuración de la relación con Categoria
            //    entity.HasOne(n => n.Categoria)
            //        .WithMany(e => e.Notas)
            //        .HasForeignKey(n => n.CategoriaId)
            //        .OnDelete(DeleteBehavior.Restrict); // Opcional: Configura la acción en cascada al eliminar
            //});
            //OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
