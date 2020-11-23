namespace RxGraphicsLibrary__Tester
{
    using RxGraphicsLibrary.Core;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;



    public sealed partial class CMainWindow : RxMainWindowBase
    {
        public CMainWindow()
        {
            InitializeComponent();            
        }

        protected override void pf_Loaded(object tsd, RoutedEventArgs tea)
        {
            base.pf_Loaded(tsd, tea);

            Title = GetType().Namespace;
        }

        protected override void pf_ContentRendered(object tsd, EventArgs tea)
        {
            base.pf_ContentRendered(tsd, tea);



            // {{{ ########################################
            //TesterWindow1 tttwnd1 = new TesterWindow1();
            //tttwnd1.Owner = this;
            //tttwnd1.Closed += pf_TesterWnd_Closed;
            //tttwnd1.ShowDialog();
            // }}}



            // {{{ ########################################
            //TesterWindow2 tttwnd2 = new TesterWindow2();
            //tttwnd2.Owner = this;
            //tttwnd2.Closed += pf_TesterWnd_Closed;
            //tttwnd2.ShowDialog();
            // }}}



            // {{{ ########################################
            TesterWindow3 tttwnd3 = new TesterWindow3();
            tttwnd3.Owner = this;
            tttwnd3.Closed += pf_TesterWnd_Closed;
            tttwnd3.ShowDialog();
            // }}}
        }

        private void pf_TesterWnd_Closed(object tsd, EventArgs tea)
        {
            Close();
        }
    }
}
