using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

public class DownLoadAssetBundle : MonoBehaviour {

    private string mainAssetBundleURL = @"http://www.mkcode.net/mkdemo/AssetBundles/AssetBundles";
    private string allAssetBundelURL = @"http://www.mkcode.net/mkdemo/AssetBundles/";

	void Start () {
        StartCoroutine("DownLoadMainAssetBundel");
	}


    /// <summary>
    /// 下载主[目录]AssetBundle文件.
    /// </summary>
    /// <returns></returns>
    IEnumerator DownLoadMainAssetBundel()
    {
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(mainAssetBundleURL);
        yield return request.Send();
        AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
        AssetBundleManifest manifest = ab.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] names = manifest.GetAllAssetBundles();
        for (int i = 0; i < names.Length; i++)
        {
            Debug.Log(allAssetBundelURL + names[i]);
            //StartCoroutine(DownLoadSingleAssetBundel(allAssetBundelURL + names[i]));
            StartCoroutine(DownLoadAssetBundleAndSave(allAssetBundelURL + names[i]));
        }
    }

    /// <summary>
    /// 下载单个AssetBundle文件.
    /// </summary>
    IEnumerator DownLoadSingleAssetBundel(string url)
    {
        UnityWebRequest request = UnityWebRequest.GetAssetBundle(url);
        yield return request.Send();
        AssetBundle ab = DownloadHandlerAssetBundle.GetContent(request);
        string[] names = ab.GetAllAssetNames();
        for (int i = 0; i < names.Length; i++)
        {
            string tempName = Path.GetFileNameWithoutExtension(names[i]);
            //Debug.Log(tempName);
            GameObject obj = ab.LoadAsset<GameObject>(tempName);
            GameObject.Instantiate<GameObject>(obj);
        }
    }

    /// <summary>
    /// 下载AssetBundle并且保存到本地.
    /// </summary>
    IEnumerator DownLoadAssetBundleAndSave(string url)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.isDone)
        {
            //使用IO技术将www对象存储到本地.
            SaveAssetBundle(Path.GetFileName(url), www.bytes, www.bytes.Length);
        }
    }

    /// <summary>
    /// 存储AssetBundle为本地文件.
    /// </summary>
    private void SaveAssetBundle(string fileName, byte[] bytes, int count)
    {
        FileInfo fileInfo = new FileInfo(Application.streamingAssetsPath + "//" + fileName);
        FileStream fs = fileInfo.Create();
        fs.Write(bytes, 0, count);
        fs.Flush();
        fs.Close();
        fs.Dispose();
        Debug.Log(fileName + "下载完毕~~~");
    }

}
