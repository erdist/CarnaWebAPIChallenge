using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Authorization
{
    public class GetAdmin
    {
        public class Query : IRequest<AuthAdmin>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, AuthAdmin>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<AuthAdmin> Handle(Query request, CancellationToken cancellationToken)
            {
                var admin = await _context.AuthAdmins.Include(a => a.Admin).FirstOrDefaultAsync(a => a.Id == request.Id);

                return admin;
            }
        }
    }
}