using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CreatePNR_AutomationApp.PricingService;
using CreatePNR_AutomationApp.FlightService;
using CreatePNR_AutomationApp.ReservationService;
using CreatePNR_AutomationApp;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Xml;
using System.Net;
using System.IO;
using System.Deployment.Application;

namespace CreatePNR_AutomationApp
{
    public partial class frmCreatePNRs : Form
    {
        #region Global variables
        List<FlightRequest> lstFlightRequest;
        public int FailedCount { get; set; }
        public int ProcessedCount { get; set; }
        public int SuccessCount { get; set; }
        List<PNRsProcessed> lstPNRsProcessed = new List<PNRsProcessed>();
        string ReceipentEmail { get; set; }
        string[] EconomyMeals = new string[] { "SEBA",
                                            "SECA"
                                            ,"SELA"
                                            ,"SEVA"
                                            ,"SIVA"
                                            ,"BAVC"
                                            ,"BECA"
                                            ,"BEVB"
                                            ,"MAVA"
                                            ,"MECA"
                                            ,"MEVG"
                                            ,"MICC"
                                            ,"MIFB"
                                            ,"MIVF"
                                            ,"MOCH"
                                            ,"MRCE"
                                            ,"MRVD"
                                             };
        string[] BusinessMeals = new string[] { "AVML"
                                            ,"CHML"
                                            ,"DBML"
                                            ,"HNML"
                                            ,"LFML"
                                            ,"LSML"
                                            ,"SPML"
                                            ,"VGML"
                                            ,"VLML"
                                            ,"VOML"

                                             };


        #endregion

        public frmCreatePNRs()
        {
            InitializeComponent();

            dtDepartureDate.MinDate = DateTime.Now;
            dtArrivalDate.MinDate = DateTime.Now;
            FailedCount = 0;
            ProcessedCount = 0;
            SuccessCount = 0;
            lstFlightRequest = new List<FlightRequest>();
            lblUploadInfo.Text = string.Empty;
            btnProcessPNRs.Enabled = false;
            cmbOBCabin.SelectedIndex = 0;
            cmbIBCabin.SelectedIndex = 0;
            FailedCount = 0;
            ProcessedCount = 0;
            SuccessCount = 0;
            //Version version = ApplicationDeployment.CurrentDeployment.CurrentVersion;
            //lblApplicationVersion.Text = version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision; //change form title
        }



        private void btnCreate_Click(object sender, EventArgs e)
        {
            string message = IsValid();
            if (string.IsNullOrEmpty(message))
            {
                FailedCount = 0;
                ProcessedCount = 0;
                SuccessCount = 0;

                ReceipentEmail = string.Empty;
                frmEmailPopup popup = new frmEmailPopup();
                popup.ShowDialog();
                ReceipentEmail = popup.ReceipentEmail;
                if (!popup.IsClose)
                {
                    lstPNRsProcessed = new List<PNRsProcessed>();
                    FlightRequest objFlightRequest = new FlightRequest();
                    objFlightRequest.Origin = txtOrigin.Text.Trim();
                    objFlightRequest.Destination = cmbDestination.Text.Trim();
                    objFlightRequest.TotalPNRsRequired = Convert.ToInt16(txtPNRs.Text.Trim());
                    objFlightRequest.AdtCount = Convert.ToInt16(txtAdtCount.Text);
                    objFlightRequest.ChdCount = Convert.ToInt16(txtChildCount.Text);
                    objFlightRequest.InfCount = Convert.ToInt16(txtInfantCount.Text);
                    objFlightRequest.DepartureDate = Convert.ToDateTime(dtDepartureDate.Text);
                    objFlightRequest.ReturnDate = Convert.ToDateTime(dtArrivalDate.Text);
                    objFlightRequest.OutboundFlightNo = Convert.ToString(txtOBFlightNo.Text);
                    objFlightRequest.InboundFlightNo = Convert.ToString(txtIBFlightNo.Text);
                    objFlightRequest.IsRT = chkRT.Checked;
                    objFlightRequest.VoucherNo = (string.IsNullOrEmpty(txtVoucherNo.Text) ? string.Empty : txtVoucherNo.Text.Trim());
                    objFlightRequest.OutboundCabin = (cmbOBCabin.Text != null ? Convert.ToString(cmbOBCabin.Text) : EnumCabin.Economy.ToString());
                    objFlightRequest.InboundCabin = (cmbIBCabin.Text != null ? Convert.ToString(cmbIBCabin.Text) : EnumCabin.Economy.ToString());
                    objFlightRequest.OutboundBaggage = cmbOBBaggage.Text;
                    objFlightRequest.InboundBaggage = cmbIBBaggage.Text;
                    objFlightRequest.OutboundMeal = cmbOBMeal.Text;
                    objFlightRequest.InboundMeal = cmbIBMeal.Text;
                    objFlightRequest.IFE = chkIFE.Checked;
                    objFlightRequest.Seating = chkSeating.Checked;

                    lblInfo.Text = "PROCESS STARTED";
                    lblInfo.ForeColor = Color.Blue;
                    progressBar.Refresh();
                    progressBar.MarqueeAnimationSpeed = 30;
                    progressBar.Visible = true;
                    btnCreate.Enabled = false;
                    Logger.LogToFile("====== Create PNR Started at : " + DateTime.Now.ToString());
                    List<FlightRequest> request = new List<FlightRequest>();
                    request.Add(objFlightRequest);
                    Task.Factory.StartNew(() => CreateBooking(request));
                }
            }
            else
            {
                MessageBox.Show(message);
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            lstPNRsProcessed = new List<PNRsProcessed>();

            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK) // 
            {
                string file = fileDialog.FileName;
                try
                {

                    var sData = new LinqToExcel.ExcelQueryFactory(file);
                    lstFlightRequest = (from row in sData.Worksheet<FlightRequest>("Sheet1") where !string.IsNullOrEmpty(row.Origin) && !string.IsNullOrEmpty(row.Destination) select row).ToList();
                    if (lstFlightRequest != null && lstFlightRequest.Count > 0)
                    {
                        btnProcessPNRs.Enabled = true;
                        lblUploadInfo.Text = string.Format("Total {0} record(s) to process", lstFlightRequest.Count);
                    }
                    else
                    {
                        btnProcessPNRs.Enabled = false;
                        lblUploadInfo.Text = "There are no record in Excel file. Please check.";
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show((ex.InnerException != null ? ex.InnerException.Message.ToString() : ex.Message), "Exception: On browse button click", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }

        private void btnProcessPNRs_Click(object sender, EventArgs e)
        {
            FailedCount = 0;
            ProcessedCount = 0;
            SuccessCount = 0;

            ReceipentEmail = string.Empty;
            frmEmailPopup popup = new frmEmailPopup();
            popup.ShowDialog();
            ReceipentEmail = popup.ReceipentEmail;

            if (!popup.IsClose)
            {
                lblInfo.Text = "PROCESS STARTED";
                lblInfo.ForeColor = Color.Blue;
                progressBar.Refresh();
                progressBar.MarqueeAnimationSpeed = 30;
                progressBar.Visible = true;
                btnCreate.Enabled = false;
                btnProcessPNRs.Enabled = false;
                Logger.LogToFile("====== Create PNR Started at : " + DateTime.Now.ToString());
                Task[] objTask = new Task[1];

                //int i = 0;
                //foreach (var flight in lstFlightRequest)
                //{
                objTask[0] = Task.Factory.StartNew(() => CreateBooking(lstFlightRequest));

                //  i++;

                //}
                Task.Factory.ContinueWhenAll(objTask, FinalWork);
            }


        }

        private void CreateBooking(List<FlightRequest> objFlightRequests)
        {
            foreach (var objFlightRequest in objFlightRequests)
            {
                if (!string.IsNullOrEmpty(objFlightRequest.Origin) && !string.IsNullOrEmpty(objFlightRequest.Destination) && objFlightRequest.TotalPNRsRequired > 0)
                {
                    for (int i = 0; i < objFlightRequest.TotalPNRsRequired; i++)
                    {
                        ProcessedCount++;
                        var token = GetToken();
                        if (!string.IsNullOrEmpty(token))
                        {
                            try
                            {
                                List<PaxWiseFareIdResult> objColPaxWiseFareId = CreateFareQuoteRequest(objFlightRequest.Origin, objFlightRequest.Destination, token,
                                    objFlightRequest.AdtCount, objFlightRequest.ChdCount, objFlightRequest.InfCount, objFlightRequest.DepartureDate,
                                    objFlightRequest.ReturnDate, objFlightRequest.OutboundFlightNo, objFlightRequest.InboundFlightNo,
                                    objFlightRequest.IsRT, objFlightRequest.OutboundCabin,
                                    objFlightRequest.InboundCabin);
                                if (objColPaxWiseFareId != null && objColPaxWiseFareId.Count > 0)
                                {
                                    var lastName = Helper.RandomString(5);

                                    ViewPNR objSummaryPNR = GenerateSummary(objColPaxWiseFareId, token, Convert.ToInt16(objFlightRequest.AdtCount),
                                                          Convert.ToInt16(objFlightRequest.ChdCount), Convert.ToInt16(objFlightRequest.InfCount), lastName,
                                                          objFlightRequest);
                                    List<Voucher> colVoucher = new List<Voucher>();
                                    if (!string.IsNullOrEmpty(objFlightRequest.VoucherNo))
                                    {
                                        colVoucher = GetVoucherInfo(token, objFlightRequest.VoucherNo);
                                    }
                                    if (colVoucher != null && colVoucher.Count > 0 && colVoucher[0].BalanceAmount > 0 || string.IsNullOrEmpty(objFlightRequest.VoucherNo))
                                    {
                                        decimal reservationBalance = 0;
                                        if (!string.IsNullOrEmpty(objFlightRequest.VoucherNo))
                                            reservationBalance = AddVoucher(token, colVoucher[0].VoucherNumFull);

                                        if (reservationBalance <= 0 || string.IsNullOrEmpty(objFlightRequest.VoucherNo))
                                        {
                                            using (ReservationService.ConnectPoint_Reservation objReservationClient = new ReservationService.ConnectPoint_Reservation())
                                            {
                                                ViewPNR PNR = objReservationClient.CreatePNR(new CreatePNR()
                                                {
                                                    SecurityGUID = token,
                                                    CarrierCodes = GetCarrierCodes(),
                                                    ClientIPAddress = string.Empty,
                                                    HistoricUserName = string.Empty,
                                                    ActionType = CreatePNRActionTypes.CommitSummary,
                                                    ReservationInfo = new ReservationInfo() { ConfirmationNumber = string.Empty, SeriesNumber = "299" }

                                                });
                                                if (PNR.Exceptions != null)
                                                {
                                                    foreach (var exception in PNR.Exceptions)
                                                    {
                                                        if (exception.ExceptionCode != 0)
                                                        {
                                                            var radixxException = new RadixxException(
                                                           message: exception.ExceptionDescription,
                                                           radixxExceptionCode: this.ParseErrorCode(exception.ExceptionDescription) ?? exception.ExceptionCode,
                                                           radixxExceptionSource: exception.ExceptionSource,
                                                           type: new StackFrame(1, true).GetType(),
                                                           flydubaiOperation: new StackFrame(1, true).GetMethod().Name,
                                                           securityGuid: token,
                                                           request: "",//DataHelpers.SerializeToString(request),
                                                           responseMessage: "");//DataHelpers.SerializeToString(response));



                                                            throw radixxException;
                                                        }
                                                    }
                                                }
                                                lstPNRsProcessed.Add(new PNRsProcessed()
                                                {
                                                    DepartureDate = objFlightRequest.DepartureDate,
                                                    ReturnDate = (objFlightRequest.IsRT ? objFlightRequest.ReturnDate : DateTime.MinValue),
                                                    Origin = objFlightRequest.Origin,
                                                    Destination = objFlightRequest.Destination,
                                                    PNR = PNR.ConfirmationNumber,
                                                    LastName = lastName,
                                                    FlightNo = objFlightRequest.OutboundFlightNo + (string.IsNullOrEmpty(objFlightRequest.InboundFlightNo) ? " - " + objFlightRequest.InboundFlightNo : string.Empty)
                                                });
                                                SuccessCount++;
                                                Logger.LogToFile("PNR: " + PNR.ConfirmationNumber + ", Lastname: " + lastName + " created successfully.");
                                            }
                                        }
                                        else
                                        {
                                            Logger.LogToFile("Origin: " + objFlightRequest.Origin + ", Destination: " + objFlightRequest.Destination + " not processed.");
                                            Logger.LogToFile(string.Format("Error message: Voucher no: {0} doesn't have enough amount to pay this booking ,", objFlightRequest.VoucherNo));
                                            FailedCount++;
                                        }
                                    }
                                    else
                                    {
                                        Logger.LogToFile("Origin: " + objFlightRequest.Origin + ", Destination: " + objFlightRequest.Destination + " not processed.");
                                        Logger.LogToFile(string.Format("Error message: Voucher no: {0} not valid or Balanced amount is 0 ,", objFlightRequest.VoucherNo));
                                        FailedCount++;
                                    }


                                }
                                else
                                {
                                    Logger.LogToFile("Origin: " + objFlightRequest.Origin + ", Destination: " + objFlightRequest.Destination + " not processed.");
                                    Logger.LogToFile("Error message: Fares not available");
                                    FailedCount++;
                                }
                            }
                            catch (Exception ex)
                            {
                                FailedCount++;
                                Logger.LogToFile("Origin: " + objFlightRequest.Origin + ", Destination: " + objFlightRequest.Destination + " not processed.");
                                Logger.LogToFile("Error message: " + ex.Message);
                            }

                        }
                        else
                        {
                            FailedCount++;
                            Logger.LogToFile("Origin: " + objFlightRequest.Origin + ", Destination: " + objFlightRequest.Destination + " not processed.");
                            Logger.LogToFile("Error message: Security service is not accessible.");
                        }
                    }
                }
            }

            this.progressBar.BeginInvoke((MethodInvoker)delegate()
            {
                progressBar.Visible = false;

                progressBar.Refresh();
                lblInfo.Text = "PROCESS COMPLETED";
                lblInfo.Refresh();
                lblInfo.ForeColor = Color.Green;

                btnCreate.Enabled = true;
            });

            frmProcessReport objfrmProcessReport = new frmProcessReport();
            objfrmProcessReport.DSPNRsProcessed = lstPNRsProcessed;
            objfrmProcessReport.StartPosition = FormStartPosition.CenterParent;
            objfrmProcessReport.Failed = FailedCount;
            objfrmProcessReport.Processed = ProcessedCount;
            objfrmProcessReport.Successful = SuccessCount;
            Invoke((Action)(() => { objfrmProcessReport.ShowDialog(); }));

            Logger.LogToFile("====== Create PNR Process ended at : " + DateTime.Now.ToString());

        }


        #region Radixx calls
        /// <summary>
        /// 1. Get security token
        /// </summary>
        /// <returns></returns>
        public string GetToken()
        {
            SecurityService.CarrierCode [] carrierLst = new SecurityService.CarrierCode[1];
            carrierLst[0] = (new SecurityService.CarrierCode() { AccessibleCarrierCode = "FZ" });
            try
            {
                using (SecurityService.ConnectPoint_Security client = new SecurityService.ConnectPoint_Security())
                {
                    SecurityService.ViewSecurityToken objToken = client.RetrieveSecurityToken(new SecurityService.RetrieveSecurityToken()
                    {
                        CarrierCodes = carrierLst,
                        LogonID = "SPLENDID_FZ_U",
                        Password = "splendid"
                    });
                    if (!String.IsNullOrEmpty(objToken.SecurityToken))
                    {
                        return objToken.SecurityToken;
                    }

                }
            }
            catch (Exception ex)
            { MessageBox.Show("Error: " + ex.Message); }
            return string.Empty;
        }

        /// <summary>
        /// 2. Get farequote request
        /// </summary>
        /// <param name="originAirportCode"></param>
        /// <param name="destAirportCode"></param>
        /// <param name="token"></param>
        /// <param name="adtCount"></param>
        /// <param name="childCount"></param>
        /// <param name="infantCount"></param>
        /// <param name="departureDate"></param>
        /// <param name="returnDate"></param>
        /// <param name="flightNo"></param>
        /// <param name="isRT"></param>
        /// <param name="OBCabin"></param>
        /// <param name="IBCabin"></param>
        /// <returns></returns>
        private List<PaxWiseFareIdResult> CreateFareQuoteRequest(string originAirportCode, string destAirportCode, string token, int adtCount, int childCount, int infantCount,
            DateTime? departureDate, DateTime? returnDate, string OBflightNo, string IBflightNo,
            bool isRT, string OBCabin, string IBCabin)
        {
            List<PaxWiseFareIdResult> colPaxWiseFareIdResult = new List<PaxWiseFareIdResult>();
            PaxWiseFareIdResult objPaxWiseFareId = new PaxWiseFareIdResult();
            if (string.IsNullOrEmpty(OBCabin))
            {
                OBCabin = EnumCabin.Economy.ToString();
            }
            if (string.IsNullOrEmpty(IBCabin))
            {
                IBCabin = EnumCabin.Economy.ToString();
            }
            var OBFareType = (OBCabin.ToLower() == EnumCabin.Economy.ToString().ToLower() ? "Pay To Change" : "Basic");
            var IBFareType = (IBCabin.ToLower() == EnumCabin.Economy.ToString().ToLower() ? "Pay To Change" : "Basic");
            try
            {
                PricingService.CarrierCode[] carrierLst = new PricingService.CarrierCode[1];
                carrierLst[0] = (new PricingService.CarrierCode() { AccessibleCarrierCode = "FZ" });

                using (PricingService.ConnectPoint_Pricing client = new PricingService.ConnectPoint_Pricing())
                {
                    ViewFareQuote ViewFareQuoteResult = client.RetrieveFareQuoteDateRange(new RetrieveFareQuoteByDateRange()
                    {
                        SecurityGUID = token,
                        CarrierCodes = carrierLst,
                        ClientIPAddress = "",
                        CorporationID = -2147483648,
                        CurrencyOfFareQuote = "AED",
                        FareFilterMethod = EnumsFareFilterMethodType.NoCombinabilityRoundtripLowestFarePerFareType,
                        FareGroupMethod = EnumsFareGroupMethodType.WebFareTypes,
                        InventoryFilterMethod = EnumsInventoryFilterMethodType.Available,
                        FareQuoteDetails = GetFareQuoteDetail(originAirportCode, destAirportCode, adtCount, childCount, infantCount, isRT, departureDate, returnDate).ToArray()

                    });


                    if (ViewFareQuoteResult != null && !string.IsNullOrEmpty(OBflightNo) && !string.IsNullOrEmpty(IBflightNo))
                    {

                        if (ViewFareQuoteResult.SegmentDetails != null && ViewFareQuoteResult.SegmentDetails.Count() > 0)
                        {
                            var OBdetail = (from p in ViewFareQuoteResult.SegmentDetails
                                            where p.FlightNum == OBflightNo
                                            select new { p.LFID, p.DepartureDate }).FirstOrDefault();
                            //select fare id for ADT

                            FareType[] fareTypes = (from p in ViewFareQuoteResult.FlightSegments
                                                        where p.LFID == OBdetail.LFID
                                                        select p.FareTypes
                                                 ).FirstOrDefault();
                            FareInfo[] lstFareInfo = (from p in fareTypes
                                                          where p.FareTypeName == OBFareType
                                                          select p.FareInfos
                                                       ).FirstOrDefault();
                            objPaxWiseFareId.LFID = OBdetail.LFID;
                            objPaxWiseFareId.origin = originAirportCode;
                            objPaxWiseFareId.destination = destAirportCode;
                            objPaxWiseFareId.DepartureDate = OBdetail.DepartureDate;
                            objPaxWiseFareId.ADTFareID = (from p in lstFareInfo
                                                          where p.PTCID == 1
                                                          select p.FareID
                                                 ).FirstOrDefault();
                            objPaxWiseFareId.CHDFareID = (from p in lstFareInfo
                                                          where p.PTCID == 6
                                                          select p.FareID
                                                 ).FirstOrDefault();
                            objPaxWiseFareId.INFFareID = (from p in lstFareInfo
                                                          where p.PTCID == 5
                                                          select p.FareID
                                                 ).FirstOrDefault();
                            objPaxWiseFareId.Cabin = OBCabin;
                            objPaxWiseFareId.EnumFlightType = FlightType.Outbound;
                            colPaxWiseFareIdResult.Add(objPaxWiseFareId);
                            if (isRT)
                            {

                                objPaxWiseFareId = new PaxWiseFareIdResult();
                                var RTdetail = (from p in ViewFareQuoteResult.SegmentDetails
                                                where p.FlightNum == IBflightNo
                                                select new { p.LFID, p.DepartureDate }).FirstOrDefault();
                                //select fare id for ADT

                                FareType[] fareTypesRT = (from p in ViewFareQuoteResult.FlightSegments
                                                              where p.LFID == RTdetail.LFID
                                                              select p.FareTypes
                                                     ).FirstOrDefault();
                                FareInfo[] lstFareInfoRT = (from p in fareTypesRT
                                                                where p.FareTypeName == IBFareType
                                                                select p.FareInfos
                                                           ).FirstOrDefault();
                                objPaxWiseFareId.LFID = RTdetail.LFID;
                                objPaxWiseFareId.origin = destAirportCode;
                                objPaxWiseFareId.destination = originAirportCode;
                                objPaxWiseFareId.DepartureDate = RTdetail.DepartureDate;
                                objPaxWiseFareId.ADTFareID = (from p in lstFareInfoRT
                                                              where p.PTCID == 1
                                                              select p.FareID
                                                     ).FirstOrDefault();
                                objPaxWiseFareId.CHDFareID = (from p in lstFareInfoRT
                                                              where p.PTCID == 6
                                                              select p.FareID
                                                     ).FirstOrDefault();
                                objPaxWiseFareId.INFFareID = (from p in lstFareInfoRT
                                                              where p.PTCID == 5
                                                              select p.FareID
                                                     ).FirstOrDefault();
                                objPaxWiseFareId.Cabin = IBCabin;
                                objPaxWiseFareId.EnumFlightType = FlightType.Inbound;
                                colPaxWiseFareIdResult.Add(objPaxWiseFareId);
                            }

                        }
                        else
                            objPaxWiseFareId.Result = "No result";
                    }
                    else
                    {
                        if (ViewFareQuoteResult.SegmentDetails != null && ViewFareQuoteResult.SegmentDetails.Count() > 0)
                        {
                            var OBDetail = (from p in ViewFareQuoteResult.SegmentDetails
                                            where p.Origin == originAirportCode && p.Destination == destAirportCode
                                            select new { p.LFID, p.DepartureDate }).FirstOrDefault();
                            FareType[] fareTypes = (from p in ViewFareQuoteResult.FlightSegments
                                                        where p.LFID == OBDetail.LFID
                                                        select p.FareTypes
                                                 ).FirstOrDefault();
                            FareInfo[] lstFareInfo = (from p in fareTypes
                                                          where p.FareTypeName == OBFareType
                                                          select p.FareInfos
                                                       ).FirstOrDefault();
                            objPaxWiseFareId.LFID = OBDetail.LFID;
                            objPaxWiseFareId.origin = originAirportCode;
                            objPaxWiseFareId.destination = destAirportCode;
                            objPaxWiseFareId.DepartureDate = OBDetail.DepartureDate;
                            objPaxWiseFareId.ADTFareID = (from p in lstFareInfo
                                                          where p.PTCID == 1
                                                          select p.FareID
                                                 ).FirstOrDefault();
                            objPaxWiseFareId.CHDFareID = (from p in lstFareInfo
                                                          where p.PTCID == 6
                                                          select p.FareID
                                                 ).FirstOrDefault();
                            objPaxWiseFareId.INFFareID = (from p in lstFareInfo
                                                          where p.PTCID == 5
                                                          select p.FareID
                                                 ).FirstOrDefault();
                            objPaxWiseFareId.Cabin = OBCabin;
                            objPaxWiseFareId.EnumFlightType = FlightType.Outbound;
                            colPaxWiseFareIdResult.Add(objPaxWiseFareId);

                            if (isRT)
                            {
                                objPaxWiseFareId = new PaxWiseFareIdResult();
                                var RTDetail = (from p in ViewFareQuoteResult.SegmentDetails
                                                where p.Origin == destAirportCode && p.Destination == originAirportCode
                                                select new { p.LFID, p.DepartureDate }).FirstOrDefault();
                                FareType[] fareTypesRT = (from p in ViewFareQuoteResult.FlightSegments
                                                              where p.LFID == RTDetail.LFID
                                                              select p.FareTypes
                                                     ).FirstOrDefault();
                                FareInfo[] lstFareInfoRT = (from p in fareTypesRT
                                                                where p.FareTypeName == IBFareType
                                                                select p.FareInfos
                                                           ).FirstOrDefault();

                                objPaxWiseFareId.LFID = RTDetail.LFID;
                                objPaxWiseFareId.origin = destAirportCode;
                                objPaxWiseFareId.destination = originAirportCode;
                                objPaxWiseFareId.DepartureDate = RTDetail.DepartureDate;
                                objPaxWiseFareId.ADTFareID = (from p in lstFareInfoRT
                                                              where p.PTCID == 1
                                                              select p.FareID
                                                     ).FirstOrDefault();
                                objPaxWiseFareId.CHDFareID = (from p in lstFareInfoRT
                                                              where p.PTCID == 6
                                                              select p.FareID
                                                     ).FirstOrDefault();
                                objPaxWiseFareId.INFFareID = (from p in lstFareInfoRT
                                                              where p.PTCID == 5
                                                              select p.FareID
                                                     ).FirstOrDefault();
                                objPaxWiseFareId.Cabin = IBCabin;
                                objPaxWiseFareId.EnumFlightType = FlightType.Inbound;
                                colPaxWiseFareIdResult.Add(objPaxWiseFareId);
                            }

                        }
                        else
                            objPaxWiseFareId.Result = "No result";
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;


            }
            return colPaxWiseFareIdResult;
        }

        /// <summary>
        /// 2. Get farequote
        /// </summary>
        /// <param name="originAirportCode"></param>
        /// <param name="destAirportCode"></param>
        /// <param name="adtCount"></param>
        /// <param name="childCount"></param>
        /// <param name="infantCount"></param>
        /// <param name="isRT"></param>
        /// <param name="departureDate"></param>
        /// <param name="returnDate"></param>
        /// <returns></returns>
        public List<FareQuoteDetailDateRange> GetFareQuoteDetail(string originAirportCode, string destAirportCode, int adtCount, int childCount, int infantCount, bool isRT,
            DateTime? departureDate, DateTime? returnDate)
        {
            // Adult = 1,
            //Child = 6,
            //Infant = 5
            List<FareQuoteDetailDateRange> colFareQuoteDetailDateRange = new List<FareQuoteDetailDateRange>();
            List<FareQuoteRequestInfo> reqFareQuotes = GetFarequoteRequestInfo(adtCount, childCount, infantCount);

            FareQuoteDetailDateRange objFareQuoteDetailDateRange = new FareQuoteDetailDateRange()
            {
                Origin = originAirportCode,
                Destination = destAirportCode,
                UseAirportsNotMetroGroups = true,
                UseAirportsNotMetroGroupsAsRule = true,
                UseAirportsNotMetroGroupsForFrom = true,
                UseAirportsNotMetroGroupsForTo = true,
                DateOfDepartureStart = departureDate.Value,
                DateOfDepartureEnd = departureDate.Value,
                FareTypeCategory = 1,
                FareClass = string.Empty,
                FareBasisCode = string.Empty,
                Cabin = string.Empty,
                LFID = -2147483648,
                OperatingCarrierCode = "FZ",
                MarketingCarrierCode = "FZ",
                LanguageCode = "en",
                TicketPackageID = string.Empty,
                FareQuoteRequestInfos = reqFareQuotes.ToArray()
            };

            colFareQuoteDetailDateRange.Add(objFareQuoteDetailDateRange);

            if (isRT)
            {
                objFareQuoteDetailDateRange = new FareQuoteDetailDateRange()
                {
                    Origin = destAirportCode,
                    Destination = originAirportCode,
                    UseAirportsNotMetroGroups = true,
                    UseAirportsNotMetroGroupsAsRule = true,
                    UseAirportsNotMetroGroupsForFrom = true,
                    UseAirportsNotMetroGroupsForTo = true,
                    DateOfDepartureStart = returnDate.Value,
                    DateOfDepartureEnd = returnDate.Value,
                    FareTypeCategory = 1,
                    FareClass = string.Empty,
                    FareBasisCode = string.Empty,
                    Cabin = string.Empty,
                    LFID = -2147483648,
                    OperatingCarrierCode = "FZ",
                    MarketingCarrierCode = "FZ",
                    LanguageCode = "en",
                    TicketPackageID = string.Empty,
                    FareQuoteRequestInfos = reqFareQuotes.ToArray()
                };
                colFareQuoteDetailDateRange.Add(objFareQuoteDetailDateRange);
            }


            return colFareQuoteDetailDateRange;
        }

        /// <summary>
        /// 2. Get farequote
        /// </summary>
        /// <param name="adtCount"></param>
        /// <param name="childCount"></param>
        /// <param name="infantCount"></param>
        /// <returns></returns>
        public List<FareQuoteRequestInfo> GetFarequoteRequestInfo(int adtCount, int childCount, int infantCount)
        {
            List<FareQuoteRequestInfo> colFareQuoteRequestInfo = new List<FareQuoteRequestInfo>();

            if (adtCount > 0)
            {
                colFareQuoteRequestInfo.Add(new FareQuoteRequestInfo()//adult
                {
                    PassengerTypeID = 1,
                    TotalSeatsRequired = adtCount
                });
            }
            if (childCount > 0)//child
            {
                colFareQuoteRequestInfo.Add(new FareQuoteRequestInfo()
                {
                    PassengerTypeID = 6,
                    TotalSeatsRequired = childCount
                });
            }
            if (infantCount > 0)//infant
            {
                colFareQuoteRequestInfo.Add(new FareQuoteRequestInfo()
                {
                    PassengerTypeID = 5,
                    TotalSeatsRequired = infantCount
                });
            }

            return colFareQuoteRequestInfo;
        }

        #region 3. Summary creation part

        /// <summary>
        ///  3. Create summary
        /// </summary>
        /// <param name="objColPaxWiseFareId"></param>
        /// <param name="token"></param>
        /// <param name="adtCount"></param>
        /// <param name="childCount"></param>
        /// <param name="infantCount"></param>
        /// <param name="lastName"></param>
        /// <returns></returns>
        public ViewPNR GenerateSummary(List<PaxWiseFareIdResult> objColPaxWiseFareId, string token, int adtCount,
            int childCount, int infantCount, string lastName, FlightRequest objFlightRequest)
        {

            ViewPNR viewPnr = new ViewPNR();
            try
            {
                var request = new SummaryPNR
                {
                    ActionType = SummaryPNRActionTypes.GetSummary,
                    Address = GetEmptyAddress(),
                    CarrierCodes = GetCarrierCodes(),
                    SecurityGUID = token,
                    ClientIPAddress = string.Empty,
                    SecurityToken = token,
                    ReceiptLanguageID = "1",
                    ReservationInfo = new ReservationInfo
                    {
                        ConfirmationNumber = "NA",
                        SeriesNumber = "299"
                    },
                    User = "WEB2_LIVE",
                    CarrierCurrency = ReservationService.EnumerationsCurrencyCodeTypes.AED,
                    ContactInfos = GetPassengerContactInfos().ToArray(),
                    DisplayCurrency = ReservationService.EnumerationsCurrencyCodeTypes.AED,
                    Payments = GetEmptyPayments().ToArray(),
                };
                List<Person> lstPersons = GetPersons(adtCount, childCount, infantCount, lastName);
                request.Passengers = lstPersons.ToArray();
                request.Segments = GetSegments(token, lstPersons,
                    objColPaxWiseFareId, objFlightRequest).ToArray();
                using (ReservationService.ConnectPoint_Reservation client = new ReservationService.ConnectPoint_Reservation())
                {
                    viewPnr = client.SummaryPNR(request);

                    if (viewPnr.Exceptions != null)
                    {
                        foreach (var exception in viewPnr.Exceptions)
                        {
                            if (exception.ExceptionCode != 0)
                            {
                                var radixxException = new RadixxException(
                               message: exception.ExceptionDescription,
                               radixxExceptionCode: this.ParseErrorCode(exception.ExceptionDescription) ?? exception.ExceptionCode,
                               radixxExceptionSource: exception.ExceptionSource,
                               type: new StackFrame(1, true).GetType(),
                               flydubaiOperation: new StackFrame(1, true).GetMethod().Name,
                               securityGuid: token,
                               request: "",//DataHelpers.SerializeToString(request),
                               responseMessage: "");//DataHelpers.SerializeToString(response));



                                throw radixxException;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return viewPnr;
        }

        public static Address GetEmptyAddress()
        {
            return new Address
            {
                Address1 = string.Empty,
                Address2 = string.Empty,
                AreaCode = string.Empty,
                City = string.Empty,
                Country = string.Empty,
                CountryCode = string.Empty,
                Display = string.Empty,
                PhoneNumber = string.Empty,
                Postal = string.Empty,
                State = string.Empty
            };
        }

        public static ReservationService.CarrierCode[] GetCarrierCodes()
        {
            return new ReservationService.CarrierCode[] { new ReservationService.CarrierCode { AccessibleCarrierCode = "FZ" } };
        }

        private static List<Payment> GetEmptyPayments()
        {
            return new List<Payment>();
        }

        private List<ContactInfo> GetPassengerContactInfos()
        {
            var contactInfos = new List<ContactInfo>();
            int count = 0;
            contactInfos.Add(new ContactInfo
            {
                ContactID = count++,
                PersonOrgID = -1,
                ContactType = EnumerationsContactTypes.Email,
                AreaCode = string.Empty,
                CountryCode = string.Empty,
                ContactField = ReceipentEmail,
                Display = string.Empty,
                Extension = string.Empty,
                PhoneNumber = string.Empty,
                PreferredContactMethod = true
            });
            contactInfos.Add(new ContactInfo
            {
                ContactID = count++,
                PersonOrgID = -1,
                ContactType = EnumerationsContactTypes.MobilePhone,
                AreaCode = string.Empty,
                CountryCode = string.Empty,
                ContactField = "123456789",
                Display = string.Empty,
                Extension = string.Empty,
                PhoneNumber = string.Empty
            });
            return contactInfos;
        }


        private List<Person> GetPersons(int adtCount, int childCount, int infantCount, string lastName)
        {
            var persons = new List<Person>();
            var paxId = -1;
            char a = 'a';
            for (int i = 1; i <= adtCount; i++)
            {


                persons.Add(
                new Person
                {
                    PersonOrgID = paxId,
                    FirstName = "John" + a++,
                    MiddleName = "",
                    LastName = lastName,
                    Address = GetEmptyAddress(),
                    Age = int.MinValue,
                    DOB = DateTime.MinValue,
                    Gender = EnumerationsGenderTypes.Unknown,
                    Title = "Mr",
                    NationalityLaguageID = 1,
                    RelationType = EnumerationsRelationshipTypes.Self,
                    WBCID = int.MinValue,
                    PTCID = 1,
                    PTC = string.Empty,
                    TravelsWithPersonOrgID = int.MinValue,
                    UseInventory = false,
                    MarketingOptIn = true,
                    KnownTravelerNumber = string.Empty,
                    RedressNumber = string.Empty,
                    Comments = string.Empty,
                    Company = string.Empty,
                    Passport = string.Empty,
                    Nationality = "1",
                    ProfileId = long.MinValue,
                    IsPrimaryPassenger = (paxId == -1 ? true : false),
                    ContactInfos = GetContactInfo(paxId).ToArray()
                });
                paxId = paxId - 1;
            }
            a = 'a';
            for (int i = 1; i <= childCount; i++)
            {
                persons.Add(
                new Person
                {
                    PersonOrgID = paxId,
                    FirstName = "Kid" + a++,
                    MiddleName = "",
                    LastName = lastName,
                    Address = GetEmptyAddress(),
                    Age = int.MinValue,
                    DOB = new DateTime(2009, 1, 1),
                    Gender = EnumerationsGenderTypes.Unknown,
                    Title = "Mstr",
                    NationalityLaguageID = 1,
                    RelationType = EnumerationsRelationshipTypes.Self,
                    WBCID = int.MinValue,
                    PTCID = 6,
                    PTC = string.Empty,
                    TravelsWithPersonOrgID = int.MinValue,
                    UseInventory = false,
                    MarketingOptIn = true,
                    KnownTravelerNumber = string.Empty,
                    RedressNumber = string.Empty,
                    Comments = string.Empty,
                    Company = string.Empty,
                    Passport = string.Empty,
                    Nationality = "1",
                    ProfileId = long.MinValue,
                    IsPrimaryPassenger = false,
                    ContactInfos = new List<ContactInfo>().ToArray()
                });
                paxId = paxId - 1;
            }
            a = 'a';
            for (int i = 1; i <= infantCount; i++)
            {
                persons.Add(
                new Person
                {
                    PersonOrgID = paxId,
                    FirstName = "Baby" + a++,
                    MiddleName = "",
                    LastName = lastName,
                    Address = GetEmptyAddress(),
                    Age = int.MinValue,
                    DOB = new DateTime(2015, 1, 1),
                    Gender = EnumerationsGenderTypes.Unknown,
                    Title = "Mstr",
                    NationalityLaguageID = 1,
                    RelationType = EnumerationsRelationshipTypes.Self,
                    WBCID = int.MinValue,
                    PTCID = 5,
                    PTC = string.Empty,
                    TravelsWithPersonOrgID = -1,
                    UseInventory = false,
                    MarketingOptIn = true,
                    KnownTravelerNumber = string.Empty,
                    RedressNumber = string.Empty,
                    Comments = string.Empty,
                    Company = string.Empty,
                    Passport = string.Empty,
                    Nationality = "1",
                    ProfileId = long.MinValue,
                    IsPrimaryPassenger = false,
                    ContactInfos = new List<ContactInfo>().ToArray()
                });
                paxId = paxId - 1;
            }


            return persons;
        }

        private List<ContactInfo> GetContactInfo(int paxId)
        {
            List<ContactInfo> colContats = new List<ContactInfo>();
            colContats.Add(new ContactInfo()
            {
                ContactID = 0,
                PersonOrgID = paxId,
                ContactType = EnumerationsContactTypes.Email,
                AreaCode = string.Empty,
                CountryCode = string.Empty,
                ContactField = ReceipentEmail,
                Display = string.Empty,
                Extension = string.Empty,
                PhoneNumber = string.Empty,
                PreferredContactMethod = true
            });
            return colContats;
        }

        public List<Segment> GetSegments(string token, List<Person> lstPersons, List<PaxWiseFareIdResult> objPaxWiseFareId, FlightRequest objFlightRequest)
        {

            List<SeatsInfo> objSeatsInfo = new List<SeatsInfo>();
            var segments = new List<Segment>();
            foreach (var item in objPaxWiseFareId)
            {

                List<AlreadyTakenSeat> colAlreadyUsedInThisProcessSpecialService = new List<AlreadyTakenSeat>();
                if (objFlightRequest.Seating)
                {
                    objSeatsInfo = RetrieveSeatsAvailability(token, item.DepartureDate, item.LFID, item.Cabin);
                }
                foreach (var person in lstPersons)
                {

                    List<SpacialServiceAndSeat> colSpecialService = new List<SpacialServiceAndSeat>();
                    SeatCharge seat = new SeatCharge();
                    //Seating
                    //if (!objFlightRequest.Seating)
                    //    return colSpecialService;
                    //else 
                    if (objFlightRequest.Seating && person.PTCID != 5)// Not an infant
                    {
                        colSpecialService = GetAvailableSeatForAllLegs(objSeatsInfo, item, person, ref colAlreadyUsedInThisProcessSpecialService);//get seat from all physical flight for the person i.e for OW per person

                    }
                    Segment segment = new Segment();
                    segment.FareInformationID = GetFareId(item, person.PTCID);
                    segment.MarketingCode = "NA";
                    segment.StoreFrontID = "NA";
                    segment.PersonOrgID = person.PersonOrgID;
                    segment.SpecialServices = GetSpecialService(objFlightRequest, item, person).ToArray(); //new List<SpecialService>(), //GetSpecialServices(p, localFlight, currency),

                    List<Seat> seatassigned = new List<Seat>();

                    if (colSpecialService != null && colSpecialService.Count > 0)
                    {
                        seatassigned = new List<Seat>();
                        foreach (var service in colSpecialService)
                        {
                            
                            if (service.Special_Service.CodeType != string.Empty)
                            {
                                var serviceList = segment.SpecialServices == null ? new List<SpecialService>() : segment.SpecialServices.ToList();
                                serviceList.Add(service.Special_Service);
                                segment.SpecialServices = serviceList.ToArray();
                            }

                            seatassigned.Add(new Seat()
                            {
                                DepartureDate = item.DepartureDate.Date,
                                LogicalFlightID = item.LFID,
                                PersonOrgID = person.PersonOrgID,
                                RowNumber = Convert.ToString(service.Seat_Charges.RowNumber),
                                PhysicalFlightID = service.PhysicalFlightID,
                                SeatSelected = service.Seat_Charges.Seat
                            });

                        }

                    }


                    segment.Seats = seatassigned.ToArray();
                    segments.Add(segment);

                }

            }

            return segments;
        }

        private List<SpecialService> GetSpecialService(FlightRequest objFlightRequest, PaxWiseFareIdResult objPaxWiseFareIdResult, Person person)
        {
            List<SpecialService> colSpecialService = new List<SpecialService>();
            if (person.PTCID != 5)
            {
                //Baggage
                var BaggageRequested = objPaxWiseFareIdResult.EnumFlightType == FlightType.Inbound ? objFlightRequest.InboundBaggage : objFlightRequest.OutboundBaggage;

                if (!String.IsNullOrEmpty(BaggageRequested) && objPaxWiseFareIdResult.Cabin == EnumCabin.Economy.ToString())
                {
                    colSpecialService.Add(new SpecialService()
                    {
                        CodeType = BaggageRequested,
                        ServiceID = -2147483648,
                        SSRCategory = 99,
                        LogicalFlightID = objPaxWiseFareIdResult.LFID,
                        DepartureDate = objPaxWiseFareIdResult.DepartureDate.Date,
                        Amount = (BaggageRequested == "BAGB" ? 50 : (BaggageRequested == "BAGL" ? 100 : 200)),
                        OverrideAmount = false,
                        CurrencyCode = ReservationService.EnumerationsCurrencyCodeTypes.AED,
                        PersonOrgID = person.PersonOrgID,
                        ChargeComment = string.Empty

                    });
                }


                //Meal
                var MealRequested = objPaxWiseFareIdResult.EnumFlightType == FlightType.Inbound ? objFlightRequest.InboundMeal : objFlightRequest.OutboundMeal;
                if (!String.IsNullOrEmpty(MealRequested))
                {

                    colSpecialService.Add(new SpecialService()
                    {
                        CodeType = MealRequested,
                        ServiceID = -2147483648,
                        SSRCategory = 162,
                        LogicalFlightID = objPaxWiseFareIdResult.LFID,
                        DepartureDate = objPaxWiseFareIdResult.DepartureDate.Date,
                        Amount = 50,
                        OverrideAmount = false,
                        CurrencyCode = ReservationService.EnumerationsCurrencyCodeTypes.AED,
                        PersonOrgID = person.PersonOrgID,
                        ChargeComment = string.Empty

                    });
                }

                //IFE
                if (objFlightRequest.IFE && objPaxWiseFareIdResult.Cabin == EnumCabin.Economy.ToString())
                {
                    colSpecialService.Add(new SpecialService()
                    {
                        CodeType = "IFPP",
                        ServiceID = -2147483648,
                        SSRCategory = 141,
                        LogicalFlightID = objPaxWiseFareIdResult.LFID,
                        DepartureDate = objPaxWiseFareIdResult.DepartureDate.Date,
                        Amount = 30,
                        OverrideAmount = false,
                        CurrencyCode = ReservationService.EnumerationsCurrencyCodeTypes.AED,
                        PersonOrgID = person.PersonOrgID,
                        ChargeComment = string.Empty

                    });
                }


            }
            return colSpecialService;
        }

        private int GetFareId(PaxWiseFareIdResult objPaxFare, int PTCID)
        {
            switch (PTCID)
            {
                case 1: return objPaxFare.ADTFareID;
                case 6: return objPaxFare.CHDFareID;
                case 5: return objPaxFare.ADTFareID;
                default: return -1;
            }
        }

        #endregion

        /// <summary>
        /// 4. Get voucher information
        /// </summary>
        /// <param name="token"></param>
        /// <param name="VoucherNo"></param>
        /// <returns></returns>
        public List<Voucher> GetVoucherInfo(string token, string VoucherNo)
        {
            var vouchers = new List<Voucher>();
            string voucherString =
                    string.Format("<VoucherRequests><VoucherRequest><VoucherNumber>{0}</VoucherNumber></VoucherRequest></VoucherRequests>",
                        VoucherNo);

            using (BookingTPAPIService.RadixxBooking client = new BookingTPAPIService.RadixxBooking())
            {
                var response = client.GetVoucherInfo(token, voucherString);
                var doc = new XmlDocument();
                doc.LoadXml(response);
                var namespaceManager = new XmlNamespaceManager(doc.NameTable);
                namespaceManager.AddNamespace("radixx", "http://tempuri.org/VouchersResponse.xsd");


                foreach (XmlNode node in doc.SelectNodes("/radixx:VoucherResponses/radixx:VoucherResponse", namespaceManager))
                {
                    if (node.SelectSingleNode("radixx:NotFound", namespaceManager).InnerText == "false")
                    {
                        vouchers.Add(new Voucher
                        {
                            BalanceAmount = decimal.Parse(node.SelectSingleNode("radixx:BalanceAmount", namespaceManager).InnerText, System.Globalization.CultureInfo.InvariantCulture),
                            VoucherPW = node.SelectSingleNode("radixx:VoucherPW", namespaceManager).InnerText,
                            VoucherNumFull = node.SelectSingleNode("radixx:VoucherNumberFull", namespaceManager).InnerText
                        });

                    }
                }
            }
            return vouchers;
        }

        /// <summary>
        /// 5. Add voucher
        /// </summary>
        /// <param name="token"></param>
        /// <param name="VoucherNo"></param>
        /// <returns></returns>
        private decimal AddVoucher(string token, string VoucherNo)
        {
            decimal reservationBalance = 0;
            using (BookingTPAPIService.RadixxBooking client = new BookingTPAPIService.RadixxBooking())
            {
                var response = client.ResAddVoucher(token, VoucherNo);

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(response);
                XmlNode voucherXML = doc.SelectSingleNode("/ReservationBO/Reservation/Vouchers/Voucher[VoucherNumberFull='" + VoucherNo + "'][IsNew='true'][last()]");

                var voucherPaymentId = int.Parse(voucherXML.SelectSingleNode("VoucherNumber").InnerText);



                reservationBalance = decimal.Parse(doc.SelectSingleNode("/ReservationBO/Reservation/ReservationBalance").InnerText, System.Globalization.CultureInfo.InvariantCulture);


            }
            return reservationBalance;
        }


        private List<SeatsInfo> RetrieveSeatsAvailability(string token, DateTime departureDate, int LFID, string cabin)
        {
            List<SeatsInfo> colSeatsInfo = new List<SeatsInfo>();

            using (FlightService.ConnectPoint_Flight client = new FlightService.ConnectPoint_Flight())
            {
                ViewSeatAvailability availability = client.RetrieveSeatAvailabilityList(new RetrieveSeatAvailabilityList()
                {
                    CabinName = string.Empty,
                    CarrierCodes = new List<FlightService.CarrierCode> { new FlightService.CarrierCode { AccessibleCarrierCode = "FZ" } }.ToArray(),
                    Currency = "AED",
                    DepartureDate = departureDate.Date,
                    LogicalFlightID = LFID,
                    SecurityGUID = token,
                    UTCOffset = 0
                });

                if (availability != null && availability.PhysicalFlights != null && availability.PhysicalFlights.Count() > 0)
                {
                    foreach (var item in availability.PhysicalFlights)
                    {
                        SeatsInfo objSeatsInfo = new SeatsInfo();
                        var v = (from p in item.Cabins
                                 where p.CabinName == cabin.ToUpper()
                                 select new { p.SeatAssignments, p.SeatCharges }).FirstOrDefault();
                        objSeatsInfo.PhysicalFlightID = item.PhysicalFlightID;
                        objSeatsInfo.IsCircular = item.IsCircularFlight;
                        if (v.SeatAssignments != null)
                            objSeatsInfo.Seat_Assignment = v.SeatAssignments.ToList();
                        if (v.SeatCharges != null)
                            objSeatsInfo.Seat_Charges = v.SeatCharges.ToList();
                        colSeatsInfo.Add(objSeatsInfo);
                    }


                }

            }
            return colSeatsInfo;
        }

        private List<SpacialServiceAndSeat> GetAvailableSeatForAllLegs(List<SeatsInfo> objSeatsInfo, PaxWiseFareIdResult item, Person person, ref List<AlreadyTakenSeat> AlreadyTakenSeat)
        {
            AlreadyTakenSeat previousSeat = new CreatePNR_AutomationApp.AlreadyTakenSeat();
            List<SpacialServiceAndSeat> colSpecialService = new List<SpacialServiceAndSeat>();
            foreach (var seatinfo in objSeatsInfo.ToArray())
            {
                if (seatinfo.IsCircular && previousSeat != null && previousSeat.PhysicalFlightID > 0 && seatinfo.PhysicalFlightID > 0)
                {
                    SeatCharge seata = new SeatCharge();
                    if (seatinfo.Seat_Charges != null)
                    {
                        seata = (from p in seatinfo.Seat_Charges
                                 where previousSeat.Row == p.RowNumber && previousSeat.SeatNumber == p.Seat
                                 select p).FirstOrDefault();
                    }
                    if (seata != null && seata.RowNumber != 0 && seata.Seat != string.Empty)
                    {
                        colSpecialService.Add(new SpacialServiceAndSeat()
                        {
                            Special_Service = new SpecialService()
                            {
                                CodeType = seata.ServiceCode.Trim(),
                                ServiceID = -2147483648,
                                SSRCategory = 21,
                                LogicalFlightID = item.LFID,
                                DepartureDate = item.DepartureDate.Date,
                                Amount = seata.Amount,
                                OverrideAmount = false,
                                CurrencyCode = ReservationService.EnumerationsCurrencyCodeTypes.AED,
                                PersonOrgID = person.PersonOrgID,
                                ChargeComment = string.Empty
                            },
                            Seat_Charges = seata,
                            PhysicalFlightID = seatinfo.PhysicalFlightID
                        });
                        AlreadyTakenSeat.Add(new AlreadyTakenSeat()
                        {
                            PhysicalFlightID = seatinfo.PhysicalFlightID,
                            Row = seata.RowNumber,
                            SeatNumber = seata.Seat
                        });
                    }
                    else
                    {
                        colSpecialService.Add(new SpacialServiceAndSeat()
                        {
                            Special_Service = new SpecialService()
                            {
                                CodeType = previousSeat.ServiceCode.Trim(),
                                ServiceID = -2147483648,
                                SSRCategory = 21,
                                LogicalFlightID = item.LFID,
                                DepartureDate = item.DepartureDate.Date,
                                Amount = previousSeat.Amount,
                                OverrideAmount = false,
                                CurrencyCode = ReservationService.EnumerationsCurrencyCodeTypes.AED,
                                PersonOrgID = person.PersonOrgID,
                                ChargeComment = string.Empty
                            },
                            Seat_Charges = previousSeat.Seat_Charges,
                            PhysicalFlightID = seatinfo.PhysicalFlightID
                        });
                        AlreadyTakenSeat.Add(new AlreadyTakenSeat()
                        {
                            PhysicalFlightID = seatinfo.PhysicalFlightID,
                            Row = previousSeat.Row,
                            SeatNumber = previousSeat.SeatNumber
                        });
                    }
                }
                else
                {
                    SeatCharge seat = new SeatCharge();
                    List<CreatePNR_AutomationApp.FlightService.SeatAssignment> assignedSeats = seatinfo.Seat_Assignment;
                    if (assignedSeats == null)
                        assignedSeats = new List<FlightService.SeatAssignment>();
                    if (AlreadyTakenSeat != null && AlreadyTakenSeat.Count > 0)
                    {
                        var seatForPhysicalFlight = (from p in AlreadyTakenSeat
                                                     where p.PhysicalFlightID == seatinfo.PhysicalFlightID
                                                     select p).ToList();
                        foreach (var takenitem in seatForPhysicalFlight)
                        {

                            assignedSeats.Add(new FlightService.SeatAssignment()
                            {
                                Seat = takenitem.SeatNumber,
                                RowNumber = takenitem.Row
                            });


                        }

                    }
                    if (assignedSeats != null && assignedSeats.Count > 0)
                    {
                        //seat = (from p in seatinfo.Seat_Charges
                        //        join
                        //            c in assignedSeats on
                        //            new { p.Seat, p.RowNumber } equals new { c.Seat, c.RowNumber }
                        //        where (c.RowNumber != p.RowNumber && c.Seat != p.Seat)
                        //        select p).FirstOrDefault();
                        if (seatinfo.Seat_Charges != null)
                            seat = (from p in seatinfo.Seat_Charges
                                    where !assignedSeats.Any(x => x.RowNumber == p.RowNumber && x.Seat == p.Seat)
                                    select p).FirstOrDefault();


                    }
                    else
                    {
                        if (seatinfo.Seat_Charges != null)
                            seat = (from p in seatinfo.Seat_Charges
                                    select p).FirstOrDefault();
                    }
                    if (seat != null && seat.RowNumber != 0 && seat.Seat != string.Empty)
                    {

                        colSpecialService.Add(new SpacialServiceAndSeat()
                        {
                            Special_Service = new SpecialService()
                            {
                                CodeType = seat.ServiceCode.Trim(),
                                ServiceID = -2147483648,
                                SSRCategory = 21,
                                LogicalFlightID = item.LFID,
                                DepartureDate = item.DepartureDate.Date,
                                Amount = seat.Amount,
                                OverrideAmount = false,
                                CurrencyCode = ReservationService.EnumerationsCurrencyCodeTypes.AED,
                                PersonOrgID = person.PersonOrgID,
                                ChargeComment = string.Empty
                            },
                            Seat_Charges = seat,
                            PhysicalFlightID = seatinfo.PhysicalFlightID
                        });
                        AlreadyTakenSeat.Add(new AlreadyTakenSeat()
                        {
                            PhysicalFlightID = seatinfo.PhysicalFlightID,
                            Row = seat.RowNumber,
                            SeatNumber = seat.Seat
                        });
                        previousSeat.Seat_Charges = seat;
                        previousSeat.ServiceCode = seat.ServiceCode;
                        previousSeat.Amount = seat.Amount;
                        previousSeat.PhysicalFlightID = seatinfo.PhysicalFlightID;
                        previousSeat.Row = seat.RowNumber;
                        previousSeat.SeatNumber = seat.Seat;

                    }
                }
            }
            return colSpecialService;

        }
        #endregion

        protected int? ParseErrorCode(string exceptionDescription)
        {
            if (Regex.IsMatch(exceptionDescription, @"ERR\d{4}"))
            {
                var matches = Regex.Matches(exceptionDescription, @"ERR\d{4}");
                return int.Parse(matches[matches.Count - 1].Captures[0].Value.Remove(0, 3));
            }

            return null;
        }

        #region Other Form events

        private void dtDepartureDate_ValueChanged(object sender, EventArgs e)
        {
            dtArrivalDate.MinDate = dtDepartureDate.Value;
            dtArrivalDate.Value = dtDepartureDate.Value.AddDays(3);
        }

        private void chkRT_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRT.Checked) { dtArrivalDate.Enabled = true; } else dtArrivalDate.Enabled = false;
        }


        private void FinalWork(Task[] tasks)
        {
            if (tasks.All(t => t.Status == TaskStatus.RanToCompletion))
            {
                Logger.LogToFile("======== Create PNR process ended at :" + DateTime.Now.ToString());

                this.progressBar.BeginInvoke((MethodInvoker)delegate()
                {
                    btnCreate.Enabled = true;
                    btnProcessPNRs.Enabled = true;
                    progressBar.Visible = false;
                    progressBar.Refresh();
                    lblInfo.Text = "PROCESS COMPLETED";
                    lblInfo.Refresh();
                    lblInfo.ForeColor = Color.Green;
                    lblUploadInfo.Text = string.Empty;
                });

            }
        }

        private void lnkOpenLog_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(Environment.CurrentDirectory + @"\Log\");

        }
        #endregion

        private void btnClear_Click(object sender, EventArgs e)
        {
            cmbDestination.Focus();
            dtDepartureDate.MinDate = DateTime.Now;
            dtArrivalDate.MinDate = DateTime.Now;
            FailedCount = 0;
            ProcessedCount = 0;
            SuccessCount = 0;
            lstFlightRequest = new List<FlightRequest>();
            lblUploadInfo.Text = string.Empty;
            btnProcessPNRs.Enabled = false;
            cmbOBCabin.SelectedIndex = 0;
            cmbIBCabin.SelectedIndex = 0;
            FailedCount = 0;
            ProcessedCount = 0;
            SuccessCount = 0;
            cmbDestination.Text = string.Empty;
            txtOrigin.Text = "DXB";
            txtAdtCount.Value = 1;
            txtChildCount.Value = 0;
            txtInfantCount.Value = 0;
            txtOBFlightNo.Text = string.Empty;
            txtIBFlightNo.Text = string.Empty;
            txtVoucherNo.Text = string.Empty;
            txtPNRs.Value = 1;
            chkRT.Checked = false;
            cmbOBBaggage.SelectedIndex = 0;
            cmbIBBaggage.SelectedIndex = 0;
            cmbOBMeal.SelectedIndex = 0;
            cmbIBMeal.SelectedIndex = 0;
        }


        private string IsValid()
        {
            string validation = string.Empty;
            if (string.IsNullOrEmpty(cmbDestination.Text))
            {
                validation += "Destination is required." + Environment.NewLine;
            }
            if (chkRT.Checked && !string.IsNullOrEmpty(txtOBFlightNo.Text))
            {
                if (string.IsNullOrEmpty(txtIBFlightNo.Text.Trim()))
                {
                    validation += "Inbound Flight No required." + Environment.NewLine;
                }
            }
            if (chkRT.Checked && !string.IsNullOrEmpty(txtIBFlightNo.Text))
            {
                if (string.IsNullOrEmpty(txtOBFlightNo.Text.Trim()))
                {
                    validation += "Outbound Flight No required." + Environment.NewLine;
                }
            }
            return validation;
        }

        private void txtOrigin_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtOrigin.Text))
            {
                txtOrigin.Text = "DXB";
            }
            txtOrigin.Text = txtOrigin.Text.ToUpper();
        }

        private void txtDestination_TextChanged(object sender, EventArgs e)
        {
            cmbDestination.Text = cmbDestination.Text.ToUpper();
        }



        private void lnkDownload_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WebClient obj = new WebClient();
            string applicationDirectory = Application.ExecutablePath;
            obj.DownloadFile(Path.GetDirectoryName(Application.ExecutablePath) + "\\Template\\Records.xlsx", "Template");
        }

        private void BuildBaggage(ComboBox cmbCabin, ComboBox cmbBaggage)
        {
            if (!string.IsNullOrEmpty(cmbCabin.Text))
            {
                if (cmbCabin.Text == EnumCabin.Economy.ToString())
                {
                    cmbBaggage.Items.Clear();
                    cmbBaggage.Items.Add("");
                    cmbBaggage.Items.Add("BAGB");
                    cmbBaggage.Items.Add("BAGL");
                    cmbBaggage.Items.Add("BAGX");
                }
                else
                {
                    cmbBaggage.Items.Clear();
                    cmbBaggage.Items.Add("");
                }
            }
        }
        private void BuildMeal(ComboBox cmbCabin, ComboBox cmbMeal)
        {

            if (!string.IsNullOrEmpty(cmbCabin.Text))
            {
                if (cmbCabin.Text == EnumCabin.Economy.ToString())
                {
                    cmbMeal.Items.Clear();
                    cmbMeal.Items.Add("");
                    for (int i = 0; i < EconomyMeals.Count(); i++)
                    {
                        cmbMeal.Items.Add(EconomyMeals[i]);

                    }


                }
                else
                {
                    cmbMeal.Items.Clear();
                    cmbMeal.Items.Add("");
                    for (int i = 0; i < BusinessMeals.Count(); i++)
                    {
                        cmbMeal.Items.Add(BusinessMeals[i]);

                    }
                }
            }
        }

        private void cmbOBCabin_SelectedValueChanged(object sender, EventArgs e)
        {
            BuildBaggage(cmbOBCabin, cmbOBBaggage);
            BuildMeal(cmbOBCabin, cmbOBMeal);
        }
        private void cmbIBCabin_SelectedValueChanged(object sender, EventArgs e)
        {
            BuildBaggage(cmbIBCabin, cmbIBBaggage);
            BuildMeal(cmbIBCabin, cmbIBMeal);
        }
    }


}
