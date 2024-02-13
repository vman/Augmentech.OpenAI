using Azure.AI.OpenAI;
using Azure;
using Microsoft.AspNetCore.Mvc;

namespace Augmentech.OpenAI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DallE3Controller : ControllerBase
    {
        public IConfiguration Configuration { get; set; }

        private OpenAIClient _openAIClient { get; set; }

        public DallE3Controller(IConfiguration configuration, OpenAIClient openAIClient)
        {
            Configuration = configuration;
            _openAIClient = openAIClient;
        }

        [HttpGet]
        public async Task<IActionResult> Get(string imagePrompt)
        {
            string deploymentName = Configuration["AzureOpenAI:ImageModelName"];

            Response<ImageGenerations> imageGenerations = await _openAIClient.GetImageGenerationsAsync(
                new ImageGenerationOptions()
                {
                    DeploymentName = deploymentName,
                    Prompt = imagePrompt,
                    Size = ImageSize.Size1024x1024
                });

            Uri imageUri = imageGenerations.Value.Data[0].Url;
            string revisedPrompt = imageGenerations.Value.Data[0].RevisedPrompt;

            return Ok(new 
            {
                Url = imageUri.ToString(),
                RevisedPrompt = revisedPrompt
            });
        }
    }
}
