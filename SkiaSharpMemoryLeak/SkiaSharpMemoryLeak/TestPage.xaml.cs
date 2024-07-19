namespace SkiaSharpMemoryLeak;

public partial class TestPage : ContentPage
{
	public TestPage(int counter)
	{
		InitializeComponent();
		TitleLabel.Text = "Counter: " + counter;
	}
}