namespace RxGraphicsLibrary.Core
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;


    public abstract class RxRenderElementBase : FrameworkElement, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void pf_PropertyChanged(string tpnm)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(tpnm));
        }



        public RxRenderElementBase()
        {
        }

        protected override void OnRender(DrawingContext tdc)
        {
            base.OnRender(tdc);
        }
    }
}
