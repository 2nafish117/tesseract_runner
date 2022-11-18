using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.InputModule;
using UnityEngine.Video;

public class testBackAction : MonoBehaviour, IBackHandler, ISelectClickHandler
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.LogWarning("sidtest onStart");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnEnable()
    {
        Debug.LogWarning("sidtest adding listener uimgr");
        JMRInputManager.Instance.AddGlobalListener(gameObject);
    }

    public void OnDisable()
    {
        Debug.LogWarning("sidtest removing listener uimgr");
        JMRInputManager.Instance.RemoveGlobalListener(gameObject);
    }


    public void OnBackAction()
	{
		Debug.LogWarning("sidtest OnBackAction");
	
	}

    public void OnSelectClicked(SelectClickEventData eventData)
    {
        Debug.Log("sidtest OnSelectClicked");
    }
}
