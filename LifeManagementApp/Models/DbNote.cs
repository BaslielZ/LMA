using System;
using System.ComponentModel.DataAnnotations;

namespace LifeManagementApp.Models
{
    public class DbNote
    {
        [Key]
        public int Id { get; set; }  // Primary Key

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }  // Short title

        public string Text { get; set; }   // Full note content

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }
}