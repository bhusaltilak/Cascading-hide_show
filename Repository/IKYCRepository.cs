using KYC.Models.DbEntities;
using KYC.Models.ViewModel;

namespace KYC.Repository
{
    public interface IKYCRepository
    {

        Task<List<KYCViewModel>> GetAllAsync();

        Task<List<Province>> GetAllProvincesAsync();
        Task<List<District>> GetDistrictsByProvinceIdAsync(int provinceId);
        Task<List<VDC>> GetVDCsByDistrictIdAsync(int districtId);
        Task<KYCViewModel> GetByIdAsync(int? id);
        Task SaveAsync(KYCViewModel model);
        Task DeleteAsync(int id);

    }
}
