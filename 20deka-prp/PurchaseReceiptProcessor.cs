using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Resources;

using RestSharp;
using QRCoder;


namespace TwentyDeka
{
    public class PurchaseReceiptProcessor
    {
        #if DEBUG
            public const string BASE_URL = "https://b0af3668.ngrok.io";
        #else
            public const string BASE_URL = "https://20deka.com";       
        #endif

        public const int MERCHANT_DOES_NOT_EXIST = 53;
        public const int MERCHANT_BRANCH_DOES_NOT_EXIST = 77;

        private string accessToken;
        
        public PurchaseReceiptProcessor(string accessToken)
        {
            this.accessToken = accessToken;
        }

        public PurchaseResult Process(PurchaseReceipt purchaseReceipt)
        {
            var restClient = new RestClient(BASE_URL);

            var request = new RestRequest(String.Format("/api/v1/seller/pos/{0}/{1}/receipts/upload", purchaseReceipt.merchantId,
                purchaseReceipt.merchantBranchId), Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Authorization", String.Format("Bearer {0}", this.accessToken));
            request.AddBody(new { loyaltyCardId = purchaseReceipt.customerCardId, purchaseItems = purchaseReceipt.ToJson() }); 

            IRestResponse response = restClient.Execute(request);
            MemoryStream responseStream = new MemoryStream(Encoding.UTF8.GetBytes(response.Content));
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(PurchaseResult));
            var purchaseResult = jsonSerializer.ReadObject(responseStream) as PurchaseResult;
            responseStream.Close();

            if (purchaseResult.status == 0)
            {
                return purchaseResult;
            }
            else if (purchaseResult.status == MERCHANT_DOES_NOT_EXIST)
            {
                throw new InvalidMerchantException(String.Format("Merchant id {0} does not exist.", purchaseReceipt.merchantId));
            }
            else if (purchaseResult.status == MERCHANT_BRANCH_DOES_NOT_EXIST)
            {
                throw new InvalidMerchantBranchException(String.Format("Merchant branch id {0} does not exist.", purchaseReceipt.merchantBranchId));
            }
            else
            {
                throw new PurchaseReceiptException(String.Format("Failed to upload purchase receipt, status: {0}", purchaseResult.status));
            }
        }
    }
}