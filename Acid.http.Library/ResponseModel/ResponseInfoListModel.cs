using Acid.common.Library.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acid.http.Library.ResponseModel
{
    public class ResponseInfoListModel
    {
        public int total { get; set; }
        public int pageSize { get; set; }
        public int currPage { get; set; }
        public List<InfoListModel> data { get; set; }

        public List<InfoListModel> addIndex(List<InfoListModel> data)
        {
            int current = 1;
            foreach (var item in data)
            {
                item.index = current;
                item.updateText = "编辑";
                item.Editor_company = false;
                item.Editor_homeAddress = false;
                current++;

            }
            return data;
        }
    }
}
