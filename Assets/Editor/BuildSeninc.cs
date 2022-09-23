using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;
using Core.App.MVC.Services;
using Core.App.Utils;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.IO;
using Core.App;

// Output the build size or a failure depending on BuildPlayer.

public class BuildSeninc : MonoBehaviour
{
    private const string MAC_PATH = "/StreamingAssets/";

    private static List<SenincVariants> _senincVariantsData;

    [MenuItem("Seninc Builds/Build All")]
    public static void BuildAll()
    {
        ReadJson("senincvariants", "builds");

        Build(_senincVariantsData[0].Entities.Bluebird.FileName.Test, _senincVariantsData[0].Entities.Bluebird.PackageName.Test);
        Build(_senincVariantsData[0].Entities.Bluebird.FileName.Prod, _senincVariantsData[0].Entities.Bluebird.PackageName.Prod);
    }
    [MenuItem("Seninc Builds/Bluebird/Test")]
    public static void BuildBlueBirdTest()
    {
        ReadJson("senincvariants", "builds");
        Build(_senincVariantsData[0].Entities.Bluebird.FileName.Test, _senincVariantsData[0].Entities.Bluebird.PackageName.Test);
    }
    [MenuItem("Seninc Builds/Bluebird/Prod")]
    public static void BuildBlueBirdProd()
    {
        ReadJson("senincvariants", "builds");
        Build(_senincVariantsData[0].Entities.Bluebird.FileName.Prod, _senincVariantsData[0].Entities.Bluebird.PackageName.Prod);
    }

    private static void Build(string filename, string packagename)
    {
        if (_senincVariantsData == null)
        {
            Debug.Log("failed to load data from senincvariants.json to senincvariantsdata instance");
        }

        PlayerSettings.Android.keystoreName = _senincVariantsData[0].KeyStore.Name; // this should be the keystore location you defined on your gradle.properties file. add it to senincvariants.json
        PlayerSettings.keystorePass = _senincVariantsData[0].KeyStore.Password;

        if ((filename.Equals(_senincVariantsData[0].Entities.Bluebird.FileName.Test) &&
            packagename.Equals(_senincVariantsData[0].Entities.Bluebird.PackageName.Test)) ||
            (filename.Equals(_senincVariantsData[0].Entities.Redpanda.FileName.Test) &&
            packagename.Equals(_senincVariantsData[0].Entities.Redpanda.PackageName.Test)))
        {
            PlayerSettings.Android.keyaliasName = _senincVariantsData[0].KeyStore.DebugKeyAlias;
            PlayerSettings.Android.keyaliasPass = _senincVariantsData[0].KeyStore.DebugKeyPassword;
        }
        else
        {
            PlayerSettings.Android.keyaliasName = _senincVariantsData[0].KeyStore.ReleaseKeyAlias;
            PlayerSettings.Android.keyaliasPass = _senincVariantsData[0].KeyStore.ReleaseKeyPassword;
        }

        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, packagename);

        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = new[] { "Assets/Scenes/Loading.unity", "Assets/Scenes/Main.unity" };
        buildPlayerOptions.locationPathName = filename;
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
            Debug.Log("Built " + filename);
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
        }
    }

    private static void ReadJson(string filename, string propertyName)
    {
        string path = StreamingAssetsPath() + filename + ".json";
        JArray jArray = null;
        if (File.Exists(path))
        {
            StreamReader reader = File.OpenText(path);
            JObject jObject = JObject.Parse(reader.ReadToEnd());
            jArray = (JArray)jObject[propertyName];
            reader.Close();

            _senincVariantsData = new List<SenincVariants>();
            _senincVariantsData.AddRange(jArray.ToObject<SenincVariants[]>());
        }
        else
        {
            Debug.Log("Error: " + filename + " file missing on your StreamingAssets folder");
        }
    }

    private static string StreamingAssetsPath()
    {
        if (Application.platform == RuntimePlatform.OSXEditor)
            return Application.dataPath + MAC_PATH;
        else
            return Application.streamingAssetsPath + "/";
    }
}