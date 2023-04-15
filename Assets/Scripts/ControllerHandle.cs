using System.Collections;
using JMRSDK.InputModule;
using UnityEngine;

public class ControllerHandle : MonoBehaviour
{
    [Header("Dock Script")] 
    public bool isDock = true;
    private JMRSystemDockManager _jmrSystemDockManager;
    public Follower follower;
    public bool isEditor = false;
    void Start()
    {
        _jmrSystemDockManager = GetComponent<JMRSystemDockManager>();
        if(isDock)
            StartCoroutine(HandleSystemDock());
            
    }
    IEnumerator HandleSystemDock()
    {
        var t = new WaitForSeconds(3);
        
        _jmrSystemDockManager.enabled = true;
        
        for (int i = 0; i < 7; i++) yield return null;
        
        _jmrSystemDockManager.enabled = false;
        follower.gameObject.SetActive(false);
        
        yield return new WaitForSeconds(1f);
        while (true)
        {
            CheckControllerConnection();
            yield return t;
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus == false)
        {
			if(_jmrSystemDockManager == null)
			{
				_jmrSystemDockManager = GetComponent<JMRSystemDockManager>();
			}
            StopCoroutine(HandleSystemDock());
            StartCoroutine(HandleSystemDock());
        }
    }


    void CheckControllerConnection()
    {
        if (Application.isEditor && isEditor)
        {
            follower.gameObject.SetActive(true);
            follower.startFollow = true;
        }
        else if (JMRInteractionManager.Instance.isControllerConnected())
        {
            follower.gameObject.SetActive(false);
            follower.startFollow = false;
        }
        else
        {
            follower.gameObject.SetActive(true);
            follower.startFollow = true;
        }

        if (_jmrSystemDockManager.enabled)
            _jmrSystemDockManager.enabled = false;
    }
}
