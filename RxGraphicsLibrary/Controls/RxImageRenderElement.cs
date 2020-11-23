namespace RxGraphicsLibrary.Controls
{
    using RxGraphicsLibrary.Core;
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;




    public class RxImageRenderElement : Image
    {
        public RxImageRenderElement()
        {
            SnapsToDevicePixels = true;
            ClipToBounds = true;
            //IsHitTestVisible = false;
            Stretch = Stretch.None;
            RenderTransformOrigin = new Point(0.5, 0.5);
        }

        public new ImageSource Source
        {
            get { return base.Source; }
            set
            {
                base.Source = value;
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs tea)
        {
            base.OnMouseLeftButtonDown(tea);

            var txa = VisualTreeHelper.GetContentBounds(this);
            var txb = VisualTreeHelper.GetDescendantBounds(this);
        }

        protected override void OnRender(DrawingContext tdc)
        {
            base.OnRender(tdc);


            tdc.DrawRectangle(Brushes.Red, null, new Rect(0, 0, 20, 20));
            //Point tcpt = new Point(_rctImg.Width / 2, _rctImg.Height / 2);
            //tdc.DrawEllipse(Brushes.Red, null, tcpt, 10, 10);
        }
    }
}
