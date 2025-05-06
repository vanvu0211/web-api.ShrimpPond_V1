using Microsoft.EntityFrameworkCore;
using ShrimpPond.Application.Feature.TimeSetting.Command.CreateTimeSetting;
using ShrimpPond.Domain.Configuration;
using ShrimpPond.Domain.Farm;
using ShrimpPond.Domain.Food;
using ShrimpPond.Domain.Medicine;
using ShrimpPond.Domain.PondData;
using ShrimpPond.Domain.PondData.CleanSensor;
using ShrimpPond.Domain.PondData.Harvest;
using ShrimpPond.Domain.TimeSetting;


namespace ShrimpPond.Persistence.DatabaseContext
{
    public class ShrimpPondDbContext : DbContext
    {

        public ShrimpPondDbContext(DbContextOptions<ShrimpPondDbContext> options) : base(options)
        {
            Database.SetCommandTimeout(60);
        }

        public DbSet<Pond> Pond { get; set; }
        public DbSet<Food> Food { get; set; }
        public DbSet<Medicine> Medicine { get; set; }
        public DbSet<Certificate> Certificate { get; set; }
        public DbSet<PondType> PondType { get; set; }
        public DbSet<CleanSensor> CleanSensor { get; set; }

        public DbSet<FoodFeeding> FoodFeeding { get; set; }
        public DbSet<FoodForFeeding> FoodForFeeding { get; set; }

        public DbSet<MedicineFeeding> MedicineFeeding { get; set; }
        public DbSet<MedicineForFeeding> MedicineForFeeding { get; set; }
        public DbSet<SizeShrimp> SizeShrimp { get; set; }
        public DbSet<LossShrimp> LossShrimp { get; set; }
        public DbSet<Domain.Environments.EnvironmentStatus> EnvironmentStatus { get; set; }
        public DbSet<Harvest> Harvests { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<TimeSetting> TimeSettings { get; set; }
        public DbSet<TimeSettingObject> timeSettingObjects { get; set; }
        public DbSet<Domain.Machine.Machine> Machines { get; set; }
        public DbSet<Domain.Machine.PondId> PondIds { get; set; }
        public DbSet<Domain.Alarm.Alarm> Alarm { get; set; }
        public DbSet<Configuration> Configuration { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Farm -> CleanSensor (1-n)
            modelBuilder.Entity<CleanSensor>()
                .HasOne(cs => cs.Farm)
                .WithMany()
                .HasForeignKey(cs => cs.FarmId)
                .OnDelete(DeleteBehavior.NoAction); // Tắt cascade delete để tránh xung đột

            // Farm -> CleanSensor (1-n)
            modelBuilder.Entity<Domain.Alarm.Alarm>()
                .HasOne(cs => cs.Farm)
                .WithMany()
                .HasForeignKey(cs => cs.FarmId)
                .OnDelete(DeleteBehavior.NoAction); // Tắt cascade delete để tránh xung đột
            // Pond -> EnvironmentStatus (1-n)
            modelBuilder.Entity<Domain.Environments.EnvironmentStatus>()
                .HasOne(es => es.Pond)
                .WithMany(p => p.EnvironmentStatus)
                .HasForeignKey(es => es.PondId)
                .OnDelete(DeleteBehavior.NoAction); // Khi xóa Pond, xóa EnvironmentStatus

            // Farm -> Food (1-n)
            modelBuilder.Entity<Food>()
                .HasOne(f => f.Farm)
                .WithMany()
                .HasForeignKey(f => f.FarmId)
                .OnDelete(DeleteBehavior.NoAction);


            // Pond -> FoodFeeding (1-n)
            modelBuilder.Entity<FoodFeeding>()
                .HasOne(ff => ff.Pond)
                .WithMany(p => p.FoodFeedings)
                .HasForeignKey(ff => ff.PondId)
                .OnDelete(DeleteBehavior.NoAction);

            // FoodFeeding -> FoodForFeeding (1-n)
            modelBuilder.Entity<FoodForFeeding>()
                .HasOne(fff => fff.FoodFeeding)
                .WithMany(ff => ff.Foods)
                .HasForeignKey(fff => fff.FoodFeedingId)
                .OnDelete(DeleteBehavior.NoAction);



            // Pond -> Harvest (1-n)
            modelBuilder.Entity<Harvest>()
                .HasOne(h => h.Pond)
                .WithMany(p => p.Harvests)
                .HasForeignKey(h => h.PondId)
                .OnDelete(DeleteBehavior.NoAction);

            // Farm -> Medicine (1-n)
            modelBuilder.Entity<Medicine>()
                .HasOne(m => m.Farm)
                .WithMany()
                .HasForeignKey(m => m.FarmId)
                .OnDelete(DeleteBehavior.NoAction);
            // Farm -> machine (1-n)
            modelBuilder.Entity<Domain.Machine.Machine>()
                .HasOne(m => m.Farm)
                .WithMany()
                .HasForeignKey(m => m.FarmId)
                .OnDelete(DeleteBehavior.NoAction);
            // Farm -> machine (1-n)

            modelBuilder.Entity<Configuration>()
                .HasOne(m => m.Farm)
                .WithMany()
                .HasForeignKey(m => m.FarmId)
                .OnDelete(DeleteBehavior.NoAction);
         


            // Pond -> MedicineFeeding (1-n)
            modelBuilder.Entity<MedicineFeeding>()
                .HasOne(mf => mf.Pond)
                .WithMany(p => p.MedicineFeedings)
                .HasForeignKey(mf => mf.PondId)
                .OnDelete(DeleteBehavior.NoAction);

            // MedicineFeeding -> MedicineForFeeding (1-n)
            modelBuilder.Entity<MedicineForFeeding>()
                .HasOne(mff => mff.MedicineFeeding)
                .WithMany(mf => mf.Medicines)
                .HasForeignKey(mff => mff.MedicineFeedingId)
                .OnDelete(DeleteBehavior.NoAction);



            // Pond -> Certificate (1-n)
            modelBuilder.Entity<Certificate>()
                .HasOne(c => c.Pond)
                .WithMany(p => p.Certificates)
                .HasForeignKey(c => c.PondId)
                .OnDelete(DeleteBehavior.NoAction);



            // Pond -> LossShrimp (1-n)
            modelBuilder.Entity<LossShrimp>()
                .HasOne(ls => ls.Pond)
                .WithMany(p => p.LossShrimps)
                .HasForeignKey(ls => ls.PondId)
                .OnDelete(DeleteBehavior.NoAction);


            // PondType -> Pond (1-n)
            modelBuilder.Entity<Pond>()
       .HasOne(p => p.PondType)
       .WithMany()
       .HasForeignKey(p => p.PondTypeId)
       .IsRequired(false)
       .OnDelete(DeleteBehavior.NoAction); // Ngăn chặn tự động cập nhật/xóa

            // Farm -> PondType (1-n)
            modelBuilder.Entity<PondType>()
                .HasOne(pt => pt.Farm)
                .WithMany()
                .HasForeignKey(pt => pt.FarmId)
                .OnDelete(DeleteBehavior.NoAction);



            // Pond -> SizeShrimp (1-n)
            modelBuilder.Entity<SizeShrimp>()
                .HasOne(ss => ss.Pond)
                .WithMany(p => p.SizeShrimps)
                .HasForeignKey(ss => ss.PondId)
                .OnDelete(DeleteBehavior.NoAction);

            // Farm -> TimeSetting (1-n)
            modelBuilder.Entity<TimeSetting>()
                .HasOne(ts => ts.Farm)
                .WithMany()
                .HasForeignKey(ts => ts.FarmId)
                .OnDelete(DeleteBehavior.NoAction);

            // TimeSetting -> TimeSettingObject (1-n)
            modelBuilder.Entity<TimeSettingObject>()
                .HasOne(tso => tso.TimeSetting)
                .WithMany(ts => ts.TimeSettingObjects)
                .HasForeignKey(tso => tso.TimeSettingId)
                .OnDelete(DeleteBehavior.Cascade);



            // Đặt khóa chính cho các entity
            modelBuilder.Entity<CleanSensor>().HasKey(cs => cs.CleanSensorId);
            modelBuilder.Entity<CleanSensor>().Property(cs => cs.CleanSensorId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Domain.Environments.EnvironmentStatus>().HasKey(es => es.EnvironmentStatusId);
            modelBuilder.Entity<Domain.Environments.EnvironmentStatus>().Property(cs => cs.EnvironmentStatusId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Farm>().HasKey(f => f.FarmId);
            modelBuilder.Entity<Farm>().Property(cs => cs.FarmId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Food>().HasKey(f => f.FoodId);
            modelBuilder.Entity<Food>().Property(cs => cs.FoodId).ValueGeneratedOnAdd();
            modelBuilder.Entity<FoodFeeding>().HasKey(ff => ff.FoodFeedingId);
            modelBuilder.Entity<FoodFeeding>().Property(cs => cs.FoodFeedingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<FoodForFeeding>().HasKey(fff => fff.FoodForFeedingId);
            modelBuilder.Entity<FoodForFeeding>().Property(cs => cs.FoodForFeedingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Harvest>().HasKey(h => h.HarvestId);
            modelBuilder.Entity<Harvest>().Property(cs => cs.HarvestId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Medicine>().HasKey(m => m.MedicineId);
            modelBuilder.Entity<Medicine>().Property(cs => cs.MedicineId).ValueGeneratedOnAdd();
            modelBuilder.Entity<MedicineFeeding>().HasKey(mf => mf.MedicineFeedingId);
            modelBuilder.Entity<MedicineFeeding>().Property(cs => cs.MedicineFeedingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<MedicineForFeeding>().HasKey(mff => mff.MedicineForFeedingId);
            modelBuilder.Entity<MedicineForFeeding>().Property(cs => cs.MedicineForFeedingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Certificate>().HasKey(c => c.CertificateId);
            modelBuilder.Entity<Certificate>().Property(cs => cs.CertificateId).ValueGeneratedOnAdd();
            modelBuilder.Entity<LossShrimp>().HasKey(ls => ls.LossShrimpId);
            modelBuilder.Entity<LossShrimp>().Property(cs => cs.LossShrimpId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Pond>().HasKey(p => p.PondId);
            modelBuilder.Entity<PondType>().HasKey(pt => pt.PondTypeId);
            modelBuilder.Entity<SizeShrimp>().HasKey(ss => ss.SizeShrimpId);
            modelBuilder.Entity<SizeShrimp>().Property(cs => cs.SizeShrimpId).ValueGeneratedOnAdd();
            modelBuilder.Entity<TimeSetting>().HasKey(ts => ts.TimeSettingId);
            modelBuilder.Entity<TimeSetting>().Property(cs => cs.TimeSettingId).ValueGeneratedOnAdd();
            modelBuilder.Entity<TimeSettingObject>().HasKey(tso => tso.TimeSettingObjectId);
            modelBuilder.Entity<TimeSettingObject>().Property(cs => cs.TimeSettingObjectId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Domain.Machine.Machine>().HasKey(tso => tso.MachineId);
            modelBuilder.Entity<Domain.Machine.Machine>().Property(cs => cs.MachineId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Domain.Machine.PondId>().HasKey(tso => tso.Id);
            modelBuilder.Entity<Domain.Machine.PondId>().Property(cs => cs.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<Domain.Alarm.Alarm>().HasKey(tso => tso.AlarmId);
            modelBuilder.Entity<Domain.Alarm.Alarm>().Property(cs => cs.AlarmId).ValueGeneratedOnAdd();
            modelBuilder.Entity<Configuration>().HasKey(tso => tso.Id);
        }
    }
}
