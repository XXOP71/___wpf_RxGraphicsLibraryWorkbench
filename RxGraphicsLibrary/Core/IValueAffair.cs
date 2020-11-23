namespace RxGraphicsLibrary.Core
{
    public interface IValueAffair<T>
    {
        void ValueUpDown(char tt = 'u', uint ti = 0);

        T MinValue { get; set; }

        T MaxValue { get; set; }

        T Value { get; set; }

        string ValueFixed { get; }

        T Ratio { get; set; }
    }
}
