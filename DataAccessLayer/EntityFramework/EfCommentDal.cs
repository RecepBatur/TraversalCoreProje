using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfCommentDal : GenericRepository<Comment>, ICommentDal
    {
        public List<Comment> GetListCommentsWithDestination()
        {
            using (var c = new Context())
            {
                //Include metodu view tarafında farklı tablodan veri getirmemizi sağlıyor. Örn:Destination table.
                return c.Comments.Include(c => c.Destination).ToList();
            }
        }
    }
}
