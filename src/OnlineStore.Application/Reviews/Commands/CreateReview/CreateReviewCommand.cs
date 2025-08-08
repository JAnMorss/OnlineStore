using OnlineStoreAPI.Shared.Kernel.Application.Command;

namespace OnlineStore.Application.Reviews.Commands.CreateReview
{
    public sealed record CreateReviewCommand(
        Guid ProductId,
        Guid UserId,
        int Rating,
        string Comment) : ICommand<Guid>;
}
