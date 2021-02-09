using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MaquininhaTheos.Domain.Shared
{
    public class HttpRequestBuilder
    {
        private HttpMethod method = null;
        private string requestUri = "";
        private HttpContent content = null;
        private string bearerToken = "";
        private string acceptHeader = "application/json";
        private TimeSpan timeout = new TimeSpan(1, 15, 115);
        private readonly bool allowAutoRedirect = false;
        private bool useProxy;
        private bool useCredencial;
        private string proxyHost;
        private string proxyPort;
        private string proxyUserName;
        private string proxyUsePassword;

        public HttpRequestBuilder()
        {
        }

        public HttpRequestBuilder AddMethod(HttpMethod method)
        {
            this.method = method;
            return this;
        }

        public HttpRequestBuilder AddRequestUri(string requestUri)
        {
            this.requestUri = requestUri;
            return this;
        }

        public HttpRequestBuilder AddContent(HttpContent content)
        {
            this.content = content;
            return this;
        }

        public HttpRequestBuilder AddBearerToken(string bearerToken)
        {
            this.bearerToken = bearerToken;
            return this;
        }

        public HttpRequestBuilder AddAcceptHeader(string acceptHeader)
        {
            this.acceptHeader = acceptHeader;
            return this;
        }

        public HttpRequestBuilder AddUseProxy(bool useProxy)
        {
            this.useProxy = useProxy;
            return this;
        }

        public HttpRequestBuilder AddUseCredencial(bool useCredencial)
        {
            this.useCredencial = useCredencial;
            return this;
        }

        public HttpRequestBuilder AddProxyHost(string proxyHost)
        {
            this.proxyHost = proxyHost;
            return this;
        }
        public HttpRequestBuilder AddProxyPort(string proxyPort)
        {
            this.proxyPort = proxyPort;
            return this;
        }

        public HttpRequestBuilder AddProxyUserName(string proxyUserName)
        {
            this.proxyUserName = proxyUserName;
            return this;
        }

        public HttpRequestBuilder AddProxyUsePassword(string proxyUsePassword)
        {
            this.proxyUsePassword = proxyUsePassword;
            return this;
        }

        public HttpRequestBuilder CreateHttpContent(object content)
        {
            HttpContent httpContent = null;

            if (content != null)
            {
                var ms = new MemoryStream();
                SerializeJsonIntoStream(content, ms);
                ms.Seek(0, SeekOrigin.Begin);
                httpContent = new StreamContent(ms);
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            }

            this.content = httpContent;

            return this;
        }

        public async Task<HttpResponseMessage> SendAsync()
        {
            EnsureArguments();

            var request = new HttpRequestMessage
            {
                Method = method,
                RequestUri = new Uri(requestUri)
            };

            if (content != null)
            {
                request.Content = content;
            }

            if (!string.IsNullOrEmpty(bearerToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }

            request.Headers.Accept.Clear();
            if (!string.IsNullOrEmpty(acceptHeader))
            {
                request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(acceptHeader));
            }

            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = allowAutoRedirect
            };

            if (useProxy)
                handler.Proxy = new WebProxy
                {
                    Address = new Uri($"{proxyHost}:{proxyPort}"),
                    BypassProxyOnLocal = false,
                    UseDefaultCredentials = false
                };

            if (useCredencial)
                handler.Credentials = new NetworkCredential(userName: proxyUserName, password: proxyUsePassword);

            var client = new HttpClient(handler)
            {
                Timeout = timeout,

            };

            return await client.SendAsync(request);
        }

        private void EnsureArguments()
        {
            if (method == null)
            {
                throw new ArgumentNullException("Method");
            }

            if (string.IsNullOrEmpty(requestUri))
            {
                throw new ArgumentNullException("Request Uri");
            }
        }

        public static void SerializeJsonIntoStream(object value, Stream stream)
        {
            using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
            using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
            {
                var js = new JsonSerializer();
                js.Serialize(jtw, value);
                jtw.Flush();
            }
        }

    }
}
