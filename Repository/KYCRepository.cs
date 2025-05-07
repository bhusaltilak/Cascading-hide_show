    using KYC.Data;
    using KYC.Models.DbEntities;
    using KYC.Models.ViewModel;
    using Microsoft.EntityFrameworkCore;

    namespace KYC.Repository
    {
        public class KYCRepository : IKYCRepository
        {
            private readonly ApplicationDbContext _context;

            public KYCRepository(ApplicationDbContext context)
            {
                _context = context;
            }

        public async Task<KYCViewModel> GetByIdAsync(int? id)
        {
            if (id == null)
                return new KYCViewModel();

            var data = await (
                from kyc in _context.KYCMs
                join province in _context.Provinces on kyc.ProvinceId equals province.ProvinceId
                join district in _context.Districts on kyc.DistrictId equals district.DistrictId
                join vdc in _context.VDCs on kyc.VDCId equals vdc.VDCId
                where kyc.Id == id && !kyc.IsDeleted
                select new KYCViewModel
                {
                    Id = kyc.Id,
                    Name = kyc.Name,
                    PhoneNumber = kyc.PhoneNumber,
                    Email = kyc.Email,
                    ProvinceId = kyc.ProvinceId,
                    DistrictId = kyc.DistrictId,
                    VDCId = kyc.VDCId,
                    ProvinceName = province.ProvinceName,
                    DistrictName = district.DistrictName,
                    VDCName = vdc.VDCName
                }
            ).FirstOrDefaultAsync();

            return data ?? new KYCViewModel();
        }



        public async Task<List<KYCViewModel>> GetAllAsync()
        {
            var data = await (
                from kyc in _context.KYCMs
                join province in _context.Provinces on kyc.ProvinceId equals province.ProvinceId
                join district in _context.Districts on kyc.DistrictId equals district.DistrictId
                join vdc in _context.VDCs on kyc.VDCId equals vdc.VDCId
                where !kyc.IsDeleted
                select new KYCViewModel
                {
                    Id = kyc.Id,
                    Name = kyc.Name,
                    PhoneNumber = kyc.PhoneNumber,
                    Email = kyc.Email,
                    ProvinceId = kyc.ProvinceId,
                    DistrictId = kyc.DistrictId,
                    VDCId = kyc.VDCId,
                    ProvinceName = province.ProvinceName,
                    DistrictName = district.DistrictName,
                    VDCName = vdc.VDCName
                }
            ).ToListAsync();

            return data ?? new List<KYCViewModel>();
        }



        public async Task<List<Province>> GetAllProvincesAsync() =>
             await _context.Provinces.ToListAsync();

            public async Task<List<District>> GetDistrictsByProvinceIdAsync(int provinceId) =>
                await _context.Districts.Where(d => d.ProvinceId == provinceId).ToListAsync();

            public async Task<List<VDC>> GetVDCsByDistrictIdAsync(int districtId) =>
                await _context.VDCs.Where(v => v.DistrictId == districtId).ToListAsync();



        public async Task SaveAsync(KYCViewModel model)
        {
            KYCM entity;

            if (model.Id == 0)
            {
               
                entity = new KYCM
                {
                    Id = model.Id 
                };
                _context.KYCMs.Add(entity);
            }
            else
            {
               
                entity = await _context.KYCMs
                    .FirstOrDefaultAsync(e => e.Id == model.Id && e.Id == model.Id);

                if (entity == null) 
                    return; 
            }

          
            entity.Name = model.Name;
            entity.PhoneNumber = model.PhoneNumber;
            entity.Email = model.Email;
            entity.ProvinceId = model.ProvinceId;
            entity.DistrictId = model.DistrictId;
            entity.VDCId = model.VDCId;

            await _context.SaveChangesAsync();
        }


        public async Task DeleteAsync(int id)
            {
                var entity = await _context.KYCMs.FindAsync(id);
                if (entity != null)
                {
                entity.IsDeleted = true;
                await _context.SaveChangesAsync();
                }
            }



    }
}

