using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Reviews.Commands.DeleteReview
{
    public sealed record DeleteReviewCommand(Guid ReviewId) : ICommand<Guid>;
}
