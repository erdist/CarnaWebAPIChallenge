using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Admins
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
                var Admin = await _context.Admins.Include(a => a.Users).Include(a => a.Contents).FirstOrDefaultAsync(a => a.Id == request.Id);

                if (Admin == null)
                {
                    throw new Exception("No such Admin found.");
                }
                _context.Remove(Admin);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                else throw new Exception("A problem has occurred when deleting an Admin.");
            }
        }
    }
}