using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    public class Farmer
    {
        public Farmer() {
            MilkProducts = new HashSet<MilkCollection>();
        }
        public int FarmerID { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<MilkCollection> MilkProducts { get; set; }
    }
}
