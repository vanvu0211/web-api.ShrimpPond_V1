using ShrimpPond.Application.Contract.Persistence;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Persistence.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Persistence.Repository.Generic
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ShrimpPondDbContext _context;

        public UnitOfWork(ShrimpPondDbContext context)
        {
            _context = context;
            pondRepository = new PondRepository(context);
            certificateRepository = new CertificateRepository(context);
            sizeShrimpRepository = new SizeShrimpRepository(context);
            pondTypeRepository = new PondTypeRepository(context);
            foodFeedingRepository = new FoodFeedingRepository(context);
            foodForFeedingRepository = new FoodForFeedingRepository(context);
            foodRepository = new FoodRepository(context);
            medicineFeedingRepository = new MedicineFeedingRepository(context);
            medicineForFeedingRepository = new MedicineForFeedingRepository(context);
            medicineRepository = new MedicineRepository(context);
            lossShrimpRepository = new LossShrimpRepository(context);
            environmentStatusRepository = new EnvironmentStatusRepository(context);
            harvestRepository = new HarvestRepository(context);
            farmRepository = new FarmRepository(context);
            timeSettingRepository = new TimeSettingRepository(context);
            timeSettingObjectRepository = new TimeSettingObjectRepository(context);
            cleanSensorRepository = new CleanSensorRepository(context);
            machineRepository = new MachineRepository(context);
            pondIdRepository = new PondIdRepository(context);
            alarmRepository = new AlarmRepository(context);
            configurationRepository = new ConfigurationRepository(context);
            farmRoleRepository = new FarmRoleRepository(context);
        }
        public IPondRepository pondRepository { get; private set; }
        public ICertificateRepository certificateRepository { get; private set; }
        public ISizeShrimpRepository sizeShrimpRepository { get; private set; }
        public IPondTypeRepository pondTypeRepository { get; private set; }
        public IFoodFeedingRepository foodFeedingRepository { get; private set; }
        public IFoodForFeedingRepository foodForFeedingRepository { get; private set; }
        public IFoodRepository foodRepository { get; private set; }
        public IMedicineFeedingRepository medicineFeedingRepository { get; }
        public IMedicineForFeedingRepository medicineForFeedingRepository { get; }
        public IMedicineRepository medicineRepository { get; private set; }
        public ILossShrimpRepository lossShrimpRepository { get; private set; }
        public IEnvironmentStatusRepository environmentStatusRepository { get; private set; }
        public IHarvestRepository harvestRepository { get; private set; }
        public IFarmRepository farmRepository { get; private set; }
        public ITimeSettingRepository timeSettingRepository { get; private set; }
        public ITimeSettingObjectRepository timeSettingObjectRepository { get; private set; }
        public ICleanSensorRepository cleanSensorRepository { get; private set; }
        public IMachineRepository machineRepository { get; private set; }
        public IPondIdRepository pondIdRepository { get; private set; }
        public IAlarmRepository alarmRepository { get; private set; }
        public IConfigurationRepository configurationRepository { get; private set; }
        public IFarmRoleRepository farmRoleRepository { get; private set; }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task SaveChangeAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
