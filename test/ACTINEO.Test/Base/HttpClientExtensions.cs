using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ACTINEO.Test.Base {
   public static class HttpClientExtensions {
        public static Task<HttpResponseMessage> PostAsync(this HttpClient client, string uri, object content) {
            var strContent = new StringContent(JsonConvert.SerializeObject(content), UTF8Encoding.UTF8, "application/json");
            return client.PostAsync(uri, strContent);
        }

        public static async Task<T> GetAsync<T>(this HttpClient client, string uri) {
            var res = await client.GetAsync(uri);
            res.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<T>(await res.Content.ReadAsStringAsync());
        }
    }
}
