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
    public class PurchaseItem : IPurchasable
    {
        public const int ITEM_ID_MAX_LENGTH = 32;

        [DataMember]
        public string itemId { get; set; }

        [DataMember]
        public ushort amount { get; set; }

        public PurchaseItem(string itemId, ushort amount)
        {
            // ensure itemId passes sanity check            
            if (itemId == null)
            {
                throw new InvalidPurchaseItemIdException("item id can not be null");
            }
            else if (itemId.Length == 0)
            {
                throw new InvalidPurchaseItemIdException("item id can not be empty");
            }
            else if (itemId.Length > ITEM_ID_MAX_LENGTH)
            {
                throw new InvalidPurchaseItemIdException(String.Format("expected up to {0} characters, got {1}", ITEM_ID_MAX_LENGTH, itemId.Length));
            }

            this.itemId = itemId.PadLeft(ITEM_ID_MAX_LENGTH, '0');
            this.amount = amount;
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