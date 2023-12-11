using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Zefirrat.ImageComparer.Abstractions;

namespace Zefirrat.ImageComparer.Hash
{
    public class Hasher: IImageHasher
    {
        private readonly ImageComparerOptions _options;
        public Hasher() : this(new ImageComparerOptions()){}

        private Hasher(ImageComparerOptions options)
        {
            _options = options;
        }

        public bool AreEqual(double[] image1, double[] image2)
        {
            for (var i = 0; i < image1.Length; i++)
            {
                if (Math.Abs(image1[i] - image2[i]) > _options.CustomRatio * _options.Accuracy)
                {
                    return false;
                }
            }

            return true;
        }

        public bool AreSimilar(double[] image1, double[] image2)
        {
            var standardDeviation1 =
                CalculateStandardDeviation(image1, image1.Sum() / image1.Length, image1.Length);
            var standardDeviation2 =
                CalculateStandardDeviation(image2, image2.Sum() / image2.Length, image2.Length);
            var standardDeviation3 = CalculateStandardDeviation(image1.Concat(image2)
                    .ToArray(),
                (image1.Sum() + image2.Sum()) / (image1.Length + image2.Length),
                image1.Length + image2.Length);

            var accuracy = _options.CustomRatio * _options.Accuracy;

            return Math.Abs(standardDeviation3 - standardDeviation1) < accuracy &&
                Math.Abs(standardDeviation3 - standardDeviation2) < accuracy;
        }

        public double[] ToVector(Image image)
        {
            image.Mutate(i => i.Grayscale());
            image.Mutate(i => i.Resize(new Size(16, 16)));
            using var cloned = image.CloneAs<RgbaVector>();

            var result = new List<double>();

            for (var i = 0;
                 i <
                 cloned.Size()
                     .Height;
                 i++)
            {
                for (var j = 0;
                     j <
                     cloned.Size()
                         .Width;
                     j++)
                {
                    result.Add(cloned[i, j]
                        .ToVector4()
                        .Length());
                }
            }

            return result.ToArray();
        }

        public double[] ToVector(Stream image)
        {
            return ToVector(Image.Load(image));
        }

        public double[] ToVector(byte[] image)
        {
            return ToVector(Image.Load(image));
        }

        public double[] ToVector(string imagePath)
        {
            return ToVector(Image.Load(imagePath));
        }
        
        private static double CalculateStandardDeviation(IEnumerable<double> converted2, double s, int n)
        {
            var tmp = 0.0;
            foreach (var x in converted2)
            {
                tmp += Math.Pow(x - s, 2);
            }

            var sko = Math.Sqrt(tmp / n);

            return sko;
        }
    }
}