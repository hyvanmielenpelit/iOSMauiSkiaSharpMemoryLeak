using SkiaSharp;
using SkiaSharp.Views.Maui;
using SkiaSharp.Views.Maui.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkiaSharpMemoryLeak
{
    public enum BackgroundBitmaps
    {
        OldPaper = 0,
        Button
    }
    public class BackgroundView : SKCanvasView
    {
        public BackgroundView() : base()
        {
            PaintSurface += Base_PaintSurface;
        }

        BackgroundBitmaps _backgroundBitmap;
        public BackgroundBitmaps BackgroundBitmap
        {
            get { return _backgroundBitmap; } set { _backgroundBitmap = value; InvalidateSurface(); }
        }

        private void Base_PaintSurface(object sender, SKPaintSurfaceEventArgs e)
        {
            SKImageInfo info = e.Info;
            SKSurface surface = e.Surface;
            SKCanvas canvas = surface.Canvas;
            float canvaswidth = e.Info.Width;
            float canvasheight = e.Info.Height;

            canvas.Clear();

            SKImage bmp = null;
            BackgroundBitmaps bitmapStyle = BackgroundBitmap;
            switch (bitmapStyle)
            {
                case BackgroundBitmaps.OldPaper:
                    bmp = MainPage.OldPaperBitmap;
                    break;
                case BackgroundBitmaps.Button:
                    bmp = MainPage.ButtonBitmap;
                    break;
            }

            if (bmp != null)
            {
                SKRect target_rect = new SKRect();
                target_rect.Left = 0;
                target_rect.Top = 0;
                target_rect.Right = canvaswidth;
                target_rect.Bottom = canvasheight;
                canvas.DrawImage(bmp, target_rect);
            }
            canvas.Flush();
        }


        private double _currentWidth = 0;
        private double _currentHeight = 0;
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            if (width != _currentWidth || height != _currentHeight)
            {
                _currentWidth = width;
                _currentHeight = height;
                if(IsVisible)
                    InvalidateSurface();
            }
        }

    }
}
