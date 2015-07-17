namespace NKCraddock.AmazonItemLookup
{
    public class AwsOffer
    {
        public string Condition { get; set; }
        public double? Amount { get; set; }
        public string Availability { get; set; }
        public string Merchant { get; set;  }
        public string FormattedPrice { get; set; }
        public string FormattedSavedPrice { get; set; }
        public string PercentageSaved { get; set; }
    }
}