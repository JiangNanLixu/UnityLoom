using System;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    public Image image_001;

    string _path = String.Empty;
    string _Str = "Loom继承自MonoBehaviour,在Unity流程管理中Update方法下检查需要回调";

    void Start()
    {
        _path = string.Format("{0}/{1}.txt", Application.streamingAssetsPath, "test");
        if (!File.Exists(_path))
            File.Create(_path);
        
//        MainThread();
        
        Loom.RunAsync(() =>
        {
            Thread thread = new Thread(NewThread);
            thread.Start();
        });
    }

    void NewThread()
    {
        if (!File.Exists(_path))
        {
            File.Create(_path);
        }

        try
        {
            for (int i = 0; i < 100000; i++)
            {
                File.AppendAllText(_path, _Str);
                Loom.QueueOnMainThread((p) => { image_001.fillAmount = (float) i / (float) 100000; }, null);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e);
            throw;
        }
    }

    void MainThread()
    {
        for (int i = 0; i < 100000; i++)
        {
            File.AppendAllText(_path, _Str);
            image_001.fillAmount = (float) i / (float) 100000;
        }
    }
}