using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Senai.OpFlix.WebApi.Domains {
    public partial class OpFlixContext : DbContext {
        public OpFlixContext () {
        }

        public OpFlixContext (DbContextOptions<OpFlixContext> options)
            : base(options) {
        }

        public virtual DbSet<Categoria> Categoria { get; set; }
        public virtual DbSet<Lancamentos> Lancamentos { get; set; }
        public virtual DbSet<Permissoes> Permissoes { get; set; }
        public virtual DbSet<Plataformas> Plataformas { get; set; }
        public virtual DbSet<Tipo> Tipo { get; set; }
        public virtual DbSet<Usuarios> Usuarios { get; set; }
        public virtual DbSet<LancamentosFavoritos> LancamentosFavoritos { get; set; }

        // Unable to generate entity type for table 'dbo.LancamentosFavoritos'. Please see the warning messages.

        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=.\\SqlExpress;Initial Catalog=T_OpFlix;User Id=sa;Pwd=132");
            }
        }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {

            modelBuilder.Entity<LancamentosFavoritos>().
                HasKey(e => new { e.IdUsuario , e.IdLancamento });

            modelBuilder.Entity<LancamentosFavoritos>().
                HasOne(e => e.Lancamento)
                .WithMany(e => e.LancamentosFavoritos)
                .HasForeignKey(e => e.IdLancamento);

            modelBuilder.Entity<LancamentosFavoritos>().
              HasOne(e => e.Usuario)
              .WithMany(e => e.LancamentosFavoritos)
              .HasForeignKey(e => e.IdUsuario);


            modelBuilder.Entity<Categoria>(entity => {
                entity.HasKey(e => e.IdCategoria);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Lancamentos>(entity => {
                entity.HasKey(e => e.IdLancamento);

                entity.Property(e => e.DataLancamento).HasColumnType("date");

                entity.Property(e => e.Sinopse)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Titulo)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Lancamentos)
                    .HasForeignKey(d => d.IdCategoria)
                    .HasConstraintName("FK__Lancament__IdCat__5629CD9C");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.Lancamentos)
                    .HasForeignKey(d => d.IdTipo)
                    .HasConstraintName("FK__Lancament__IdTip__5535A963");

                entity.HasOne(d => d.PlataformaNavigation)
                    .WithMany(p => p.Lancamentos)
                    .HasForeignKey(d => d.Plataforma)
                    .HasConstraintName("FK__Lancament__Plata__75A278F5");
            });

            modelBuilder.Entity<Permissoes>(entity => {
                entity.HasKey(e => e.IdPermissao);

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Plataformas>(entity => {
                entity.HasKey(e => e.IdPlataforma);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Tipo>(entity => {
                entity.HasKey(e => e.IdTipo);

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Usuarios>(entity => {
                entity.HasKey(e => e.IdUsuario);

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Usuarios__A9D105346E131BFF")
                    .IsUnique();

                entity.Property(e => e.DataNascimento).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Imagem)
                    .HasMaxLength(2083)
                    .HasDefaultValueSql("('http://cdn.onlinewebfonts.com/svg/img_333639.png')");

                entity.Property(e => e.Nome)
                    .IsRequired()
                    .HasMaxLength(45)
                    .IsUnicode(false);

                entity.Property(e => e.Senha)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdPermissaoNavigation)
                    .WithMany(p => p.Usuarios)
                    .HasForeignKey(d => d.IdPermissao)
                    .HasConstraintName("FK__Usuarios__IdPerm__4CA06362");
            });
        }
    }
}
