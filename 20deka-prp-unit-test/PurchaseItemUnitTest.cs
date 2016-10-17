using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using TwentyDeka;

namespace TwentyDekaUnitTest
{
    [TestClass]
    public class PurchaseItemUnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidPurchaseItemIdException))]
        public void TestMissingItemId()
        {
            PurchaseItem purchaseItem = new PurchaseItem(null, 3);
        }
    }
}
