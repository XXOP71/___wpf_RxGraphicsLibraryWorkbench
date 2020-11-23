namespace RxGraphicsLibrary.Controls
{
    using RxGraphicsLibrary.Core;
    using RxGraphicsLibrary.Tools;
    using System;
    using System.ComponentModel;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Data;
    using System.Windows.Input;




    public class RxDoubleInputer : TextBlock, IInputer<double>
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void pf_PropertyChanged(string tpnm)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(tpnm));
        }



        public RxDoubleInputer()
        {
            SnapsToDevicePixels = true;
            ClipToBounds = true;
            Cursor = Cursors.Hand;
            TextAlignment = TextAlignment.Center;

            ValueAffair = new RxDoubleAffair();

            Loaded += pf_Loaded;
            Unloaded += pf_Unloaded;

        }
        private void pf_Loaded(object tsd, RoutedEventArgs tea)
        {
            Loaded -= pf_Loaded;

            Binding tdb = new Binding()
            {
                Source = this,
                Path = new PropertyPath(_PnmValueStr),
                Mode = BindingMode.OneWay
            };
            BindingOperations.SetBinding(this, TextProperty, tdb);


            PreviewMouseWheel += pf_PreviewMouseWheel;
            PreviewMouseLeftButtonDown += pf_PreviewMouseLeftButtonDown;

            _wnd = Window.GetWindow(this);
            if (_wnd != null)
                _wnd.PreviewKeyDown += pf_PreviewKeyDownEvent;            
        }
        protected Window _wnd;

        public IValueAffair<double> ValueAffair { get; private set; }        
        public Action Callback;


        private void pf_Unloaded(object tsd, RoutedEventArgs tea)
        {
            Unloaded -= pf_Unloaded;

            if (_wnd == null) return;
            
            ValueAffair = null;
            Callback = null;

            BindingOperations.ClearBinding(this, TextProperty);

            PreviewMouseWheel -= pf_PreviewMouseWheel;
            PreviewMouseLeftButtonDown -= pf_PreviewMouseLeftButtonDown;

            _wnd.PreviewKeyDown -= pf_PreviewKeyDownEvent;
            _wnd = null;
        }



        private void pf_ValueUpAndDown(char tt)
        {
            uint ti = 0;
            if (Keyboard.Modifiers == (ModifierKeys.Control | ModifierKeys.Shift))
                ti = 1;
            else if (Keyboard.Modifiers == ModifierKeys.Control)
                ti = 2;
            else if (Keyboard.Modifiers == ModifierKeys.Shift)
                ti = 3;

            ValueAffair.ValueUpDown(tt, ti);

            if (Callback != null)
                Callback();
        }

        private void pf_PreviewKeyDownEvent(object tsd, KeyEventArgs tea)
        {
            if (IsMouseOver)
            {
                if (tea.Key == Key.Up)
                {
                    pf_ValueUpAndDown('u');

                    pf_PropertyChanged(_PnmValue);
                    pf_PropertyChanged(_PnmValueStr);
                }
                else if (tea.Key == Key.Down)
                {
                    pf_ValueUpAndDown('d');

                    pf_PropertyChanged(_PnmValue);
                    pf_PropertyChanged(_PnmValueStr);
                }
            }
        }

        private void pf_PreviewMouseWheel(object tsd, MouseWheelEventArgs tea)
        {
            if (tea.Delta > 0)
            {
                pf_ValueUpAndDown('u');

                pf_PropertyChanged(_PnmValue);
                pf_PropertyChanged(_PnmValueStr);
            }
            else if (tea.Delta < 0)
            {
                pf_ValueUpAndDown('d');

                pf_PropertyChanged(_PnmValue);
                pf_PropertyChanged(_PnmValueStr);
            }
        }

        private void pf_PreviewMouseLeftButtonDown(object tsd, MouseButtonEventArgs tea)
        {
            Point tpt = Mouse.GetPosition(this);
            double th = RenderSize.Width / 2;
            if (tpt.X > th)
            {
                pf_ValueUpAndDown('u');

                pf_PropertyChanged(_PnmValue);
                pf_PropertyChanged(_PnmValueStr);
            }
            else if (tpt.X < th)
            {
                pf_ValueUpAndDown('d');

                pf_PropertyChanged(_PnmValue);
                pf_PropertyChanged(_PnmValueStr);
            }
        }
        





        private const string _PnmValue = "Value";
        private const string _PnmValueStr = "ValueStr";

        public double MinValue
        {
            get { return ValueAffair.MinValue; }
            set
            {
                if (value == ValueAffair.MinValue) return;
                ValueAffair.MinValue = value;

                pf_PropertyChanged(_PnmValue);
                pf_PropertyChanged(_PnmValueStr);
            }
        }

        public double MaxValue
        {
            get { return ValueAffair.MaxValue; }
            set
            {
                if (value == ValueAffair.MaxValue) return;
                ValueAffair.MaxValue = value;

                pf_PropertyChanged(_PnmValue);
                pf_PropertyChanged(_PnmValueStr);
            }
        }

        public double Value
        {
            get { return ValueAffair.Value; }
            set
            {
                if (value == ValueAffair.Value) return;
                ValueAffair.Value = value;

                pf_PropertyChanged(_PnmValue);
                pf_PropertyChanged(_PnmValueStr);
            }
        }
        
        public string ValueStr
        {
            get
            {
                string tstr = ValueAffair.ValueFixed;
                tstr = string.Format("< {0} >", tstr);
                return tstr;
            }
        }

        public double Ratio
        {
            get { return ValueAffair.Ratio; }
            set
            {
                if (value == ValueAffair.Ratio) return;
                ValueAffair.Ratio = value;

                pf_PropertyChanged(_PnmValue);
                pf_PropertyChanged(_PnmValueStr);
            }
        }

    }
}
