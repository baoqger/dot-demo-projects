using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;

namespace ROPPredictor
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            var onnxModelPath = @"C:\develop\study\dotnet\DemoPro\ROPPredictor\Model\RFModel.onnx";

            var inputTensor = new DenseTensor<float>(new float[] { 100, 10, 20, 30, 40, 50 }, new int[] { 1, 6 });

            var features_input = new List<NamedOnnxValue> { NamedOnnxValue.CreateFromTensor<float>("feature_input", inputTensor) };

            var inferenceSession = new InferenceSession(onnxModelPath);
            using (var result = inferenceSession.Run(features_input))
            {
                var highestIndex = result.First().AsTensor<float>().First();
            }

        }
    }
}
