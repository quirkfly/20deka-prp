using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace TwentyDeka
{
    [DataContract]
    public class PurchaseReceipt : IPurchasable
    {
        [DataMember]
        public uint sellerId { get; set; }

        [DataMember]
        public uint sellerBranchId { get; set; }

        [DataMember]
        public string customerCardId { get; set; }

        [DataMember]
        private Dictionary<string, ushort> purchaseItems;

        public PurchaseReceipt(uint sellerId, uint sellerBranchId)
        {
            this.sellerId = sellerId;
            this.sellerBranchId = sellerBranchId;
            this.purchaseItems = new Dictionary<string, ushort>();
        }

        public void Add(PurchaseItem purchaseItem)
        {
            if (this.purchaseItems.ContainsKey(purchaseItem.itemId))
            {
                this.purchaseItems[purchaseItem.itemId] += purchaseItem.amount;
            }
            else
            {
                this.purchaseItems.Add(purchaseItem.itemId, purchaseItem.amount);
            }
        }

        override
        public string ToString()
        {
            string purchaseReceipt = String.Format("{0}{1}", this.sellerId.ToString().PadLeft(10, '0'), this.sellerBranchId.ToString().PadLeft(10, '0'));
            purchaseReceipt += String.Format("{0}{1}", this.sellerId.ToString().PadLeft(10, '0'), this.sellerBranchId.ToString().PadLeft(10, '0'));
            foreach (KeyValuePair<string, ushort> purchaseItem in this.purchaseItems)
            {
                purchaseReceipt += String.Format("{0}{1}", purchaseItem.Key, purchaseItem.Value.ToString().PadLeft(3, '0'));
            }

            return purchaseReceipt;
        }

        public string ToJson()
        {
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(PurchaseReceipt));

            using (MemoryStream ms = new MemoryStream())
            {
                jsonSerializer.WriteObject(ms, this);

                return Encoding.Default.GetString(ms.ToArray());
            }
        }
    }
}