using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conference.Domain.Entities
{
    public partial class Speaker
    {
        public Speaker()
        {
            Talks = new HashSet<Talk>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        public string Bio { get; set; }
        public string JobTitle { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public string TwitterHandle { get; set; }
        public string CompanyName { get; set; }

        [InverseProperty("Speaker")]
        public virtual ICollection<Talk> Talks { get; set; }
    }
}
