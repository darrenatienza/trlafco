using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Queries.Core.Domain
{
    public class ExpenseType
    {
        public ExpenseType() { }
        public int ExpenseTypeID {get; set;}
        public string Description { get; set; }
    }
}
