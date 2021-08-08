using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Models
{
    public class ContactModel
    {
        public int ContactId { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string Mobile { get; set; }
        public string Image { get; set; }
        public string Gender { get; set; }
        public string Country { get; set; }
    }
}
