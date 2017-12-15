using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TwentyDeka;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string merchantId = "887309d048beef83ad3eabf2a79a64a389ab1c9f";
                uint merchantBranchId = 6565;
                string accessToken = "fbb7fb129aa79e3ea3ee25fda569b703a25ca08aa02f7740e91ded422e5823a168f54e33362878b6d1876a12febaa4104bb3b895796431ece3b7557286627779";
                
                PurchaseReceipt purchaseReceipt = new PurchaseReceipt(merchantId, merchantBranchId);
                purchaseReceipt.customerCardId = "63400950019900345";
                purchaseReceipt.Add(new PurchaseItem("2002120307521", 1));
                purchaseReceipt.Add(new PurchaseItem("2002006255673", 2));
                purchaseReceipt.Add(new PurchaseItem("2002120712588", 2));

                PurchaseReceiptProcessor purchaseReceiptProcessor = new PurchaseReceiptProcessor(accessToken);
                PurchaseResult purchaseResult = purchaseReceiptProcessor.Process(purchaseReceipt);

                if (purchaseResult.purchaseQRCode != null)
                {
                    purchaseResult.purchaseQRCode.Save("c:\\temp\\purchase_qrcode.png");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: '{0}'", e);
            }
        }
    }
}
