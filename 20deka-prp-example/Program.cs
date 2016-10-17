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
                PurchaseReceipt purchaseReceipt = new PurchaseReceipt(1, 1);
                purchaseReceipt.customerCardId = "63400950019900345";
                purchaseReceipt.Add(new PurchaseItem("2002120307521", 1));
                purchaseReceipt.Add(new PurchaseItem("2002006255673", 2));
                purchaseReceipt.Add(new PurchaseItem("2002120712588", 2));

                PurchaseReceiptProcessor purchaseReceiptProcessor = new PurchaseReceiptProcessor();
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
