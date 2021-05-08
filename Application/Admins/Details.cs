using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Admins
{
    public class Details
    {
        public class Query : IRequest<Admin>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Admin>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Admin> Handle(Query request, CancellationToken cancellationToken)
            {
                var admin = await _context.Admins.Include(a => a.Users).Include(a => a.Contents).FirstOrDefaultAsync(a => a.Id == request.Id);

                return admin;
            }
        }
    }
}