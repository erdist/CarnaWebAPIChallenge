using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using MediatR;
using Persistence;

namespace Application.Authorization
{
    public class Register
    {
        public class Command : IRequest
        {
            public AuthAdmin AuthAdmin { get; set; }
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
                var AuthAdmin = new AuthAdmin
                {
                    Email = request.AuthAdmin.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(request.AuthAdmin.Password),
                    Admin = new Admin
                    {
                        Id = request.AuthAdmin.AdminId,
                        FirstName = request.AuthAdmin.Admin.FirstName,
                        Surname = request.AuthAdmin.Admin.Surname,

                    },
                    AdminId = request.AuthAdmin.AdminId
                };
                _context.AuthAdmins.Add(AuthAdmin);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) return Unit.Value;
                else throw new Exception("A problem occurred when registering an Admin");
            }
        }

    }
}