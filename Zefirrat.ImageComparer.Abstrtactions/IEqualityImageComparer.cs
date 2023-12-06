namespace Zefirrat.ImageComparer.Abstrtactions
{
    public interface IEqualityImageComparer<T>
    {
        bool AreEqual(T image1, T image2);
    }
}