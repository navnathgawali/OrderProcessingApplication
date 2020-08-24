using AutoMapper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using OrderProcessingApplication.Helpers;
using OrderProcessingApplication.Models.DataModels;
using OrderProcessingApplication.Models.Email;
using OrderProcessingApplication.Models.ViewModels;
using OrderProcessingApplication.Repositories;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web.Mvc;

namespace OrderProcessingApplication.Controllers
{
    public class PaymentDetailsController : Controller
    {
        private readonly IPaymentDetailsService _paymentDetailsService = null;
        public PaymentDetailsController(IPaymentDetailsService paymentDetailsService)
        {
            _paymentDetailsService = paymentDetailsService;
        }

        /// <summary>
        /// Get new order view
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            PaymentDetailsViewModel viewModel = new PaymentDetailsViewModel();
            var paymentTypes = new List<SelectListItem>()
            {
                new SelectListItem() { Text = Helpers.EnumHelper.GetDescription(Enums.PaymentCategories.PhysicalProduct).ToString(), Value = ((int)Enums.PaymentCategories.PhysicalProduct).ToString() },

                new SelectListItem() { Text = Enums.PaymentCategories.Book.ToString(), Value = ((int)Enums.PaymentCategories.Book).ToString() },

                new SelectListItem() { Text = Enums.PaymentCategories.Membership.ToString(), Value = ((int)Enums.PaymentCategories.Membership).ToString() },

                new SelectListItem() { Text = Helpers.EnumHelper.GetDescription(Enums.PaymentCategories.UpgradeMembership).ToString(), Value = ((int)Enums.PaymentCategories.UpgradeMembership).ToString() },
            };
            viewModel.PaymentCategories = paymentTypes;
            return View("PaymentDetails", viewModel);
        }

        /// <summary>
        /// Get Order Payment Details
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult GetOrderPaymentDetails()
        {
            var orderPaymentDetailList = _paymentDetailsService.GetAllOrderPaymentDetails();
            var viewModel = MapperHelper.MapList<OrderPaymentDetail, PaymentDetailsViewModel>(orderPaymentDetailList);

            foreach (var item in viewModel)
            {
                item.PaymentItem = EnumHelper.GetDescription((Enums.PaymentCategories)Convert.ToInt32(item.PaymentItem));
            }
            return View("GetOrderPaymentDetails", viewModel);
        }

        /// <summary>
        /// Add Order Payment Details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddOrderPaymentDetails(PaymentDetailsViewModel model)
        {
            if (model != null)
            {
                var orderPaymentDetail = MapperHelper.MapSingle<PaymentDetailsViewModel, OrderPaymentDetail>(model);
                _paymentDetailsService.AddOrderPaymentDetails(orderPaymentDetail);

                //Send an email
                if (model.IsActivationUpgradeEmailSent)
                {
                    //Hardcoded some values to send an email..To be configured in config file
                    var mailModel = new MailModel()
                    {
                        From = "navnathgawali6812@gmail.com",
                        To = "navnathgawali6812@gmail.com",
                        Subject = model.IsActiveMembership ? "Activate Membership" : "Upgrade Membership",
                        Body = model.IsActiveMembership ? "Membership activated successfully" : "Membership upgraded successfully"
                    };
                    EmailHelper.SendEmail(mailModel);
                }

            }
            return RedirectToAction("GetOrderPaymentDetails");
        }

        /// <summary>
        ///Create Pdf
        /// </summary>
        /// <param name="itemDetails"></param>
        /// <returns></returns>
        public FileResult CreatePdf(PaymentDetailsViewModel itemDetails)
        {
            MemoryStream workStream = new MemoryStream();
            StringBuilder status = new StringBuilder("");
            DateTime dTime = DateTime.Now;
            //file name to be created   
            string strPDFFileName = string.Format("ShipmentPackagingSlip" + dTime.ToString("yyyyMMdd") + "-" + ".pdf");
            Document doc = new Document();
            //doc.SetMargins(0 f, 0 f, 0 f, 0 f);
            //Create PDF Table with 5 columns  
            PdfPTable tableLayout = new PdfPTable(5);
            //doc.SetMargins(0 f, 0 f, 0 f, 0 f);
            //Create PDF Table  

            string strAttachment = Server.MapPath("~/Downloads/" + strPDFFileName);


            PdfWriter.GetInstance(doc, workStream).CloseStream = false;
            doc.Open();

            //Add Content to PDF   
            doc.Add(_paymentDetailsService.AddContentToPDF(tableLayout));

            // Closing the document  
            doc.Close();

            byte[] byteInfo = workStream.ToArray();
            workStream.Write(byteInfo, 0, byteInfo.Length);
            workStream.Position = 0;


            return File(workStream, "application/pdf", strPDFFileName);

        }

        /// <summary>
        /// Get Shipment Order Details
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public ActionResult GetShipmentOrderDetails(int orderId)
        {
            try
            {
                var list = _paymentDetailsService.GetShipmentOrderDetails(orderId);
                var viewModel = MapperHelper.MapList<ShipmentItemDetail, ShipmentItemDetailsViewModel>(list);

                return View(viewModel);
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        ///Add Shipment Order Details
        /// </summary>
        /// <param name="shipmentItemDetailsViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddShipmentOrderDetails(ShipmentItemDetailsViewModel shipmentItemDetailsViewModel)
        {
            if (shipmentItemDetailsViewModel != null)
            {
                var shipmentItemDetail = MapperHelper.MapSingle<ShipmentItemDetailsViewModel, ShipmentItemDetail>(shipmentItemDetailsViewModel);
                _paymentDetailsService.AddShipmentOrderDetails(shipmentItemDetail);

            }
            return RedirectToAction("GetShipmentOrderDetails", new { orderId = shipmentItemDetailsViewModel.OrderId });
        }
    }
}
