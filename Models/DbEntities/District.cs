namespace KYC.Models.DbEntities
{
    public class District
    {
        public int DistrictId { get; set; }
        public string DistrictName { get; set; }
        public int ProvinceId { get; set; }
        public Province Province { get; set; }
        public List<VDC> VDCs { get; set; }
    }
}
    