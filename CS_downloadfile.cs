using System.Collections;
using UnityEngine;
using System.IO;

/// <summary>
/// 从FTP服务器下载ab文件的简单模板
/// </summary>
public class CS_downloadfile : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(FN_loadassetBundle());
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FN_ReadFileInfo();
        }
    }

    /// <summary>
    /// 下载的函数
    /// </summary>
    /// <returns></returns>
    private IEnumerator FN_loadassetBundle()
    {
        string url = "http://gewuji0127.gz01.bdysite.com/Administrator/HomeFile/modle.unity3d";

        WWW wWW = new WWW(url);

        yield return wWW;

        if (wWW.isDone == false)
        {
            MonoBehaviour.print(wWW.error);
            yield break;
        }

        byte[] vs = new byte[1024];

        //Application.persistentDataPath一直指向以这个项目名命名的文件夹，在这个文件夹下的的子文件夹里的文件
        FileInfo fileInfo = new FileInfo(Application.persistentDataPath + "/programab_asset/newModle.unity3d");

        FileStream fileStream = fileInfo.Create();

        fileStream.Write(vs, 0, 1024);

        fileStream.Flush();

        fileStream.Close();

        fileStream.Dispose();

        MonoBehaviour.print("文件下载完毕!");
    }

    /// <summary>
    /// 读取txt文件的简单模板
    /// </summary>
    private void FN_ReadFileInfo()
    {
        string path = Application.persistentDataPath + "/programab_asset/update.txt";

        StreamReader sr = new StreamReader(path);

        string txtinfo = sr.ReadToEnd();

        print(txtinfo);
    }
}
