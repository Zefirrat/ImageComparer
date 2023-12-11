using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Zefirrat.ImageComparer.Abstractions;
using Zefirrat.ImageComparer.Hash;

namespace Zefirrat.ImageComparer
{

    public class ImageComparer : IImageComparer
    {
        private readonly ImageComparerOptions _options;
        private readonly SemaphoreSlim _semaphoreSlim;
        private readonly IImageHasher _hasher;
        
        public ImageComparer() : this(new ImageComparerOptions())
        {
        }

        public ImageComparer(ImageComparerOptions options)
        {
            Debug.Assert(options != null, nameof(options) + " != null");
            _options = options;
            _semaphoreSlim = new SemaphoreSlim((int)_options.MaxConcurrency);
            _hasher = new Hasher(_options);
        }

        private bool CompareEquality(Image image1, Image image2)
        {
            var vectors1 = ImageToVectors(image1);
            var vectors2 = ImageToVectors(image2);

            for (var i = 0; i < vectors1.Length; i++)
            {
                if (Math.Abs(vectors1[i] - vectors2[i]) > _options.CustomRatio * _options.Accuracy)
                {
                    return false;
                }
            }

            return true;
        }

        private bool CompareSimilarity(Image image1, Image image2)
        {
            var vectors1 = _hasher;
            var vectors2 = ImageToVectors(image2);

            var standardDeviation1 =
                CalculateStandardDeviation(vectors1, vectors1.Sum() / vectors1.Length, vectors1.Length);
            var standardDeviation2 =
                CalculateStandardDeviation(vectors2, vectors2.Sum() / vectors2.Length, vectors2.Length);
            var standardDeviation3 = CalculateStandardDeviation(vectors1.Concat(vectors2)
                    .ToArray(),
                (vectors1.Sum() + vectors2.Sum()) / (vectors1.Length + vectors2.Length),
                vectors1.Length + vectors2.Length);

            var accuracy = _options.CustomRatio * _options.Accuracy;

            return Math.Abs(standardDeviation3 - standardDeviation1) < accuracy &&
                Math.Abs(standardDeviation3 - standardDeviation2) < accuracy;
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

        private void WaitForSemaphore()
        {
            _semaphoreSlim.Wait();
        }

        public bool AreEqual(Stream image1, Stream image2)
        {
            WaitForSemaphore();
            using var imageSharp1 = Image.Load(image1);
            using var imageSharp2 = Image.Load(image2);
            return CompareEquality(imageSharp1, imageSharp2);
        }

        public bool AreEqual(string image1, string image2)
        {
            WaitForSemaphore();
            using var imageSharp1 = Image.Load(image1);
            using var imageSharp2 = Image.Load(image2);
            return CompareEquality(imageSharp1, imageSharp2);
        }

        public bool AreEqual(byte[] image1, byte[] image2)
        {
            WaitForSemaphore();
            using var imageSharp1 = Image.Load(image1);
            using var imageSharp2 = Image.Load(image2);
            return CompareEquality(imageSharp1, imageSharp2);
        }

        public bool AreSimilar(Stream image1, Stream image2)
        {
            WaitForSemaphore();
            using var imageSharp1 = Image.Load(image1);
            using var imageSharp2 = Image.Load(image2);
            return CompareSimilarity(imageSharp1, imageSharp2);
        }

        public bool AreSimilar(string image1, string image2)
        {
            WaitForSemaphore();
            using var imageSharp1 = Image.Load(image1);
            using var imageSharp2 = Image.Load(image2);
            return CompareSimilarity(imageSharp1, imageSharp2);
        }

        public bool AreSimilar(byte[] image1, byte[] image2)
        {
            WaitForSemaphore();
            using var imageSharp1 = Image.Load(image1);
            using var imageSharp2 = Image.Load(image2);
            return CompareSimilarity(imageSharp1, imageSharp2);
        }

        public bool AreEqual(Image image1, Image image2)
        {
            WaitForSemaphore();
            return CompareEquality(image1, image2);
        }

        public bool AreSimilar(Image image1, Image image2)
        {
            WaitForSemaphore();
            return CompareSimilarity(image1, image2);
        }
    }
}