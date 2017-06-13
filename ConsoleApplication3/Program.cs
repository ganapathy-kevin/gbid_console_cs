using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml;
using System.Web;
using System.Collections;
using System.Xml.Serialization;
using System.Net;
using System.IO;

namespace ConsoleApplication3
{
    class Program
    {
        /// <summary>
        /// Execute a Soap WebService call
        /// </summary>
        /// 

        public static string Username = "casa@easyconvey.com";
        public static string Password = "C@sa&&77Conv323Eas";
        public static GB.basicHttpBinding_GlobalProfile GlobalProfilesCheck = new GB.basicHttpBinding_GlobalProfile();
        public static GB.basicHttpBinding_GlobalCredentials GlobalCredentialsCheck = new GB.basicHttpBinding_GlobalCredentials();

        public static string CheckingCredentials()
        {
            GB.GlobalAccount globalAccount = new GB.GlobalAccount();
            globalAccount = GlobalCredentialsCheck.CheckCredentials(Username, Password);

                Console.WriteLine(globalAccount.Username);
                Console.WriteLine(globalAccount.AccountID);
                Console.WriteLine(globalAccount.Email);
                Console.WriteLine("ORGINIZATION ID: " + globalAccount.OrgID);
                Console.WriteLine(globalAccount.OrgName);
            
            Console.ReadLine();
            
            return globalAccount.OrgID;
        }

        public static void GettingProfiles()
        {
            try
            {
                GB.GlobalProfile[] globalProfiles = GlobalProfilesCheck.GetProfiles(CheckingCredentials(), true, true, true, true, true, true, true, true);
            }
            catch
            {
                Console.WriteLine("Could not get profiles from GB");
                Console.ReadLine();
            }
           
        }

        public static void Execute()
        {
            HttpWebRequest request = CreateWebRequest();
            Console.WriteLine(request.Address);
            Console.WriteLine(request.Connection);
            Console.WriteLine(request.Credentials);
            Console.ReadLine();
            XmlDocument soapEnvelopeXml = new XmlDocument();
            //soapEnvelopeXml.LoadXml(@"<?xml version=""1.0"" encoding=""utf-8""?>
            //    <soap:Envelope xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:soap=""http://schemas.xmlsoap.org/soap/envelope/"">
            //      <soap:Body>
            //        <HelloWorld xmlns=""http://tempuri.org/"" />
            //      </soap:Body>
            //    </soap:Envelope>");

            //using (Stream stream = request.GetRequestStream())
            //{
            //    soapEnvelopeXml.Save(stream);
            //}

            Console.WriteLine("using Webresponse") ;
            Console.ReadLine();
            StreamWriter rd2 = new StreamWriter(@"C:\Users\kevin\Desktop\WSDL.txt");

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader rd = new StreamReader(response.GetResponseStream()))
                {
                    string soapResult = rd.ReadToEnd();
                    rd2.WriteLine(soapResult);
                }
            }
        }
        /// <summary>
        /// Create a soap webrequest to [Url]
        /// </summary>
        /// <returns></returns>
        public static HttpWebRequest CreateWebRequest()
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(@"https://pilot.id3global.com/ID3gWS/ID3global.svc?Singlewsdl");
            //webRequest.Headers.Add(@"SOAP:Action");
            //webRequest.ContentType = "text/xml;charset=\"utf-8\"";
            //webRequest.Accept = "text/xml";
            webRequest.Method = "GET";
            return webRequest;
        }

        static void Main(string[] args)
        {
            CheckingCredentials();///    Execute();

            GettingProfiles();
            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
        }

    }
}