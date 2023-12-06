namespace Zefirrat.ImageComparer.Abstractions
{
    public interface IEqualityImageComparer<T>
    {
        bool AreEqual(T image1, T image2);
    }
}