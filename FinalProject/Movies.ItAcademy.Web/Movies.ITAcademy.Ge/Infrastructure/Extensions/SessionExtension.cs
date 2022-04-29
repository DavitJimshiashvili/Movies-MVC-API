using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Movies.ITAcademy.Ge.Infrastructure.Extensions
{
    public static class  SessionExtension
    {
        public static T GetSessionData<T>(this ISession session, string key)
        {
            var data = session.GetString(key);
            if (data == null)
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        public static void SetSessionData(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
    }
}
