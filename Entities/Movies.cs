using System;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moviesnet.Entities
{
     public class Movies
     {
          public Movies(){
              this.MoivesActors = new HashSet<MoivesActors>();
          }
          [Key]
          [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
          public int MoviesId { get; set; }
          [Required]
          [MaxLength(100)]
          public string Name { get; set; }
          [Required]
          public int Year { get; set; }
          [Required]
          [MaxLength(1000)]
          public string Plot { get; set; }
          [Required]
          [MaxLength(200)]
          public string Poster { get; set; }
          [Required]
          public Producers Producers { get; set; }
          [JsonIgnore]
          public int ProducersId { get; set; }
          [Required]
          public ICollection<MoivesActors> MoivesActors { get; set; }
     }
}