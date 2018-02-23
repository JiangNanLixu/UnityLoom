using System;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour
{
    public Image image_001;
    public bool isLoom = false;

    string _path = String.Empty;
    string _Str = "Loom继承自MonoBehaviour,在Unity流程管理中Update方法下检查需要回调";
    void Start()
    {
        _path = string.Format("{0}/{1}.txt", Application.streamingAssetsPath, "test");
        if (!File.Exists(_path))
            File.Create(_path);
        OnWriteFinish();
        
        if (isLoom)
        {
            Loom.RunAsync(NewThread);
        }
        else
        {
            MainThread();
        }
    }

    void NewThread()
    {
        try
        {
            for (int i = 0; i < 100000; i++)
            {
                File.AppendAllText(_path, _Str);
                Loom.QueueOnMainThread((p) => { image_001.fillAmount = (float) i / (float) 100000; }, null);
            }
            OnWriteFinish();
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

        OnWriteFinish();
    }

    void OnWriteFinish()
    {
        File.WriteAllText(_path, ""); 
    }
}