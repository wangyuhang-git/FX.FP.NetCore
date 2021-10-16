using FX.FP.NetCore.Interface;
using FX.FP.NetCore.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX.FP.NetCore.Business
{
    public class BlogPostBusiness<T> : IBlogPost<T> where T : class,new()
    {
        MySqlHelper db = new MySqlHelper();
        public bool Insert(string title, string content)
        {
            Hashtable tb = new Hashtable();
            tb["title"] = title;
            tb["content"] = content;
            return db.Submit_AddOrEdit(System.Data.CommandType.Text, "BlogPost", "", "", tb);
        }
    }
}
