using static Pacifica.API.Helper.GlobalEnums;

namespace Pacifica.API.Dtos.Inventory
{
    public class ResponseNormalizeProduct
    {
        public int BranchId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? SKU { get; set; }
        public DateTime NormalizationDate { get; set; }
        public decimal ActualQuantity { get; set; }
        public decimal SystemQuantity { get; set; }
        public decimal AdjustedQuantity { get; set; }
        public decimal DiscrepancyValue { get; set; }
        public decimal CostPrice { get; set; }
        public string? Remarks { get; set; }
        public string? CreatedBy { get; set; }

    }
}