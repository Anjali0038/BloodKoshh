using BloodKoshh.Data;
using BloodKoshh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Repository
{
    public interface ISeekerRepository : IRepository<Seeker>
    {
    }
    public class SeekerRepository : Repository<Seeker>, ISeekerRepository
    {
        public SeekerRepository(BloodKoshhContext context) : base(context)
        {
        }
    }
}
