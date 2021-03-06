﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acid.common.Library.config
{
    public class UrlModel
    {
        public static readonly string ip = "https://hsjc.zsbzply.com";
        public static readonly string ipc = "http://192.168.16.111:8092";
        public static readonly string login = "/user/login";
        public static readonly string nucleic = "/staff-nucleic/page";
        public static readonly string nucleic_add = "/staff-nucleic/add";
        public static readonly string nucleic_update = "/staff-nucleic/update";
        public static readonly string nucleic_delete = "/staff-nucleic/delete";
        public static string Token = "";
        public static bool autoPrint = true;
    }
}
