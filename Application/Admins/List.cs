using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Admins
{
    public class List
    {
        public class Query : IRequest<List<Admin>>
        {
        }

        public class Handler : IRequestHandler<Query, List<Admin>>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }
            public async Task<List<Admin>> Handle(Query request, CancellationToken cancellationToken)
            {
                var Admins = await _context.Admins.ToListAsync();
                return Admins;
            }
        }
    }
}