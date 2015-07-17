using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NKCraddock.AmazonItemLookup;
using NKCraddock.AmazonItemLookup.Client;
using NKCraddock.AmazonItemLookup.Client.Operations;
using NKCraddock.AmazonItemLookup.Client.Responses;
using NKCraddock.AmazonItemLookupTests.TestHelpers;
using System.Collections.Generic;

namespace NKCraddock.AmazonItemLookupTests.Client
{
    [TestClass]
    public class ProductApiTest
    {
        AwsProductApiClient api;
        Mock<ICommunicator> communicatorMock;
        const string ASIN = "0131103628";

        [TestInitialize]
        public void Initialize()
        {
            communicatorMock = new Mock<ICommunicator>();
            api = new AwsProductApiClient(new AwsTestConnectionInfo(), communicatorMock.Object);
        }

        [TestMethod]
        public void ItemLookup_WithLargeResponse_RetrievesAFewPropertiesIWillSelectHaphazardly()
        {
            const string ISBN = "0131103628";
            const double LIST_PRICE = 67;
            const string LARGE_IMAGE_URL = "http://ecx.images-amazon.com/images/I/41G0l2eBPNL.jpg";

            WithItemLookupResponseLarge();
            var item = api.ItemLookupByAsin(ASIN);

            Assert.IsNotNull(item);
            Assert.AreEqual<string>(ASIN, item.ASIN);
            Assert.AreEqual<string>(ISBN, item.ItemAttributes["ISBN"]);
            Assert.AreEqual<double>(LIST_PRICE, item.ListPrice.Value);
            Assert.AreEqual<string>(LARGE_IMAGE_URL, item.PrimaryImageSet[AwsImageType.LargeImage].URL);
        }

        [TestMethod]
        public void ItemLookup_WithMultipleIds()
        {
            const string ITEM1_ASIN = "B00DFFT76U";
            const string ITEM1_TITLE = "Pampers Swaddlers Diapers Size 3 Economy Pack Plus 162 Count";
            const int ITEM1_OFFER_COUNT = 1;
            const string ITEM2_ASIN = "B0015XGNEI";
            const string ITEM2_TITLE = "Hamilton Beach 2 Slice Cool Touch Toaster";
            const int ITEM2_OFFER_COUNT = 2;

            GetItemLookupResponseMultipleIds();
            var items = api.ItemLookupByAsin(new ItemLookupRequestInfo()
            {
                ItemIds = new List<string>() { ITEM1_ASIN, ITEM2_ASIN }
            });

            Assert.IsNotNull(items);
            Assert.AreEqual(2, items.Count);
            //Verify first item data
            Assert.AreEqual<string>(ITEM1_ASIN, items[0].ASIN);
            Assert.AreEqual<string>(ITEM1_TITLE, items[0].ItemAttributes["Title"]);
            Assert.AreEqual<int>(ITEM1_OFFER_COUNT, items[0].Offers.Count);

            //Verify second item data
            Assert.AreEqual<string>(ITEM2_ASIN, items[1].ASIN);
            Assert.AreEqual<string>(ITEM2_TITLE, items[1].ItemAttributes["Title"]);
            Assert.AreEqual<int>(ITEM2_OFFER_COUNT, items[1].Offers.Count);

        }
        [TestMethod]
        public void ItemLookup_WithMultipleIds_Offers()
        {
            const string ITEM1_ASIN = "B00DFFT76U";
            const string ITEM2_ASIN = "B0015XGNEI";
          
            GetItemLookupResponseMultipleIds();
            var items = api.ItemLookupByAsin(new ItemLookupRequestInfo()
            {
                ItemIds = new List<string>() { ITEM1_ASIN, ITEM2_ASIN }
            });

            Assert.IsNotNull(items);
            Assert.AreEqual(2, items.Count);
            Assert.AreEqual<int>(2, items[1].Offers.Count);
            Assert.AreEqual(AwsItemCondition.New, items[1].Offers[0].Condition);
            Assert.AreEqual(21, items[1].Offers[0].Amount);
            Assert.AreEqual("Amazon.com", items[1].Offers[0].Merchant);
            Assert.AreEqual("$21.00", items[1].Offers[0].FormattedPrice);
            Assert.AreEqual("$3.99", items[1].Offers[0].FormattedSavedPrice);
        }

        [TestMethod]
        public void MyTestMethod()
        {
            WithCartCreateResponse();

            var cart = api.CreateCart(new CartItem { Asin = ASIN });

            Assert.IsNotNull(cart);
            Assert.AreEqual<int>(1, cart.CartItems.Count);
        }

        private void GetItemLookupResponseMultipleIds()
        {
            string responseText = AwsTestHelper.GetItemLookupResponseMultipleIds();
            communicatorMock.Setup(x => x.GetResponseFromUrl(It.IsAny<string>())).Returns(responseText);
        }

        private void WithItemLookupResponseLarge()
        {
            string responseText = AwsTestHelper.GetItemLookupResponseLarge();
            communicatorMock.Setup(x => x.GetResponseFromUrl(It.IsAny<string>())).Returns(responseText);
        }

        private void WithCartCreateResponse()
        {
            string responseText = AwsTestHelper.GetCartCreate();
            communicatorMock.Setup(x => x.GetResponseFromUrl(It.IsAny<string>())).Returns(responseText);
        }
    }
}