using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using moviesnet.Services;
using moviesnet.Entities;
using Microsoft.AspNetCore.Cors;
using System.Text.RegularExpressions;   
using System.Net;

namespace moviesnet.Controllers
{
     [Route("api/[controller]")]
     public class MoviesController : Controller
     {
          private MoviesDbContext _context;
          public MoviesController(MoviesDbContext context)
          {
               _context = context;
          }

          //Get List of Movies
          [EnableCors("CorsPolicy")]
          public IActionResult GetMovies()
          {
               var movies = _context.Movies
                    .Include(s => s.Producers)
                    .Include(s => s.MoivesActors)
                    .ThenInclude(t => t.Actors);

               return Ok(movies);
          }

          //Add New Moive Entity or Update Moive Entity
          [EnableCors("CorsPolicy")]
          [HttpPost]
          public IActionResult postResult([FromBody] Movies movies)
          {
               if(movies == null){
                    PostRespone usererror = new PostRespone();
                    usererror.Status = 0;
                    usererror.Message = "Cannot find the Submited Data";
                    return Ok(usererror);
               }
               var updatemovie = new Movies();
               var updatepro = new Producers();
               //Check if MovieId is present in the post
               if(movies.MoviesId > 0)
               {
                    try {
                         updatemovie = _context.Movies
                              .First(w => w.MoviesId == movies.MoviesId);
                    } catch(Exception){
                         PostRespone usererror = new PostRespone();
                         usererror.Status = 0;
                         usererror.Message = "Cannot find the Movie";
                         return Ok(usererror);
                    }
               }
               //Check if Producer is Present
               if(movies.Producers != null && movies.Producers.ProducerId > 0)
               {
                    try {
                    updatepro = _context.Producers
                         .First(w => w.ProducerId == movies.Producers.ProducerId);
                    } catch(Exception){
                         PostRespone usererror = new PostRespone();
                         usererror.Status = 0;
                         usererror.Message = "Cannot Find the Producer";
                         return Ok(usererror);
                    }
               }
               var regx = new Regex(@"^[a-zA-Z \.]{2,30}$");

               //Update Movie Name if Present
               if(movies.Name != null && !movies.Name.Equals(updatemovie.Name))
               {
                    updatemovie.Name = movies.Name;
               }
               //Update Movie Plot if Present
               if(movies.Plot != null && !movies.Plot.Equals(updatemovie.Plot))
                    updatemovie.Plot = movies.Plot;
               //Update Movie Poster if Present and If Image Present.
               if(movies.Poster != null && !movies.Poster.Equals(updatemovie.Poster))
               {
                    if(!ImageExist.doesImageExistRemotely(movies.Poster, "image"))
                    {
                         PostRespone usererror = new PostRespone();
                         usererror.Status = 0;
                         usererror.Message = "Movies Poster is wrong";
                         return Ok(usererror);
                    }
                    updatemovie.Poster = movies.Poster;
               }
               //Update Movie Year if Present
               if(movies.Year > 0 && movies.Year != updatemovie.Year)
                    updatemovie.Year = movies.Year;

               Producers pro = movies.Producers;
               //Update Producer Bio if Present
               if(pro.Bio != null && !pro.Bio.Equals(updatepro.Bio))
                    updatepro.Bio = pro.Bio;
               //Update Producer Name if Present
               if(pro.Name != null && !pro.Name.Equals(updatepro.Name))
               {
                    if(!regx.IsMatch(pro.Name))
                    {
                         PostRespone usererror = new PostRespone();
                         usererror.Status = 0;
                         usererror.Message = "Producer Name is in wrong format";
                         return Ok(usererror);
                    }
                    updatepro.Name = pro.Name;
               }
               //Update Producer Sex if Present
               if(pro.Sex > 0 && pro.Sex != updatepro.Sex)
                    updatepro.Sex = pro.Sex;
               //Update Producer DoB if Present
               if(pro.DOB != null && pro.DOB.CompareTo(updatepro.DOB) != 0)
               {
                    if(pro.DOB.CompareTo(DateTime.Now) >= 0)
                    {
                         PostRespone usererror = new PostRespone();
                         usererror.Status = 0;
                         usererror.Message = "Producer DOB is wrong";
                         return Ok(usererror);
                    }
                    updatepro.DOB = pro.DOB;
               }
               
               foreach(var movieactor in movies.MoivesActors)
               {
                    var updateactor = new Actors();
                    var actor = movieactor.Actors;
                    //Check if Cast Id is Present
                    if(actor.ActorId > 0){
                         try {
                              updateactor = _context.Actors
                                   .First(w => w.ActorId == actor.ActorId);
                         } catch(Exception){
                              PostRespone usererror = new PostRespone();
                              usererror.Status = 0;
                              usererror.Message = "Cannot Find the Actor";
                              return Ok(usererror);
                         }
                    }
                    //Update Cast Name if Present
                    if(actor.Name != null && !actor.Name.Equals(updateactor.Name))
                    {
                         if(!regx.IsMatch(actor.Name))
                         {
                              PostRespone usererror = new PostRespone();
                              usererror.Status = 0;
                              usererror.Message = "Cast Name is in wrong format";
                              return Ok(usererror);
                         }
                         updateactor.Name = actor.Name;
                    }
                    //Update Cast Bio if Present
                    if(actor.Bio != null && !actor.Bio.Equals(updateactor.Bio))
                         updateactor.Bio = actor.Bio;
                    //Update Cast DoB if Present and Less than today's date
                    if(actor.DOB != null && actor.DOB.CompareTo(updateactor.DOB) != 0)
                    {
                         if(actor.DOB.CompareTo(DateTime.Now) >= 0)
                         {
                              PostRespone usererror = new PostRespone();
                              usererror.Status = 0;
                              usererror.Message = "Cast DOB is wrong";
                              return Ok(usererror);
                         }
                         updateactor.DOB = actor.DOB;
                    }
                    //Update Cast Sex if Present
                    if(actor.Sex > 0 && actor.Sex != updateactor.Sex)
                         updateactor.Sex = actor.Sex;
                    //Add Cast Entity in table if not present
                    if(actor.ActorId == 0){
                         _context.Actors.Add(updateactor);
                    } 
                    //Update Movie Enitity MoviesActor
                     updatemovie.MoivesActors.Add(
                         new MoivesActors
                         {
                              Movies = updatemovie,
                              MoviesId = updatemovie.MoviesId,
                              Actors = updateactor,
                              ActorsId = updateactor.ActorId
                         });
               }
               //Check if Movie Enity Id is present
               if(movies.MoviesId > 0)
               {
                    var movieactors = _context.MoivesActors.Where(w => w.MoviesId == movies.MoviesId);
                    //Remove and add unwanted MoivesActor Enity 
                    foreach (var item in movieactors)
                    {
                        if(!updatemovie.MoivesActors.Contains(item))
                        {
                             _context.MoivesActors.Remove(item);
                        } 
                        else 
                        {
                             updatemovie.MoivesActors.Remove(item);
                        }
                    }
               }
               //Add Producer Entity in the Table if not Present
               if(movies.MoviesId == 0 && movies.Producers != null && movies.Producers.ProducerId == 0)
               {
                    _context.Producers.Add(updatepro);
               }
               //Add Movie Entity if not Present
               if(movies.MoviesId == 0)
               {
                    _context.Movies.Add(updatemovie);
               }
               updatemovie.Producers = updatepro;               
               
               PostRespone postRespone = new PostRespone();
               postRespone.Message = "Database Updated";
               postRespone.Status = 1;
               //Save the Movies Entity Context
               try {
                    _context.SaveChanges();
               } catch(Exception){
                    postRespone.Status = 0;
                    postRespone.Message = "An error occurred while updating the entries.";
               }
               //Send the Response
               return Ok(postRespone);
          }

          //Send the Movie Entity by the Id.
          [EnableCors("CorsPolicy")]
          [HttpGet("movie/{id}", Name = "Movie_List")]
          public IActionResult GetMovie(int id) 
          { 
               //Search the Movie by Id, if not Present send Error Response
               try {
                    var movie = _context.Movies.Include(s => s.Producers)
                    .Include(s => s.MoivesActors)
                    .ThenInclude(t => t.Actors).Where(w => w.MoviesId == id).FirstOrDefault();
                    return Ok(movie);
               } catch(Exception){
                    PostRespone usererror = new PostRespone();
                    usererror.Status = 0;
                    usererror.Message = "Cannot Find the Movie";
                    return Ok(usererror);
               }
          }

          //Send the Casts and Producers List
          [EnableCors("CorsPolicy")]
          [Route("pa")]
          public IActionResult getPAList()
          {
               PARespone paRespone = new PARespone();
               paRespone.Actor = _context.Actors;
               paRespone.Producer = _context.Producers;
               return Ok(paRespone);
          }

          //Send the Producers List
          [EnableCors("CorsPolicy")]
          [Route("producers")]
          public IActionResult getProducerList()
          {
               return Ok(_context.Producers);
          }

          //Send the Producers by the Id or Error if not Present
          [EnableCors("CorsPolicy")]
          [HttpGet("producer/{id}", Name = "Producer_List")]
          public IActionResult GetProducer(int id) 
          { 
               try {
                    var producer = _context.Producers.Where(w => w.ProducerId == id).FirstOrDefault();
                    return Ok(producer);
               } catch(Exception){
                    PostRespone usererror = new PostRespone();
                    usererror.Status = 0;
                    usererror.Message = "Cannot Find the Actor";
                    return Ok(usererror);
               }
          }
          //Send the Casts List
          [EnableCors("CorsPolicy")]
          [Route("actors")]
          public IActionResult getActorsList()
          {
               return Ok(_context.Actors);
          }

          //Send the Cast by the Id or Error if not Present
          [EnableCors("CorsPolicy")]
          [HttpGet("actor/{id}", Name = "Actor_List")]
          public IActionResult getActor(int id) 
          { 
               try {
                    var actor = _context.Actors.Where(w => w.ActorId == id).FirstOrDefault();
                    return Ok(actor);
               } catch(Exception){
                    PostRespone usererror = new PostRespone();
                    usererror.Status = 0;
                    usererror.Message = "Cannot Find the Actor";
                    return Ok(usererror);
               }
          }
     }

     //Used to Send the Response
     public class PostRespone 
     {
          public int Status;
          public string Message;
     }

     //Used to Send Actor and Producer List
     public class PARespone 
     {
          public Object Actor;
          public Object Producer;
     }

     //Used to Check if the Image Exist in the URL or not
     public class ImageExist {
          public static bool doesImageExistRemotely(string uriToImage, string mimeType)
          {
               HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriToImage);
               request.Method = "HEAD";

               try
               {
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                    if (response.StatusCode == HttpStatusCode.OK && response.ContentType.Contains(mimeType))
                    {
                         return true;
                    }
                    else
                    {
                         return false;
                    }   
               }
               catch
               {
                    return false;
               }
          }
     }
}