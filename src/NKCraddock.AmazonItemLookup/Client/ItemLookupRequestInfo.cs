using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKCraddock.AmazonItemLookup.Client
{
    public class ItemLookupRequestInfo
    {
        public List<string> ResponseGroups { get; set;}
        public List<string> ItemIds { get; set;}
        public String Condition { get; set; }
        
        public ItemLookupRequestInfo()
        {
            ResponseGroups = new List<string> { ResponseGroup.Large };
            Condition = AwsItemCondition.New;
        }
    }
}
