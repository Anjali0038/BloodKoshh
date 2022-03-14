using BloodKoshh.Data;
using BloodKoshh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Repository
{
    public interface IEventRepository : IRepository<Event>
    {
    }
    public class EventRepository : Repository<Event>, IEventRepository
    {
        public EventRepository(BloodKoshhContext context) : base(context)
        {
        }
    }
}
