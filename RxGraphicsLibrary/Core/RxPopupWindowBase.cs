namespace RxGraphicsLibrary.Core
{
    using System;
    using System.Windows;



    public abstract class RxPopupWindowBase : RxWindowCore
    {
        public RxPopupWindowBase()
        {
            ResizeMode = ResizeMode.NoResize;
            SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ShowInTaskbar = false;
        }
    }
}
