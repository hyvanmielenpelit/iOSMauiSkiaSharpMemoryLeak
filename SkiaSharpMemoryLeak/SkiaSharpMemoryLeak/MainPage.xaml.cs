using SkiaSharp;
using System.Reflection;

namespace SkiaSharpMemoryLeak
{
    public partial class MainPage : ContentPage
    {
        int count = 0;
        public static SKImage OldPaperBitmap = null;
        public static SKImage ButtonBitmap = null;

        public MainPage()
        {
            InitializeComponent();
            Assembly assembly = typeof(App).GetTypeInfo().Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("SkiaSharpMemoryLeak.Assets.oldpaper.png"))
            {
                SKBitmap bmp = SKBitmap.Decode(stream);
                bmp.SetImmutable();
                OldPaperBitmap = SKImage.FromBitmap(bmp);
            }
            using (Stream stream = assembly.GetManifestResourceStream("SkiaSharpMemoryLeak.Assets.button.png"))
            {
                SKBitmap bmp = SKBitmap.Decode(stream);
                bmp.SetImmutable();
                ButtonBitmap = SKImage.FromBitmap(bmp);
            }
        }

        private async void OnCounterClicked(object sender, EventArgs e)
        {
            CounterBtn.IsEnabled = false;
            count = 500;

            for (int i = 0; i < count; i++)
            {
                TestPage tp = new TestPage(i + 1);
                await App.Current.MainPage.Navigation.PushModalAsync(tp, false);
                await Task.Delay(350);
                await App.Current.MainPage.Navigation.PopModalAsync();
                await Task.Delay(150);
            }
            CounterBtn.IsEnabled = true;
        }
    }

}
