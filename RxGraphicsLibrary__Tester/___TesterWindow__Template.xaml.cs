namespace RxGraphicsLibrary__Tester
{
    using RxGraphicsLibrary.Core;
    using System;
    using System.Windows;
    using System.Windows.Media.Imaging;




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



    }
}
