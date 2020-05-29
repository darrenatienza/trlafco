﻿using TRLAFCoSys.Queries.Core.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.EntityConfiguration
{
    public class UserConfiguration  : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            Property(u => u.UserName)
            .IsRequired()
            .HasMaxLength(250);

            Property(u => u.FirstName)
             .IsRequired()
             .HasMaxLength(250);

            Property(u => u.MiddleName)
            .IsRequired()
            .HasMaxLength(250);

            Property(u => u.LastName)
             .IsRequired()
             .HasMaxLength(250);

            Property(u => u.Password)
              .IsRequired();

            Property(u => u.PasswordSalt)
             .IsRequired();

           

            HasMany(u => u.Roles)
                .WithMany(p => p.Users)
                .Map(m =>
                {
                    m.ToTable("UserRoles");
                    m.MapLeftKey("UserID");
                    m.MapRightKey("RoleID");
                });
        }

        
    }
}
