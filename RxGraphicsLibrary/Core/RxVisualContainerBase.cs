namespace RxGraphicsLibrary.Core
{
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Media;




    public abstract class RxVisualContainerBase : FrameworkElement, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void pf_PropertyChanged(string tpnm)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(tpnm));
        }



        public RxVisualContainerBase()
        {
            ClipToBounds = true;
            HorizontalAlignment = HorizontalAlignment.Stretch;
            VerticalAlignment = VerticalAlignment.Stretch;

            _children = new VisualCollection(this);

            Loaded += pf_Loaded;
            Unloaded += pf_Unloaded;
        }
        protected Rect _rctBounds;
        public Rect GetBounds()
        {
            return _rctBounds;
        }

        protected VisualCollection _children;
        public VisualCollection GetChildren()
        {
            return _children;
        }
        public void AddChild(Visual tv)
        {
            _children.Add(tv);
            //AddVisualChild(tv);
            //AddLogicalChild(tv);
        }


        protected override Visual GetVisualChild(int ti)
        {
            if ((_children != null) && (_children.Count > 0))
                return _children[ti];
            else
                return null;
        }

        protected override int VisualChildrenCount
        {
            get { return _children.Count; }
        }




        protected virtual void pf_Loaded(object tsd, RoutedEventArgs tea)
        {
            Loaded -= pf_Loaded;
            

        }

        protected virtual void pf_Unloaded(object tsd, RoutedEventArgs tea)
        {
            Unloaded -= pf_Unloaded;

            _children.Clear();
        }
        

    }
}
