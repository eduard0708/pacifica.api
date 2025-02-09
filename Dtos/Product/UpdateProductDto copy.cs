namespace Pacifica.API.Dtos.Product
{
    public class GetFilterProductDto
    {
        public string? ProductName { get; set; }
        public string? SKU { get; set; }
        public string? ProductStatus { get; set; }
        public int CategoryId { get; set; }
        public int SupplierId { get; set; }
        public bool IsActive { get; set; }
        public string? UpdatedBy { get; set; }

    }
}