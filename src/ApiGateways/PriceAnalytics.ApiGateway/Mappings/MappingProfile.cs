using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using PriceAnalytics.ApiGateway.Models;
using PriceAnalytics.ApiGateway.Product;
using PriceAnalytics.ApiGateway.Proposal;
using PriceAnalytics.ApiGateway.Seller;
using ProductDto = PriceAnalytics.ApiGateway.Product.ProductDto;

namespace PriceAnalytics.ApiGateway.Mappings
{
    public class MappingProfile : Profile
    {
        public static Action<IMapperConfigurationExpression> AutoMapperConfig =
            config =>
            {
                config.CreateMap<CreateProductRequest, CreateProductCommand>();
                config.CreateMap<UpdateProductRequest, UpdateProductCommand>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                config.CreateMap<DeactivateProductRequest, DeactivateProductCommand>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                config.CreateMap<Models.ProductDto, ProductDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                config.CreateMap<ProductDto, Models.ProductDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                config.CreateMap<ProductPaginatedList, Models.PaginatedList<Models.ProductDto>>().ReverseMap();
                config.CreateMap<Product.Page, Models.Page>().ReverseMap();

                config.CreateMap<CreateProposalRequest, CreateProposalCommand>().ReverseMap();
                config.CreateMap<Models.ProposalDto, Proposal.ProposalDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                config.CreateMap<Proposal.ProposalDto, Models.ProposalDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                 config.CreateMap<ProposalPaginatedList, Models.PaginatedList<Models.ProposalDto>>().ReverseMap();
                config.CreateMap<Proposal.Page, Models.Page>().ReverseMap();

                config.CreateMap<CreateSellerRequest, CreateSellerCommand>();
                config.CreateMap<UpdateSellerRequest, UpdateSellerCommand>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                config.CreateMap<DeactivateSellerRequest, DeactivateSellerCommand>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                config.CreateMap<Models.SellerDto, Seller.SellerDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                config.CreateMap<Seller.SellerDto, Models.SellerDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                config.CreateMap<SellerPaginatedList, Models.PaginatedList<Models.SellerDto>>().ReverseMap();
                config.CreateMap<Seller.Page, Models.Page>().ReverseMap();

            };
    }
}
