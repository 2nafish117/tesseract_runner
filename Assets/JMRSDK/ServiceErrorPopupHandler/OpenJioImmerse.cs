using JMRSDK;
using UnityEngine;

public class OpenJioImmerse : MonoBehaviour
{

    public static OpenJioImmerse instance;
    void Start()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        if (Application.isEditor)
        {
            Destroy(gameObject);
        }
        
        if (JMRSDKManager.Instance.isServiceReady())
        {
            Destroy(gameObject);
        }

        
    }

    public void OpenJioImmerseApp()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.jiotesseract.mr.jxr");
    }
   
}
