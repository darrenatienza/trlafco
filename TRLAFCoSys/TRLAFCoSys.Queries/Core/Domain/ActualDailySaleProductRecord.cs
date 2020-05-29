using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    public class ActualDailySaleProductRecord
    {
        public ActualDailySaleProductRecord()
        {

        }
        public int ActualDailySaleProductReportID { get; set; }
        public DateTime CreateDateTime { get; set; }

        public virtual ActualDailySaleProduct ActualDailySaleProduct { get; set; }
        public int ActualDailySaleProductID { get; set; }
        public virtual DailySaleRecord ActualDailySaleReport { get; set; }
        public int ActualDailySaleReportID { get; set; }
        public int Quantity { get; set; }


    }
}
