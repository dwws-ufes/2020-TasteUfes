using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TasteUfes.Models
{
    public class PreparationStep : Entity
    {
        [Required]
        public int Step { get; set; }

        [Required]
        [StringLength(2048)]
        public string Description { get; set; }

        [ForeignKey("PreparationId")]
        public Preparation Preparation { get; set; }
        public Guid PreparationId { get; set; }
    }
}