using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace WhereIsMyPhoto
{



    class OSMNominatim
    {

        WebClient webClient;

        public double Latitude { get; private set; }
        public double Longitude { get; private set; }


        public string Home { get; private set; }
        public string Street { get; private set; }
        public string City { get; private set; }
        public string Country { get; private set; }
        private bool details_flag;
        public string FullInformation { get; private set; }
        public OSMNominatim(double lat, double lng, bool loaddetails)
        {
            details_flag = loaddetails;
            Latitude = lat;
            Longitude = lng;
        }

        public bool TryReverseGeocoding()
        {
            bool isOK = false;
            XmlTextReader reader = null;
            System.IO.Stream stream = null;
            try
            {
                StringBuilder for_uri = new StringBuilder(100);

                for_uri.Append("https://nominatim.openstreetmap.org/reverse?format=xml");
                for_uri.Append("&lat=");
                for_uri.Append(Latitude.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")));
                for_uri.Append("&lon=");
                for_uri.Append(Longitude.ToString(System.Globalization.CultureInfo.GetCultureInfo("en-US")));
                for_uri.Append("&zoom=18&addressdetails=");
                for_uri.Append((details_flag == true) ? "1" : "0");
                for_uri.Append("&email=viktor70@protonmail.com");

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(for_uri.ToString());
                request.UserAgent = "WhereIsMyPhoto " + Application.ProductVersion.ToString();
                request.Timeout = 1000;
                WebResponse response = request.GetResponse();
                
                stream = response.GetResponseStream();

                reader = new XmlTextReader(stream);
                XmlDocument document = new XmlDocument();
                document.Load(reader);
                XmlNode resultNode = document.GetElementsByTagName("result")[0];

                FullInformation = resultNode.InnerText;

                if(details_flag)
                {
                    XmlNode addressparts = document.GetElementsByTagName("addressparts")[0];
                    foreach (XmlNode node in addressparts)
                    {
                        switch (node.Name)
                        {
                            case "house_number":
                                Home = node.InnerText;
                                break;
                            case "road":
                                Street = node.InnerText;
                                break;
                            case "city":
                                City = node.InnerText;
                                break;
                            case "country":
                                Country = node.InnerText;
                                break;
                        }
                    }
                }
                

                stream.Close();
                reader.Close();
                isOK = true;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                isOK = false;
            }
            finally
            {
                stream?.Close();
                reader?.Close();
            }
            return isOK;
        }
    }
}
