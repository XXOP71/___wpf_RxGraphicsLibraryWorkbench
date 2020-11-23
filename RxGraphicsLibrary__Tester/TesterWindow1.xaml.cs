namespace RxGraphicsLibrary__Tester
{
    using RxGraphicsLibrary.Core;
    using System;
    using System.Windows;



    public sealed partial class TesterWindow1 : RxPopupWindowBase
    {
        public TesterWindow1()
        {
            InitializeComponent();

            Title = GetType().Namespace;
        }
    }
}
