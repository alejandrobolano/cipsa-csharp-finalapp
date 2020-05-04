using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoClub.Common.BusinessLogic.Contracts
{
    public interface IService<T>
    {
        bool Add(T model);
        bool Remove(string id);
        T Get(string id);
        bool Update(T modelDto);
        List<T> All();
    }
}
