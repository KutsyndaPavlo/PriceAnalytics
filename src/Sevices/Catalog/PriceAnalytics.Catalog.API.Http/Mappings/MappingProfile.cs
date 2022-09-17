using AutoMapper;
using PriceAnalytics.Catalog.API.Models;
using PriceAnalytics.Catalog.Application.Product.Commands.CreateProduct;
using PriceAnalytics.Catalog.Application.Product.Commands.CreateSeller;
using PriceAnalytics.Catalog.Application.Product.Commands.UpdateProduct;
using PriceAnalytics.Catalog.Application.Proposal.Commands.CreateProposal;
using PriceAnalytics.Catalog.Application.Seller.Commands.UpdateSeller;

namespace PriceAnalytics.Catalog.API.Mappings
{
    public class MappingProfile : Profile
    {
        public static Action<IMapperConfigurationExpression> AutoMapperConfig =
            config =>
            {
                config.CreateMap<CreateProductRequest, CreateProductCommand>().ReverseMap();
                config.CreateMap<UpdateProductRequest, UpdateProductCommand>().ReverseMap();
                config.CreateMap<CreateSellerRequest, CreateSellerCommand>().ReverseMap();
                config.CreateMap<UpdateSellerRequest, UpdateSellerCommand>().ReverseMap();
                config.CreateMap<CreateProposalRequest, CreateProposalCommand>().ReverseMap();
            };
    }
}
