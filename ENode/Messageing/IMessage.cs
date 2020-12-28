using System;
using System.Collections.Generic;

namespace ENode.Messageing
{
    /// <summary>
    /// 表示消息
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// 表示消息的唯一标识符
        /// </summary>
        string Id { get; set; }
        /// <summary>
        /// 表示消息的时间戳
        /// </summary>
        DateTime Timestamp { get; set; }
        /// <summary>
        /// 表示消息的扩展键/值数据
        /// </summary>
        IDictionary<string,string> Items { get; set; }
        /// <summary>
        /// 将所有的键/值合并到当前项中
        /// </summary>
        /// <param name="items"></param>
        void MergeItems(IDictionary<string, string> items);
    }
}
