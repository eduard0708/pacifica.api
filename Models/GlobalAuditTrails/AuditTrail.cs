using System.ComponentModel.DataAnnotations;
using System;

namespace Pacifica.API.Models.GlobalAuditTrails
{
    public class AuditTrail
    {
      
        [Required]
        public string Action { get; set; } = string.Empty; // "Created", "Updated", "Deleted", or "Restored"

        public string? OldValue { get; set; } // JSON or string representation of old data

        public string? NewValue { get; set; } // JSON or string representation of new data

        [Required]
        public DateTime ActionDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string? ActionBy { get; set; } // User who performed the action

        [StringLength(1500)]
        public string? Remarks { get; set; } // Optional field to store additional notes
      
    }
}
