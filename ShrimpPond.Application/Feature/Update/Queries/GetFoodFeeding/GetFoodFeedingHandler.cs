using AutoMapper;
using MediatR;
using ShrimpPond.Application.Contract.Logging;
using ShrimpPond.Application.Contract.Persistence.Genenric;
using ShrimpPond.Application.Exceptions;
using ShrimpPond.Application.Feature.Feeding.Commands.Feeding;
using ShrimpPond.Application.Feature.Update.Queries.GetSizeUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShrimpPond.Application.Feature.Update.Queries.GetFoodFeeding
{
    public class GetFoodFeedingHandler : IRequestHandler<GetFoodFeeding, List<GetFoodFeedingDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAppLogger<GetFoodFeeding> _logger;

        public GetFoodFeedingHandler(IUnitOfWork unitOfWork, IAppLogger<GetFoodFeeding> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<List<GetFoodFeedingDTO>> Handle(GetFoodFeeding request, CancellationToken cancellationToken)
        {
            //query
            var foodFeedings = _unitOfWork.foodFeedingRepository.FindByCondition(f => f.PondId == request.pondId );

            List<GetFoodFeedingDTO> getFoodFeedingDTOs = new List<GetFoodFeedingDTO>();
            List<Foods> foods = new List<Foods>();
            foreach(var foodFeeding in foodFeedings)
            {
                var foodforFeedings = _unitOfWork.foodForFeedingRepository.FindAll().Where(f => f.FoodFeedingId == foodFeeding.FoodFeedingId).ToList();
                foreach(var foodforFeeding in foodforFeedings)
                {
                    var food = new Foods()
                    {
                        name = foodforFeeding.Name,
                        amount = foodforFeeding.Amount,
                    };
                    foods.Add(food);
                }

                var getFoodFeedingDTO = new GetFoodFeedingDTO()
                {
                    PondId = request.pondId,
                    FeedingDate = foodFeeding.FeedingDate,
                    Foods = foods
                };
                getFoodFeedingDTOs.Add(getFoodFeedingDTO);
                foods = new();
            }

            //logging
            _logger.LogInformation("Get sizeShrimps successfully");
            // convert
           
            return getFoodFeedingDTOs;
        }
    }
}
