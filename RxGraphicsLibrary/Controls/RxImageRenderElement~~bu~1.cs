namespace RxGraphicsLibrary.Controls
{
    using RxGraphicsLibrary.Core;
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;




    public class RxImageRenderElement : RxRenderElementBase, IDisposable
    {
        public RxImageRenderElement()
        {
            SnapsToDevicePixels = true;
            ClipToBounds = true;
            IsHitTestVisible = false;
        }

        public virtual void Dispose()
        {
            if (_bmpsrc == null) return;
            _bmpsrc = null;
        }

        protected BitmapSource _bmpsrc;
        public BitmapSource BitmapSource
        {
            get { return _bmpsrc; }
            set
            {
                if (value == _bmpsrc) return;
                _bmpsrc = value;
                ImageOriginalSize = new Size(_bmpsrc.PixelWidth, _bmpsrc.PixelHeight);

                InvalidateVisual();
            }
        }
        public Action Callback;
        public Size ImageOriginalSize { get; private set; }      


        protected override void OnRender(DrawingContext tdc)
        {
            base.OnRender(tdc);

            pf_DrawBackground(tdc);
            pf_DrawImage(tdc);
        }

        protected void pf_DrawBackground(DrawingContext tdc)
        {
            Rect trct = new Rect(new Point(0, 0), RenderSize);
            tdc.DrawRectangle(Brushes.White, null, trct);
        }

        protected void pf_DrawImage(DrawingContext tdc)
        {
            if (_bmpsrc != null)
            {
                Rect trct = new Rect(new Point(0, 0), ImageOriginalSize);
                tdc.DrawImage(_bmpsrc, trct);
            }
        }
    }
}
