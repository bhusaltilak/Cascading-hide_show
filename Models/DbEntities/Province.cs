namespace KYC.Models.DbEntities
{
    public class Province
    {
        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; }
        public List<District> Districts { get; set; }
    }
}
