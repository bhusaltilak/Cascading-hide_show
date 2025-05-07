using KYC.Models.DbEntities;
using KYC.Models.ViewModel;
using KYC.Repository;
using Microsoft.AspNetCore.Mvc;

namespace KYC.Controllers
{
    public class KYCController : Controller
    {
        private readonly IKYCRepository _repo;

        public KYCController(IKYCRepository repo)
        {
            _repo = repo;
        }

      
        public async Task<IActionResult> Index()
        {
            var list = await _repo.GetAllAsync();
            return View(list);
        }


        [HttpGet]
        public async Task<IActionResult> CreateEdit(int? id)
        {
            var model = await _repo.GetByIdAsync(id);

            model.Provinces = await _repo.GetAllProvincesAsync();
            model.Districts = model.ProvinceId != 0
                ? await _repo.GetDistrictsByProvinceIdAsync(model.ProvinceId)
                : new List<District>();
            model.VDCs = model.DistrictId != 0
                ? await _repo.GetVDCsByDistrictIdAsync(model.DistrictId)
                : new List<VDC>();

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> CreateEdit(KYCViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Provinces = await _repo.GetAllProvincesAsync();
                model.Districts = await _repo.GetDistrictsByProvinceIdAsync(model.ProvinceId);
                model.VDCs = await _repo.GetVDCsByDistrictIdAsync(model.DistrictId);
                return View(model);
            }

            await _repo.SaveAsync(model);
            return RedirectToAction(nameof(Index));
        }

      
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _repo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        
        [HttpGet]
        public async Task<JsonResult> GetDistricts(int provinceId)
        {
            var data = await _repo.GetDistrictsByProvinceIdAsync(provinceId);
            return Json(data.Select(d => new { d.DistrictId, d.DistrictName }));
        }

 
        [HttpGet]
        public async Task<JsonResult> GetVDCs(int districtId)
        {
            var data = await _repo.GetVDCsByDistrictIdAsync(districtId);
            return Json(data.Select(v => new { v.VDCId, v.VDCName }));
        }

       
    }
}
