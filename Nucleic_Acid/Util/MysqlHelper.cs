using Acid.common.Library.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nucleic_Acid.Util
{
    public class MysqlHelper
    {
        public static Task<int> InsertAsync(List<InfoListModel> data)
        {
            try
            {
                using (FaceContext db = new FaceContext())
                {
                    List<staff_nucleic_acid> list = new List<staff_nucleic_acid>();
                    foreach (var item in data)
                    {
                        staff_nucleic_acid ac = new staff_nucleic_acid()
                        {
                            acid_no = item.acidNo,
                            address = item.address,
                            card_no = item.cardNo,
                            company = item.company,
                            create_time = item.createTime,
                            detection_name = CommonHelper.detectionName,
                            home_address = item.homeAddress,
                            jcd_name = CommonHelper.jcdName,
                            serial_number = item.serialNumber,
                            sex = SByte.Parse(item.sex),
                            user_name = item.userName,
                            update_name = item.updateName,
                            xzjd_name = CommonHelper.xzjdName,
                            cyd_name = CommonHelper.cydName,
                            district_name = CommonHelper.districtName
                        };
                        list.Add(ac);
                    }
                    db.staff_nucleic_acid.AddRange(list);
                    return db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                Logger.Default.Error(ex.Message);
                return Task.FromResult<int>(0);
            }
        }
    }
}
