using System;
using System.Collections.Generic;
using System.Linq;
using Domain;

namespace Persistence
{
    public class Seed
    {
        public static void SeedData(DataContext context)
        {
            if (!context.AuthAdmins.Any())
            {
                var AuthAdmins = new List<AuthAdmin>
                {

                    new AuthAdmin
                    {
                        Email = "erdist@mail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("Define1234"),
                        Admin = new Admin
                        {
                            Surname = "Kocak",
                            FirstName = "Erdi"
                        }
                    },
                    new AuthAdmin
                    {
                        Email = "a@mail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("Define1234"),
                        Admin = new Admin
                        {
                            Surname = "Surname1",
                            FirstName = "Firstname1"
                        }
                    },
                    new AuthAdmin
                    {
                        Email = "b@mail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("Define1234"),
                        Admin = new Admin
                        {
                            Surname = "Surname2",
                            FirstName = "Firstname2"
                        }
                    },
                    new AuthAdmin
                    {
                        Email = "c@mail.com",
                        Password = BCrypt.Net.BCrypt.HashPassword("Define1234"),
                        Admin = new Admin
                        {
                            Surname = "Surname3",
                            FirstName = "Firstname3"
                        }
                    }


                };
                var Users = new List<User>
                {
                    new User
                    {
                        Surname = "Surname1",
                        FirstName = "Firstname1",
                        BirthDate = DateTime.Now,
                        Country = "Turkey",
                        City = "Ankara",
                        Admin = AuthAdmins[0].Admin,
                    },
                    new User
                    {
                        Surname = "Surname2",
                        FirstName = "Firstname2",
                        BirthDate = DateTime.Now,
                        Country = "Turkey",
                        City = "Istanbul",
                        Admin = AuthAdmins[0].Admin,
                    },
                    new User
                    {
                        Surname = "Surname3",
                        FirstName = "Firstname3",
                        BirthDate = DateTime.Now,
                        Country = "Turkey",
                        City = "Izmir",
                        Admin = AuthAdmins[0].Admin,
                    },
                    new User
                    {
                        Surname = "Surname1",
                        FirstName = "Firstname1",
                        BirthDate = DateTime.Now,
                        Country = "Turkey",
                        City = "Ankara",
                        Admin = AuthAdmins[1].Admin,
                    },
                    new User
                    {
                        Surname = "Surname2",
                        FirstName = "Firstname2",
                        BirthDate = DateTime.Now,
                        Country = "Turkey",
                        City = "Istanbul",
                        Admin = AuthAdmins[1].Admin,
                    },
                    new User
                    {
                        Surname = "Surname3",
                        FirstName = "Firstname3",
                        BirthDate = DateTime.Now,
                        Country = "Turkey",
                        City = "Izmir",
                        Admin = AuthAdmins[1].Admin,
                    },
                    new User
                    {
                        Surname = "Surname1",
                        FirstName = "Firstname1",
                        BirthDate = DateTime.Now,
                        Country = "Turkey",
                        City = "Ankara",
                        Admin = AuthAdmins[2].Admin,
                    },
                    new User
                    {
                        Surname = "Surname2",
                        FirstName = "Firstname2",
                        BirthDate = DateTime.Now,
                        Country = "Turkey",
                        City = "Istanbul",
                        Admin = AuthAdmins[2].Admin,
                    },
                    new User
                    {
                        Surname = "Surname3",
                        FirstName = "Firstname3",
                        BirthDate = DateTime.Now,
                        Country = "Turkey",
                        City = "Izmir",
                        Admin = AuthAdmins[2].Admin,
                    }
                };
                var Contents = new List<Content>
                {
                    new Content
                    {
                        Title="Content1",
                        ContentBody="Body1",
                        Admin= AuthAdmins[0].Admin
                    },
                    new Content
                    {
                        Title="Content2",
                        ContentBody="Body2",
                        Admin= AuthAdmins[0].Admin
                    },
                    new Content
                    {
                        Title="Content1",
                        ContentBody="Body1",
                        Admin= AuthAdmins[1].Admin
                    },
                    new Content
                    {
                        Title="Content1",
                        ContentBody="Body1",
                        Admin= AuthAdmins[1].Admin
                    },
                    new Content
                    {
                        Title="Content1",
                        ContentBody="Body1",
                        Admin= AuthAdmins[2].Admin
                    },
                    new Content
                    {
                        Title="Content2",
                        ContentBody="Body2",
                        Admin= AuthAdmins[2].Admin
                    }
                };
                
                context.Users.AddRange(Users);
                context.Contents.AddRange(Contents);
                context.AuthAdmins.AddRange(AuthAdmins);
                context.SaveChanges();
            }
        }
    }
}