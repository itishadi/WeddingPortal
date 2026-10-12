namespace WeddingPortal.Models
{
    public class WeddingImage
    {
        public int Id { get; set; }

        public string ImagePath { get; set; } = "";

        public int WeddingInfoId { get; set; }

        public WeddingInfo? WeddingInfo { get; set; }
    }
}