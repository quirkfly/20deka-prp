using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Drawing;
using System.Resources;

using RestSharp;
using QRCoder;


namespace TwentyDeka
{
    public class PurchaseReceiptProcessor
    {
        #if DEBUG
            public const string BASE_URL = "https://2aa3db95.ngrok.io";
        #else
            public const string BASE_URL = "https://20deka.com";       
        #endif

        public const int SELLER_DOES_NOT_EXIST = 53;
        public const int SELLER_BRANCH_DOES_NOT_EXIST = 77;

        private string accessToken;
        private static Bitmap twentyDekaLogo;

        public PurchaseReceiptProcessor(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public PurchaseResult Process(PurchaseReceipt purchaseReceipt)
        {
            var restClient = new RestClient(BASE_URL);

            var request = new RestRequest(String.Format("/api/v1/seller/pos/{0}/{1}/receipts/upload", purchaseReceipt.merchantId,
                purchaseReceipt.merchantBranchId), Method.POST);
            request.AddHeader("Authorization", String.Format("Bearer {0}", this.accessToken));
            request.AddParameter("customerCardId", purchaseReceipt.customerCardId);
            request.AddParameter("purchaseItems", purchaseReceipt.ToJson());

            IRestResponse response = restClient.Execute(request);
            MemoryStream responseStream = new MemoryStream(Encoding.UTF8.GetBytes(response.Content));
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(PurchaseReceiptUploadResponse));
            var purchaseReceiptUploadResponse = jsonSerializer.ReadObject(responseStream) as PurchaseReceiptUploadResponse;
            responseStream.Close();

            if (purchaseReceiptUploadResponse.status == 0)
            {
                //QRCodeGenerator qrGenerator = new QRCodeGenerator();
                //QRCodeData qrCodeData = qrGenerator.CreateQrCode(String.Format("{0}/prp/{1}", BASE_URL, purchaseReceiptUploadResponse.path), QRCodeGenerator.ECCLevel.Q);
                //QRCode qrCode = new QRCode(qrCodeData);

                //return new PurchaseResult(!purchaseReceiptUploadResponse.isLinkedWithHousehold ?
                //    qrCode.GetGraphic(20, Color.Black, Color.White, getTwentyDekaLogo()) : null);
                return new PurchaseResult(null);
            }
            else if (purchaseReceiptUploadResponse.status == SELLER_DOES_NOT_EXIST)
            {
                throw new InvalidSellerIdException(String.Format("Merchant id {0} does not exist.", purchaseReceipt.merchantId));
            }
            else if (purchaseReceiptUploadResponse.status == SELLER_BRANCH_DOES_NOT_EXIST)
            {
                throw new InvalidSellerBranchIdException(String.Format("Merchant branch id {0} does not exist.", purchaseReceipt.merchantBranchId));
            }
            else
            {
                throw new PurchaseReceiptUploadException(String.Format("Failed to upload purchase receipt, status: {0}", purchaseReceiptUploadResponse.status));
            }
        }

        public static Bitmap getTwentyDekaLogo()
        {
            if (twentyDekaLogo == null)
            {
                twentyDekaLogo = new Bitmap(Properties.Resources.twentydeka_logo);
            }

            return twentyDekaLogo;
        }
    }

    [DataContract]
    public class PurchaseReceiptUploadResponse
    {
        [DataMember(Name = "status")]
        public int status { get; set; }

        [DataMember(Name = "msg")]
        public string msg { get; set; }

        [DataMember(Name = "receiptEANUrl")]
        public string receiptEANUrl { get; set; }
    }

}