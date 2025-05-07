using KYC.Models.DbEntities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace KYC.Models.ViewModel
{
    public class KYCViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please select a province.")]
        public int ProvinceId { get; set; }

        [Required(ErrorMessage = "Please select a district.")]
        public int DistrictId { get; set; }

        [Required(ErrorMessage = "Please select a VDC.")]
        public int VDCId { get; set; }

      
        public string ProvinceName { get; set; }
     
        public string DistrictName { get; set; }
   
        public string VDCName { get; set; }


        public List<Province> Provinces { get; set; }
      
        public List<District> Districts { get; set; }

        public List<VDC> VDCs { get; set; }

    }
}

