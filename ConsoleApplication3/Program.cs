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
            Execute();
            Console.WriteLine("Press Enter to Exit");
            Console.ReadLine();
        }

    }
}