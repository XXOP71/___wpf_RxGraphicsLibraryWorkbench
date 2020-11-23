namespace RxGraphicsLibrary.Tools
{
    using RxGraphicsLibrary.Core;
    using System;



    public class RxDoubleAffair : IValueAffair<double>
    {
        public RxDoubleAffair(
                    double minval = 0.0, double maxval = 1.0, double val = 0.0,
                    double[] tvga = null, string tfd = "N2")
        {
            if (minval >= maxval)
                throw new Exception("max must be greater than min.");

            if (tvga == null)
                tvga = new double[] { 0.1, 10.0, 0.01, 1.0 };


            _minval = minval;
            _maxval = maxval;
            _val = val;
            if (_val < _minval)
                _val = _minval;
            else if (_val > _maxval)
                _val = _maxval;

            _vga = tvga;
            _fd = tfd;
        }
        private double _minval;
        private double _maxval;
        private double _val;

        private double[] _vga;
        private string _fd;


        public void ValueUpDown(char tt = 'u', uint ti = 0)
        {
            if ((tt == 'u') || (tt == 'd'))
            {
                if ((_vga == null) || (_vga.Length == 0)) return;
                if ((ti >= 0) && (ti < _vga.Length))
                {
                    double ta = _vga[ti];
                    double tv = _val;

                    if (tt == 'u')
                        tv = _val + ta;
                    else if (tt == 'd')
                        tv = _val - ta;

                    if (tv < _minval)
                        tv = _minval;
                    else if (tv > _maxval)
                        tv = _maxval;

                    _val = tv;
                }
            }
        }


        public double MinValue
        {
            get { return _minval; }
            set
            {
                if (value == _minval) return;

                _minval = value;
                if (_maxval < _minval)
                    _maxval = _minval;

                if (_val < _minval)
                    _val = _minval;
            }
        }

        public double MaxValue
        {
            get { return _maxval; }
            set
            {
                if (value == _maxval) return;

                _maxval = value;
                if (_minval > _maxval)
                    _minval = _maxval;

                if (_val > _maxval)
                    _val = _maxval;
            }
        }

        public double Value
        {
            get { return _val; }
            set
            {
                if (value == _val) return;

                if (value < _minval)
                    value = _minval;
                else if (value > _maxval)
                    value = _maxval;

                _val = value;
            }
        }

        public string ValueFixed
        {
            get { return _val.ToString(_fd); }
        }

        public double Ratio
        {
            get
            {
                double tsv = _maxval - _minval;
                if (tsv <= 0) return 0;

                double tr = (_val - _minval) / tsv;
                return tr;
            }
            set
            {
                if (value < 0)
                    value = 0;
                else if (value > 1)
                    value = 1;

                double tsv = _maxval - _minval;
                if (tsv < 0)
                    tsv = 0;
                _val = _minval + (tsv * value);
            }
        }

    }
}
