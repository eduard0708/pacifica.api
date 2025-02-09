using System.ComponentModel.DataAnnotations;

namespace Pacifica.API.Dtos.InventoryNormalization
{
    public class InventoryNormalizationDto
    {
        [Required]
        public int InventoryId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Adjusted Quantity must be a positive number.")]
        public decimal AdjustedQuantity { get; set; }
        public decimal SystemQuantity { get; set; }
        public decimal ActualQuantity { get; set; }
        public decimal DiscrepancyValue { get; set; }
        
        [Required]
        public DateTime NormalizationDate { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedBy { get; set; }
    }
}