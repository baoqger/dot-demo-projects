using System.Drawing;
using System.Drawing.Drawing2D;
using ObjectDetection;
using Microsoft.ML;


namespace ObjectDetection
{
    internal class Program
    {
        static string assetsRelativePath = @"../../../assets";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            var assetsPath = GetAbsolutePath(assetsRelativePath);
            var modelFilePath = Path.Combine(assetsPath, "Model", "TinyYolo2_model.onnx");
            var imagesFolder = Path.Combine(assetsPath, "images");
            var outputFolder = Path.Combine(assetsPath, "images", "output");
            Console.WriteLine("Hello, World!");

            MLContext mlContext = new MLContext();
        }

        static string GetAbsolutePath(string relativePath)
        {
            FileInfo _dataRoot = new FileInfo(typeof(Program).Assembly.Location);
            string assemblyFolderPath = _dataRoot.Directory.FullName;

            string fullPath = Path.GetFullPath(Path.Combine(assemblyFolderPath, relativePath));

            return fullPath;
        }
    }
}
