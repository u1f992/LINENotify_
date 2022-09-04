using Xunit;

using System;
using System.IO;
using System.Net;

public class UnitTest1
{
    string GetToken()
    {
        var ret = Environment.GetEnvironmentVariable("LINENOTIFY_TOKEN", EnvironmentVariableTarget.User);
        if (ret == null) throw new Exception("LINENOTIFY_TOKEN environment variable was not found.");
        return ret;
    }
    readonly Stream Image = File.OpenRead(Path.Join(AppContext.BaseDirectory, "sample.png"));

    [Fact]
    public async void Test1()
    {
        var notifier = new LINENotify(GetToken());
        var ret = await notifier.SendAsync("てすてすasync C#から画像なしのメッセージだよ！");
        Assert.Equal(HttpStatusCode.OK, ret.StatusCode);
    }

    [Fact]
    public async void Test2()
    {
        var notifier = new LINENotify(GetToken());
        var ret = await notifier.SendAsync("てすてすasync C#から画像付きのメッセージだよ！", Image);
        Assert.Equal(HttpStatusCode.OK, ret.StatusCode);
    }

    [Fact]
    public void Test3()
    {
        var notifier = new LINENotify(GetToken());
        var ret = notifier.Send("てすてす C#から画像なしのメッセージだよ！");
        Assert.Equal(HttpStatusCode.OK, ret.StatusCode);
    }

    [Fact]
    public void Test4()
    {
        var notifier = new LINENotify(GetToken());
        var ret = notifier.Send("てすてす C#から画像付きのメッセージだよ！", Image);
        Assert.Equal(HttpStatusCode.OK, ret.StatusCode);
    }
}