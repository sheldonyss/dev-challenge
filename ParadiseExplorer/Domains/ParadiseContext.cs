using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ParadiseExplorer.Domains
{
    public partial class ParadiseContext : DbContext
    {
        public ParadiseContext()
        {
        }

        public ParadiseContext(DbContextOptions<ParadiseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Address { get; set; }
        public virtual DbSet<Edges> Edges { get; set; }
        public virtual DbSet<Entity> Entity { get; set; }
        public virtual DbSet<Intermediary> Intermediary { get; set; }
        public virtual DbSet<Officer> Officer { get; set; }
        public virtual DbSet<Other> Other { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=Paradise;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.NodeId);

                entity.ToTable("address");

                entity.Property(e => e.NodeId)
                    .HasColumnName("node_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address1)
                    .IsRequired()
                    .HasColumnName("address")
                    .HasMaxLength(2000);

                entity.Property(e => e.Countries)
                    .IsRequired()
                    .HasColumnName("countries")
                    .HasMaxLength(500);

                entity.Property(e => e.CountryCodes)
                    .IsRequired()
                    .HasColumnName("country_codes")
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(2000);

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(500);

                entity.Property(e => e.SourceId)
                    .HasColumnName("sourceID")
                    .HasMaxLength(500);

                entity.Property(e => e.ValidUntil)
                    .HasColumnName("valid_until")
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Edges>(entity =>
            {
                entity.ToTable("edges");

                entity.Property(e => e.EndDate)
                    .IsRequired()
                    .HasColumnName("end_date")
                    .HasMaxLength(500);

                entity.Property(e => e.EndId).HasColumnName("END_ID");

                entity.Property(e => e.Link)
                    .HasColumnName("link")
                    .HasMaxLength(500);

                entity.Property(e => e.SourceId)
                    .IsRequired()
                    .HasColumnName("sourceID")
                    .HasMaxLength(500);

                entity.Property(e => e.StartDate)
                    .IsRequired()
                    .HasColumnName("start_date")
                    .HasMaxLength(500);

                entity.Property(e => e.StartId).HasColumnName("START_ID");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("TYPE")
                    .HasMaxLength(500);

                entity.Property(e => e.ValidUntil)
                    .IsRequired()
                    .HasColumnName("valid_until")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Entity>(entity =>
            {
                entity.HasKey(e => e.NodeId);

                entity.ToTable("entity");

                entity.Property(e => e.NodeId)
                    .HasColumnName("node_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.ClosedDate)
                    .HasColumnName("closed_date")
                    .HasMaxLength(500);

                entity.Property(e => e.CompanyType)
                    .HasColumnName("company_type")
                    .HasMaxLength(500);

                entity.Property(e => e.Countries)
                    .HasColumnName("countries")
                    .HasMaxLength(500);

                entity.Property(e => e.CountryCodes)
                    .HasColumnName("country_codes")
                    .HasMaxLength(500);

                entity.Property(e => e.IbcRuc)
                    .HasColumnName("ibcRUC")
                    .HasMaxLength(500);

                entity.Property(e => e.InactivationDate)
                    .HasColumnName("inactivation_date")
                    .HasMaxLength(500);

                entity.Property(e => e.IncorporationDate)
                    .HasColumnName("incorporation_date")
                    .HasMaxLength(500);

                entity.Property(e => e.Jurisdiction)
                    .IsRequired()
                    .HasColumnName("jurisdiction")
                    .HasMaxLength(500);

                entity.Property(e => e.JurisdictionDescription)
                    .HasColumnName("jurisdiction_description")
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(1000);

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(500);

                entity.Property(e => e.ServiceProvider)
                    .HasColumnName("service_provider")
                    .HasMaxLength(500);

                entity.Property(e => e.SourceId)
                    .HasColumnName("sourceID")
                    .HasMaxLength(500);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(500);

                entity.Property(e => e.StruckOffDate)
                    .HasColumnName("struck_off_date")
                    .HasMaxLength(500);

                entity.Property(e => e.ValidUntil)
                    .HasColumnName("valid_until")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Intermediary>(entity =>
            {
                entity.HasKey(e => e.NodeId);

                entity.ToTable("intermediary");

                entity.Property(e => e.NodeId)
                    .HasColumnName("node_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Countries)
                    .HasColumnName("countries")
                    .HasMaxLength(500);

                entity.Property(e => e.CountryCodes)
                    .HasColumnName("country_codes")
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(1000);

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(1500);

                entity.Property(e => e.SourceId)
                    .HasColumnName("sourceID")
                    .HasMaxLength(500);

                entity.Property(e => e.ValidUntil)
                    .HasColumnName("valid_until")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Officer>(entity =>
            {
                entity.HasKey(e => e.NodeId);

                entity.ToTable("officer");

                entity.Property(e => e.NodeId)
                    .HasColumnName("node_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Countries)
                    .HasColumnName("countries")
                    .HasMaxLength(500);

                entity.Property(e => e.CountryCodes)
                    .HasColumnName("country_codes")
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(1000);

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(250);

                entity.Property(e => e.SourceId)
                    .IsRequired()
                    .HasColumnName("sourceID")
                    .HasMaxLength(500);

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(500);

                entity.Property(e => e.ValidUntil)
                    .IsRequired()
                    .HasColumnName("valid_until")
                    .HasMaxLength(1000);
            });

            modelBuilder.Entity<Other>(entity =>
            {
                entity.HasKey(e => e.NodeId);

                entity.ToTable("other");

                entity.Property(e => e.NodeId)
                    .HasColumnName("node_id")
                    .ValueGeneratedNever();

                entity.Property(e => e.Countries)
                    .HasColumnName("countries")
                    .HasMaxLength(500);

                entity.Property(e => e.CountryCodes)
                    .HasColumnName("country_codes")
                    .HasMaxLength(500);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(1500);

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(500);

                entity.Property(e => e.SourceId)
                    .IsRequired()
                    .HasColumnName("sourceID")
                    .HasMaxLength(500);

                entity.Property(e => e.ValidUntil)
                    .IsRequired()
                    .HasColumnName("valid_until")
                    .HasMaxLength(1000);
            });
        }
    }
}
