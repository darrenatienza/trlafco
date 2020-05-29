using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    [Table("dbo.Users")]
    public class User
    {
        public User()
        {
            Roles = new HashSet<Role>();
        }
        public int UserID { get; set; }
        [Display(Name="User Name")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Display(Name="Password")]
        public string Password { get; set; }

        public string PasswordSalt { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public virtual ICollection<Role> Roles { get; set; }

    }
}
