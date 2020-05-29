using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    public class ActualDailySaleOutletReport
    {
        public ActualDailySaleOutletReport() { }
        public int ActualDailySaleOutletReportID { get; set; }
        public virtual ActualDailySaleOutlet ActualDailySaleOutlet { get; set; }
        public int ActualDailySaleOutletID { get; set; }
        public virtual DailySaleRecord ActualDailySaleReport { get; set; }
        public int ActualDailySaleReportID { get; set; }
        public double Sales { get; set; }
    }
}
