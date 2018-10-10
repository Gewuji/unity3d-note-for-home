using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;


/// <summary>
/// 在编辑器环境下打包和加载assetbundle
/// </summary>
public class CS_BuildAssetBundleTool
{
    /// <summary>
    /// 公开一个全局变量，方便读取
    /// </summary>
    private static string allPath;

    /// <summary>
    /// 打包路径在Application.persistentDataPath文件夹下
    /// </summary>
    [MenuItem("AssetBundle/打包assetbundle到固定路径文件夹")]
    static void FN_buildandroidassetbundle()
    {
        string path = Application.persistentDataPath + "/programab_asset";

        if (Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);

        MonoBehaviour.print("Build android assetbundle for:" + path);

        allPath = path;
    }

    /// <summary>
    /// 加载assetbundle资源（从本地加载）
    /// </summary>
    [MenuItem("AssetBundle/从本地读取assetbundle资源")]
    static void FN_readassetbundle()
    {
        //modle.unity3d是ab包的名称
        var ab = AssetBundle.LoadFromFile(allPath + "/modle.unity3d");

        var xx = ab.LoadAllAssets<GameObject>();

        foreach (var item in xx)
        {
            GameObject.Instantiate(item);
        }

        MonoBehaviour.print("读取模型资源成功!");
    }

    /// <summary>
    /// 从服务器下载ab资源---在编辑器模式下无法运行协程程序
    /// </summary>
    //[MenuItem("AssetBundle/从FTP服务器下载assetbundle文件")]
    //static IEnumerator FN_loadassetBundle()
    //{
    //    string url = "http://gewuji0127.gz01.bdysite.com/Administrator/HomeFile/modle.unity3d";

    //    WWW wWW = new WWW(url);

    //    yield return wWW;

    //    if (wWW.isDone == false)
    //    {
    //        MonoBehaviour.print(wWW.error);
    //        yield break;
    //    }

    //    byte[] vs = new byte[1024];

    //    FileInfo fileInfo = new FileInfo(Application.persistentDataPath + "/programab_asset/newModle.unity3d");

    //    FileStream fileStream = fileInfo.Create();

    //    fileStream.Write(vs, 0, 1024);

    //    fileStream.Flush();

    //    fileStream.Close();

    //    fileStream.Dispose();

    //    MonoBehaviour.print("文件下载完毕!");
    //}
}
