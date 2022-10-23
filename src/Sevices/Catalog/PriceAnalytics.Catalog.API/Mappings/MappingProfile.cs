using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using PriceAnalytics.Catalog.Application.Common.Model;
using PriceAnalytics.Administration.Services.Product;
using PriceAnalytics.Administration.Services.Proposal;
using PriceAnalytics.Administration.Services.Seller;

namespace PriceAnalytics.Administration.Services
{
    public class MappingProfile : Profile
    {
        public static Action<IMapperConfigurationExpression> AutoMapperConfig =
            config =>
            {
                config.CreateMap<GetProductsQuery, PriceAnalytics.Catalog.Application.Product.Queries.GetProducts.GetProductsQuery>().ReverseMap();
                config.CreateMap<GetProductByIdQuery, PriceAnalytics.Catalog.Application.Product.Queries.GetProductById.GetProductByIdQuery>().ReverseMap();
                config.CreateMap<PriceAnalytics.Administration.Services.Product.Page, PriceAnalytics.Catalog.Application.Common.Model.Page>().ReverseMap();
                config.CreateMap<ProductPaginatedList, PaginatedList<PriceAnalytics.Catalog.Application.Product.Queries.GetProductById.ProductDto>>().ReverseMap();// ToDo
                config.CreateMap<CreateProductCommand, PriceAnalytics.Catalog.Application.Product.Commands.CreateProduct.CreateProductCommand>().ReverseMap();
                config.CreateMap<UpdateProductCommand, PriceAnalytics.Catalog.Application.Product.Commands.UpdateProduct.UpdateProductCommand>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                config.CreateMap<DeactivateProductCommand, PriceAnalytics.Catalog.Application.Product.Commands.DeactivateProduct.DeactivateProductCommand>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                config.CreateMap<PriceAnalytics.Catalog.Application.Product.Commands.UpdateProduct.UpdateProductCommand, UpdateProductCommand>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                config.CreateMap<DeleteProductCommand, PriceAnalytics.Catalog.Application.Product.Commands.DeleteProduct.DeleteProductCommand>().ReverseMap();
                config.CreateMap<PriceAnalytics.Catalog.Application.Product.Queries.GetProductById.ProductDto, ProductDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created , DateTimeKind.Utc).ToTimestamp()))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                config.CreateMap<ProductDto, PriceAnalytics.Catalog.Application.Product.Queries.GetProductById.ProductDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));

                config.CreateMap<CreateProposalCommand, PriceAnalytics.Catalog.Application.Proposal.Commands.CreateProposal.CreateProposalCommand>().ReverseMap();
                config.CreateMap<ProposalDto, PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById.ProposalDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                config.CreateMap<PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById.ProposalDto, ProposalDto>()
               .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
               .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                config.CreateMap<GetProposalsQuery, PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposals.GetProposalsQuery>().ReverseMap();
                config.CreateMap<GetProposalByIdQuery, PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById.GetProposalByIdQuery>().ReverseMap();
                config.CreateMap<PriceAnalytics.Administration.Services.Proposal.Page, PriceAnalytics.Catalog.Application.Common.Model.Page>();
                config.CreateMap<DeleteProposalCommand, PriceAnalytics.Catalog.Application.Proposal.Commands.DeleteProposal.DeleteProposalCommand>().ReverseMap();
                config.CreateMap<ProposalPaginatedList, PaginatedList<PriceAnalytics.Catalog.Application.Proposal.Queries.GetProposalById.ProposalDto>>().ReverseMap();// ToDo
                config.CreateMap<PriceAnalytics.Administration.Services.Proposal.Page, PriceAnalytics.Catalog.Application.Common.Model.Page>().ReverseMap();

                config.CreateMap<GetSellersQuery, PriceAnalytics.Catalog.Application.Seller.Queries.GetSellers.GetSellersQuery>().ReverseMap();
                config.CreateMap<GetSellerByIdQuery, PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById.GetSellerByIdQuery>().ReverseMap();
                config.CreateMap<PriceAnalytics.Administration.Services.Seller.Page, PriceAnalytics.Catalog.Application.Common.Model.Page>();
                config.CreateMap<SellerPaginatedList, PaginatedList<PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById.SellerDto>>().ReverseMap();// ToDo
                config.CreateMap<CreateSellerCommand, PriceAnalytics.Catalog.Application.Product.Commands.CreateSeller.CreateSellerCommand>().ReverseMap();
                config.CreateMap<UpdateSellerCommand, PriceAnalytics.Catalog.Application.Seller.Commands.UpdateSeller.UpdateSellerCommand>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                config.CreateMap<DeactivateSellerCommand, PriceAnalytics.Catalog.Application.Seller.Commands.DeactivateSeller.DeactivateSellerCommand>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                config.CreateMap<PriceAnalytics.Catalog.Application.Seller.Commands.UpdateSeller.UpdateSellerCommand, UpdateSellerCommand>()
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                config.CreateMap<DeleteSellerCommand, PriceAnalytics.Catalog.Application.Seller.Commands.DeleteSeller.DeleteSellerCommand>().ReverseMap();
                config.CreateMap<PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById.SellerDto, SellerDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Created, DateTimeKind.Utc).ToTimestamp()))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.LastModified, DateTimeKind.Utc).ToTimestamp()));
                config.CreateMap<SellerDto, PriceAnalytics.Catalog.Application.Seller.Queries.GetSellerById.SellerDto>()
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.Created.ToDateTime()))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => src.LastModified.ToDateTime()));
                 config.CreateMap<PriceAnalytics.Administration.Services.Seller.Page, PriceAnalytics.Catalog.Application.Common.Model.Page>().ReverseMap();
            };
    }
}
