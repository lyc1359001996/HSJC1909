//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nucleic_Acid.Util
{
    using System;
    using System.Collections.Generic;
    
    public partial class permission
    {
        public long id { get; set; }
        public Nullable<long> pid { get; set; }
        public string permission_name { get; set; }
        public Nullable<int> is_deleted { get; set; }
        public Nullable<long> create_by { get; set; }
        public Nullable<long> update_by { get; set; }
        public string create_time { get; set; }
        public string update_time { get; set; }
        public string url { get; set; }
        public string permission_code { get; set; }
    }
}
