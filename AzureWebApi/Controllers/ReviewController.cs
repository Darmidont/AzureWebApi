using AzureWebApi.Models;
using Data.Entities;
using Logic.Interfaces.Commands;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AzureWebApi.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class ReviewController : ControllerBase
    {
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public ReviewController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpGet($"test")]
    public int Test()
    {
        return 777;
    }

    [HttpPost("add")]
    public async Task AddReview(ReviewModel reviewModel)
    {
         var review = _mapper.Map<Review>(reviewModel);
         await _mediator.Send(new AddReviewCommand(review));
    }
}
