namespace RxGraphicsLibrary.Controls
{
    using RxGraphicsLibrary.Core;
    using RxGraphicsLibrary.Tools;
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Threading;




    public class RxImageVisualContainer : RxVisualContainerBase
    {
        public RxImageVisualContainer()
        {
            //SnapsToDevicePixels = true;
            Cursor = Cursors.Hand;
            //IsHitTestVisible = false;


            _rxiv = new RxImageVisual();
            AddChild(_rxiv);

            _rxibv = new RxImageBoundsVisual();
            AddChild(_rxibv);
        }
        protected RxImageVisual _rxiv;
        protected RxImageBoundsVisual _rxibv;

        protected override void OnRender(DrawingContext tdc)
        {
            base.OnRender(tdc);

            _rctBounds = new Rect(new Point(0, 0), RenderSize);

            Brush tbrs1 = Brushes.Green.Clone();
            tdc.DrawRectangle(tbrs1, null, _rctBounds);            
        }



        public void Close()
        {
            _rxiv.Close();
        }

        public void Open(string tfp)
        {
            _rxiv.Open(tfp);
        }



        public Rect GetImageBounds()
        {
            return _rxiv.GetBounds();
        }


        public void ResizeUpdate()
        {
            _rxiv.ResizeUpdate();
        }


        public void MoveLeft(double tv)
        {
            _rxiv.MoveLeft(tv);
        }

        public void MoveTop(double tv)
        {
            _rxiv.MoveTop(tv);
        }

        public void MoveLeftCenter(double tv)
        {
            _rxiv.MoveLeftCenter(tv);
        }

        public void MoveTopCenter(double tv)
        {
            _rxiv.MoveTopCenter(tv);
        }

        public void MoveCenter(double tx, double ty)
        {
            _rxiv.MoveCenter(tx, ty);
        }

        public void SetRotateCenter(double trd)
        {
            _rxiv.SetRotateCenter(trd);
        }

        public void SetScaleCenter(double tsx, double tsy)
        {
            _rxiv.SetScaleCenter(tsx, tsy);
        }

        public void BoundsDrawUpdate()
        {
            _rxibv.DrawUpdate(_rxiv.GetBounds());
        }

    }



    public class RxImageVisual : DrawingVisual
    {
        protected static readonly Uri _baseUri = new Uri("pack://application:,,,/RxGraphicsLibrary__Tester;component");


        public RxImageVisual()
        {
            _mtrtf = new MatrixTransform();
            Transform = _mtrtf;

            VisualBitmapScalingMode = BitmapScalingMode.NearestNeighbor;
        }
        protected MatrixTransform _mtrtf;
        protected BitmapSource _bmpsrc;
        protected Size _szImage;
        protected Rect _rctImage;
        protected Rect _rctBounds;


        public void Close()
        {
            if (_bmpsrc == null) return;
            _mtrtf.Matrix = Matrix.Identity;
            _bmpsrc = null;
        }

        public void Open(string tfp)
        {
            if (_bmpsrc == null)
            {
                _bmpsrc = new BitmapImage(new Uri(_baseUri, "/___Images/pjw.png"));
                _szImage = new Size(_bmpsrc.PixelWidth, _bmpsrc.PixelHeight);

                pf_DrawImage();
            }
        }



        private void pf_MeasureBounds()
        {
            _rctBounds = RxGeom.GetVisualBounds(this, _rctImage);
            _rctBounds.Inflate(10, 10);
            //RxGeom.RectBounds(ref _rctBounds);
        }

        protected void pf_DrawImage()
        {
            if (_bmpsrc == null) return;

            using (DrawingContext tdc = RenderOpen())
            {
                _rctImage = new Rect(new Point(0, 0), _szImage);
                pf_MeasureBounds();

                tdc.DrawImage(_bmpsrc, _rctImage);
            }
        }

        protected void pf_UpdateMatrix(Func<Matrix, Matrix> twcbf)
        {
            if (_bmpsrc == null) return;

            _mtrtf.Matrix = twcbf(_mtrtf.Matrix);
            pf_MeasureBounds();
        }




        public Rect GetBounds()
        { 
            return _rctBounds;
        }
		
        public double GetWidth()
        {
            return RxGeom.GetWidth(_rctBounds);
        }
        public double GetHeight()
        {
            return RxGeom.GetHeight(_rctBounds);
        }
		

        public double GetHalfWidth()
        {
            return RxGeom.GetHalfWidth(_rctBounds);
        }
        public double GetHalfHeight()
        {
            return RxGeom.GetHalfHeight(_rctBounds);
        }

		
        public double GetLeft()
        {
            return RxGeom.GetLeft(_rctBounds);
        }		
        public double GetTop()
        {
            return RxGeom.GetTop(_rctBounds);
        }
        public double GetRight()
        {
            return RxGeom.GetRight(_rctBounds);
        }
        public double GetBottom()
        {
            return RxGeom.GetBottom(_rctBounds);
        }

		
        public double GetLeftCenter()
        {
            return RxGeom.GetLeftCenter(_rctBounds);
        }
        public double GetTopCenter()
        {
            return RxGeom.GetTopCenter(_rctBounds);
        }


        public void ResizeUpdate()
        {
            FrameworkElement tpfe = (FrameworkElement)VisualParent;
            double tcx = tpfe.ActualWidth / 2;
            double tcy = tpfe.ActualHeight / 2;
            MoveCenter(tcx, tcy);
        }


        public void SetRotateCenter(double trd)
        {
            pf_UpdateMatrix(delegate(Matrix tmtr)
            {
                double tyrd = RxGeom.GetRadian1(tmtr);
                double tnrd = RxGeom.CheckRadian(trd);
                if (tnrd != tyrd)
                {
                    double tcx = RxGeom.GetLeftCenter(_rctBounds);
                    double tcy = RxGeom.GetTopCenter(_rctBounds);
                    tmtr.Translate(-tcx, -tcy);

                    tmtr.Rotate(-RxGeom.GetRadianToAngle(tyrd));
                    tmtr.Rotate(RxGeom.GetRadianToAngle(tnrd));
                    tmtr.Translate(tcx, tcy);
                }

                return tmtr;
            });
        }

        public void SetScaleCenter(double tsx, double tsy)
        {
            if (tsx < 0.1) return;
            if (tsy < 0.1) return;

            pf_UpdateMatrix(delegate(Matrix tmtr)
            {
                double tysx = RxGeom.GetScaleX(tmtr);
                double tysy = RxGeom.GetScaleY(tmtr);

                double tnsx = RxGeom.DoubleRound(tsx);
                double tnsy = RxGeom.DoubleRound(tsy);

                if ((tysx != tnsx) && (tysy != tnsy))
                {
                    double tcx = RxGeom.GetLeftCenter(_rctBounds);
                    double tcy = RxGeom.GetTopCenter(_rctBounds);
                    tmtr.Translate(-tcx, -tcy);

                    double tbsx = 1 / tysx;
                    double tbsy = 1 / tysy;
                    tmtr.Scale(tbsx, tbsy);
                    tmtr.Scale(tnsx, tnsy);
                    tmtr.Translate(tcx, tcy);
                }

                return tmtr;
            });
        }

        public void MoveLeft(double tv)
        {
            pf_UpdateMatrix(delegate(Matrix tmtr)
            {
                double ttx = tv - RxGeom.GetLeft(_rctBounds);
                double tty = 0;
                tmtr.Translate(ttx, tty);

                return tmtr;
            });
        }

        public void MoveTop(double tv)
        {
            pf_UpdateMatrix(delegate(Matrix tmtr)
            {
                double ttx = 0;
                double tty = tv - RxGeom.GetTop(_rctBounds);
                tmtr.Translate(ttx, tty);

                return tmtr;
            });
        }

        public void MoveLeftCenter(double tv)
        {
            pf_UpdateMatrix(delegate(Matrix tmtr)
            {
                double ttx = tv - RxGeom.GetLeftCenter(_rctBounds);
                double tty = 0;
                tmtr.Translate(ttx, tty);

                return tmtr;
            });
        }

        public void MoveTopCenter(double tv)
        {
            pf_UpdateMatrix(delegate(Matrix tmtr)
            {
                double ttx = 0;
                double tty = tv - RxGeom.GetTopCenter(_rctBounds);
                tmtr.Translate(ttx, tty);

                return tmtr;
            });
        }

        public void MoveCenter(double tmx, double tmy)
        {
            pf_UpdateMatrix(delegate(Matrix tmtr)
            {
                double tcx = RxGeom.GetLeftCenter(_rctBounds);
                double tcy = RxGeom.GetTopCenter(_rctBounds);
                double ttx = tmx - tcx;
                double tty = tmy - tcy;
                tmtr.Translate(ttx, tty);

                return tmtr;
            });
        }
    }



    public class RxImageBoundsVisual : DrawingVisual
    {
        public RxImageBoundsVisual() { }
        private Rect _rct;

        protected void pf_DrawImage()
        {
            if (_rct.IsEmpty) return;

            using (DrawingContext tdc = RenderOpen())
            {
                RxLog.Trace(_rct.ToString());

                //Brush tbrs1 = Brushes.Blue.Clone();
                //tbrs1.Opacity = 0.25;
                //tdc.DrawRectangle(tbrs1, null, _rct);

                Brush tbrs2 = Brushes.Blue.Clone();
                tbrs2.Opacity = 1.0;
                Pen tp1 = new Pen(tbrs2, 3);
                tdc.DrawRectangle(null, tp1, _rct);
            }
        }

        public void DrawUpdate(Rect trct)
        {
            _rct = trct;
            pf_DrawImage();
        }
    }

}
