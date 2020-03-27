using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Results;
using Antlr.Runtime.Misc;
using Autofac.Features.OwnedInstances;
using Common.Models;
using NHibernate;

namespace RedisWebApi.Controllers
{
    public class MoviesController : ApiController
    {
        private readonly Func<Owned<ISession>> _sessionFactory;

        public MoviesController(Func<Owned<ISession>> sessionFactory)
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

        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Movies/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Movies/5
        public void Delete(int id)
        {
        }
    }
}
