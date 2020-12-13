using System;
using System.Collections.Generic;

namespace CustomLogs.Models
{
    public class LogCollectionInfo<TItem> : LogWithHeader
    {
        public LogCollectionInfo(string collectionName, IEnumerable<TItem> collection,
                                Func<TItem, DataInfo> selector, string userName, string path, string callerName,
                                int callerLine)
           : base(path, callerName, callerLine, userName)
        {
            CollectionName = collectionName;
            Collection = collection;
            Selector = selector;
        }

        public string CollectionName { get; private set; }
        public IEnumerable<TItem> Collection { get; private set; }
        public Func<TItem, DataInfo> Selector { get; private set; }
    }
}
