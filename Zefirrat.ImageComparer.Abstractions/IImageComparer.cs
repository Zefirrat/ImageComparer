using System.IO;
using SixLabors.ImageSharp;

namespace Zefirrat.ImageComparer.Abstractions
{

    public interface IImageComparer :
        IEqualityImageComparer<Stream>,
        IEqualityImageComparer<string>,
        IEqualityImageComparer<byte[]>,
        IEqualityImageComparer<Image>,
        ISimilarityImageComparer<Stream>,
        ISimilarityImageComparer<string>,
        ISimilarityImageComparer<byte[]>,
        ISimilarityImageComparer<Image>
    {
    }
}