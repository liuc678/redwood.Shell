using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace BaseClass
{
    public static class WebUtils
    {
        public static bool DownloadFile(string url, string localFilePath)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                httpWebRequest.Method = "GET";
                using (WebResponse response = httpWebRequest.GetResponse())
                {
                    Stream stream = response.GetResponseStream();
                    //检测文件路径是否存在
                    string path = Path.GetDirectoryName(localFilePath);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    using (FileStream fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        stream.CopyTo(fileStream);
                        fileStream.Close();
                        return true;
                    }
                }
            }
            catch (Exception E)
            {
                Log.Write(E, url);
                return false;
            }
        }

        public static async Task<bool> DownloadFileAsync(string url, string localFilePath)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                try
                {
                    // 发送GET请求
                    HttpResponseMessage response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
                    response.EnsureSuccessStatusCode(); // 确保响应成功
                    // 创建文件流
                    using (FileStream fileStream = new FileStream(localFilePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        // 将网络流复制到文件流
                        await response.Content.CopyToAsync(fileStream);
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"下载失败: {ex.Message}");
                    Log.Write(ex);
                    return false;
                }
            }
        }

        public static async Task<bool> startDownloadAsync(string url, string localFilePath, Action<long, long> showJinDu)
        {
            try
            {
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

                long? contentLen = response.Content.Headers.ContentLength;
                long totalLen = contentLen.HasValue ? contentLen.Value : -1;

                //检测文件路径是否存在
                string path = Path.GetDirectoryName(localFilePath);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                else
                    File.Delete(localFilePath);
                using (var downloadFile = File.Create(localFilePath))
                {
                    using (var download = await response.Content.ReadAsStreamAsync())
                    {
                        var buffer = new byte[81920];

                        long totalBytesRead = 0;

                        int bytesRead;

                        while ((bytesRead = await download.ReadAsync(buffer, 0, buffer.Length).ConfigureAwait(false)) != 0)
                        {
                            await downloadFile.WriteAsync(buffer, 0, bytesRead).ConfigureAwait(false);
                            totalBytesRead += bytesRead;

                            showJinDu(totalBytesRead, totalLen);
                        }
                    }
                    downloadFile.Close();
                }
                return true;
            }
            catch (Exception E)
            {
                Log.Write(E);
                return false;
            }
        }
    }
}
