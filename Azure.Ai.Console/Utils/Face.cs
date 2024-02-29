using Azure.Ai.Console.Dtos;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace Azure.Ai.Console.Utils;
public class Face
{
    // see https://learn.microsoft.com/en-us/azure/ai-services/computer-vision/quickstarts-sdk/identity-client-library?tabs=windows%2Cvisual-studio&pivots=programming-language-csharp
    // see: https://learn.microsoft.com/en-us/azure/ai-services/computer-vision/how-to/identity-detect-faces
    // sample: https://gwinnettmagazine.com/wp-content/uploads/2018/07/HERO-770x415-SmilingFaces-696x375.jpg
    private readonly string endpoint;
    private readonly string apiKey;
    private readonly string detectionModel;
    private readonly string recognitionModel;
    
    public string DefaultRecognitionModel => RecognitionModel.Recognition04;
    public string DefaultDetectionModel => DetectionModel.Detection01;

    private FaceAttributeType[] DetectFaceAttributes => new [] {
        FaceAttributeType.Accessories,
        FaceAttributeType.Age,
        FaceAttributeType.HeadPose,
        FaceAttributeType.Emotion,
        FaceAttributeType.QualityForRecognition
    };

    public Face(Azure.Ai.Console.Settings.Face settings)
    {
        this.endpoint = settings.Endpoint;
        this.apiKey = settings.ApiKey;
        this.detectionModel = string.IsNullOrEmpty(settings.DetectionModel)? DefaultDetectionModel: settings.DetectionModel;
        this.recognitionModel = string.IsNullOrEmpty(settings.RecognitionModel)? DefaultRecognitionModel : settings.RecognitionModel;
    }

    public async Task<List<FaceDto>> DetectFaces(string imageUrl)
    {
        var client = Authenticate();
        var faces = await client.Face.DetectWithUrlAsync(
            url: imageUrl, 
            returnFaceId: false, 
            returnFaceAttributes: new[]
            {
                FaceAttributeType.Age, 
                FaceAttributeType.Gender,
                FaceAttributeType.Emotion,
                FaceAttributeType.HeadPose,
            }, 
            detectionModel: detectionModel,     //e.g. RecognitionModel.Recognition04
            recognitionModel: recognitionModel);//e.g. DetectionModel.Detection01

        var found = faces != null && faces.Any();
        return found ? faces.Select(FaceDto.From).ToList() : new List<FaceDto>();
    }

    private IFaceClient Authenticate() => new FaceClient(new ApiKeyServiceClientCredentials(apiKey)) { Endpoint = endpoint };
}