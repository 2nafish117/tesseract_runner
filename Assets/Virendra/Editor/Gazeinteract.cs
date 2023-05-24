using JMRSDK.Toolkit;
using JMRSDK.Toolkit.UI;
using UnityEngine;
using UnityEngine.UI;

public class Gazeinteract : MonoBehaviour
{
    
    [ContextMenu("Do Something")]
    void DoSomething()
    {
        var g = GameObject.FindObjectsOfType<JMRUIPrimaryButton>(true);
        foreach (var button in g)
        {
            if(button.GetComponent<JMRGazeInteractable>()==null)
                button.gameObject.AddComponent<JMRGazeInteractable>();
        }
        var gg = GameObject.FindObjectsOfType<JMRUIButton>(true);
        foreach (var button in gg)
        {
            if(button.GetComponent<JMRGazeInteractable>()==null)
                button.gameObject.AddComponent<JMRGazeInteractable>();
        }
        var ggg = GameObject.FindObjectsOfType<Button>(true);
        foreach (var button in ggg)
        {
            if(button.GetComponent<JMRGazeInteractable>()==null)
                button.gameObject.AddComponent<JMRGazeInteractable>();
        }
        var gggg = GameObject.FindObjectsOfType<JMRUISecondaryButton>(true);
        foreach (var button in gggg)
        {
            if(button.GetComponent<JMRGazeInteractable>()==null)
                button.gameObject.AddComponent<JMRGazeInteractable>();
        }
        var ggggg = GameObject.FindObjectsOfType<JMRUITertiaryButton>(true);
        foreach (var button in ggggg)
        {
            if(button.GetComponent<JMRGazeInteractable>()==null)
                button.gameObject.AddComponent<JMRGazeInteractable>();
        }
        var gggggg = GameObject.FindObjectsOfType<JMRUIPrimaryInputField>(true);
        foreach (var button in gggggg)
        {
            if(button.GetComponent<JMRGazeInteractable>()==null)
                button.gameObject.AddComponent<JMRGazeInteractable>();
        }
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
