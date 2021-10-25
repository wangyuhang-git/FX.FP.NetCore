using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FX.FP.NetCore.WebApi
{
    public class Class
    {
        public static List<string> list;
        public List<string> Add()
        {
            list.Add("测试1");
            list.Add("测试2");
            list.Add("测试3");
            return list;
        }

        /*
        public object searchUnit(int pageCode)
        {

            Page<UnitCustom> page = new Page<UnitCustom>();
            page.pageCode = pageCode;
            page.pageSize = 10;
            string sqlTotal = "SELECT COUNT(*) FROM v_unit ";
            page.totolRecord = SQLHelper.ExcuteScalarSQL(sqlTotal);
            if ((page.totolRecord % page.pageSize) == 0)
            {
                page.totalPage = page.totolRecord / page.pageSize;
            }
            else
            {
                page.totalPage = page.totolRecord / page.pageSize + 1;
            }

            string sql = "SELECT * FROM v_unit LIMIT " + (page.pageCode - 1) * page.pageSize + "," + page.pageSize;

            DataTable dt = SQLHelper.GetTable(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                UnitCustom unitCustom = new UnitCustom();
                unitCustom.F_CODE = dt.Rows[i]["F_CODE"].ToString();
                unitCustom.F_BUILD_CODE = dt.Rows[i]["F_BUILD_CODE"].ToString();
                unitCustom.F_POSITION = dt.Rows[i]["F_POSITION"].ToString();
                unitCustom.F_NAME = dt.Rows[i]["F_NAME"].ToString();
                unitCustom.F_POSITION_DES = dt.Rows[i]["F_POSITION_DES"].ToString();
                unitCustom.F_POSITION_REMARK = dt.Rows[i]["F_POSITION_REMARK"].ToString();
                unitCustom.F_TYPE = dt.Rows[i]["F_TYPE"].ToString();
                unitCustom.F_MAP_CODE = dt.Rows[i]["F_MAP_CODE"].ToString();
                unitCustom.F_BUILD_NAME = dt.Rows[i]["F_BUILD_NAME"].ToString();
                page.listBean.Add(unitCustom);
            }
            return page;
        }
        */
    }

    public class Page<T>
    {
        public int pageCode { get; set; }
        public int pageSize { get; set; }
        public int totalPage { get; set; }
        public int totolRecord { get; set; }
        public List<T> listBean { get; set; }
        public Page()
        {
            listBean = new List<T>();
        }
    }
}
