using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Users
{
    public class Edit
    {
        public class Command : IRequest
        {
            public User User { get; set; }
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
                var User = await _context.Users.FindAsync(request.User.Id);
                if (User == null)
                {
                    throw new Exception("No such User found.");
                }

                _mapper.Map(request.User, User);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                else throw new Exception("A problem occurred when updating an User");
            }
        }
    }
}