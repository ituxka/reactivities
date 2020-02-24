using System;
using System.Threading.Tasks;
using Application.Activities.Commands.CreateActivity;
using Application.Activities.Commands.DeleteActivity;
using Application.Activities.Commands.EditActivity;
using Application.Activities.Queries.GetActivities;
using Application.Activities.Queries.GetActivity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<ActivitiesVm>> List()
        {
            return await Mediator.Send(new GetActivitiesQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ActivityVm>> Details(Guid id)
        {
            return await Mediator.Send(new GetActivityQuery {Id = id});
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(CreateActivityCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Unit>> Edit([FromRoute] Guid id, EditActivityCommand command)
        {
            command.Id = id;
            return await Mediator.Send(command);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Unit>> Delete(Guid id)
        {
            return await Mediator.Send(new DeleteActivityCommand {Id = id});
        }
    }
}
