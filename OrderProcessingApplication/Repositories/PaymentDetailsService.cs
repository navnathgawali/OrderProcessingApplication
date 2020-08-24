using iTextSharp.text;
using iTextSharp.text.pdf;
using OrderProcessingApplication.Controllers;
using OrderProcessingApplication.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderProcessingApplication.Repositories
{
    public class PaymentDetailsService : IPaymentDetailsService
    {
        OrderProcessingDBEntities _context = null;
        public PaymentDetailsService()
        {
            _context = new OrderProcessingDBEntities();
        }

        /// <summary>
        ///Get All Order Payment Details
        /// </summary>
        /// <returns></returns>
        public List<OrderPaymentDetail> GetAllOrderPaymentDetails()
        {
            try
            {
                return _context.OrderPaymentDetails?.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        ///Get Shipment Order Details
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<ShipmentItemDetail> GetShipmentOrderDetails(int orderId)
        {
            try
            {
                return _context.ShipmentItemDetails.Where(x => x.OrderId == orderId)?.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// Add Order Payment Details
        /// </summary>
        /// <param name="orderPaymentDetail"></param>
        /// <returns></returns>
        public bool AddOrderPaymentDetails(OrderPaymentDetail orderPaymentDetail)
        {
            try
            {
                _context.OrderPaymentDetails.Add(orderPaymentDetail);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Add Shipment Order Details
        /// </summary>
        /// <param name="shipmentItemDetail"></param>
        /// <returns></returns>
        public bool AddShipmentOrderDetails(ShipmentItemDetail shipmentItemDetail)
        {
            try
            {
                _context.ShipmentItemDetails.Add(shipmentItemDetail);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Add contents to PDF
        /// </summary>
        /// <param name="tableLayout"></param>
        /// <returns></returns>
        public PdfPTable AddContentToPDF(PdfPTable tableLayout)
        {
            try
            {
                float[] headers = { 50, 24, 45, 35, 50 }; //Header Widths  
                tableLayout.SetWidths(headers); //Set the pdf headers  
                tableLayout.WidthPercentage = 100; //Set the PDF File witdh percentage  
                tableLayout.HeaderRows = 1;

                List<ShipmentItemDetail> itemDetails = _context.ShipmentItemDetails.ToList();

                ////Add header  
                AddCellToHeader(tableLayout, "Item Id");
                AddCellToHeader(tableLayout, "Item Name");
                AddCellToHeader(tableLayout, "Precausion Desc");
                AddCellToHeader(tableLayout, "From Location");
                AddCellToHeader(tableLayout, "To Location");

                ////Add body  
                foreach (var item in itemDetails)
                {
                    AddCellToBody(tableLayout, item.ItemId.ToString());
                    AddCellToBody(tableLayout, item.ItemName);
                    AddCellToBody(tableLayout, item.PrecausionsDesc);
                    AddCellToBody(tableLayout, item.FromLocation);
                    AddCellToBody(tableLayout, item.ToLocation);
                }
                return tableLayout;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Method to add single cell to the Header 
        /// </summary>
        /// <param name="tableLayout"></param>
        /// <param name="cellText"></param>
        private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
        {

            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.YELLOW)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 5,
                BackgroundColor = new iTextSharp.text.BaseColor(128, 0, 0)
            });
        }

        /// <summary>
        /// Method to add single cell to the body 
        /// </summary>
        /// <param name="tableLayout"></param>
        /// <param name="cellText"></param>        
        private static void AddCellToBody(PdfPTable tableLayout, string cellText)
        {
            tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.FontFamily.HELVETICA, 8, 1, iTextSharp.text.BaseColor.BLACK)))
            {
                HorizontalAlignment = Element.ALIGN_LEFT,
                Padding = 5,
                BackgroundColor = new iTextSharp.text.BaseColor(255, 255, 255)
            });
        }

    }
}