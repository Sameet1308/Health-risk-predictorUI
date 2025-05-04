using Microsoft.AspNetCore.Mvc;

namespace HealthRiskApi.Controllers
{
    [ApiController]
    [Route("predict")]
    public class PredictController : ControllerBase
    {
        public class HealthInput
        {
            public int Age { get; set; }
            public string Gender { get; set; }
            public double Cholesterol { get; set; }
            public double Glucose { get; set; }
            public double BMI { get; set; }
            public double Systolic { get; set; }
            public double Diastolic { get; set; }
            public double SpO2 { get; set; }
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
            switch (input.Condition.ToLower())
            {
                case "heart":
                    risk = input.Age * 0.25 + input.Cholesterol * 0.2 + input.BMI * 0.4;
                    break;
                case "diabetes":
                    risk = input.Glucose * 0.35 + input.BMI * 0.35 + input.Age * 0.2;
                    break;
                case "apnea":
                    risk = input.BMI * 0.6 + (100 - input.SpO2) * 1.2;
                    break;
                case "hypertension":
                    risk = input.Systolic * 0.25 + input.Diastolic * 0.25 + input.BMI * 0.25;
                    break;
                default:
                    return BadRequest("Unknown condition");
            }

            risk = Math.Min(100, Math.Round(risk, 2));
            return Ok(new PredictionResult { RiskPercentage = risk });
        }
    }
}
