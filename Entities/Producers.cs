using System;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace moviesnet.Entities
{
     public class Producers
     {
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int ProducerId { get; set; }
          [Required]
          [MaxLength(100)]
          public string Name { get; set; }
          [Required]
          public int Sex { get; set; }
          [Required]
          public DateTime DOB { get; set; }
          [Required]
          [MaxLength(1000)]
          public string Bio { get; set; }
     }
}