using System.Collections;
using JMRSDK.InputModule;
using UnityEngine;

public class ControllerHandler : MonoBehaviour
{
    [Header("Dock Script")] 
    public bool isDock = true;
    private JMRSystemDockManager _jmrSystemDockManager;
    public Follower _follower;
    void Start()
    {
        _jmrSystemDockManager = GetComponent<JMRSystemDockManager>();
        if(isDock)
            InitializeButtonActions();
    }
    void InitializeButtonActions()
    {
        _jmrSystemDockManager.enabled = true;
        StartCoroutine(DisableScript());
    }
    IEnumerator DisableScript()
    {
        yield return new WaitForSeconds(1);
        _jmrSystemDockManager.enabled = false;
        _follower.gameObject.SetActive(true);
        _follower.startFollow = true;

        var t = new WaitForSeconds(3);
        var deviceType = JMRInteractionManager.Instance.GetSupportedInteractionDeviceType();
        while (true)
        {
            yield return t; 
            deviceType = JMRInteractionManager.Instance.GetSupportedInteractionDeviceType();
            if (deviceType == JMRInteractionManager.InteractionDeviceType.GAZE_AND_CLICK ||
                deviceType == JMRInteractionManager.InteractionDeviceType.GAZE_AND_DWELL)
            {
                _follower.gameObject.SetActive(true);
            }
            else
            {
                _follower.gameObject.SetActive(false);
            }
        }
    }
}
