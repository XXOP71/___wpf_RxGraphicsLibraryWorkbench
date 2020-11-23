namespace RxGraphicsLibrary.Controls
{
    using RxGraphicsLibrary.Core;
    using System;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;




    public class RxImageRenderElement : RxRenderElementBase
    {
        public RxImageRenderElement()
        {
            SnapsToDevicePixels = true;
            ClipToBounds = true;
            IsHitTestVisible = false;
        }

        protected BitmapSource _bmpsrc;
        protected MatrixTransform _mtrtf;
        protected Rect _rctImg;
        protected Rect _rctRender;
        protected Rect _rctArea;
        


        public void Open(Uri turi)
        {
            if (_bmpsrc == null)
            {
                _bmpsrc = new BitmapImage(turi);
                _mtrtf = new MatrixTransform();


                //Matrix tmtr = _mtrtf.Matrix;
                //tmtr.RotateAt(45, 250, 250);
                //_mtrtf.Matrix = tmtr;
                RenderTransform = _mtrtf;

                _rctImg = new Rect(0, 0, _bmpsrc.PixelWidth, _bmpsrc.PixelHeight);
                Width = _rctImg.Width;
                Height = _rctImg.Height;
                /*
                Width = _rctimg.Width;
                Height = _rctimg.Height;
                _rctrender = VisualTreeHelper.GetContentBounds(this);


                var txa = VisualTreeHelper.GetDescendantBounds(this);
                */
                

                InvalidateVisual();
                //InvalidateArrange();
                //InvalidateMeasure();
            }
        }

        public void Close()
        {
            if (_bmpsrc == null) return;
            _bmpsrc = null;
            _mtrtf = null;
            RenderTransform = null;
        }



        private void pf_DrawBorder(DrawingContext tdc)
        {
            const double ttw = 20;

            Rect trct;

            trct = _rctImg;
            trct.Height = ttw;
            tdc.DrawRectangle(Brushes.Blue, null, trct);

            trct = _rctImg;
            trct.Inflate(0, -ttw);
            trct.Width = ttw;
            tdc.DrawRectangle(Brushes.Blue, null, trct);

            trct = _rctImg;
            trct.Y = _rctImg.Bottom - ttw;
            trct.Height = ttw;
            tdc.DrawRectangle(Brushes.Blue, null, trct);

            trct = _rctImg;
            trct.Inflate(0, -ttw);
            trct.X = _rctImg.Right - ttw;
            trct.Width = ttw;
            tdc.DrawRectangle(Brushes.Blue, null, trct);
        }

        protected override void OnRender(DrawingContext tdc)
        {
            base.OnRender(tdc);


            if (_bmpsrc == null) return;

            tdc.DrawRectangle(Brushes.Red, null, _rctImg);
            tdc.DrawImage(_bmpsrc, _rctImg);
            pf_DrawBorder(tdc);

            tdc.DrawRectangle(Brushes.Red, null, new Rect(0, 0, 20, 20));
            Point tcpt = new Point(_rctImg.Width / 2, _rctImg.Height / 2);
            tdc.DrawEllipse(Brushes.Red, null, tcpt, 10, 10);

            _rctRender = new Rect(new Point(0, 0), ((UIElement)Parent).RenderSize);
            _rctArea = VisualTreeHelper.GetContentBounds(this);
        }

    }
}
