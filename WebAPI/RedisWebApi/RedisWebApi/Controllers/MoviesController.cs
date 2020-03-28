using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;
using Autofac.Features.OwnedInstances;
using Common;
using Common.Models;
using NHibernate;

namespace RedisWebApi.Controllers
{
    [EnableCors("*", "*", "*")]
    public class MoviesController : ApiController
    {
        private readonly Antlr.Runtime.Misc.Func<Owned<ISession>> _sessionFactory;

        public MoviesController(Antlr.Runtime.Misc.Func<Owned<ISession>> sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        // GET: api/Movies
        public JsonResult<Movie[]> Get()
        {
            using (var session = _sessionFactory())
            {

                IList<Category> categoryAlias = null;

                var movies = session.Value.QueryOver<Movie>()
                    .Left.JoinAlias(x => x.Category, () => categoryAlias);

                return Json(movies.List().ToArray());
            }
        }

        public void Post([FromBody] Movie movie)
        {
            RedisManager.OnWork((cacheClient, db) =>
            {
                cacheClient.Add(movie.Name, movie, DateTimeOffset.Now.AddMinutes(10));
            });
        }
    }
}
