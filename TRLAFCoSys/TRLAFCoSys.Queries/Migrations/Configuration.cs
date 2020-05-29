namespace TRLAFCoSys.Queries.Migrations
{
    using TRLAFCoSys.Queries.Core.Domain;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TRLAFCoSys.Queries.Persistence.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TRLAFCoSys.Queries.Persistence.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //



            #region Add Permissions
            var permissions = new Dictionary<string, Role>
            {
                //Users Lookup
                {"1", new Role {RoleID = 1, Description = "User - Add"}},
                {"2", new Role {RoleID = 2, Description = "User - Delete"}},
                {"3", new Role {RoleID = 3, Description = "User - Save"}},
                {"4", new Role {RoleID = 4, Description = "Permission - Delete"}},
                {"5", new Role {RoleID = 5, Description = "Permission - Check All"}},
                {"6", new Role {RoleID = 6, Description = "Permission - Add"}},
                {"7", new Role {RoleID = 7, Description = "Permission - Insert All"}},
                {"8", new Role {RoleID = 8, Description = "User Lookup"}},
                //Login
                {"9", new Role {RoleID = 9, Description = "Login"}},
                //Employee Lookup
                {"10", new Role {RoleID = 10, Description = "Employee Lookup"}},
                {"11", new Role {RoleID = 11, Description = "Employee - Add"}},
                {"12", new Role {RoleID = 12, Description = "Employee - Delete"}},
                {"13", new Role {RoleID = 13, Description = "Employee - Save"}},
                 //Item Lookup
                {"14", new Role {RoleID = 14, Description = "Item Lookup"}},
                {"15", new Role {RoleID = 15, Description = "Item - Add"}},
                {"16", new Role {RoleID = 16, Description = "Item - Delete"}},
                {"17", new Role {RoleID = 17, Description = "Item - Save"}},
                 //Reservation
                {"18", new Role {RoleID = 18, Description = "Reservation"}},
                {"19", new Role {RoleID = 19, Description = "Reservation - Add"}},
                {"20", new Role {RoleID = 20, Description = "Reservation - Delete"}},
                //Reservation Lookup
                {"21", new Role {RoleID = 21, Description = "Reservation - Save"}},
                {"22", new Role {RoleID = 22, Description = "Reservation - Reset"}},
                 //Device Setup
                {"23", new Role {RoleID = 23, Description = "Device Setup"}},

            };

            foreach (var permission in permissions.Values)
                context.Roles.AddOrUpdate(t => t.RoleID, permission);
            #endregion

            #region Add Users
            var users = new Dictionary<string, User>
            {
                {"admin", 
                    new User 
                    { UserID = 1, 
                        FirstName = "admin", 
                        MiddleName ="admin", 
                        LastName="admin",
                        UserName ="admin",
                        Password = "hhG78E33nyu+81r6HoIBtKfrFHq85FmR52igWa9Ii9rvkpPBzPx0wZM/5bjO+g1USqh2/UDgthyDUcYTLqZU9g==",
                        PasswordSalt="100000.VX80RHacuhEU/pBeUcAWgzdvgMu5UP17Gad8+Xf7ZuYWHQ=="
                    }
                }
            };

            foreach (var user in users.Values)
                context.Users.AddOrUpdate(t => t.UserID, user);
            #endregion
        }
    }
}
