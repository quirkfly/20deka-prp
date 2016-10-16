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

    public class InvalidSellerIdException : Exception
    {
        public InvalidSellerIdException(string message)
            : base(message)
        {
        }
    }

    public class InvalidSellerBranchIdException : Exception
    {
        public InvalidSellerBranchIdException(string message)
            : base(message)
        {
        }
    }

    public class PurchaseReceiptUploadException : Exception
    {
        public PurchaseReceiptUploadException(string message)
            : base(message)
        {
        }
    }
}