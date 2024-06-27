using eComWebApp.Data.Base;
using eComWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eComWebApp.Data.Services
{
    public class OrdersService : EntityBaseRepository<Order>, IOrdersService
    {
        public OrdersService(ApplicationDbContext context) : base(context) { }

    }
}
