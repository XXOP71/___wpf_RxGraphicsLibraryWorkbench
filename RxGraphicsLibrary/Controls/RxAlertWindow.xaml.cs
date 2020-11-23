namespace RxGraphicsLibrary.Controls
{
    using RxGraphicsLibrary.Core;
    using System;
    using System.Windows;
    using System.Windows.Input;




    public partial class RxAlertWindow : RxPopupWindowBase
    {
        public RxAlertWindow(string title, double tw, double th)
        {
            InitializeComponent();

            _title = title;
            _rw = tw;
            _rh = th;
        }
        protected string _title;
        protected double _rw;
        protected double _rh;

        public const string TypeAlert = "Alert";
        public const string TypeConfirm = "Confirm";
        protected string _type;


        protected override void pf_Loaded(object tsd, RoutedEventArgs tea)
        {
            base.pf_Loaded(tsd, tea);

            if (string.IsNullOrWhiteSpace(_title))
                _title = "Alert";
            Title = _title;

            if (!double.IsNaN(_rw))
                _pnlrt.Width = _rw;
            if (!double.IsNaN(_rh))
                _pnlrt.Height = _rh;
        }

        protected override void pf_KeyDown(object tsd, KeyEventArgs tea)
        {
            if (tea.Key == Key.Escape)
            {
                Close();
            }
        }

        private void pf_CheckLayout()
        {
            if (_type== TypeAlert)
            {
            }
            else if (_type == TypeConfirm)
            {
            }
        }



        public bool OpenDialog(Window owner, string type, string text)
        {
            if (owner == null)
                WindowStartupLocation = WindowStartupLocation.CenterScreen;
            else
                Owner = owner;
            _type = type ?? TypeAlert;
            pf_CheckLayout();
            _TxbRoot.Text = text;
            return (bool)ShowDialog();
        }



        public static bool Open(Window owner, string title, string type, string text)
        {
            RxAlertWindow taw = new RxAlertWindow(title, double.NaN, double.NaN);
            return taw.OpenDialog(owner, type, text);
        }

        public static bool Open(Window owner, string title, string type, string text, double width, double height)
        {
            RxAlertWindow taw = new RxAlertWindow(title, width, height);
            return taw.OpenDialog(owner, type, text);
        }

    }
}
