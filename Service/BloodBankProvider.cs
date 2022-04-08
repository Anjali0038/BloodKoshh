using AutoMapper;
using BloodKoshh.Models;
using BloodKoshh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKosh.Service
{
    public interface IBloodBankProvider
    {
        int SaveBloodBank(BloodBankViewModel model);
        void DeleteBloodBank(int id);
        BloodBankViewModel GetList();
        BloodBankViewModel GetById(int id);
        BloodBankViewModel GetApprovedBanks();
        int Edit(BloodBankViewModel model);
    }
    public class BloodBankProvider : IBloodBankProvider
    {
        private readonly IBloodBankRepository _iBloodBankRepository;
        private readonly IMapper _mapper;

        public BloodBankProvider(IBloodBankRepository iBloodBankRepository, IMapper mapper)
        {
            _iBloodBankRepository = iBloodBankRepository;
            _mapper = mapper;
        }
        public int SaveBloodBank(BloodBankViewModel model)
        {
            BloodBank bank = new BloodBank();
            bank = _mapper.Map<BloodBank>(model);
            if (bank.Id > 0)
            {
                _iBloodBankRepository.Update(bank);
                return 200;
            }
            else
            {
                _iBloodBankRepository.Add(bank);
                return 200;
            }
        }
        public void DeleteBloodBank(int id)
        {
            var item = _iBloodBankRepository.GetSingle(x => x.Id == id);
            _iBloodBankRepository.Delete(item);
        }
        public BloodBankViewModel GetById(int id)
        {
            var item = _iBloodBankRepository.GetSingle(x => x.Id == id);
            BloodBankViewModel data = _mapper.Map<BloodBankViewModel>(item);
            return data;
        }
        public BloodBankViewModel GetList()
        {
            BloodBankViewModel model = new BloodBankViewModel();
            var list = new List<BloodBankViewModel>();
            List<BloodBank> data = _iBloodBankRepository.GetAll().ToList();
            list = _mapper.Map<List<BloodBank>, List<BloodBankViewModel>>(data);
            model.BankList = list;
            return model;
        }
        public BloodBankViewModel GetApprovedBanks()
        {

            BloodBankViewModel model = new BloodBankViewModel();
            var list = new List<BloodBankViewModel>();
            List<BloodBank> data = _iBloodBankRepository.GetAll().ToList();
            foreach (var item in data)
            {
                if (item.ApprovedStatus == true)
                {
                    list = _mapper.Map<List<BloodBank>, List<BloodBankViewModel>>(data);
                    model.BankList = list;
                }
            }
            return model;
        }
        public int Edit(BloodBankViewModel model)
        {
            BloodBank bank = new BloodBank();
            bank = _mapper.Map<BloodBank>(model);
            _iBloodBankRepository.Update(bank);
            return 200;
        }
    }
}
