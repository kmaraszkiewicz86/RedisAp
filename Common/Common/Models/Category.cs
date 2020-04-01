using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace Common.Models
{
    public class Category
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual IList<Movie> Movies { get; set; }
    }

    public class CategoryMap : ClassMapping<Category>
    {
        public CategoryMap()
        {
            Id(x => x.Id, m => m.Generator(Generators.Identity));
            Property(x => x.Name);
            Bag(c => c.Movies, mapper => mapper.Key(km => km.Column("CategoryId")));
        }
    }
}