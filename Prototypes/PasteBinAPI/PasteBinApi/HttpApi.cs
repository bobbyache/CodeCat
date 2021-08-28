using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PasteBinApi
{
    public class HttpApi
    {
        // Test using User Secrets
        // https://patrickhuber.github.io/2017/07/26/avoid-secrets-in-dot-net-core-tests.html

        public async Task<string> GetPaste()
        {
            var client = new HttpClient();
            var keys = new Dictionary<string, string>()
            {
                { "api_dev_key", "2cee9c4d75043519a29c7896f8132fa4"},
                { "api_option", "show_paste"},
                { "api_paste_key", "GBtmTp8g"},
                { "api_user_key", "abb982d3914f4fa40e94b91d40f67c8e"}
            };

            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(@"https://pastebin.com/api/api_raw.php"),
                Method = HttpMethod.Post,
                Content = new FormUrlEncodedContent(keys)
            };

            HttpResponseMessage response = await client.SendAsync(request);

            return await response.Content.ReadAsStringAsync();
        }
    }
}
