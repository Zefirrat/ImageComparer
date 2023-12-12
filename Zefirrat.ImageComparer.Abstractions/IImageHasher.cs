using System.IO;
using SixLabors.ImageSharp;

namespace Zefirrat.ImageComparer.Abstractions
{
    public interface IImageHasher: IEqualityImageComparer<double[]>, ISimilarityImageComparer<double[]>
    {
        double[] ToVector(Image image);
        double[] ToVector(Stream image);
        double[] ToVector(byte[] image);
        double[] ToVector(string imagePath);
    }
}