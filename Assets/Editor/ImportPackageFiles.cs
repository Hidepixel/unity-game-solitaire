using System.IO;
using UnityEditor;
using UnityEngine;

public class ImportPackageFiles : MonoBehaviour
{
    private const string PACKAGE_CACHE_FOLDER = "Library/PackageCache/";

    private const string ANDROID_FOLDER = "Assets/Plugins/Android";
    private const string EDITOR_FOLDER = "Assets/Editor";
    private const string WEBGL_FOLDER = "Assets/WebGLTemplates";
    private const string PACKAGE_ANDROID_FOLDER = "/Runtime/Plugins/Android~";
    private const string PACKAGE_EDITOR_FOLDER = "/Runtime/Editor~";
    private const string PACKAGE_WEBGL_FOLDER = "/Runtime/WebGLTemplates~";

    [MenuItem("Core Package/Import files")]
    static void ImportFiles()
    {
        ReplaceDirectory(PACKAGE_ANDROID_FOLDER, ANDROID_FOLDER);
        ReplaceDirectory(PACKAGE_EDITOR_FOLDER, EDITOR_FOLDER);
        ReplaceDirectory(PACKAGE_WEBGL_FOLDER, WEBGL_FOLDER);
    }

    static private string GetTempPackageCoreFolder()
    {
        DirectoryInfo directory = new DirectoryInfo("Library/PackageCache/");
        foreach (var subdirectory in directory.GetDirectories())
        {
            if (subdirectory.Name.Contains("com.sioslife.unity.core"))
            {
                return subdirectory.Name;
            }
        }

        return "";
    }

    static private void ReplaceDirectory(string packagePath, string destinationPath)
    {
        FileUtil.DeleteFileOrDirectory(destinationPath);
        FileUtil.CopyFileOrDirectory(PACKAGE_CACHE_FOLDER + GetTempPackageCoreFolder() + packagePath, destinationPath);

        DirectoryInfo directory = new DirectoryInfo(destinationPath);

        foreach (var file in directory.GetFiles())
        {
            if (!file.Name.Contains(".meta"))
            {
                AssetDatabase.ImportAsset(destinationPath + "/" + file.Name);
            }
        }
    }
}