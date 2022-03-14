using BloodKoshh.Areas.Identity.Data;
using BloodKoshh.Data;
using BloodKoshh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Repository
{
    public interface IDonorRepository: IRepository<Donor>
    {
    }
    public class DonorRepository: Repository<Donor>, IDonorRepository
    {
        public DonorRepository(BloodKoshhContext context) : base(context)
        {
        }
    }
}
