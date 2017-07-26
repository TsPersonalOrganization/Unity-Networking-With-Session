using System;
using System.Collections.Generic;
using System.Reflection;
using System.Net.Sockets;

namespace UnityEngine.Networking
{
    internal static class DotNetCompatibility
    {
        internal static string GetMethodName(this Delegate func)
        {
#if NETFX_CORE
            return func.GetMethodInfo().Name;
#else
            return func.Method.Name;
#endif
        }

        internal static Type GetBaseType(this Type type)
        {
#if NETFX_CORE
            return type.GetTypeInfo().BaseType;
#else
            return type.BaseType;
#endif
        }

        internal static string GetErrorCode(this SocketException e)
        {
#if UNITY_UAP
            return e.SocketErrorCode.ToString();
#else
            return e.ErrorCode.ToString();
#endif
        }

#if NETFX_CORE
        internal static bool IsSubclassOf(this Type type, Type baseType)
        {
            return WinRTLegacy.TypeExtensions.IsSubClassOf(type, baseType);
        }

#endif

#if UNITY_METRO_8_1 || UNITY_WP_8_1
        internal static Type[] GetTypes(this Assembly assembly)
        {
            var types = new List<Type>();
            foreach (var typeInfo in assembly.DefinedTypes)
                types.Add(typeInfo.AsType());
            return types.ToArray();
        }

        internal static MethodInfo GetMethod(this Type type, string name, BindingFlags flags)
        {
            return WinRTLegacy.TypeExtensions.GetMethod(type, name, flags);
        }

#endif
    }
}
