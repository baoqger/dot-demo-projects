using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using System.Reflection;

namespace ROPPredictorService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredictorController : ControllerBase
    {
        private readonly ILogger<PredictorController> _logger;

        private InferenceSession _session;

        public PredictorController(ILogger<PredictorController> logger, InferenceSession session)
        {
            _logger = logger;
            _session = session;
        }

        [HttpPost]
        public async Task<float> Score([FromBody] InputParameters request)
        {
            
            Type type = typeof(InputParameters);
            PropertyInfo[] properties = type.GetProperties();
            var parameters = new List<float> { };
            foreach (PropertyInfo property in properties)
            {
                // Get property name
                // string propName = property.Name;
                // Get property value for the object 'person'
                float value = (float)property.GetValue(request);
                parameters.Add(value);
            }

            float rop;
            var inputTensor = new DenseTensor<float>(parameters.ToArray(), new int[] { 1, 6 });

            var features_input = new List<NamedOnnxValue> { NamedOnnxValue.CreateFromTensor<float>("feature_input", inputTensor) };
            using (var result = _session.Run(features_input))
            {
                rop = result.First().AsTensor<float>().First();
            }
            return rop;
        }
    }
}
