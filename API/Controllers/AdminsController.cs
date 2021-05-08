using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Domain;
using Application.Admins;
using System;

namespace API.Controllers
{

    public class AdminsController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Admin>>> List()
        {
            return await Mediator.Send(new List.Query());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> Details(Guid id)
        {
            return await Mediator.Send(new Details.Query { Id = id });
        }

        [HttpPost]
        public async Task<ActionResult<Unit>> Create(Admin Admin)
        {
            return Ok(await Mediator.Send(new Create.Command { Admin = Admin }));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(Guid id, Admin Admin)
        {
            Admin.Id = id;
            return Ok(await Mediator.Send(new Edit.Command { Admin = Admin }));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return Ok(await Mediator.Send(new Delete.Command { Id = id }));
        }
    }
}