using System;

namespace ENode.Infrastructure
{
    /// <summary>
    /// 表示提供类型和类型名称映射信息的提供程序
    /// </summary>
    public interface ITypeNameProvider
    {

        string GetTypeName(Type type);

        Type GetType(string typeName);
    }
}
