using System.Collections;
using System.Collections.Generic;
using JMRSDK.InputModule;
using UnityEngine;
using UnityEngine.UI;
public class SwapTutorialImage : MonoBehaviour
{

    public Texture2D JioGlassLiteTutImg;
    public Texture2D JioGlassProTutImg;
    // Start is called before the first frame update


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnEnable()
    {
       

        JMRInteractionManager.InteractionDeviceType deviceType;
        deviceType = JMRInteractionManager.Instance.GetSupportedInteractionDeviceType();

        Debug.Log("swaptut enable device type:" + deviceType.ToString());

        if (deviceType == JMRInteractionManager.InteractionDeviceType.JIOGLASS_CONTROLLER)
        {
            Debug.Log("swaptut jiopro");
            gameObject.GetComponent<RawImage>().texture = JioGlassProTutImg;
        }
        else if (deviceType == JMRInteractionManager.InteractionDeviceType.VIRTUAL_CONTROLLER)
        {
            Debug.Log("swaptut jiolite");
            gameObject.GetComponent<RawImage>().texture = JioGlassLiteTutImg;
        }
        else
        {
            Debug.Log("swaptut noConfig");
        }
    }
}
