using System.Net.Http.Headers;

namespace LINENotify;
/// <summary>
/// LINE Notify
/// </summary>
public static class Notifier
{
    static readonly string RequestUri = "https://notify-api.line.me/api/notify";
    /// <summary>
    /// メッセージを送信します。
    /// </summary>
    /// <param name="token"></param>
    /// <param name="message"></param>
    /// <returns><see cref="System.Net.Http.HttpResponseMessage"/></returns>
    public static HttpResponseMessage Send(string token, string message) { return SendAsync(token, message).Result; }
    /// <summary>
    /// 画像付きメッセージを送信します。
    /// </summary>
    /// <param name="token"></param>
    /// <param name="message"></param>
    /// <param name="image"></param>
    /// <returns><see cref="System.Net.Http.HttpResponseMessage"/></returns>
    public static HttpResponseMessage Send(string token, string message, Stream image) { return SendAsync(token, message, image).Result; }
    /// <summary>
    /// メッセージを非同期で送信します。
    /// </summary>
    /// <param name="token"></param>
    /// <param name="message"></param>
    /// <returns><see cref="System.Net.Http.HttpResponseMessage"/></returns>
    public static async Task<HttpResponseMessage> SendAsync(string token, string message) { return await SendAsync(token, message, Stream.Null); }
    /// <summary>
    /// 画像付きメッセージを非同期で送信します。
    /// </summary>
    /// <param name="token"></param>
    /// <param name="message"></param>
    /// <param name="image"></param>
    /// <returns><see cref="System.Net.Http.HttpResponseMessage"/></returns>
    public static async Task<HttpResponseMessage> SendAsync(string token, string message, Stream image) { return await SendAsync(token, message, image, CancellationToken.None); }
    /// <summary>
    /// メッセージを非同期で送信します。
    /// </summary>
    /// <param name="token"></param>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="System.Net.Http.HttpResponseMessage"/></returns>
    public static async Task<HttpResponseMessage> SendAsync(string token, string message, CancellationToken cancellationToken) { return await SendAsync(token, message, Stream.Null, cancellationToken); }
    /// <summary>
    /// 画像付きメッセージを非同期で送信します。
    /// </summary>
    /// <param name="token"></param>
    /// <param name="message"></param>
    /// <param name="image"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="System.Net.Http.HttpResponseMessage"/></returns>
    public static async Task<HttpResponseMessage> SendAsync(string token, string message, Stream image, CancellationToken cancellationToken)
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var contents = new MultipartFormDataContent();
            contents.Add(new StringContent(message), "message");
            
            if (image != Stream.Null)
            {
                var imageContent = new StreamContent(image);
                contents.Add(imageContent, "imageFile", "*");
            }

            var result = await client.PostAsync(RequestUri, contents, cancellationToken).ConfigureAwait(false);
            return result;
        }
    }
}
