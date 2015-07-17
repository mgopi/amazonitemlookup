namespace NKCraddock.AmazonItemLookup.Client
{
    internal static class NodePath
    {
        internal static class ItemLookupResponse
        {
            public const string ItemPath = "/ItemLookupResponse/Items/Item";
            public const string ASIN = "ASIN";
            public const string DetailPageUrl = "DetailPageURL";
            public const string SalesRank = "SalesRank";
            public const string ReviewIFrameUrl = "CustomerReviews/IFrameURL";
            public const string ItemLinks = "ItemLinks/ItemLink";
            public const string ImageSets = "ImageSets/ImageSet";
            public const string ItemAttributes = "ItemAttributes";
            public const string Reviews = "EditorialReviews/EditorialReview";
            public const string SimilarProducts = "SimilarProducts/SimilarProduct";
            public const string OfferPrice = "Offers/Offer/OfferListing/Price/Amount";
            public const string ListPrice = "ItemAttributes/ListPrice/Amount";
            public const string LowestOfferPrice = "OfferSummary/LowestNewPrice/Amount";
            public const string Offers = "Offers/Offer";
            public static class Offer
            {
                public const string Price = "OfferListing/Price/Amount";
                public const string Condition = "OfferAttributes/Condition";
                public const string Merchant = "Merchant/Name";
                public const string FormattedPrice = "OfferListing/Price/FormattedPrice";
                public const string FormattedSavedPrice = "OfferListing/AmountSaved/FormattedPrice";
                public const string PercentageSaved = "OfferListing/PercentageSaved";
            }
        }
        
        internal static class CartCreateResponse
        {
            public const string CartPath = "CartCreateResponse/Cart/";
            public const string CartId = CartPath + "CartId";
            public const string HMAC = CartPath + "HMAC";
            public const string URLEncodedHMAC = CartPath + "URLEncodedHMAC";
            public const string PurchaseURL = CartPath + "PurchaseURL";
            public const string SubTotal = CartPath + "SubTotal/Amount";
            public const string CartItems = CartPath + "CartItems/CartItem";
        }
    }
}