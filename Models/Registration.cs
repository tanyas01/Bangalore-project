using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XtraCoverBBGA.Models
{
    public class Registration
    {
        public int ID { get; set; }        
        public string DeviceType { get; set; }
        public string IMEINo { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string ModelName { get; set; }
        public string EmailId { get; set; }
        public string DealerCode { get; set; }
        public string Phone { get; set; }
        public decimal PurchasePrice { get; set; }
        public string AlternatePhone { get; set; }
        public DateTime PurchaseDate { get; set; }
        public string AddLine1 { get; set; }
        public string AddLine2 { get; set; }
        public string Invoicefile { get; set; }
        public DateTime InsertDate { get; set; }
    }
}