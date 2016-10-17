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
        public void TestNullItemId()
        {
            PurchaseItem purchaseItem = new PurchaseItem(null, 3);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPurchaseItemIdException))]
        public void TestEmptyItemId()
        {
            PurchaseItem purchaseItem = new PurchaseItem("", 3);
        }

        [TestMethod]
        public void TestValidItemId()
        {
            PurchaseItem purchaseItem = new PurchaseItem("933920932", 3);
            Assert.AreEqual("00000000000000000000000933920932", purchaseItem.itemId);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidPurchaseItemIdException))]
        public void TestItemIdLengthExceeded()
        {
            PurchaseItem purchaseItem = new PurchaseItem("34343343434343434343434343434343903434344348080804343808043408800843438080843", 3);
        }

    }
}
