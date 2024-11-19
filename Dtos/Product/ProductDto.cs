namespace Pacifica.API.Dtos.Product
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public string? SKU { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
 
    }
}