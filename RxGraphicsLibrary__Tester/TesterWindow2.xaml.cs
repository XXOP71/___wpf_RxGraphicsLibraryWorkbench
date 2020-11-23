namespace RxGraphicsLibrary__Tester
{
    using RxGraphicsLibrary.Core;
    using System;
    using System.Windows;
    using System.Windows.Media.Imaging;




    public sealed partial class TesterWindow2 : RxPopupWindowBase
    {
        public TesterWindow2()
        {
            InitializeComponent();

            Title = GetType().Namespace;
        }

        protected override void pf_Loaded(object tsd, RoutedEventArgs tea)
        {
            base.pf_Loaded(tsd, tea);

            //_rximgre.Source = new BitmapImage(new Uri("pack://application:,,,/RxGraphicsLibrary__Tester;component/__Images/pjw.png"));
            //ResizeMode = System.Windows.ResizeMode.CanResize;

            //Uri turi = new Uri("pack://application:,,,/RxGraphicsLibrary__Tester;component/__Images/pjw.png");
            //_rximgre.Open(turi);

            //_rximgre.BitmapSource = new BitmapImage(new Uri("__Images/pjw.png", UriKind.Relative));
            //_rximgre.Callback = delegate()
            //{
            //};
        }

        /*
        private override void pf_Loaded(object tsd, RoutedEventArgs tea)
        {
            _rximgre.BitmapSource = new BitmapImage(new Uri("__Images/pjw.png", UriKind.Relative));
            _rximgre.Callback = delegate()
            {
                double tmx;
                double tmy;

                double tmh = _fximg.AreaRect.Height;
                double tih = _fximg.ImageRect.Height;
                double tssrv = tmh / tih;
                _fxsbv.ScrollSizeRatio = tssrv;

                if (tssrv < 1)
                {
                    double tssh = tih - tmh;
                    double tprh = _fxsbv.PositionRatio;
                    double tiy = _fximg.AreaRect.Top - (tssh * tprh);
                    _fximg.MoveTop(tiy);
                }
                else
                {
                    tmy = _fximg.GetTopCenter();
                    _fximg.MoveTopCenter(tmy);
                }
            };
        }
        */



    }
}
