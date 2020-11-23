namespace RxGraphicsLibrary.Core
{
    using RxGraphicsLibrary.Controls;
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;




    public abstract class RxWindowCore : Window, IDisposable
    {
        protected static void pf_Trace(string tmsg)
        {
            Debug.WriteLine(tmsg);
        }

        public RxWindowCore()
        {
            SnapsToDevicePixels = true;
            ResizeMode = ResizeMode.CanResize;
            SizeToContent = SizeToContent.Manual;
            WindowStartupLocation = WindowStartupLocation.Manual;

            LinearGradientBrush tlgb = new LinearGradientBrush()
            {
                EndPoint = new Point(0.5, 1),
                StartPoint = new Point(0.5, 0)
            };
            tlgb.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FFD3D3D3"), 0));
            tlgb.GradientStops.Add(new GradientStop((Color)ColorConverter.ConvertFromString("#FFEEEEEE"), 1));
            Background = tlgb;

            FontFamily = new FontFamily("맑은 고딕");
            Loaded += pf_Loaded;
            ContentRendered += pf_ContentRendered;
            Closing += pf_Closing;
            KeyDown += pf_KeyDown;
        }
        protected Panel _pnlrt;
        protected Action<object[]> _callback;
        public void SetCallback(Action<object[]> tcb)
        {
            _callback = tcb;
        }
        protected void pf_Callback(object[] targs)
        {
            if (_callback != null)
                _callback(targs);
        }

        protected virtual void pf_Loaded(object tsd, RoutedEventArgs tea)
        {
            //pf_Trace("RxWindowCore.pf_Loaded");
            _pnlrt = (Panel)Content;
        }

        protected virtual void pf_ContentRendered(object tsd, EventArgs tea)
        {
            //pf_Trace("RxWindowCore.pf_ContentRendered");
            //SizeToContent = SizeToContent.Manual;
            //MinWidth = ActualWidth;
            //MinHeight = ActualHeight;
            //_pnlrt.Width = double.NaN;
            //_pnlrt.Height = double.NaN;
        }

        protected virtual void pf_Closing(object tsd, CancelEventArgs tea)
        {
            //pf_Trace("RxWindowCore.pf_Closing");
        }

        protected virtual void pf_KeyDown(object tsd, KeyEventArgs tea)
        {
            //pf_Trace("RxWindowCore.pf_KeyDown");
        }

        public virtual void Dispose()
        {
            if (_pnlrt == null) return;
            Loaded -= pf_Loaded;
            ContentRendered -= pf_ContentRendered;
            Closing -= pf_Closing;
            KeyDown -= pf_KeyDown;
            _callback = null;
            _pnlrt = null;
        }


        public bool Alert(string tmsg)
        {
            return RxAlertWindow.Open(this, null, RxAlertWindow.TypeAlert, tmsg);
        }

        public bool Alert(string tmsg, double tw, double th)
        {
            return RxAlertWindow.Open(this, null, RxAlertWindow.TypeAlert, tmsg, tw, th);
        }

        public bool Confirm(string tmsg)
        {
            return RxAlertWindow.Open(this, null, RxAlertWindow.TypeConfirm, tmsg);
        }

        public bool Confirm(string tmsg, double tw, double th)
        {
            return RxAlertWindow.Open(this, null, RxAlertWindow.TypeConfirm, tmsg, tw, th);
        }

    }
}
