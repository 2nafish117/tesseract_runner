using System;
using JMRSDK.InputModule;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour, IBackHandler
{
    public bool ResetInStart = false;

    public Vector3 newPosition;
    
    private void Start()
    {
        if (ResetInStart)
        {
            ResetPlayerPosition(newPosition);
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ResetPlayerPosition()
    {
        GameObject Player=GameObject.FindWithTag("JmrRig");
        Player.transform.position=Vector3.zero;
    }
    
    public void ResetPlayerPosition(Vector3 newPosition)
    {
        GameObject Player=GameObject.FindWithTag("JmrRig");
        Player.transform.position=newPosition;
    }

    public void OnBackAction()
    {
        ChangeScene("Main");
    }
}
