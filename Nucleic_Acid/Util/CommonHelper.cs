﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleic_Acid.Util
{
   public class CommonHelper
    {
        /// <summary>
        /// 操作员姓名
        /// </summary>
        public static string detectionName = "";
        /// <summary>
        /// 操作员姓名
        /// </summary>
        public static string jcdName = "";
        /// <summary>
        /// 帐号
        /// </summary>
        public static string userName = "";
        /// <summary>
        /// 密码
        /// </summary>
        public static string passWord = "";

        public static string xzjdName = "";
        public static string cydName = "";
        public static string districtName = "";
        /// <summary>
        /// 负责人名称
        /// </summary>
        public static string principal = "";
        
        public static string ToSex(string sexs)
        {
            if (sexs!="0"&&sexs!="1")
            {
                return sexs.ToString();
            }
            int sex = int.Parse(sexs);
            switch (sex)
            {
                case 0:return "女"; 
                case 1: return "男"; 
                default:return "男";
            }
        }

        public static string SexToInt(string sex)
        {
            if (sex!="男"&&sex!="女")
            {
                return "1";
            }
            switch (sex)
            {
                case "女": return "0";
                case "男": return "1";
                default: return "1";
            }
        }
    }
}
