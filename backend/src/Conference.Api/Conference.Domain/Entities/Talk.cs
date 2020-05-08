using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Core.Domain
{
    public partial class Talk
    {
        public Talk()
        {
        }

        [Key]
        public int Id { get; set; }
        public int SpeakerId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(1500)]
        public string Description { get; set; }

        [ForeignKey("SpeakerId")]
        public Speaker Speaker { get; set; }



    }
}
