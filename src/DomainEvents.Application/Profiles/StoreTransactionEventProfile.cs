using AutoMapper;
using DomainEvents.Application.Commands;
using DomainEvents.Application.Events;

namespace DomainEvents.Application.Profiles
{
    public class StoreTransactionEventProfile : Profile
    {
        public StoreTransactionEventProfile()
        {
            CreateMap<MakePurchaseCommand, StoreTransactionEvent>();
        }
    }
}
