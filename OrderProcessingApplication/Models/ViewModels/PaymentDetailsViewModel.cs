using OrderProcessingApplication.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrderProcessingApplication.Models.ViewModels
{
    public class PaymentDetailsViewModel
    {
        public List<SelectListItem> PaymentCategories = new List<SelectListItem>();
        // public PaymentCategories PaymentCategories { get; set; }

        public string Id { get; set; }
        public string PaymentItem { get; set; }
        public bool IsActiveMembership { get; set; }
        public bool IsUpgradeMembership { get; set; }
        public decimal ApplicableAgentCommision { get; set; }
        public bool IsActivationUpgradeEmailSent { get; set; }
    }
}