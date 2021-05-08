using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Admins
{
    public class Create
    {
        public class Command : IRequest
        {
           public Admin Admin { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {

                _context.Admins.Add(request.Admin);

                var success = await _context.SaveChangesAsync() > 0;

                if(success) return Unit.Value;
                else throw new Exception("A problem occurred when creating an Admin");
            }
        }
    }
}