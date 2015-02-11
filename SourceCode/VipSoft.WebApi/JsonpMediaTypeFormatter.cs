using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace VipSoft.WebApi
{
    public class JsonpMediaTypeFormatter : JsonMediaTypeFormatter
    {
        public JsonpMediaTypeFormatter(string callback = null)
        {
            Callback = callback;
        }

        public string Callback { get; private set; }

        public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content,
            TransportContext transportContext)
        {
            if (string.IsNullOrEmpty(Callback))
            {
                return base.WriteToStreamAsync(type, value, writeStream, content, transportContext);
            }
            try
            {
                WriteToStream(type, value, writeStream, content);
                return Task.FromResult(new AsyncVoid());
            }
            catch (Exception exception)
            {
                var source = new TaskCompletionSource<AsyncVoid>();
                source.SetException(exception);
                return source.Task;
            }
        }

        private void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
        {
            JsonSerializer serializer = JsonSerializer.Create(SerializerSettings);
            using (var streamWriter = new StreamWriter(writeStream, SupportedEncodings.First()))
            using (var jsonTextWriter = new JsonTextWriter(streamWriter) {CloseOutput = false})
            {
                jsonTextWriter.WriteRaw(Callback + "(");
                serializer.Serialize(jsonTextWriter, value);
                jsonTextWriter.WriteRaw(")");
            }
        }

        public override MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request,
            MediaTypeHeaderValue mediaType)
        {
            if (request.Method != HttpMethod.Get)
            {
                return this;
            }
            string callback;
            if (request.GetQueryNameValuePairs().ToDictionary(pair => pair.Key,
                pair => pair.Value).TryGetValue("callback", out callback))
            {
                return new JsonpMediaTypeFormatter(callback);
            }
            return this;
        }

        [StructLayout(LayoutKind.Sequential, Size = 1)]
        private struct AsyncVoid
        {
        }
    }
}