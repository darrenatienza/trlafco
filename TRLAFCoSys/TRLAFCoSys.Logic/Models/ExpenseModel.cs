using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Models
{
    public class ExpenseModel
    {

        public DateTime Date { get; set; }

        public int ID { get; internal set; }

        public string Particular { get; set; }

        public int ExpenseTypeID { get; set; }

        public double UnitPrice { get; set; }

        public double Quantity { get; set; }

        public double Total { get; internal set; }
    }
    public class ExpenseListModel
    {
      
        public int ID { get; set; }
        public DateTime Date { get; set; }
        public string Particular { get; set; }
        public string ExpenseTypeDescription {get; set;}
        public double Quantity { get; set; }
        public double Total { get; set; }

        public double UnitPrice { get; set; }
       
    }
    public class ExpenseSummaryListModel
    {
        public string ExpenseTypeDescription { get; set; }
        public double Total { get; internal set; }
    }
    
}
