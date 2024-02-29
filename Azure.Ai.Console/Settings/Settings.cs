namespace Azure.Ai.Console.Settings;
public class Settings
{
    public Vision Vision { get; set; }
    public Face Face { get; set; }
}

public class Face
{
    public string Endpoint { get; set; }
    public string ApiKey { get; set; }
    public string DetectionModel { get; set; }
    public string RecognitionModel { get; set; }
}

public class Vision
{
    public string Endpoint { get; set; }
    public string ApiKey { get; set; }
}