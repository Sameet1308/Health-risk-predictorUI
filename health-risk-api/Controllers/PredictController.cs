using Microsoft.AspNetCore.Mvc;

namespace HealthRiskApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PredictController : ControllerBase
    {
        public class HealthInput
        {
            public int Age { get; set; }
            public string Gender { get; set; }
            public double Cholesterol { get; set; }
            public double Glucose { get; set; }
            public double BMI { get; set; }
            public string Condition { get; set; }
        }

        public class PredictionResult
        {
            public double RiskPercentage { get; set; }
        }

        [HttpPost]
        public ActionResult<PredictionResult> Post([FromBody] HealthInput input)
        {
            double risk = 0;
            if (input.Condition.ToLower() == "heart")
            {
                risk = (input.Age * 0.3 + input.Cholesterol * 0.2 + input.BMI * 0.5);
            }
            else
            {
                risk = (input.Age * 0.25 + input.Glucose * 0.4 + input.BMI * 0.4);
            }

            risk = Math.Min(100, Math.Round(risk, 2));
            return Ok(new PredictionResult { RiskPercentage = risk });
        }
    }
}