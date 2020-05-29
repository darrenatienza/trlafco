using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRLAFCoSys.Logic.Contracts
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">Add Edit Models</typeparam>
    public interface IBaseLogic<T> where T : class
    {
        void Add(T model);
        void Edit(int id, T model);
        void Delete(int id);
    }
}
