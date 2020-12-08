using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acid.common.Library.config
{
   public class SettingModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string userName { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string passWord { get; set; }
        /// <summary>
        /// 是否记住密码
        /// </summary>
        public bool isRember { get; set; }
        /// <summary>
        /// 是否自动登录
        /// </summary>
        public bool isAuto { get; set; }
    }
}
