using AutoMapper;
using BloodKoshh.Models;
using BloodKoshh.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKosh.Service
{
    public interface IEventProvider
    {
        int SaveEvent(EventViewModel model);
        void DeleteEvent(int id);
        EventViewModel GetList();
        EventViewModel GetById(int id);
    }
    public class EventProvider : IEventProvider
    {
        private readonly IEventRepository _iEventRepository;
        private readonly IMapper _mapper;

        public EventProvider(IEventRepository iEventRepository, IMapper mapper)
        {
            _iEventRepository = iEventRepository;
            _mapper = mapper;
        }
        public int SaveEvent(EventViewModel model)
        {
            Event data = new Event();
            data = _mapper.Map<Event>(model);
            if (data.EventId > 0)
            {
                _iEventRepository.Update(data);
                return 200;
            }
            else
            {
                _iEventRepository.Add(data);
                return 200;
            }
        }
        public void DeleteEvent(int id)
        {
            var item = _iEventRepository.GetSingle(x => x.EventId == id);
            _iEventRepository.Delete(item);
        }
        public EventViewModel GetById(int id)
        {
            var item = _iEventRepository.GetSingle(x => x.EventId == id);
            EventViewModel data = _mapper.Map<EventViewModel>(item);
            return data;
        }
        public EventViewModel GetList()
        {
            EventViewModel model = new EventViewModel();
            var list = new List<EventViewModel>();
            List<Event> data = _iEventRepository.GetAll().ToList();
            list = _mapper.Map<List<Event>, List<EventViewModel>>(data);
            model.EventList = list;
            return model;
        }
    }
}
