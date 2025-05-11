using Microsoft.EntityFrameworkCore;

namespace Repository;

public class EquipmentDbContext : DbContext
{
    public EquipmentDbContext(DbContextOptions<EquipmentDbContext> options) : base(options) { }

    public DbSet<EquipmentType> EquipmentTypes { get; set; }
    public DbSet<Equipment> Equipments { get; set; }
    public DbSet<MaintenanceTask> MaintenanceTasks { get; set; }
    public DbSet<EquipmentMaintenance> EquipmentMaintenances { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EquipmentMaintenance>()
            .HasKey(em => new { em.EquipmentId, em.MaintenanceTaskId });

        modelBuilder.Entity<EquipmentMaintenance>()
            .HasOne(em => em.Equipment)
            .WithMany(e => e.EquipmentMaintenances)
            .HasForeignKey(em => em.EquipmentId);

        modelBuilder.Entity<EquipmentMaintenance>()
            .HasOne(em => em.MaintenanceTask)
            .WithMany(mt => mt.EquipmentMaintenances)
            .HasForeignKey(em => em.MaintenanceTaskId);
    }
} 