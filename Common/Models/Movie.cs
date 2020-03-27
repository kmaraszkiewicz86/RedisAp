using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Common.Models
{
    public class Movie
    {
        public virtual int Id { get; protected set; }
        public virtual string Name { get; set; }
        public virtual string Describe { get; set; }

        public virtual Category Category { get; set; }
    }

    public class MovieMap : ClassMapping<Movie>
    {
        public MovieMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Name);
            Property(x => x.Describe);


            ManyToOne(x => x.Category, map =>
            {
                map.Column("CategoryId");
            });
        }
    }
}