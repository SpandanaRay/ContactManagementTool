using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class User: IdentityUser<int>
    {
        //public int UserId { get; set; }
        //public string Name { get; set; }
        public string Address { get; set; }
        [NotMapped]
        public string[] Roles { get; set; }
        public string Name { get; set; }
    }

}
