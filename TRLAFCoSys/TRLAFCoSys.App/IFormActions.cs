using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.App
{
    public interface IFormActions
    {
        void Save();
        void Delete();

        void LoadMainGrid();
        void SetData();
        void Add();
        void Edit();
        void ResetInputs();

        bool ValidateFields();

    }
}
