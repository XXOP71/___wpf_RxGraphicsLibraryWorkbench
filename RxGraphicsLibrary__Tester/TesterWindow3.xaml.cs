namespace RxGraphicsLibrary__Tester
{
    using RxGraphicsLibrary.Core;
    using RxGraphicsLibrary.Tools;
    using System;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;




    public sealed partial class TesterWindow3 : RxPopupWindowBase
    {
        public TesterWindow3()
        {
            InitializeComponent();

            Title = GetType().Namespace;
        }

        protected override void pf_Loaded(object tsd, RoutedEventArgs tea)
        {
            base.pf_Loaded(tsd, tea);
        }

        protected override void pf_ContentRendered(object tsd, EventArgs tea)
        {            
            base.pf_ContentRendered(tsd, tea);


            ResizeMode = ResizeMode.CanResize;
            SizeToContent = SizeToContent.Manual;
            _grdrt.Width = double.NaN;
            _grdrt.Height = double.NaN;


            _rxivc.Open("");
            Dispatcher.BeginInvoke(DispatcherPriority.Render,
                (Action)delegate()
                {
                    OnRenderSizeChanged(null);
                });

            _rxsbVert.Callback = pf_rxsbVert__cbf;
            _rxsbHori.Callback = pf_rxsbHori__cbf;
            _rxsiScale.Callback = pf__rxsiScale_Callback;
            _rxsiRotate.Callback = pf__rxsiRotate_Callback;
        }

        
        protected override void OnRenderSizeChanged(SizeChangedInfo tsci)
        {
            if (tsci != null)
                base.OnRenderSizeChanged(tsci);

            //_rxivc.ResizeUpdate();
            //_rxivc.BoundsDrawUpdate();

            pf_ImageHeightUpdate();
            pf_ImageWidthUpdate();
        }


		private void pf_ImageHeightUpdate()
		{
			Rect rctArea = new Rect(new Point(0, 0), _rxivc.RenderSize);
			Rect rctImg = _rxivc.GetImageBounds();
		
			double tmh = rctArea.Height;
			double tih = rctImg.Height;
			double tssrh = tmh / tih;
			
            _rxsbVert.ScrollSizeRatio = tssrh;
			if (tssrh < 1)
			{
				double tssh = tih - tmh;
				double tprh = _rxsbVert.PositionRatio;
				double tiy = rctArea.Top - (tssh * tprh);
		
				_rxivc.MoveTop(tiy);
                _rxivc.BoundsDrawUpdate();
			}
			else
			{
				double tmy = RxGeom.GetTopCenter(rctArea);

				_rxivc.MoveTopCenter(tmy);
				_rxivc.BoundsDrawUpdate();
			}
		}
		
		private void pf_ImageWidthUpdate()
		{
			Rect rctArea = new Rect(new Point(0, 0), _rxivc.RenderSize);
			Rect rctImg = _rxivc.GetImageBounds();
			
			double tmw = rctArea.Width;
			double tiw = rctImg.Width;
			double tssrw = tmw / tiw;
			
			_rxsbHori.ScrollSizeRatio = tssrw;
			if (tssrw < 1)
			{
				double tssw = tiw - tmw;
                double tprw = _rxsbHori.PositionRatio;
				double tix = rctArea.Left - (tssw * tprw);
		
				_rxivc.MoveLeft(tix);
				_rxivc.BoundsDrawUpdate();
			}
			else
			{
				double tmx = RxGeom.GetLeftCenter(rctArea);

                _rxivc.MoveLeftCenter(tmx);
                _rxivc.BoundsDrawUpdate();
			}
		}
		
		
		private void pf_rxsbVert__cbf()
		{
			Rect rctArea = new Rect(new Point(0, 0), _rxivc.RenderSize);
			Rect rctImg = _rxivc.GetImageBounds();
		
			double tssrh = _rxsbVert.ScrollSizeRatio;
			if (tssrh < 1)
			{
				double tmh = rctArea.Height;
				double tih = rctImg.Height;
				double tssh = tih - tmh;
                double tprh = _rxsbVert.PositionRatio;
				double tiy = rctArea.Top - (tssh * tprh);

                _rxivc.MoveTop(tiy);
                _rxivc.BoundsDrawUpdate();
			}
		}

		private void pf_rxsbHori__cbf()
		{
			Rect rctArea = new Rect(new Point(0, 0), _rxivc.RenderSize);
			Rect rctImg = _rxivc.GetImageBounds();
		
			double tssrw = _rxsbHori.ScrollSizeRatio;
			if (tssrw < 1)
			{
				double tmw = rctArea.Width;
				double tiw = rctImg.Width;
				double tssw = tiw - tmw;
                double tprw = _rxsbHori.PositionRatio;
				double tix = rctArea.Left - (tssw * tprw);
				
				_rxivc.MoveLeft(tix);
                _rxivc.BoundsDrawUpdate();
			}
		}




        private void pf__rxsiScale_Callback()
        {
            //pf_Trace(_rxsiScale.Value.ToString());

            double tsa = _rxsiScale.Value;

            _rxivc.SetScaleCenter(tsa, tsa);
            _rxivc.BoundsDrawUpdate();

            pf_ImageHeightUpdate();
            pf_ImageWidthUpdate();
        }

        private void pf__rxsiRotate_Callback()
        {
            //pf_Trace(_rxsiRotate.Value.ToString());

            double tag = _rxsiRotate.Value;

            _rxivc.SetRotateCenter(RxGeom.GetAngleToRadian(tag));
            _rxivc.BoundsDrawUpdate();

            pf_ImageHeightUpdate();
            pf_ImageWidthUpdate();
        }



        /*
        protected override void OnPreviewKeyDown(KeyEventArgs tea)
        {
            base.OnPreviewKeyDown(tea);

            if ((tea.Key == Key.System) && Keyboard.IsKeyDown(Key.LeftAlt))
                tea.Handled = true;
        }

        protected override void OnMouseWheel(MouseWheelEventArgs tea)
        {
            base.OnMouseWheel(tea);

            if (Keyboard.IsKeyDown(Key.LeftAlt))
            {
                if (tea.Delta < 0)
                {
                    _sa -= 0.1;
                    if (_sa < 0.1) _sa = 0.1;
                    _rxivc.SetScaleCenter(_sa, _sa);
                    _rxivc.BoundsDrawUpdate();
                }
                else if (tea.Delta > 0)
                {
                    _sa += 0.1;
                    _rxivc.SetScaleCenter(_sa, _sa);
                    _rxivc.BoundsDrawUpdate();
                }
            }
            else
            {
                if (tea.Delta < 0)
                {
                    _ag -= 5;
                    if (_ag < 0) _ag = 0;
                    _rxivc.SetRotateCenter(RxGeom.GetAngleToRadian(_ag));
                    _rxivc.BoundsDrawUpdate();
                }
                else if (tea.Delta > 0)
                {
                    _ag += 5;
                    if (_ag > 360) _ag = 360;
                    _rxivc.SetRotateCenter(RxGeom.GetAngleToRadian(_ag));
                    _rxivc.BoundsDrawUpdate();
                }

                RxLog.Trace(_ag.ToString());
            }
        }

        private double _ag = 0;
        private double _sa = 1;
        */


    }
}
