using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{

    [NotMapped]
    public class Payroll
    {
        public Payroll() { }
        public int PayrollID { get; set; }
        public DateTime CreateDateTime { get; set; }
        public virtual Farmer Farmer { get; set; }
        public int FarmerID { get; set; }
        
        //Get TotalVolume on BL
        //Get TotalAmount on BL
        //Get FirstQuarterAmount (Partial Collected Amount) on BL
        //Get VolumeOfMilk from BL
        //Get Savings from BL
        //Get SecondQuarterAmount (To be Collect) on BL
       
        
        
    }
}
