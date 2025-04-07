namespace KYC.Models.DbEntities
{
    public class VDC
    {
        public int VDCId { get; set; }
        public string VDCName { get; set; }
        public int DistrictId { get; set; }
        public District District { get; set; }
    }
}
