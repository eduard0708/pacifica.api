
using System.ComponentModel.DataAnnotations.Schema;

namespace Pacifica.API.Models.Inventory
{
    public class InventorySnapshot : AuditDetails
    {
        public int Id { get; set; }                // Unique ID for each inventory snapshot
        public int BranchId { get; set; }  // Reference to the specific branch-product
        public int ProductId { get; set; }  // Reference to the specific branch-product
        public DateTime InventoryDate { get; set; }  // Date of the inventory
        public int Year { get; set; }  // Date of the inventory
        public int Month { get; set; }  // Date of the inventory

        [Column(TypeName = "decimal(18, 2)")]
        public decimal ActualQuantity { get; set; }  // Actual quantity counted
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CostPrice { get; set; }  // Actual quantity counted
        [Column(TypeName = "decimal(8, 1)")]
        public decimal SystemQuantity { get; set; }  // Quantity from the system
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Discrepancy { get; set; }    // The difference between system and actual quantity
        // Method to calculate discrepancy

        public void CalculateDiscrepancy()
        {
            Discrepancy = ActualQuantity - SystemQuantity;
        }

        // Week number based on the 7th, 14th, 21st, or 28th date
        public int WeekNumber
        {
            get
            {
                if (InventoryDate.Day <= 7) return 1;
                if (InventoryDate.Day <= 14) return 2;
                if (InventoryDate.Day <= 21) return 3;
                return 4; // For the 28th
            }
        }

        public BranchProduct? BranchProduct { get; set; }    // The difference between system and actual quantity
    }
}
