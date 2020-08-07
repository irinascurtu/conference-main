using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conference.Domain.Entities
{
    public partial class Talk
    {
        [Key]
        public int Id { get; set; }
        public int SpeakerId { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [Required]
        [StringLength(1500)]
        public string Description { get; set; }

        [ForeignKey(nameof(SpeakerId))]
        [InverseProperty(nameof(Entities.Speaker.Talks))]
        public virtual Speaker Speaker { get; set; }
    }
}
