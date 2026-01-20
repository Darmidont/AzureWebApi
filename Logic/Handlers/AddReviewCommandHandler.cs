using Data;
using Logic.Interfaces.Commands;
using Logic.Interfaces.Interfaces;
using Logic.Interfaces.Messages;
using Logic.Services;
using MediatR;
using System.Text.Json;

namespace Logic.Interfaces.Handlers
{
    public class AddReviewCommandHandler : IRequestHandler<AddReviewCommand, int>
    {
        private readonly IProductsDbContext _dbContext;
        private readonly IAzureTopicService _topicService;

        public AddReviewCommandHandler(IProductsDbContext dbContext, IAzureTopicService topicService)
        {
            _dbContext = dbContext;
            _topicService = topicService;
        }


        public async Task<int> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var review = request.Review;
            await _dbContext.Reviews.AddAsync(review, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            var id = review.ReviewId;
            var reviewAdded = new ReviewAdded
            {
                ReviewId = id,
                ProductId = review.ProductId,
            };

            string jsonBody = JsonSerializer.Serialize(reviewAdded);
            await _topicService.SendMessageAsync(jsonBody, AzureServiceBusConstants.TopicName, AzureServiceBusConstants.ReviewAddedSubscription, cancellationToken);
            return id;
        }
    }
}
