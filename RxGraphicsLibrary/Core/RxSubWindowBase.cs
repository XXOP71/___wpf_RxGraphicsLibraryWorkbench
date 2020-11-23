namespace RxGraphicsLibrary.Core
{
    using System;
    using System.Windows;



    public abstract class RxSubWindowBase : RxWindowCore
    {
        public RxSubWindowBase()
        {
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
        }

        protected override void pf_ContentRendered(object tsd, EventArgs tea)
        {
            SizeToContent = SizeToContent.Manual;
            MinWidth = ActualWidth;
            MinHeight = ActualHeight;
            _pnlrt.Width = double.NaN;
            _pnlrt.Height = double.NaN;
        }
    }
}
