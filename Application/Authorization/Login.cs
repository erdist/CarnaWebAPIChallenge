using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Helpers;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Authorization
{
    public class Login
    {
        public class Query : IRequest<TResponse<AuthAdmin>>
        {
            public AuthAdmin AuthAdmin { get; set; }
        }

        public class Handler : IRequestHandler<Query, TResponse<AuthAdmin>>
        {
            private readonly DataContext _context;
            public Handler(DataContext context)
            {
                _context = context;

            }
            public async Task<TResponse<AuthAdmin>> Handle(Query request, CancellationToken cancellationToken)
            {
                var AuthAdmin = await _context.AuthAdmins.Include(a => a.Admin).FirstOrDefaultAsync(a => a.Email == request.AuthAdmin.Email);
                if (AuthAdmin == null)
                {
                    return TResponse<AuthAdmin>.GetResult(0, "No such user found.", null);
                }

                if (!BCrypt.Net.BCrypt.Verify(request.AuthAdmin.Password, AuthAdmin.Password))
                {
                    return TResponse<AuthAdmin>.GetResult(0, "Wrong password", null);
                }
                else
                {
                    var response = TResponse<AuthAdmin>.GetResult(1, "Login successful", AuthAdmin);
                    response.Data.Password = "";
                    return response;
                }
            }
        }
    }
}