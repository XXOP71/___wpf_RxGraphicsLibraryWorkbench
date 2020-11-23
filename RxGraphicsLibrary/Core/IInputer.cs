namespace RxGraphicsLibrary.Core
{
    using System;
    using System.ComponentModel;


    public interface IInputer<T> : INotifyPropertyChanged
    {
        IValueAffair<T> ValueAffair { get; }

        T MinValue { get; set; }

        T MaxValue { get; set; }

        T Value { get; set; }

        string ValueStr { get; }

        T Ratio { get; set; }
    }
}
