namespace Zefirrat.ImageComparer.Abstrtactions
{

    public interface ISimilarityImageComparer<T>
    {
        bool AreSimilar(T image1, T image2);
    }
}