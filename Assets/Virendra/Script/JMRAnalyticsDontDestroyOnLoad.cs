using JMRSDK;
using JMRSDK.InputModule;
using UnityEngine;

[DefaultExecutionOrder(-50)]
public class JMRAnalyticsDontDestroyOnLoad : JMRAnalyticsManager
{
    void Start()
    {
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
    }
}