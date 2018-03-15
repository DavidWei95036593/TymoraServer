using System;
using System.Collections;
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Tymora.Models{
    public partial class TymoraContext : DbContext{
        public virtual DbSet<TymoraRules> TymoraRules { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            if (!optionsBuilder.IsConfigured){
                optionsBuilder.UseMySql(ConfigurationManager.ConnectionStrings["TymoraDatabase"].ConnectionString);
            }
        }

        public TymoraContext(DbContextOptions options):base(options) {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            modelBuilder.Entity<TymoraRules>(entity =>
            {
                entity.ToTable("tymora_rules");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Creator)
                    .HasColumnName("creator")
                    .HasMaxLength(255);

                entity.Property(e => e.Father)
                    .HasColumnName("father")
                    .HasColumnType("int(255)");

                entity.Property(e => e.Rule)
                    .HasColumnName("rule")
                    .HasMaxLength(255);

                entity.Property(e => e.RuleContent).HasColumnName("rule_content");
            });
        }
    }
}
