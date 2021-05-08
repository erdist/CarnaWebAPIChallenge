using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Contents
{
    public class Details
    {
        public class Query : IRequest<Content>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Content>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Content> Handle(Query request, CancellationToken cancellationToken)
            {
                var Content = await _context.Contents.FindAsync(request.Id);

                return Content;
            }
        }
    }
}