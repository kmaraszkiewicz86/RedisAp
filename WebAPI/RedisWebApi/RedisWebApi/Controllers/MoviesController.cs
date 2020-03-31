using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Features.OwnedInstances;
using Common;
using Common.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using NHibernate.Linq;

namespace RedisWebApi.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/Movies")]
    public class MoviesController : ControllerBase
    {
        private readonly Antlr.Runtime.Misc.Func<Owned<ISession>> _sessionFactory;

        public MoviesController(Antlr.Runtime.Misc.Func<Owned<ISession>> sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        [HttpPost("All")]
        public ActionResult Get()
        {
            using (var session = _sessionFactory())
            {

                IList<Category> categoryAlias = null;

                var movies = session.Value.QueryOver<Movie>()
                    .Left.JoinAlias(x => x.Category, () => categoryAlias);

                return Ok(movies.List().ToArray());
            }
        }

        [HttpPost("GetDetails/{id:int}")]
        public ActionResult GetDetails(int id)
        {
            using (var session = _sessionFactory())
            {
                IList<Category> categoryAlias = null;

                var movies = session.Value.QueryOver<Movie>()
                    .Left.JoinAlias(x => x.Category, () => categoryAlias)
                    .Where(x => x.Id == id)
                    .OrderBy(m => m.Id)
                    .Asc
                    .Take(1)
                    .List()
                    .FirstOrDefault();

                return Ok(movies);
            }
        }

        [HttpPost]
        public void Save([FromBody] Movie movie)
        {
            RedisManager.OnWork((cacheClient, db) =>
            {
                cacheClient.AddAsync(movie.Name, movie, DateTimeOffset.Now.AddMinutes(10)).GetAwaiter().GetResult();
            }, () => throw new Exception("Connection for redis database failed. Try again later..."));
        }
    }
}
