using AutoMapper;
using PriceAnalytics.Catalog.Application.Product.Queries.GetProductById;
using PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById;
using PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById;
using PriceAnalytics.Catalog.Domain.Entities;
using PriceAnalytics.Catalog.Domain.Product;
using PriceAnalytics.Catalog.Domain.Proposal;
using PriceAnalytics.Catalog.Domain.Seller;

namespace PriceAnalytics.Catalog.Application.Common.Mappings
{

    public class MappingProfile : Profile
    {
        public static Action<IMapperConfigurationExpression> AutoMapperConfig =
            config =>
            {
                config.CreateMap<ProductEntity, ProductDto>().ReverseMap();
                config.CreateMap<SellerEntity, SellerDto>().ReverseMap();
                config.CreateMap<ProposalEntity, ProposalDto>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Price))
                .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Price.Currency));
                config.CreateMap<ProposalDto, ProposalEntity>()
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => new MoneyValue(src.Price, src.Currency)));
            };
    }
}