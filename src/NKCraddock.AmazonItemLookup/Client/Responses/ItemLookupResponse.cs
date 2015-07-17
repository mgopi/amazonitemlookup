using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace NKCraddock.AmazonItemLookup.Client.Responses
{
    internal class ItemLookupResponse
    {
        AwsXmlParser parser;
        string xml;

        public ItemLookupResponse(string responseXml)
        {
            xml = responseXml;
            parser = new AwsXmlParser(xml);
        }

        public List<AwsItem> ToAwsItems()
        {
            List<AwsItem> items = new List<AwsItem>();
            var itemNodes = parser.SelectNodes(NodePath.ItemLookupResponse.ItemPath);
            for (int i = 0; i < itemNodes.Count; i++)
            {
                var node = itemNodes[i];
                items.Add(ToAwsItem(node));
            }
            return items;
        }

        private AwsItem ToAwsItem(XmlNode itemNode)
        {
            //AwsXmlParser nodeParser = new AwsXmlParser(node, rootNodeParser.getNameTable(), rootNodeParser.getNamespaceUri());
            var item = new AwsItem
            {
                ASIN = parser.SelectNodeValue(itemNode, NodePath.ItemLookupResponse.ASIN),
                DetailPageURL = parser.SelectNodeValue(itemNode, NodePath.ItemLookupResponse.DetailPageUrl),
                SalesRank = XmlHelper.GetInt(parser.SelectNodeValue(itemNode, NodePath.ItemLookupResponse.SalesRank)),
                ReviewIFrameUrl = parser.SelectNodeValue(itemNode, NodePath.ItemLookupResponse.ReviewIFrameUrl),
                Links = GetLinks(itemNode),
                ImageSets = GetImageSets(itemNode),
                ItemAttributes = GetItemAttributes(itemNode),
                Reviews = GetReviews(itemNode),
                SimilarProducts = GetSimilarProducts(itemNode),
                OfferPrice = XmlHelper.GetDollars(parser.SelectNode(itemNode, NodePath.ItemLookupResponse.OfferPrice)),
                ListPrice = XmlHelper.GetDollars(parser.SelectNode(itemNode, NodePath.ItemLookupResponse.ListPrice)),
                LowestOfferPrice = XmlHelper.GetDollars(parser.SelectNode(itemNode, NodePath.ItemLookupResponse.LowestOfferPrice)),
                Offers = GetOffers(itemNode)
            };

            return item;
        }

        private List<AwsOffer> GetOffers(XmlNode itemNode)
        {
            var offers = new List<AwsOffer>();
            var offerNodes = parser.SelectNodes(itemNode, NodePath.ItemLookupResponse.Offers);
            for (int i = 0; i < offerNodes.Count; i++)
            {
                var offerNode = offerNodes[i];
                var offer = new AwsOffer()
                {
                    Amount = XmlHelper.GetDollars(parser.SelectNode(offerNode, NodePath.ItemLookupResponse.Offer.Price)),
                    Condition = parser.SelectNodeValue(offerNode, NodePath.ItemLookupResponse.Offer.Condition),
                    Merchant = parser.SelectNodeValue(offerNode, NodePath.ItemLookupResponse.Offer.Merchant),
                    FormattedPrice = parser.SelectNodeValue(offerNode, NodePath.ItemLookupResponse.Offer.FormattedPrice),
                    FormattedSavedPrice = parser.SelectNodeValue(offerNode, NodePath.ItemLookupResponse.Offer.FormattedSavedPrice),
                    PercentageSaved = parser.SelectNodeValue(offerNode, NodePath.ItemLookupResponse.Offer.PercentageSaved)
                };
                offers.Add(offer);
            }
            return offers;
        }

        private IDictionary<string, string> GetItemAttributes(XmlNode itemNode)
        {
            var attr = new Dictionary<string, string>();

            foreach (XmlNode node in parser.SelectNode(itemNode, NodePath.ItemLookupResponse.ItemAttributes).ChildNodes)
                attr[node.Name] = XmlHelper.GetValue(node);

            return attr;
        }

        private IList<AwsReview> GetReviews(XmlNode itemNode)
        {
            var reviews = new List<AwsReview>();
            foreach (XmlNode reviewNode in parser.SelectNodes(itemNode, NodePath.ItemLookupResponse.Reviews))
                reviews.Add(AwsReview.FromXmlNode(reviewNode));
            return reviews;
        }

        private IList<AwsSimilarProduct> GetSimilarProducts(XmlNode itemNode)
        {
            var similarProducts = new List<AwsSimilarProduct>();
            foreach (XmlNode node in parser.SelectNodes(itemNode, NodePath.ItemLookupResponse.SimilarProducts))
                similarProducts.Add(AwsSimilarProduct.FromXmlNode(node));
            return similarProducts;
        }

        private IList<AwsImageSet> GetImageSets(XmlNode itemNode)
        {
            var imageSets = new List<AwsImageSet>();
            foreach (XmlNode imageSetNode in parser.SelectNodes(itemNode, NodePath.ItemLookupResponse.ImageSets))
                imageSets.Add(AwsImageSet.FromXmlNode(imageSetNode));
            return imageSets;
        }

        private IList<AwsLink> GetLinks(XmlNode itemNode)
        {
            var links = new List<AwsLink>();
            foreach (XmlNode linkNode in parser.SelectNodes(itemNode, NodePath.ItemLookupResponse.ItemLinks))
                links.Add(AwsLink.FromXmlNode(linkNode));
            return links;
        }
    }
}