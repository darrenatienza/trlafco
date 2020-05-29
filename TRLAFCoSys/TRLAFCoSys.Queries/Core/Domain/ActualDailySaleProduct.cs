using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    public class ActualDailySaleProduct
    {
        public ActualDailySaleProduct()
        {

        }
        public int ActualDailySaleProductID { get; set; }
        public DateTime CreateDateTime { get; set; }

        public string Description { get; set; }

    }
}
