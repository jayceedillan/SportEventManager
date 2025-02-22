using MediatR;
using SportEventManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportEventManager.Application.Features.SportCategories.Queries.GetSportCategoryById
{
    public class GetSportCategoryByIdQuery : IRequest<SportCategory>
    {
        public int Id { get; set; }
    }
}
