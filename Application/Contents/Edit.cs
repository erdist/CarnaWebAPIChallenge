using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Contents
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Content Content { get; set; }
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
                var Content = await _context.Contents.FindAsync(request.Content.Id);
                if (Content == null)
                {
                    throw new Exception("No such Content found.");
                }

                _mapper.Map(request.Content, Content);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;
                else throw new Exception("A problem occurred when updating an Content");
            }
        }
    }
}