﻿using DataAccessLayer.Abstract;
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
    public class EfDestinationDal : GenericRepository<Destination>, IDestinationDal
    {
        public Destination GetDestinationWithGuide(int id)
        {
            using (var c = new Context())
            {
                //Include metodu view tarafında farklı tablodan veri getirmemizi sağlıyor. Örn:Guide table.
                return c.Destinations.Where(x => x.DestinationId == id).Include(c => c.Guide).FirstOrDefault();
            }
        }
    }
}
