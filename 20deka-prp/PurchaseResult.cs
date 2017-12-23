using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace TwentyDeka
{
    [DataContract]
    public class PurchaseResult
    {
        private static Bitmap twentyDekaLogo;

        [DataMember(Name = "status")]
        public int status { get; set; }

        [DataMember(Name = "messages")]
        public List<Message> messages { get; set; }

        [DataMember(Name = "receiptEANUrl")]
        public string receiptEANUrl { get; set; }

        public Bitmap GetReceiptEAN()
        {
            System.Net.WebRequest receiptEANRequest = System.Net.WebRequest.Create(receiptEANUrl);
        
            return new Bitmap(receiptEANRequest.GetResponse().GetResponseStream());
        }

        override
        public string ToString()
        {
            return String.Format("purchaseResult: {status = {0}}", status.ToString()); 
        }

        public static Bitmap GetTwentyDekaLogo()
        {
            if (twentyDekaLogo == null)
            {
                twentyDekaLogo = new Bitmap(Properties.Resources.twentydeka_logo);
            }

            return twentyDekaLogo;
        }
    }

    [DataContract]
    public class Message
    {
        [DataMember(Name = "lang")]
        public string language { get; set; }

        [DataMember(Name = "text")]
        public string text { get; set; }
    }
}
