//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OrderProcessingApplication.Models.DataModels
{
    using System;
    using System.Collections.Generic;
    
    public partial class ShipmentItemDetail
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string PrecausionsDesc { get; set; }
        public string FromLocation { get; set; }
        public string ToLocation { get; set; }
        public Nullable<int> OrderId { get; set; }
    
        public virtual OrderPaymentDetail OrderPaymentDetail { get; set; }
    }
}