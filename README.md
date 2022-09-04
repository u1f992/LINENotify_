# LINENotify

LINE Notifyを送信する.NETライブラリ

```csharp
using System.IO;
using System.Net;

var notifier = new LINENotify("TOKEN");

notifier.Send("てすてす C#から画像なしのメッセージだよ！");
// or
var ret = await notifier.SendAsync("てすてすasync C#から画像付きのメッセージだよ！", File.OpenRead("sample.png"));
```

[テスト用画像](https://www.irasutoya.com/2015/08/blog-post_937.html)（いらすとや）
