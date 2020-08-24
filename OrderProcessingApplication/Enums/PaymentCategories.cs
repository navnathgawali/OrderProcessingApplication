using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace OrderProcessingApplication.Enums
{
    public enum PaymentCategories
    {
        [Description("Physical Product")]
        PhysicalProduct = 1,
        Book =2,
        Membership = 3,
        [Description("Upgrade Membership")]
        UpgradeMembership =4
    };

}