using iTextSharp.text.pdf;
using OrderProcessingApplication.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderProcessingApplication.Repositories
{
    public interface IPaymentDetailsService 
    {
        PdfPTable AddContentToPDF(PdfPTable tableLayout);

        List<OrderPaymentDetail> GetAllOrderPaymentDetails();
        List<ShipmentItemDetail> GetShipmentOrderDetails(int orderId);
        bool AddOrderPaymentDetails(OrderPaymentDetail orderPaymentDetail);
        bool AddShipmentOrderDetails(ShipmentItemDetail shipmentItemDetail);
    }
}