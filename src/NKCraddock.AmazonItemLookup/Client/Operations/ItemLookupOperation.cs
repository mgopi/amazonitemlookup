using System.Collections.Generic;
using NKCraddock.AmazonItemLookup.Client.Responses;

namespace NKCraddock.AmazonItemLookup.Client.Operations
{
    public class ItemLookupOperation : IAwsOperation<List<AwsItem>>
    {
        Dictionary<string, string> requestArgs;
        public ItemLookupOperation(ItemLookupRequestInfo requestInfo)
        {
            requestArgs = new Dictionary<string, string>
            {
                { "Operation", "ItemLookup" },
                { "ResponseGroup", string.Join(",",requestInfo.ResponseGroups) },
                { "Condition", requestInfo.Condition},
                { "ItemId", string.Join(",", requestInfo.ItemIds) }
            };
        }
        public Dictionary<string, string> GetRequestArguments()
        {
            return requestArgs;
        }

        public List<AwsItem> GetResultsFromXml(string xml)
        {
            var itemLookupResponse = new ItemLookupResponse(xml);
            return itemLookupResponse.ToAwsItems();
        }
    }
}