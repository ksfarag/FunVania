using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenePresist : MonoBehaviour
{
    void Awake()
    {
        int ScenePresists = FindObjectsOfType<ScenePresist>().Length;
        if (ScenePresists > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public void Reset()
    {
        Destroy(gameObject);
    }
}
