using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    public class Production
    {
        public Production()
        {
            
        }
        public int ProductionID { get; set; }
        public DateTime Date { get; set; }
        public virtual Product Product { get; set; }
        public int ProductID { get; set; }

        /// <summary>
        /// Number of Products Produce
        /// </summary>
        public int Quantity { get; set; }
    }
}
