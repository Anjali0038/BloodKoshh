using AutoMapper;
using BloodKoshh.Data;
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
        int Edit(DonorViewModel model);
        DonorViewModel GetApprovedDonors();
        DonorViewModel GetFrequentDonors();


    }
    public class DonorProvider : IDonorProvider
    {
        private readonly IDonorRepository _iDonorRepository;
        private readonly IMapper _mapper;
        private BloodKoshhContext _context;

        public DonorProvider(IDonorRepository iDonorRepository, IMapper mapper, BloodKoshhContext context)
        {
            _iDonorRepository = iDonorRepository;
            _mapper = mapper;
            _context = context;
        }
        public int SaveDonor(DonorViewModel model)
        {
            string userid = model.UserId;
            Donor donor = new Donor();
            donor = _mapper.Map<Donor>(model);
            var data1 = _context.Donors.Where(x => x.UserId == userid).Count();

            if (donor.Donor_id > 0|| data1>=1)
            {
                _iDonorRepository.Update(donor);
                return 200;
            }
            else
            {
                var data = _context.Users.Where(x => x.Id == userid).First();
                //var data1 = _context.DonorLocation.Where(x => x.LocationId == model.LocationId).First();
                donor.UserId = data.Id;
                //donor.LocationId = data1.LocationId;
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
        public int Edit(DonorViewModel model)
        {
            Donor donor = new Donor();
            donor = _mapper.Map<Donor>(model);
            _iDonorRepository.Update(donor);
            return 200;
        }
        public DonorViewModel GetApprovedDonors()
        {
            
            DonorViewModel model = new DonorViewModel();
            var list = new List<DonorViewModel>();
            List<Donor> data = _iDonorRepository.GetAll().ToList();
            foreach (var item in data)
            {
                if (item.ApprovedStatus == true)
                {
                    list = _mapper.Map<List<Donor>, List<DonorViewModel>>(data);
                    model.DonorList = list;
                }
            }
            return model;
        }
        public DonorViewModel GetFrequentDonors()
        {

            DonorViewModel model = new DonorViewModel();
            var list = new List<DonorViewModel>();
            List<Donor> data = _iDonorRepository.GetAll().ToList();
           var ordered =  data.OrderByDescending(x=>x.Count).ToList();
            foreach (var item in ordered)
            {
                    list = _mapper.Map<List<Donor>, List<DonorViewModel>>(ordered);
                    model.DonorList = list;
            }
            return model;
        }

    }

}
