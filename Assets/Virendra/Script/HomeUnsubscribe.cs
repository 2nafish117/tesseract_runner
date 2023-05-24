using JMRSDK.InputModule;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeUnsubscribe : MonoBehaviour
{
   public int scene = 1;

   private void Start()
   {
      Application.targetFrameRate = 60;
      transform.parent = null;
      DontDestroyOnLoad(gameObject);
      OnHomeSceneChange();
      SceneManager.activeSceneChanged += ChangedActiveScene;
   }

   void ChangedActiveScene(Scene current, Scene next)
   {
      OnHomeSceneChange();
   }
   

   void OnHomeSceneChange()
   {
      JMRSystemDockManager JMRSystemDockManager=GameObject.FindObjectOfType<JMRSystemDockManager>();
      Button button=JMRSystemDockManager.transform.Find("Canvas").transform.Find("UIPanel").Find("ControlPanel").Find("HomeBtn").GetComponent<Button>();
      button.onClick.RemoveAllListeners();
      button.onClick.AddListener(SetHomeScene);
   }

   void SetHomeScene()
   {
      SceneManager.LoadScene(scene);
   }
}
