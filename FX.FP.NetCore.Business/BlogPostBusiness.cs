using FX.FP.NetCore.Interface;
using FX.FP.NetCore.Service;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FX.FP.NetCore.Business
{
    public class BlogPostBusiness<T> : IBlogPost<T> where T : class, new()
    {
        MySqlHelper db = new MySqlHelper();
        public bool Insert(string title, string content)
        {
            Hashtable tb = new Hashtable();
            tb["title"] = title;
            tb["content"] = content;
            //return db.Submit_AddOrEdit(System.Data.CommandType.Text, "BlogPost", "", "", tb);
            return db.Submit_AddOrEdit(System.Data.CommandType.Text, typeof(T).Name, "", "", tb);
        }

        public List<T> GetList()
        {
            List<T> list = db.GetListBySQL<T>(System.Data.CommandType.Text, "select * from BlogPost").ToList();
            return list;
        }

        public DataTable GetPageList(DateTime createDate)
        {
            List<MySql.Data.MySqlClient.MySqlParameter> mySqlParameter = new List<MySql.Data.MySqlClient.MySqlParameter>() {
                new MySql.Data.MySqlClient.MySqlParameter()
                {
                    ParameterName = "@createdate",
                    DbType = DbType.String,
                    Value = createDate.ToShortDateString()
                }
            };
            int count = 0;
            return db.GetPageList(System.Data.CommandType.Text, "select * from BlogPost where createdate>=@createdate order by createdate desc", "createdate desc",
                   5, 1, mySqlParameter.ToArray(), ref count);
        }
    }
}
