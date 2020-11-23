namespace RxGraphicsLibrary.Controls
{
    using System;
    using System.Windows;
    using System.Windows.Controls;



    public partial class RxScrollInput : Grid
    {
        public RxScrollInput()
        {
            InitializeComponent();

            Loaded += pf_Loaded;
            Unloaded += pf_Unloaded;
        }

        private void pf_Loaded(object tsd, RoutedEventArgs tea)
        {
            _rxdit1.Callback = pf__rxdit1_Callback;
            _rxsb1.Callback = pf__rxsb1_Callback;
            pf__rxdit1_Callback();
            pf__rxsb1_Callback();
        }

        private void pf_Unloaded(object tsd, RoutedEventArgs tea)
        {
            _rxdit1.Callback = null;
            _rxsb1.Callback = null;
        }

        private void pf__rxdit1_Callback()
        {
            _rxsb1.PositionRatio = _rxdit1.Ratio;

            if (Callback != null)
                Callback();
        }

        private void pf__rxsb1_Callback()
        {
            _rxdit1.Ratio = _rxsb1.PositionRatio;

            if (Callback != null)
                Callback();
        }


        public double MinValue
        {
            get { return _rxdit1.MinValue; }
            set { _rxdit1.MinValue = value; }
        }

        public double MaxValue
        {
            get { return _rxdit1.MaxValue; }
            set { _rxdit1.MaxValue = value; }
        }

        public double Value
        {
            get { return _rxdit1.Value; }
            set { _rxdit1.Value = value; }
        }

        public Action Callback;

    }
}
