using AutoMapper;
using BloodKoshh.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodKoshh.Models
{
    public class MapProfile : Profile
    {
        public MapProfile()
        {
            CreateMap<Donor, DonorViewModel>();
            CreateMap<DonorViewModel, Donor>();
            CreateMap<Seeker, SeekerViewModel>();
            CreateMap<SeekerViewModel, Seeker>();
            CreateMap<Event, EventViewModel>();
            CreateMap<EventViewModel, Event>();
            CreateMap<BloodBank, BloodBankViewModel>();
            CreateMap<BloodBankViewModel, BloodBank>();
        }
    }
}
