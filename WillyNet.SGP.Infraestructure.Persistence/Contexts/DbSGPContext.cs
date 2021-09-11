using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WillyNet.SGP.Core.Application.Interfaces;
using WillyNet.SGP.Core.Domain.Common;
using WillyNet.SGP.Core.Domain.Entities;

namespace WillyNet.SGP.Infraestructure.Persistence.Contexts
{
    public class DbSGPContext : IdentityDbContext<UserApp>
    {
        private readonly IDateTimeService _dateTime;
        public DbSGPContext(DbContextOptions options, IDateTimeService dateTime) : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            _dateTime = dateTime;
        }

        public DbSet<Archivo> Archivos {get;set;}
        public DbSet<Area> Areas {get;set;}
        public DbSet<Componente> Componentes {get;set;}
        public DbSet<Estado> Estados {get;set;}
        public DbSet<Flujo> Flujos {get;set;}
        public DbSet<Iniciativa> Iniciativas { get; set; }
        public DbSet<Modulo> Modulos { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = _dateTime.NowUtc;
                        //entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModified = _dateTime.NowUtc;
                        //entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Archivo>(entity =>
            {
                entity.HasKey(e => e.ArchiId);
                entity.ToTable("Archivo");
                entity.Property(p => p.ArchiNomb)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(p => p.ArchiUbic)
                    .IsRequired()
                    .HasMaxLength(350);
                entity.HasOne(o => o.Iniciativa)
                    .WithMany(m => m.Archivos)
                    .HasForeignKey(f => f.IniId);
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.HasKey(e => e.AreaId);
                entity.ToTable("Area");
                entity.Property(p => p.AreaNomb)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.HasOne(o => o.UserAppRespons)
                    .WithMany(m => m.Areas)
                    .HasForeignKey(f => f.UserResponsId);
            });

            modelBuilder.Entity<Componente>(entity =>
            {
                entity.HasKey(e => e.CompId);
                entity.ToTable("Componente");
                entity.Property(p => p.CompNomb)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.EstadId);
                entity.ToTable("Estado");
                entity.Property(p => p.EstadNomb)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Flujo>(entity =>
            {
                entity.HasKey(e => e.FlujoId);
                entity.ToTable("Flujo");
                entity.Property(p => p.FlujoEspecific)                    
                    .HasMaxLength(150);
                entity.HasOne(o => o.Estado)
                    .WithMany(m => m.Flujos)
                    .HasForeignKey(f => f.EstadId);
                entity.HasOne(o => o.Modulo)
                    .WithMany(m => m.Flujos)
                    .HasForeignKey(f => f.ModuId);
                entity.HasOne(o => o.Iniciativa)
                    .WithMany(m => m.Flujos)
                    .HasForeignKey(f => f.IniId);
            });

            modelBuilder.Entity<Iniciativa>(entity =>
            {
                entity.HasKey(e => e.IniId);
                entity.ToTable("Iniciativa");
                entity.Property(p => p.IniNomb)
                    .HasMaxLength(20);
                entity.Property(p => p.IniCodi)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(p => p.IniDescrip)
                    .HasMaxLength(150);
                entity.HasOne(o => o.Area)
                    .WithMany(m => m.Iniciativas)
                    .HasForeignKey(f => f.AreaId);
                entity.HasOne(o => o.Componente)
                    .WithMany(m => m.Iniciativas)
                    .HasForeignKey(f => f.CompId);
                entity.HasOne(o => o.UserAppCrea)
                    .WithMany(m => m.IniciativasUserCrea)
                    .HasForeignKey(f => f.UserCreaId);
                entity.HasOne(o => o.UserAppSolic)
                    .WithMany(m => m.IniciativasUserSolic)
                    .HasForeignKey(f => f.UserSolicId);
            });

            modelBuilder.Entity<Modulo>(entity =>
            {
                entity.HasKey(e => e.ModuId);
                entity.ToTable("Modulo");
                entity.Property(p => p.ModuNomb)
                    .IsRequired()
                    .HasMaxLength(20);
            });

        }

    }
}
