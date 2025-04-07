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


        public async Task<List<KYCViewModel>> GetAllAsync()
        {
            return await _context.KYCMs
                .Include(k => k.Province)
                .Include(k => k.District)
                .Include(k => k.VDC)
                .Select(k => new KYCViewModel
                {
                    Id = k.Id,
                    Name = k.Name,
                    PhoneNumber = k.PhoneNumber,
                    Email = k.Email,
                    ProvinceId = k.ProvinceId,
                    DistrictId = k.DistrictId,
                    VDCId = k.VDCId,
                    ProvinceName = k.Province.ProvinceName,
                    DistrictName = k.District.DistrictName,
                    VDCName = k.VDC.VDCName
                })
                .ToListAsync();
        }



        public async Task<List<Province>> GetAllProvincesAsync() =>
         await _context.Provinces.ToListAsync();

        public async Task<List<District>> GetDistrictsByProvinceIdAsync(int provinceId) =>
            await _context.Districts.Where(d => d.ProvinceId == provinceId).ToListAsync();

        public async Task<List<VDC>> GetVDCsByDistrictIdAsync(int districtId) =>
            await _context.VDCs.Where(v => v.DistrictId == districtId).ToListAsync();

        public async Task<KYCViewModel> GetByIdAsync(int id)
        {
            var entity = await _context.KYCMs.FindAsync(id);
            if (entity == null) return null;

            return new KYCViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                ProvinceId = entity.ProvinceId,
                DistrictId = entity.DistrictId,
                VDCId = entity.VDCId,
                Districts = await GetDistrictsByProvinceIdAsync(entity.ProvinceId),
                VDCs = await GetVDCsByDistrictIdAsync(entity.DistrictId)
            };
        }

        public async Task SaveAsync(KYCViewModel model)
        {
            KYCM entity;

            if (model.Id == 0)
            {
                entity = new KYCM();
                _context.KYCMs.Add(entity);
            }
            else
            {
                entity = await _context.KYCMs.FindAsync(model.Id);
                if (entity == null) return;
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
                _context.KYCMs.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}

