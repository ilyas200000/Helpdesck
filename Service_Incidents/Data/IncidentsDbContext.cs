using Microsoft.EntityFrameworkCore;
using Service_Incidents.Models;

namespace Service_Incidents.Data
{
    public class IncidentsDbContext : DbContext
    {
        public IncidentsDbContext(DbContextOptions<IncidentsDbContext> options ) : base( options )
        {
        }
        public virtual DbSet<Incident> Incidents { get; set; }
        public virtual DbSet<Types> Types { get; set; }
        public virtual DbSet<Statut> Statuts { get; set; }
        public virtual DbSet<Priorite> Priorites { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Incident>(entity =>
            {
                entity.HasKey(e => e.INCD_ID);
                entity.Property(e => e.INCD_ID).ValueGeneratedNever();
                entity.Property(e => e.INCD_DESC).HasMaxLength(255);
                entity.Property(e => e.agn_code).HasMaxLength(255);
                entity.Property(e => e.incd_audit).HasMaxLength(255);
                entity.Property(e => e.Phone).HasMaxLength(20);
                // Configure other property lengths and types as needed

                // Configure relationships
                entity.HasOne<Types>()
                      .WithMany()
                      .HasForeignKey(e => e.INCD_TYPE_ID);

                entity.HasOne<Statut>()   
                      .WithMany()
                      .HasForeignKey(e => e.INCD_STAT_ID);

                entity.HasOne<Priorite>()
                      .WithMany()
                      .HasForeignKey(e => e.INCD_PRIO_ID);

                entity.HasOne<Ticket>()
                      .WithOne()
                      .HasForeignKey<Incident>(d=> d.INCD_NUM_TICK);


            });

            modelBuilder.Entity<Types>(entity =>
            {
                entity.HasKey(e => e.INCD_TYPE_ID);
                entity.Property(e => e.TYPE_DESC).HasMaxLength(255);
                // Configure other property lengths and types as needed
            });

            modelBuilder.Entity<Statut>(entity =>
            {
                entity.HasKey(e => e.INCD_STAT_ID);
                entity.Property(e => e.STAT_DESC).HasMaxLength(255);
            });

            modelBuilder.Entity<Priorite>(entity =>
            {
                entity.HasKey(e => e.INCD_PRIO_ID);
                entity.Property(e => e.PRIO_DESC).HasMaxLength(255);
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.HasKey(e => e.INCD_NUM_TICK);
                entity.Property(e => e.TICK_DESC).HasMaxLength(255);
            });
        }
    }


}

