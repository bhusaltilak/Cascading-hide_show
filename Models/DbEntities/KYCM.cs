using System.ComponentModel.DataAnnotations.Schema;

namespace KYC.Models.DbEntities
{
    public class KYCM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public int ProvinceId { get; set; }
        public int DistrictId { get; set; }
        public int VDCId { get; set; }
        [ForeignKey("ProvinceId")]
        public Province Province { get; set; }
        [ForeignKey("DistrictId")]
        public District District { get; set; }
        [ForeignKey("VDCId")]
        public VDC VDC { get; set; }

        public bool IsDeleted { get; set; } = false;



    }

}
