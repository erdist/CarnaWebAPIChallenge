using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Users
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
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
                var User = await _context.Users.FindAsync(request.Id);

                if (User == null)
                {
                    throw new Exception("No such User found.");
                }
                _context.Remove(User);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                else throw new Exception("A problem has occurred when deleting a User.");
            }
        }
    }
}