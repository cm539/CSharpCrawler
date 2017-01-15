using CSharpCrawler.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CSharpCrawler
{
    class Downloader : IDownloader
    {
        public int StatusCode { get; private set; }
        public string UserAgent { get; set; }

        public Downloader()
        {
            UserAgent = "Mozilla/5.0 (Windows NT 10.0; rv:49.0) Gecko/20100101 Firefox/49.0";
        }

        public string Download(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return string.Empty;
            }

            #region Test urls
            //url = "https://lenta.ru/";
            //url = "https://lenta.ru/robots.txt";
            //url = "https://lenta.ru/sitemap.xml";
            //url = "https://lenta.ru/news/sitemap1.xml.gz";
            #endregion

            url = url.Trim();
            string result = string.Empty;

            try
            {
                var webRequest = CreateHttpWebRequest(url);

                using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    StatusCode = (int)webResponse.StatusCode;

                    if (webResponse.StatusCode == HttpStatusCode.OK)
                    {
                        result = IsBinary(url) ? ReadBinaryData(webResponse.GetResponseStream())
                                               : ReadTextData(webResponse.GetResponseStream());
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.ToString());
                Console.WriteLine("\tStatus:\t" + ex.Status.ToString());

                return string.Empty;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());

                return string.Empty;
            }

            return result;
        }

        private HttpWebRequest CreateHttpWebRequest(string url)
        {
            HttpWebRequest webRequest = WebRequest.CreateHttp(url);
            webRequest.Method = WebRequestMethods.Http.Get;
            webRequest.UserAgent = UserAgent;
            webRequest.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            return webRequest;
        }

        private string ReadBinaryData(Stream stream)
        {
            string result;

            using (var reader = new GZipStream(stream, CompressionMode.Decompress))
            {
                using (var memoryStream = new MemoryStream())
                {
                    reader.CopyTo(memoryStream);
                    result = Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            }

            return result;
        }

        private string ReadTextData(Stream stream)
        {
            string result;

            using (var reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        private bool IsBinary(string url)
        {
            return GetExtension(url) == "gz";
        }

        private string GetExtension(string filename)
        {
            return filename.Substring(filename.LastIndexOf('.') + 1).ToLower();
        }
    }
}
