using AutoMapper;
using DomainEvents.Api.ViewModels;
using DomainEvents.Application.Commands;

namespace DomainEvents.Api.Profiles
{
    public class MakePurchaseCommandProfile : Profile
    {
        public MakePurchaseCommandProfile()
        {
            CreateMap<PurchaseViewModel, MakePurchaseCommand>();
        }
    }
}
