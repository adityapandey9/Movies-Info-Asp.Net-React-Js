using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace moviesnet.Entities
{
     public class MoivesActors
     {
        [JsonIgnore]
        public int MoviesId { get; set; }
        public Movies Movies { get; set; }
        [JsonIgnore]
        public int ActorsId { get; set; }
        public Actors Actors { get; set; }
     }
}