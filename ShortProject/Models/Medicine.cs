namespace ShortProject.Models
{
    public class Medicine : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public int CreatedDate { get; set; }

        public Medicine()
        {
            
        }

    }
}
