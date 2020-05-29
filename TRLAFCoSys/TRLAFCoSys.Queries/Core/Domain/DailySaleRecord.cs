using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    public class DailySaleRecord
    {
        public DailySaleRecord()
        {

        }
        public int DailySaleRecordID { get; set; }
        public DateTime CreateDateTime { get; set; }   
        public double ProcessingSale { get; set; }
        public double SaleOnAccount { get; set; }
        public string Debtor { get; set; }
    }
}
