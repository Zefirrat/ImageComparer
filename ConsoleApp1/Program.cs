// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using Zefirrat.ImageComparer;
using Zefirrat.ImageComparer.Abstractions;


Console.OutputEncoding = System.Text.Encoding.UTF8;

var accuracies = new Dictionary<string, byte>()
{
    { nameof(ImageComparerOptions.AccuracyValues.VeryLow), ImageComparerOptions.AccuracyValues.VeryLow },
    { nameof(ImageComparerOptions.AccuracyValues.Low), ImageComparerOptions.AccuracyValues.Low },
    { nameof(ImageComparerOptions.AccuracyValues.Medium), ImageComparerOptions.AccuracyValues.Medium },
    { nameof(ImageComparerOptions.AccuracyValues.High), ImageComparerOptions.AccuracyValues.High },
    { nameof(ImageComparerOptions.AccuracyValues.VeryHigh), ImageComparerOptions.AccuracyValues.VeryHigh },
};
var checkMark = "\u2714";
var crossMark = "\u2716";

string GetMark(bool value) => value
    ? checkMark
    : crossMark;

foreach (var accuracy in accuracies)
{
    var imageComparer = new ImageComparer(new ImageComparerOptions(accuracy.Value));

    using var image1 = Image.Load("../../../../Test_Images/test_image_source.png");
    using var image2 = Image.Load("../../../../Test_Images/test_image_source_compressed.jpg");

    var similar = imageComparer.AreSimilar(image1, image2);
    var equal = imageComparer.AreEqual(image1, image2);

    Console.WriteLine($"Accuracy: {accuracy.Key}");
    Console.WriteLine($"    Similar: {similar} {GetMark(similar)}");
    Console.WriteLine($"    Equal: {equal} {GetMark(equal)}");
    Console.WriteLine("-------------------------");
}