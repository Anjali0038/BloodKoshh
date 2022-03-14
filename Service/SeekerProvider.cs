using AutoMapper;
using BloodKoshh.Models;
using BloodKoshh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKosh.Service
{
    public interface ISeekerProvider
    {
        int SaveSeeker(SeekerViewModel model);
        void DeleteSeeker(int id);
        SeekerViewModel GetList();
        SeekerViewModel GetById(int id);
    }
    public class SeekerProvider : ISeekerProvider
    {
        private readonly ISeekerRepository _iSeekerRepository;
        private readonly IMapper _mapper;

        public SeekerProvider(ISeekerRepository iSeekerRepository, IMapper mapper)
        {
            _iSeekerRepository = iSeekerRepository;
            _mapper = mapper;
        }
        public int SaveSeeker(SeekerViewModel model)
        {
            Seeker seeker = new Seeker();
            seeker = _mapper.Map<Seeker>(model);
            if (seeker.Seeker_Id > 0)
            {
                _iSeekerRepository.Update(seeker);
                return 200;
            }
            else
            {
                _iSeekerRepository.Add(seeker);
                return 200;
            }
        }
        public void DeleteSeeker(int id)
        {
            var item = _iSeekerRepository.GetSingle(x => x.Seeker_Id == id);
            _iSeekerRepository.Delete(item);
        }
        public SeekerViewModel GetById(int id)
        {
            var item = _iSeekerRepository.GetSingle(x => x.Seeker_Id == id);
            SeekerViewModel data = _mapper.Map<SeekerViewModel>(item);
            return data;
        }
        public SeekerViewModel GetList()
        {
            SeekerViewModel model = new SeekerViewModel();
            var list = new List<SeekerViewModel>();
            List<Seeker> data = _iSeekerRepository.GetAll().ToList();
            list = _mapper.Map<List<Seeker>, List<SeekerViewModel>>(data);
            model.SeekerList = list;
            return model;
        }
    }
}
