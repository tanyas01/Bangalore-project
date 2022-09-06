using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XtraCoverBBGA.Models
{
    public class Voucher
    {
        public int ID { get; set; }
        public string DeviceName { get; set; }
        public string SerialNo { get; set; }
        public string SecretCode { get; set; }
        public string UsedBy { get; set; }
        public DateTime? UsedDate { get; set; }
        public bool Status { get; set; }
    }
}