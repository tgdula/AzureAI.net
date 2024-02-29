using Azure.Ai.Console;
using Azure.Ai.Console.Settings;
using CommandDotNet;
using Face = Azure.Ai.Console.Utils.Face;
using Utils = Azure.Ai.Console.Utils;
public class Program
{
    private ConfigReader ConfigReader => new ();
    private Settings Settings => ConfigReader.ReadSection<Settings>("AzureAi");

    static int Main(string[] args) => new AppRunner<Program>().Run(args);
    public void Face(string imageUrl)
    {
        var faces = new Face(Settings.Face);

        var converted = Task.Run(async () => await faces.DetectFaces(imageUrl));
        foreach (var faceFeatures in converted.Result)
        {
            Console.WriteLine(faceFeatures.AsString(), Environment.NewLine);
        }
    }

    public void Vision(string imageUrl) => Console.WriteLine(imageUrl);
}