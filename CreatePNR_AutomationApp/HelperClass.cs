using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CreatePNR_AutomationApp.FlightService;
using CreatePNR_AutomationApp.ReservationService;

namespace CreatePNR_AutomationApp
{
    public static class Helper
    {
        private static readonly Random _rng = new Random();
        private const string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string RandomString(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }
    }
    public class PNRsProcessed
    {
        public PNRsProcessed()
        {
        }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public string PNR { get; set; }
        public string LastName { get; set; }
        public string FlightNo { get; set; }
    }

    public class FlightRequest
    {
        public string Origin { get; set; }
        public string Destination { get; set; }
        public bool IsRT { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int AdtCount { get; set; }
        public int ChdCount { get; set; }
        public int InfCount { get; set; }
        public int TotalPNRsRequired { get; set; }
        public string OutboundFlightNo { get; set; }
        public string InboundFlightNo { get; set; }
        public string VoucherNo { get; set; }
        public string OutboundCabin { get; set; }
        public string InboundCabin { get; set; }
        public string OutboundBaggage { get; set; }
        public string InboundBaggage { get; set; }
        public string OutboundMeal { get; set; }
        public string InboundMeal { get; set; }
        public bool IFE { get; set; }
        public bool Seating { get; set; }
    }


    public enum Status
    {
        Success,
        Error
    }
    public enum EnumCabin
    {
        Economy,
        Business
    }
    public enum FlightType
    {
        Outbound,
        Inbound
    }
    public class PaxWiseFareIdResult
    {
        public PaxWiseFareIdResult()
        {
            this.OperationResult = Status.Success;
        }
        public string Result { get; set; }
        public Status OperationResult { get; set; }
        public int ADTFareID { get; set; }
        public int CHDFareID { get; set; }
        public int INFFareID { get; set; }
        public string origin { get; set; }
        public string destination { get; set; }
        public int LFID { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Cabin { get; set; }
        public FlightType EnumFlightType { get; set; }
    }

    public class SeatsInfo
    {
        public SeatsInfo() { }
        public int PhysicalFlightID { get; set; }
        public bool IsCircular { get; set; }
        public List<FlightService.SeatAssignment> Seat_Assignment { get; set; }
        public List<FlightService.SeatCharge> Seat_Charges { get; set; }
    }

    public class SpacialServiceAndSeat
    {
        public SpacialServiceAndSeat() { }
        public int PhysicalFlightID { get; set; }
        public SpecialService Special_Service { get; set; }
        public SeatCharge Seat_Charges { get; set; }
    }

    public class AlreadyTakenSeat {
        public AlreadyTakenSeat() { }
        public string ServiceCode { get; set; }
        public decimal Amount { get; set; }
        public SeatCharge Seat_Charges { get; set; }
        public int PhysicalFlightID { get; set; }
        public string SeatNumber { get; set; }
        public long Row { get; set; }
    }

    /// <summary>
    /// Radixx Exception.
    /// </summary>
    public class RadixxException : ApplicationException
    {
        public readonly Type Type;

        public RadixxException(Type type, Exception innerException)
            : this("Radixx Service Fault", type, innerException)
        {
        }

        public RadixxException(string message, Type type, Exception innerException)
            : base(message, innerException)
        {
            this.Type = type;
        }

        public RadixxException(
            string message,
            Type type,
            int radixxExceptionCode,
            string radixxExceptionSource,
            string flydubaiOperation,
            string securityGuid,
            string request,
            string responseMessage)
            : base(message)
        {
            this.RadixxExceptionCode = radixxExceptionCode;
            this.RadixxExceptionSource = radixxExceptionSource;
            this.Type = type;
            this.FlyDubaiOperation = flydubaiOperation;
            this.SecurityGuid = securityGuid;
            this.RequestMessage = request;
            this.ResponseMessage = responseMessage;
        }

        public string FlyDubaiOperation { get; set; }

        public int RadixxExceptionCode { get; set; }

        public string RadixxExceptionFormattedCode
        {
            get
            {
                // pad numbers less than 4 chars with leading zeros.
                return "ERR" + (this.RadixxExceptionCode < 10000 ? string.Format("{0:D4}", this.RadixxExceptionCode) : this.RadixxExceptionCode.ToString());
            }
        }

        public string RadixxExceptionSource { get; set; }

        public string SecurityGuid { get; set; }

        public string ProfileId { get; set; }

        public string RequestMessage { get; set; }

        public string ResponseMessage { get; set; }
    }
    public static class Logger
    {
        public static void LogToFile(string message)
        {

            StreamWriter LogWriter;

            string sLogPath = Path.Combine(Environment.CurrentDirectory + @"\Log\");//System.Configuration.ConfigurationManager.AppSettings["LogFilePath"].ToString();

            StringBuilder logfilename = new StringBuilder();
            logfilename.Append(sLogPath);

            logfilename.Append(String.Format("{0:0000}", DateTime.Now.Year));
            logfilename.Append("_");
            logfilename.Append(String.Format("{0:00}", DateTime.Now.Month));
            logfilename.Append("_");
            logfilename.Append(String.Format("{0:00}", DateTime.Now.Day));
            logfilename.Append(".txt");

            try
            {
                if (!File.Exists(logfilename.ToString()))
                {
                    if (!Directory.Exists(sLogPath)) Directory.CreateDirectory(sLogPath);
                    LogWriter = new StreamWriter(logfilename.ToString());
                }
                else
                {
                    LogWriter = File.AppendText(logfilename.ToString());
                }

                string msg = "<DATA>" + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " : " + message + "</DATA>";
                LogWriter.WriteLine(msg);
                LogWriter.Flush();
                LogWriter.Close();
            }
            catch (Exception) { }

        }
    }


}
