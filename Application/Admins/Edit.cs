using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Admins
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Admin Admin { get; set; }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var Admin = await _context.Admins.FindAsync(request.Admin.Id);
                if (Admin == null)
                {
                    throw new Exception("No such Admin found.");
                }

                _mapper.Map(request.Admin, Admin);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                else throw new Exception("A problem occurred when updating an Admin");
            }
        }
    }
}