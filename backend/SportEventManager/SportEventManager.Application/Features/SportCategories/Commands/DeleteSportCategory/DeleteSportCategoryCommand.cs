using MediatR;

namespace SportEventManager.Application.Features.SportCategories.Commands.DeleteSportCategory
{
    public class DeleteSportCategoryCommand : IRequest
    {
        public int Id { get; set; }
        public DeleteSportCategoryCommand(int id)
        {
            Id = id;
        }
    }
}
