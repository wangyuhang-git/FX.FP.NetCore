using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX.FP.NetCore.Interface
{
    public interface IBlogPost<T> where T : class, new()
    {
        bool Insert(string title, string content);

        List<T> GetList();
    }
}
