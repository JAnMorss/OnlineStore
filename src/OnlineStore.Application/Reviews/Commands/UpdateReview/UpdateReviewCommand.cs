using OnlineStore.Application.Abstractions.Messaging;

namespace OnlineStore.Application.Reviews.Commands.UpdateReview
{
    public sealed record UpdateReviewCommand(
        Guid ReviewId,
        int Rating,
        string Comment) : ICommand;
}
