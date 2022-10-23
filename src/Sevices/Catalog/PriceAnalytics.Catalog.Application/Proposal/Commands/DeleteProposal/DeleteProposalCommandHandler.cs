using MediatR;
using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Catalog.Application.Common.Interfaces;
using PriceAnalytics.Catalog.Domain;
using PriceAnalytics.Catalog.Domain.Proposal.Events;
using PriceAnalytics.Administration.Domain.Proposal.Specification;

namespace PriceAnalytics.Catalog.Application.Proposal.Commands.DeleteProposal
{
    public class DeleteProposalCommandHandler : IRequestHandler<DeleteProposalCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public DeleteProposalCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> Handle(DeleteProposalCommand command, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Proposals.FirstOrDefaultAsync(new ProposalByIdSpecification(command.Id).ToExpression());

            if (entity == null)
            {
                throw new ProposalNotFoundException($"Proposal with id {command.Id} doesn't exist");
            }

            _applicationDbContext.Proposals.Remove(entity);

            entity.AddDomainEvent(new ProposalDeletedEvent(entity));

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
