using BloodKoshh.Data;
using BloodKoshh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Repository
{
    public interface IBloodBankRepository:IRepository<BloodBank>
    {
    }
    public class BloodBankRepository : Repository<BloodBank>, IBloodBankRepository
    {
        public BloodBankRepository(BloodKoshhContext context) : base(context)
        {
        }
    }
}
