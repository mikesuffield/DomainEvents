using AutoMapper;
using DomainEvents.Application.Commands;
using DomainEvents.Application.Events;

namespace DomainEvents.Application.Profiles
{
    public class ItemPurchasedEventProfile : Profile
    {
        public ItemPurchasedEventProfile()
        {
            CreateMap<MakePurchaseCommand, ItemPurchasedEvent>();
        }
    }
}
