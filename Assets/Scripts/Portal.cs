using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(WaitForASec());
    }
    IEnumerator WaitForASec()
    {
        yield return new WaitForSecondsRealtime(1f);
        var currentScene = SceneManager.GetActiveScene().buildIndex;
        FindObjectOfType<ScenePresist>().Reset();
        if (currentScene + 1 == SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        SceneManager.LoadScene(currentScene + 1);
    }
}
