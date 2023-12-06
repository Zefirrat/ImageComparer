namespace Zefirrat.ImageComparer.Abstractions
{

    public interface ISimilarityImageComparer<T>
    {
        bool AreSimilar(T image1, T image2);
    }
}