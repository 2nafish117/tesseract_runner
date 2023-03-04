using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JMRSDK.InputModule;


public class JMRDiveBackButtonHandler : MonoBehaviour
{

    [SerializeField]
    private Texture backButtonTexture;  


    void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 100, 100), backButtonTexture))
        {
            JMRSystemActions.Instance.OnExitCardboardMode();
        }
    }
}
