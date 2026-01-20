using Data;
using Logic.Interfaces.Commands;
using MediatR;

namespace Logic.Interfaces.Handlers
{
    public class AddReviewCommandHandler : IRequestHandler<AddReviewCommand, int>
    {
        private readonly IProductsDbContext _dbContext;

        public AddReviewCommandHandler(IProductsDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<int> Handle(AddReviewCommand request, CancellationToken cancellationToken)
        {
            var review = request.Review;
            await _dbContext.Reviews.AddAsync(review, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            var id = review.ReviewId;
            return id;
        }
    }
}
