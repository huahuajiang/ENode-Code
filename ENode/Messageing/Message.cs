using ECommon.Utilities;
using System;
using System.Collections.Generic;

namespace ENode.Messageing
{
    [Serializable]
    public abstract class Message : IMessage
    {
        public string Id { get; set; }
        public DateTime Timestamp { get; set; }
        public IDictionary<string, string> Items { get; set; }

        public Message()
        {
            Id = ObjectId.GenerateNewStringId();
            Timestamp = DateTime.UtcNow;
            Items = new Dictionary<string, string>();
        }
        public void MergeItems(IDictionary<string, string> items)
        {
            if (items == null || items.Count == 0)
            {
                return;
            }
            if (Items == null)
            {
                Items = new Dictionary<string, string>();
            }
            foreach(var entry in items)
            {
                if (!Items.ContainsKey(entry.Key))
                {
                    Items.Add(entry.Key, entry.Value);
                }
            }
        }
    }
}
