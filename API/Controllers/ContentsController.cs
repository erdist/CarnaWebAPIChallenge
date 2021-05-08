using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;
using Application.Contents;
using System;

namespace API.Controllers
{

    public class ContentsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Content>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Content>> Details(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Content Content)
        {
            return Ok(await Mediator.Send(new Create.Command { Content = Content }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, Content Content)
        {
            Content.Id = id;
            return Ok(await Mediator.Send(new Edit.Command { Content = Content }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}