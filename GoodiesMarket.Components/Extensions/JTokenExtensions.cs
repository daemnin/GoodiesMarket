using Newtonsoft.Json.Linq;

namespace GoodiesMarket.Components.Extensions
{
    public static class JTokenExtensions
    {
        public static JToken ToToken(this object entity)
        {
            return JToken.FromObject(entity);
        }
    }
}
