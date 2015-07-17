﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace NKCraddock.AmazonItemLookup
{
    public class AwsItem
    {
        public string ASIN { get; set; }
        public string DetailPageURL { get; set; }
        public IList<AwsLink> Links { get; set; }
        public int? SalesRank { get; set; }
        public IList<AwsImageSet> ImageSets { get; set; }
        public IDictionary<string, string> ItemAttributes { get; set; }
        public string ReviewIFrameUrl { get; set; }
        public IList<AwsReview> Reviews { get; set; }
        public IList<AwsSimilarProduct> SimilarProducts { get; set; }
        public double? ListPrice { get; set; }
        public double? OfferPrice { get; set; }
        public double? LowestOfferPrice { get; set; }
        public List<AwsOffer> Offers { get; set; }
        public AwsImageSet PrimaryImageSet
        {
            get
            {
                return ImageSets.Where(x => x.Category == AwsImageSetCategory.Primary).FirstOrDefault();
            }
        }

        public string Description
        {
            get
            {
                const string AMAZON_SOURCE_NAME = "Product Description";
                AwsReview description = Reviews.Where(x => x.Source == AMAZON_SOURCE_NAME).FirstOrDefault();
                if (description == null)
                    return String.Empty;
                return description.Content;
            }
        }

        public double? GetLowestPrice()
        {
            double[] prices = { ListPrice ?? 0, OfferPrice ?? 0, LowestOfferPrice ?? 0 };
            double lowest = 0;
            foreach (var price in prices)
                if (lowest == 0 || price < lowest)
                    lowest = price;

            if (lowest == 0)
                return null;

            return lowest;
        }

        public string GetImageUrl(AwsImageType type)
        {
            if (PrimaryImageSet == null)
                return null;

            var img = PrimaryImageSet[type];

            if (img == null)
                return null;

            return img.URL;
        }
    }
}