namespace Zefirrat.ImageComparer.Abstrtactions
{

    public class ImageComparerOptions
    {

        public ImageComparerOptions(byte Accuracy = AccuracyValues.Medium, uint MaxConcurrency = int.MaxValue)
        {
            this.Accuracy = Accuracy;
            this.MaxConcurrency = MaxConcurrency;
        }

        public byte Accuracy       { get; private set; }
        public uint MaxConcurrency { get; private set; }

        public void Deconstruct(out byte Accuracy, out uint MaxConcurrency)
        {
            Accuracy = this.Accuracy;
            MaxConcurrency = this.MaxConcurrency;
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