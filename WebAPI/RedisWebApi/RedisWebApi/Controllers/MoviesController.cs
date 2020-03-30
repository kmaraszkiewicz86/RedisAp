using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Features.OwnedInstances;
using Common;
using Common.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using NHibernate;

namespace RedisWebApi.Controllers
{
    [EnableCors]
    public class MoviesController : ControllerBase
    {
        private readonly Antlr.Runtime.Misc.Func<Owned<ISession>> _sessionFactory;

        public MoviesController(Antlr.Runtime.Misc.Func<Owned<ISession>> sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        [HttpGet]
        public ActionResult<Movie[]> Get()
        {
            using (var session = _sessionFactory())
            {

                IList<Category> categoryAlias = null;

                var movies = session.Value.QueryOver<Movie>()
                    .Left.JoinAlias(x => x.Category, () => categoryAlias);

                return Ok(movies.List().ToArray());
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
