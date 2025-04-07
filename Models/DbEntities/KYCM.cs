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

        public Province Province { get; set; }
        public District District { get; set; }
        public VDC VDC { get; set; }



    }

}
