using OnlineStoreAPI.Shared.Kernel.Application.Command;

namespace OnlineStore.Application.Reviews.Commands.DeleteReview
{
    public sealed record DeleteReviewCommand(Guid ReviewId) : ICommand<Guid>;
}
