using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using moviesnet.Entities;

namespace moviesnet.Services
{
     public static class MoviesDbContextExtensions
     {
          public static void CreateSeedData
               (this MoviesDbContext context)
          {
            
              //Check if Producers and Casts and Movies list Present or not
              if(context.Producers.Any() && context.Actors.Any() && context.Movies.Any()){
                  return;
              }
                //If not Present Add default data to the Table
                  var producers = new List<Producers>()
                  {
                      new Producers()
                      {
                          Name = "Kevin Feige",
                          Sex = 1,
                          DOB = DateTime.ParseExact("2 June 1973", "d MMMM yyyy", null),
                          Bio = "Kevin Feige is an American film producer who has been the president of Marvel Studios since 2007. The films he has produced have a combined worldwide box office gross of over $21 billion."
                      },
                      new Producers()
                      {
                          Name = "Joss Whedon",
                          Sex = 1,
                          DOB = DateTime.ParseExact("23 June 1964", "d MMMM yyyy", null),
                          Bio = "Joseph Hill Whedon is an American producer, director, screenwriter, comic book writer, and composer."
                      }
                  };

                  var actors = new List<Actors>()
                  {
                      new Actors()
                      {
                          Name = "Chris Hemsworth",
                          Sex = 1,
                          DOB = DateTime.ParseExact("11 August 1983", "d MMMM yyyy", null),
                          Bio = "Chris Hemsworth (born 11 August 1983) is an Australian actor. He is best known for his roles as Kim Hyde in the Australian TV series Home and Away (2004) and as Thor in the Marvel Cinematic Universe films Thor (2011), The Avengers (2012), Thor: The Dark World (2013) and Avengers: Age of Ultron (2015). He has also appeared in the science fiction action film Star Trek(2009), the thriller adventure A Perfect Getaway (2009), the horror comedy The Cabin in the Woods (2012), the dark fantasy action film Snow White and the Huntsman (2012), the war film Red Dawn (2012) and the biographical sports drama film Rush (2013)."
                      },
                      new Actors()
                      {
                          Name = "Tom Hiddleston",
                          Sex = 1,
                          DOB = DateTime.ParseExact("9 February 1981", "d MMMM yyyy", null),
                          Bio = "Thomas William \"Tom\" Hiddleston (born 9 February 1981) is an English actor. He is perhaps best known for playing Loki in the 2011 action movie Thor."
                      },
                      new Actors()
                      {
                          Name = "Cate Blanchett",
                          Sex = 2,
                          DOB = DateTime.ParseExact("14 May 1969", "d MMMM yyyy", null),
                          Bio = "Catherine Élise \"Cate\" Blanchett (born 14 May 1969) is an Australian actress and theatre director. She has won multiple acting awards, most notably two SAGs, two Golden Globe Awards, two BAFTAs, and two Academy  Awards, as well as the Volpi Cup at the 64th Venice International Film  Festival. Blanchett has earned seven Academy Award nominations between 1998 and 2015. Blanchett came to international attention for her role as Elizabeth I of England in the 1998 film Elizabeth, directed by Shekhar Kapur, a role which she reprised in Elizabeth"
                      },
                      new Actors()
                      {
                          Name = "Idris Elba",
                          Sex = 1,
                          DOB = DateTime.ParseExact("6 September 1972", "d MMMM yyyy", null),
                          Bio = "Idris Elba (born 6 September 1972) is a British television, theatre, and film actor who has starred in both British and American productions. One of his first acting roles was in the soap opera Family Affairs. Since then he has worked in a variety of TV and movie projects including Ultraviolet, The Wire, No Good Deed and Zootopia."
                      },
                      new Actors()
                      {
                          Name = "Jeff Goldblum",
                          Sex = 1,
                          DOB = DateTime.ParseExact("22 October 1952", "d MMMM yyyy", null),
                          Bio = "Jeffrey Lynn \"Jeff\" Goldblum (born October 22, 1952) is an American actor. His career began in the mid-1970s and he has appeared in major box-office successes including The Fly, Jurassic Park and its sequel Jurassic Park: The Lost World, and Independence Day. He starred as Detective Zach Nichols for the eighth and ninth seasons of the USA Network's crime drama series Law & Order: Criminal Intent."
                      },
                      new Actors()
                      {
                          Name = "Tessa Thompson",
                          Sex = 1,
                          DOB = DateTime.ParseExact("3 October 1983", "d MMMM yyyy", null),
                          Bio = "Tessa Thompson is an American film and television actress, best known for her roles as Valkyrie in the feature films Thor: Ragnarok and Avengers: Endgame, Jackie Cook on the TV series Veronica Mars, and Charlotte Hale on the HBO series Westworld."
                      },
                      new Actors()
                      {
                          Name = "Mark Ruffalo",
                          Sex = 1,
                          DOB = DateTime.ParseExact("22 November 1967", "d MMMM yyyy", null),
                          Bio = "Mark Alan Ruffalo (born November 22, 1967) is an American actor, director, producer and screenwriter. He has worked in films including Eternal Sunshine of the Spotless Mind, Zodiac, Shutter Island, Just Like Heaven, You Can Count on Me and The Kids Are All Right for which he received an Academy Award nomination for Best Supporting Actor."
                      },
                      new Actors()
                      {
                          Name = "Benedict Cumberbatch",
                          Sex = 1,
                          DOB = DateTime.ParseExact("19 July 1976", "d MMMM yyyy", null),
                          Bio = "Benedict Timothy Carlton Cumberbatch (born 19 July 1976) is an English film, television, and theatre actor. His most acclaimed roles include: Stephen Hawking in the BBC drama \"Hawking\" (2004); William Pitt in the historical film \"Amazing Grace\" (2006);"
                      },
                      new Actors()
                      {
                          Name = "Robert Downey Jr.",
                          Sex = 1,
                          DOB = DateTime.ParseExact("4 April 1965", "d MMMM yyyy", null),
                          Bio = "An American actor. Downey made his screen debut in 1970, at the age of five, when he appeared in his father's film Pound, and has worked consistently in film and television ever since. During the 1980s he had roles in a series of coming of age films associated with the Brat Pack. Less Than Zero (1987) is particularly notable, not only because it was the first time Downey's acting would be acknowledged by critics, but also because the role pushed Downey's already existing drug habit one step further."
                      },
                      new Actors()
                      {
                          Name = "Chris Evans",
                          Sex = 1,
                          DOB = DateTime.ParseExact("13 June 1981", "d MMMM yyyy", null),
                          Bio = "Christopher Robert \"Chris\" Evans (born June 13, 1981) is an American actor and filmmaker. Evans is best known for his superhero roles, as Captain America in the Marvel Cinematic Universe, and as Human Torch in Fantastic Four. In 2015, he made his directorial debut with the romantic drama Before We Go."
                      },
                      new Actors()
                      {
                          Name = "Scarlett Johansson",
                          Sex = 2,
                          DOB = DateTime.ParseExact("22 November 1984", "d MMMM yyyy", null),
                          Bio = "Scarlett Johansson, born November 22, 1984, is an American actress, model and singer. She made her film debut in North (1994) and was later nominated for the Independent Spirit Award for Best Female Lead for her performance in Manny & Lo (1996), garnering further acclaim and prominence with roles in The Horse Whisperer (1998) and Ghost World (2001). She shifted to adult roles with her performances in Girl with a Pearl Earring (2003)"
                      },
                      new Actors()
                      {
                          Name = "Jeremy Renner",
                          Sex = 1,
                          DOB = DateTime.ParseExact("7 January 1971", "d MMMM yyyy", null),
                          Bio = "Jeremy Lee Renner (born January 7, 1971, height 5' 10\" (1,78 m)) is an American actor and musician. Renner appeared in films throughout the 2000s, mostly in supporting roles. He came to prominence in films such as Dahmer (2002), S.W.A.T. (2003), Neo Ned (2005), 28 Weeks Later (2007), The Town (2010), and was nominated for an Academy Award for Best Actor for his starring role in the 2009 Best Picture-winning war thriller The Hurt Locker."
                      }
                  };
        

               var movies = new List<Movies>()
               {
                    new Movies()
                    {
                         Name = "Avengers: Endgame",
                         Year = 2019,
                         Plot = "After the devastating events of Avengers: Infinity War, the universe is in ruins due to the efforts of the Mad Titan, Thanos. With the help of remaining allies, the Avengers must assemble once more in order to undo Thanos' actions and restore order to the universe once and for all, no matter what consequences may be in store.",
                         Poster = "https://image.tmdb.org/t/p/w300/bJLYrLIHT1r7cikhWGbpZkxlUpA.jpg",
                         Producers = producers[0]
                    },
                    new Movies()
                    {
                         Name = "Thor: Ragnarock",
                         Year = 2017,
                         Plot = "Thor is imprisoned on the other side of the universe and finds himself in a race against time to get back to Asgard to stop Ragnarok, the destruction of his home-world and the end of Asgardian civilization, at the hands of an all-powerful new threat, the ruthless Hela.",
                         Poster = "https://image.tmdb.org/t/p/w300/rzRwTcFvttcN1ZpX2xv4j3tSdJu.jpg",
                         Producers = producers[0]
                    },
                    new Movies()
                    {
                         Name = "Black Panther",
                         Year = 2018,
                         Plot = "King T'Challa returns home from America to the reclusive, technologically advanced African nation of Wakanda to serve as his country's new leader. However, T'Challa soon finds that he is challenged for the throne by factions within his own country as well as without. Using powers reserved to Wakandan kings, T'Challa assumes the Black Panther mantel to join with girlfriend Nakia, the queen-mother, his princess-kid sister, members of the Dora Milaje (the Wakandan 'special forces') and an American secret agent, to prevent Wakanda from being dragged into a world war.",
                         Poster = "https://image.tmdb.org/t/p/w300/uxzzxijgPIY7slzFvMotPv8wjKA.jpg",
                         Producers = producers[1]
                    },
                    new Movies()
                    {
                         Name = "The Avengers",
                         Year = 2012,
                         Plot = "When an unexpected enemy emerges and threatens global safety and security, Nick Fury, director of the international peacekeeping agency known as S.H.I.E.L.D., finds himself in need of a team to pull the world back from the brink of disaster. Spanning the globe, a daring recruitment effort begins!",
                         Poster = "https://image.tmdb.org/t/p/w300/cezWGskPY5x7GaglTTRN4Fugfb8.jpg",
                         Producers = producers[0]
                    }
               };

            var moviesactor = new List<MoivesActors>()
            {
                new MoivesActors()
                {
                    Movies = movies[0],
                    MoviesId = movies[0].MoviesId,
                    Actors = actors[0],
                    ActorsId = actors[0].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[0],
                    MoviesId = movies[0].MoviesId,
                    Actors = actors[1],
                    ActorsId = actors[1].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[0],
                    MoviesId = movies[0].MoviesId,
                    Actors = actors[2],
                    ActorsId = actors[2].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[0],
                    MoviesId = movies[0].MoviesId,
                    Actors = actors[3],
                    ActorsId = actors[3].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[0],
                    MoviesId = movies[0].MoviesId,
                    Actors = actors[4],
                    ActorsId = actors[4].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[1],
                    MoviesId = movies[1].MoviesId,
                    Actors = actors[0],
                    ActorsId = actors[0].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[1],
                    MoviesId = movies[1].MoviesId,
                    Actors = actors[1],
                    ActorsId = actors[1].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[1],
                    MoviesId = movies[1].MoviesId,
                    Actors = actors[2],
                    ActorsId = actors[2].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[1],
                    MoviesId = movies[1].MoviesId,
                    Actors = actors[3],
                    ActorsId = actors[3].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[1],
                    MoviesId = movies[1].MoviesId,
                    Actors = actors[4],
                    ActorsId = actors[4].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[1],
                    MoviesId = movies[1].MoviesId,
                    Actors = actors[5],
                    ActorsId = actors[5].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[2],
                    MoviesId = movies[2].MoviesId,
                    Actors = actors[11],
                    ActorsId = actors[11].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[2],
                    MoviesId = movies[2].MoviesId,
                    Actors = actors[10],
                    ActorsId = actors[10].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[2],
                    MoviesId = movies[2].MoviesId,
                    Actors = actors[9],
                    ActorsId = actors[9].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[2],
                    MoviesId = movies[2].MoviesId,
                    Actors = actors[8],
                    ActorsId = actors[8].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[2],
                    MoviesId = movies[2].MoviesId,
                    Actors = actors[7],
                    ActorsId = actors[7].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[3],
                    MoviesId = movies[3].MoviesId,
                    Actors = actors[11],
                    ActorsId = actors[11].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[3],
                    MoviesId = movies[3].MoviesId,
                    Actors = actors[5],
                    ActorsId = actors[5].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[3],
                    MoviesId = movies[3].MoviesId,
                    Actors = actors[1],
                    ActorsId = actors[1].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[3],
                    MoviesId = movies[3].MoviesId,
                    Actors = actors[6],
                    ActorsId = actors[6].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[3],
                    MoviesId = movies[3].MoviesId,
                    Actors = actors[3],
                    ActorsId = actors[3].ActorId
                },
                new MoivesActors()
                {
                    Movies = movies[3],
                    MoviesId = movies[3].MoviesId,
                    Actors = actors[8],
                    ActorsId = actors[8].ActorId
                }
            };

               movies[0].MoivesActors.Add(moviesactor[0]);
               movies[0].MoivesActors.Add(moviesactor[1]);
               movies[0].MoivesActors.Add(moviesactor[2]);
               movies[0].MoivesActors.Add(moviesactor[3]);
               movies[0].MoivesActors.Add(moviesactor[4]);

               actors[0].MoivesActors.Add(moviesactor[0]);
               actors[1].MoivesActors.Add(moviesactor[1]);
               actors[2].MoivesActors.Add(moviesactor[2]);
               actors[3].MoivesActors.Add(moviesactor[3]);
               actors[4].MoivesActors.Add(moviesactor[4]);

               movies[1].MoivesActors.Add(moviesactor[5]);
               movies[1].MoivesActors.Add(moviesactor[6]);
               movies[1].MoivesActors.Add(moviesactor[7]);
               movies[1].MoivesActors.Add(moviesactor[8]);
               movies[1].MoivesActors.Add(moviesactor[9]);
               movies[1].MoivesActors.Add(moviesactor[10]);

               actors[0].MoivesActors.Add(moviesactor[5]);
               actors[1].MoivesActors.Add(moviesactor[6]);
               actors[2].MoivesActors.Add(moviesactor[7]);
               actors[3].MoivesActors.Add(moviesactor[8]);
               actors[4].MoivesActors.Add(moviesactor[9]);
               actors[5].MoivesActors.Add(moviesactor[10]);               

               movies[2].MoivesActors.Add(moviesactor[11]);
               movies[2].MoivesActors.Add(moviesactor[12]);
               movies[2].MoivesActors.Add(moviesactor[13]);
               movies[2].MoivesActors.Add(moviesactor[14]);
               movies[2].MoivesActors.Add(moviesactor[15]);

               actors[11].MoivesActors.Add(moviesactor[11]);
               actors[10].MoivesActors.Add(moviesactor[12]);
               actors[9].MoivesActors.Add(moviesactor[13]);
               actors[8].MoivesActors.Add(moviesactor[14]);
               actors[7].MoivesActors.Add(moviesactor[15]);

               movies[3].MoivesActors.Add(moviesactor[16]);
               movies[3].MoivesActors.Add(moviesactor[17]);
               movies[3].MoivesActors.Add(moviesactor[18]);
               movies[3].MoivesActors.Add(moviesactor[19]);
               movies[3].MoivesActors.Add(moviesactor[20]);
               movies[3].MoivesActors.Add(moviesactor[21]);

               actors[11].MoivesActors.Add(moviesactor[16]);
               actors[5].MoivesActors.Add(moviesactor[17]);
               actors[1].MoivesActors.Add(moviesactor[18]);
               actors[6].MoivesActors.Add(moviesactor[19]);
               actors[3].MoivesActors.Add(moviesactor[20]);
               actors[8].MoivesActors.Add(moviesactor[21]);
            
              try {
                context.AddRange(producers);
                context.AddRange(moviesactor);
                context.AddRange(actors);
                context.AddRange(movies);
                context.SaveChanges();
              } catch(Exception ){

              }
          }
     }
}