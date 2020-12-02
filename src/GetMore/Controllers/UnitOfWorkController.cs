using System;
using System.Net;
using System.Threading.Tasks;
using GetMore.Application.Commands;
using GetMore.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GetMore.Api.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class UnitOfWorkController : Controller
    {
        private readonly IMediator _mediator;

        public UnitOfWorkController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        /// <summary>
        /// Get all work done in a specific timerange
        /// </summary>
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(UnitsOfWorkForWeekDto), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get(DateTime startWorkDate, DateTime endWorkDate)
        {
            var unitsOfWork = await this._mediator.Send(new GetUnitsOfWorkForWeekQuery(startWorkDate, endWorkDate));

            return this.Ok(unitsOfWork);
        }

        /// <summary>
        /// Adds a unit of work
        /// </summary>
        [Route("")]
        [HttpPost]
        [ProducesResponseType(typeof(UnitsOfWorkForWeekDto), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> Add([FromBody] AddUnitOfWorkCommand command)
        {
            var validator = new AddUnitOfWorkValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return this.BadRequest(validationResult.Errors);
            }

            await this._mediator.Send(command);

            return this.StatusCode((int)HttpStatusCode.Created);
        }
    }
}