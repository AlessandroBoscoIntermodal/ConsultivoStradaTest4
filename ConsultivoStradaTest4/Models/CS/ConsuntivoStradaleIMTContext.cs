using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ConsultivoStradaTest4.Models.CS
{
    public partial class ConsuntivoStradaleIMTContext : DbContext
    {
        public ConsuntivoStradaleIMTContext()
        {
        }

        public ConsuntivoStradaleIMTContext(DbContextOptions<ConsuntivoStradaleIMTContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AbbinamentoTrattore> AbbinamentoTrattores { get; set; }
        public virtual DbSet<AutistiView> AutistiViews { get; set; }
        public virtual DbSet<Causale> Causales { get; set; }
        public virtual DbSet<CostoFisso> CostoFissos { get; set; }
        public virtual DbSet<OperatoriLogisticiView> OperatoriLogisticiViews { get; set; }
        public virtual DbSet<StatoTrattore> StatoTrattores { get; set; }
        public virtual DbSet<VeicoliAziendaliView> VeicoliAziendaliViews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //            if (!optionsBuilder.IsConfigured)
            //            {
            //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
            //                optionsBuilder.UseSqlServer("Server=IT01\\LOGISTICA;Database=ConsuntivoStradaleIMT;Trusted_Connection=True;");
            //            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AbbinamentoTrattore>(entity =>
            {
                entity.HasKey(e => new { e.FkeyTrattore, e.KeyDataInizio });

                entity.ToTable("AbbinamentoTrattore");

                entity.Property(e => e.FkeyTrattore)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FKeyTrattore");

                entity.Property(e => e.DataFine).HasColumnType("datetime");

                entity.Property(e => e.DataInizio).HasColumnType("datetime");

                entity.Property(e => e.FkeyAutista).HasColumnName("FKeyAutista");

                entity.Property(e => e.FkeyGestore)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FKeyGestore");
            });

            modelBuilder.Entity<AutistiView>(entity =>
            {
                entity.HasKey(e => e.IdAutista);

                entity.ToView("AutistiView");

                entity.Property(e => e.IdAutista).ValueGeneratedOnAdd();

                entity.Property(e => e.Nominativo)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Causale>(entity =>
            {
                entity.ToTable("Causale");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Descrizione)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CostoFisso>(entity =>
            {
                entity.HasKey(e => new { e.FkeyVeicolo, e.FkeyCausale, e.KeyDataInizio });

                entity.ToTable("CostoFisso");

                entity.Property(e => e.FkeyVeicolo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FKeyVeicolo");

                entity.Property(e => e.FkeyCausale).HasColumnName("FKeyCausale");

                entity.Property(e => e.DataFine).HasColumnType("date");

                entity.Property(e => e.DataInizio).HasColumnType("date");
            });

            modelBuilder.Entity<OperatoriLogisticiView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("OperatoriLogisticiView");

                entity.Property(e => e.EmailAziendale)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.FkeyAnagrafica).HasColumnName("FKeyAnagrafica");

                entity.Property(e => e.Idutente)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("IDUtente");

                entity.Property(e => e.Nominativo)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<StatoTrattore>(entity =>
            {
                entity.HasKey(e => new { e.FkeyTrattore, e.KeyValidoDal });

                entity.ToTable("StatoTrattore");

                entity.Property(e => e.FkeyTrattore)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FKeyTrattore");

                entity.Property(e => e.ValidoAl).HasColumnType("date");

                entity.Property(e => e.ValidoDal).HasColumnType("date");
            });

            modelBuilder.Entity<VeicoliAziendaliView>(entity =>
            {
                entity.HasKey(e => e.IdveicoloTarga);

                entity.ToView("VeicoliAziendaliView");

                entity.Property(e => e.Container)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.FkeyContainerDescrizione).HasColumnName("FKeyContainerDescrizione");

                entity.Property(e => e.FkeyTipoVeicolo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FKeyTipoVeicolo");

                entity.Property(e => e.IdveicoloTarga)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IDVeicoloTarga");

                entity.Property(e => e.TipoVeicolo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
