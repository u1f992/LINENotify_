# LINENotify

LINE Notifyを送信する.NETライブラリ

```csharp
using System.IO;
using System.Net;
using LINENotify;

Notifier.Send("TOKEN", "てすてす C#から画像なしのメッセージだよ！");
// or
var ret = await Notifier.SendAsync("TOKEN", "てすてすasync C#から画像付きのメッセージだよ！", File.OpenRead("sample.png"));
```
