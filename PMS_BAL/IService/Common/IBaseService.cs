using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS_BAL.IService.Common
{
    public interface IBaseService
    {
        T ExcecuteFunction<T>(Func<T> function);
    }
}
