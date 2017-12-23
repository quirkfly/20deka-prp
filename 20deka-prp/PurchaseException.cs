using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyDeka
{
    public class InvalidPurchaseItemIdException : Exception
    {
        public InvalidPurchaseItemIdException(string message)
            : base(message)
        {
        }
    }

    public class InvalidMerchantException : Exception
    {
        public InvalidMerchantException(string message)
            : base(message)
        {
        }
    }

    public class InvalidMerchantBranchException : Exception
    {
        public InvalidMerchantBranchException(string message)
            : base(message)
        {
        }
    }

    public class PurchaseReceiptException : Exception
    {
        public PurchaseReceiptException(string message)
            : base(message)
        {
        }
    }
}