using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    public class Expense
    {
        public Expense() { }

        public int ExpenseID { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime ActualDate { get; set; }
        public virtual ExpenseType ExpenseType { get; set; }
        public int ExpenseTypeID { get; set; }
        public string Particulars { get; set; }
        public double Quantity { get; set; }
        public double UnitPrice { get; set; }

        //Total was computed

    }
}
