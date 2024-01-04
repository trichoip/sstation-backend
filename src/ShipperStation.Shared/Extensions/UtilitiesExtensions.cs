using System.Collections;
using System.Net;
using System.Net.Sockets;

namespace ShipperStation.Shared.Extensions;
public static class UtilitiesExtensions
{
    public static T OrElseThrow<T, X>(this T? value, Func<X> exceptionSupplier) where X : Exception
    {
        return value ?? throw exceptionSupplier.Invoke();
    }

    public static bool IsNullOrEmpty(this IEnumerable? @this)
    {
        if (@this != null)
        {
            return !@this.GetEnumerator().MoveNext();
        }
        return true;
    }

    public static int ConvertToInteger(this string @this)
    {
        if (!int.TryParse(@this, out var result))
        {
            throw new ArgumentException("The string is not a valid integer", @this);
        }
        return result;
    }

    public static string Format(this string format, params object[] args)
    {
        return string.Format(format, args);
    }

    public static Guid ConvertToGuid(this string @this)
    {
        if (!Guid.TryParse(@this, out var result))
        {
            throw new ArgumentException("The string is not a valid Guid", @this);
        }
        return result;
    }

    public static bool RemoveIf<T>(this ICollection<T> collection, Func<T, bool> filter)
    {
        if (filter == null) throw new ArgumentNullException(nameof(filter));
        bool removed = false;
        var itemsToRemove = collection.Where(filter).ToList();
        foreach (var item in itemsToRemove)
        {
            collection.Remove(item);
            removed = true;
        }
        return removed;
    }

    public static string GetIpAddress()
    {
        var host = Dns.GetHostEntry(Dns.GetHostName());

        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                Console.WriteLine(ip.ToString());
                return ip.ToString();
            }
        }

        return "127.0.0.1";
    }

}
