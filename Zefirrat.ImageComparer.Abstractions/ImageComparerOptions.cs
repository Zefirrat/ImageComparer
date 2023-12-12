namespace Zefirrat.ImageComparer.Abstractions
{

    public class ImageComparerOptions
    {

        public ImageComparerOptions(byte Accuracy = AccuracyValues.Medium, uint MaxConcurrency = int.MaxValue, double CustomRatio = 0.00005)
        {
            this.Accuracy = Accuracy;
            this.MaxConcurrency = MaxConcurrency;
            this.CustomRatio = CustomRatio;
        }

        public byte   Accuracy       { get; private set; }
        public uint   MaxConcurrency { get; private set; }
        public double CustomRatio    { get; private set; }

        public void Deconstruct(out byte Accuracy, out uint MaxConcurrency, out double CustomRatio)
        {
            Accuracy = this.Accuracy;
            MaxConcurrency = this.MaxConcurrency;
            CustomRatio = this.CustomRatio;
        }

        public static class AccuracyValues
        {
            public const byte VeryHigh = 1;
            public const byte High = 20;
            public const byte Medium = 50;
            public const byte Low = 90;
            public const byte VeryLow = byte.MaxValue;
        }

    }
}