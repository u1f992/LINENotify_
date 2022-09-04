using System.Net.Http.Headers;

/// <summary>
/// LINE Notify
/// </summary>
public class LINENotify
{
    readonly string RequestUri = "https://notify-api.line.me/api/notify";
    readonly string Token;
    
    /// <summary>
    /// LINE Notify
    /// </summary>
    /// <param name="token"></param>
    public LINENotify(string token)
    {
        Token = token;
    }
    
    /// <summary>
    /// メッセージを送信します。
    /// </summary>
    /// <param name="message"></param>
    /// <returns><see cref="System.Net.Http.HttpResponseMessage"/></returns>
    public HttpResponseMessage Send(string message) { return SendAsync(message).Result; }
    /// <summary>
    /// 画像付きメッセージを送信します。
    /// </summary>
    /// <param name="message"></param>
    /// <param name="image"></param>
    /// <returns><see cref="System.Net.Http.HttpResponseMessage"/></returns>
    public HttpResponseMessage Send(string message, Stream image) { return SendAsync(message, image).Result; }
    /// <summary>
    /// メッセージを非同期で送信します。
    /// </summary>
    /// <param name="message"></param>
    /// <returns><see cref="System.Net.Http.HttpResponseMessage"/></returns>
    public async Task<HttpResponseMessage> SendAsync(string message) { return await SendAsync(message, Stream.Null); }
    /// <summary>
    /// 画像付きメッセージを非同期で送信します。
    /// </summary>
    /// <param name="message"></param>
    /// <param name="image"></param>
    /// <returns><see cref="System.Net.Http.HttpResponseMessage"/></returns>
    public async Task<HttpResponseMessage> SendAsync(string message, Stream image) { return await SendAsync(message, image, CancellationToken.None); }
    /// <summary>
    /// メッセージを非同期で送信します。
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="System.Net.Http.HttpResponseMessage"/></returns>
    public async Task<HttpResponseMessage> SendAsync(string message, CancellationToken cancellationToken) { return await SendAsync(message, Stream.Null, cancellationToken); }
    /// <summary>
    /// 画像付きメッセージを非同期で送信します。
    /// </summary>
    /// <param name="message"></param>
    /// <param name="image"></param>
    /// <param name="cancellationToken"></param>
    /// <returns><see cref="System.Net.Http.HttpResponseMessage"/></returns>
    public async Task<HttpResponseMessage> SendAsync(string message, Stream image, CancellationToken cancellationToken)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("multipart/form-data"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);

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
