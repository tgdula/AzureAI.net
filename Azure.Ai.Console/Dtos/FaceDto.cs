using System.Text;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;

namespace Azure.Ai.Console.Dtos;
public class FaceDto
{
    private readonly DetectedFace face;

    public FaceDto(DetectedFace face)
    {
        this.face = face;
    }
    public static FaceDto From(DetectedFace face) => new(face);
    public string AsString()
    {
        var converted = new StringBuilder();
        converted.Append($"Id:{face.FaceId}");
        converted.Append($"{Environment.NewLine}Age:{face.FaceAttributes.Age}");
        if (face.FaceAttributes.Accessories.Any())
        {
            converted.Append($"{Environment.NewLine}Face Accessories:");
            face.FaceAttributes.Accessories.ToList().ForEach(acc => converted.Append($"{acc.Type}, confidence: {acc.Confidence}; "));
        }

        converted.Append($"{Environment.NewLine}Face Emotion:");
        converted.Append($"{Environment.NewLine} Anger: {face.FaceAttributes.Emotion.Anger}");
        converted.Append($"{Environment.NewLine} Happiness: {face.FaceAttributes.Emotion.Happiness}");
        converted.Append($"{Environment.NewLine} Contempt: {face.FaceAttributes.Emotion.Contempt}");
        converted.Append($"{Environment.NewLine} Sadness: {face.FaceAttributes.Emotion.Sadness}");
        converted.Append($"{Environment.NewLine} Surprise: {face.FaceAttributes.Emotion.Surprise}");

        // head pose: https://go.digitalsmiledesign.com/hubfs/DSD%20Articles/Articles%20by%20others/Pich,roll%20and%20yaw.pdf
        converted.Append($"{Environment.NewLine}Face Head Pose:");
        converted.Append($"{Environment.NewLine} Pitch: {face.FaceAttributes.HeadPose.Pitch}");
        converted.Append($"{Environment.NewLine} Roll: {face.FaceAttributes.HeadPose.Roll}");
        converted.Append($"{Environment.NewLine} Yaw: {face.FaceAttributes.HeadPose.Yaw}");

        return converted.ToString();
    }
}