using System;
using System.Linq;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace moviesnet.Entities
{
     public class Actors
     {
          public Actors(){
               this.MoivesActors = new HashSet<MoivesActors>();
          }
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int ActorId { get; set; }
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
          [Required]
          [JsonIgnore]
          public ICollection<MoivesActors> MoivesActors { get; set; }
     }
}