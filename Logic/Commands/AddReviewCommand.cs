using Data.Entities;
using MediatR;

namespace Logic.Interfaces.Commands
{
    public class AddReviewCommand : IRequest<int>
    {
        public AddReviewCommand(Review review)
        {
            Review = review;
        }

        public Review Review { get; set; }
    }
}
