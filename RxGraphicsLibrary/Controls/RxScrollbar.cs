namespace RxGraphicsLibrary.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;




    public enum RxScrollbarType
    {
        Vertical,
        Horizontal
    }

    public enum RxScrollbarEventType
    {
        Update
    }


    public class RxScrollbar : FrameworkElement
    {
        protected const double _amg = 10;
        protected const double _amga = 20;
        protected const double _tmg = 3;
        protected const double _tmga = _tmg * 2;

        protected const double _minsz = 40;
        protected const double _maxsz = 4000;


        public RxScrollbar()
        {
            SnapsToDevicePixels = true;
            ClipToBounds = true;
            Cursor = Cursors.Hand;
            Opacity = 0.35;
            IsHitTestVisible = false;

            _brsTrack = new SolidColorBrush()
            {
                Color = Color.FromRgb(0xff, 0x66, 0x00),
                Opacity = 1
            };
            _brsTrack.Freeze();

            _brsScrollArea = new SolidColorBrush()
            {
                Color = Color.FromRgb(0x88, 0xff, 0x11),
                Opacity = 0.75
            };
            _brsScrollArea.Freeze();

            _brsThumb = new SolidColorBrush()
            {
                Color = Color.FromRgb(0xff, 0xff, 0xff),
                Opacity = 1
            };
            _brsThumb.Freeze();


            _scrollSizeRatio = 1.0;
            _positionRatio = 0.0;


            Unloaded += pf_Unloaded;
        }

        public RxScrollbarType Type { get; set; }

        protected Brush _brsTrack;
        protected Brush _brsScrollArea;
        protected Brush _brsThumb;

        protected Rect _rctTrack;
        protected Rect _rctScrollArea;
        protected Rect _rctThumb;

        protected double _scrollSizeRatio;
        protected double _positionRatio;

        protected bool _enabled;

        public Action Callback;


        protected void pf_Unloaded(object tsd, RoutedEventArgs tea)
        {
            _brsTrack = null;
            _brsScrollArea = null;
            _brsThumb = null;
            Callback = null;
        }



        public double ScrollSizeRatio
        {
            get { return _scrollSizeRatio; }
            set
            {
                if (value < 0) value = 0;
                else if (value > 1) value = 1;

                if (value == _scrollSizeRatio) return;
                _scrollSizeRatio = value;

                InvalidateVisual();
                pf_EnabledCheck();
            }
        }

        public double PositionRatio
        {
            get { return _positionRatio; }
            set
            {
                if (value < 0) value = 0;
                else if (value > 1) value = 1;

                if (value == _positionRatio) return;
                _positionRatio = value;

                InvalidateVisual();
            }
        }

        public bool Enabled
        {
            get { return _enabled; }
            set
            {
                if (_enabled == value) return;

                _enabled = value;
                if (_enabled)
                {
                    Opacity = 1;
                    IsHitTestVisible = true;
                }
                else
                {
                    Opacity = 0.15;
                    IsHitTestVisible = false;
                }
            }
        }


        private void pf_EnabledCheck()
        {
            if (_scrollSizeRatio < 1)
                Enabled = true;
            else
                Enabled = false;
        }

        private void pf_UpdateRects()
        {
            _rctTrack = new Rect(new Point(0, 0), RenderSize);
            _rctScrollArea = _rctTrack;
            _rctThumb = _rctTrack;

            if (Type == RxScrollbarType.Vertical)
            {
                double tth = _rctTrack.Height - _amga;
                if (tth < _amga) tth = _amga;

                double tsah = tth * (1 - _scrollSizeRatio);

                _rctScrollArea.Height = tsah;
                _rctScrollArea.Y = (tth / 2) - (tsah / 2) + _amg;
                _rctThumb.Height = (tth * _scrollSizeRatio) + _amga;
                _rctThumb.Y = _rctScrollArea.Height * _positionRatio;
            }
            else if (Type == RxScrollbarType.Horizontal)
            {
                double ttw = _rctTrack.Width - _amga;
                if (ttw < _amga) ttw = _amga;

                double tsaw = ttw * (1 - _scrollSizeRatio);

                _rctScrollArea.Width = tsaw;
                _rctScrollArea.X = (ttw / 2) - (tsaw / 2) + _amg;
                _rctThumb.Width = (ttw * _scrollSizeRatio) + _amga;
                _rctThumb.X = _rctScrollArea.Width * _positionRatio;
            }
        }


        protected override void OnRender(DrawingContext tdc)
        {
            base.OnRender(tdc);

            pf_UpdateRects();
            pf_DrawTrack(tdc);
            //pf_DrawScrollArea(tdc);
            pf_DrawThumb(tdc);
        }


        private void pf_DrawTrack(DrawingContext tdc)
        {
            tdc.DrawRectangle(_brsTrack, null, _rctTrack);
        }

        private void pf_DrawScrollArea(DrawingContext tdc)
        {
            tdc.DrawRectangle(_brsScrollArea, null, _rctScrollArea);
        }

        private void pf_DrawThumb(DrawingContext tdc)
        {
            Rect trct = _rctThumb;
            trct.Inflate(-_tmg, -_tmg);
            tdc.DrawRoundedRectangle(_brsThumb, null, trct, _tmg, _tmg);
        }


        protected override void OnMouseMove(MouseEventArgs tea)
        {
            if (tea != null)
                base.OnMouseMove(tea);

            if (_bmd)
            {
                Point tmpt = Mouse.GetPosition(this);


                double tv = 0;
                double tf = 0;

                if (Type == RxScrollbarType.Vertical)
                {
                    tv = tmpt.Y - _rctScrollArea.Y;
                    tf = _rctScrollArea.Height;
                }
                else if (Type == RxScrollbarType.Horizontal)
                {
                    tv = tmpt.X - _rctScrollArea.X;
                    tf = _rctScrollArea.Width;
                }

                if (tv < 0)
                    tv = 0;
                else if (tv > tf)
                    tv = tf;

                if (tf == 0)
                    _positionRatio = 0;
                else
                    _positionRatio = tv / tf;

                InvalidateVisual();


                if (Callback != null)
                    Callback();
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs tea)
        {
            if (tea != null)
                base.OnMouseLeftButtonUp(tea);

            if (_bmd)
            {
                _bmd = false;
                Mouse.Capture(null);
            }
        }

        protected override void OnLostMouseCapture(MouseEventArgs tea)
        {
            if (tea != null)
                base.OnLostMouseCapture(tea);

            OnMouseLeftButtonUp(null);
        }

        protected bool _bmd = false;
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs tea)
        {
            if (tea != null)
                base.OnMouseLeftButtonDown(tea);

            if (_bmd == false)
            {
                _bmd = true;
                Mouse.Capture(this);
                OnMouseMove(null);
            }
        }

    }
}
