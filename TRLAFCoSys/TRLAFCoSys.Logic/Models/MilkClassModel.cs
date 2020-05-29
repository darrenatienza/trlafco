using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Models
{
    public class MilkClassListModel
    {
        public MilkClassListModel() { }
        public int ID { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
    }
    public class MilkClassModel
    {
        public MilkClassModel() { }
        public int ID { get; set; }
        public string Description { get; set; }

        public double Cost { get; set; }
    }
    public class AddMilkClassModel
    {
        public AddMilkClassModel() { }
        public string Description { get; set; }
        public double Cost { get; set; }
    }
    public class EditMilkClassModel
    {
        public EditMilkClassModel() { }
        public string Description { get; set; }
        public double Cost { get; set; }
    }
}
