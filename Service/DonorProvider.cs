using AutoMapper;
using BloodKoshh.Models;
using BloodKoshh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKosh.Service
{
    public interface IDonorProvider
    {
        int SaveDonor(DonorViewModel model);
        void DeleteDonor(int id);
        DonorViewModel GetList();
        DonorViewModel GetById(int id);
    }
    public class DonorProvider : IDonorProvider
    {
        private readonly IDonorRepository _iDonorRepository;
        private readonly IMapper _mapper;

        public DonorProvider(IDonorRepository iDonorRepository, IMapper mapper)
        {
            _iDonorRepository = iDonorRepository;
            _mapper = mapper;
        }
        public int SaveDonor(DonorViewModel model)
        {
            Donor donor = new Donor();
            donor = _mapper.Map<Donor>(model);
            if (donor.Donor_id > 0)
            {
                _iDonorRepository.Update(donor);
                return 200;
            }
            else
            {
                _iDonorRepository.Add(donor);
                return 200;
            }
        }
        public void DeleteDonor(int id)
        {
            var item = _iDonorRepository.GetSingle(x => x.Donor_id == id);
            _iDonorRepository.Delete(item);
        }
        public DonorViewModel GetById(int id)
        {
            var item = _iDonorRepository.GetSingle(x => x.Donor_id == id);
            DonorViewModel data = _mapper.Map<DonorViewModel>(item);
            return data;
        }
        public DonorViewModel GetList()
        {
            DonorViewModel model = new DonorViewModel();
            var list = new List<DonorViewModel>();
            List<Donor> data = _iDonorRepository.GetAll().ToList();
            list = _mapper.Map<List<Donor>, List<DonorViewModel>>(data);
            model.DonorList = list;
            return model;
        }
    }

}
