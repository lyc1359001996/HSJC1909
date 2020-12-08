using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acid.SDK.Library
{
    public class DeviceModel
    {
        public int Index { get; set; }
        public string dwVID { get; set; }
        public string dwPID { get; set; }
        public string szManufacturer { get; set; }
        public string szDeviceName { get; set; }
        public string szSerialNumber { get; set; }

        public DeviceModel(int index, string vID, string pID, string manufacturer, string deviceName, string serialNumber)
        {
            Index = index;
            dwVID = vID;
            dwPID = pID;
            szManufacturer = manufacturer;
            szDeviceName = deviceName;
            szSerialNumber = serialNumber;
        }
    }
}
