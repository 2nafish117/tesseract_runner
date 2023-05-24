using UnityEngine;
using UnityEngine.SceneManagement;

public class camerafov : MonoBehaviour
{
    void Start()
    {
        transform.parent = null;
        DontDestroyOnLoad(gameObject);
        ChangeCamera();
        SceneManager.activeSceneChanged += ChangedActiveScene;
    }
   
   void ChangedActiveScene(Scene current, Scene next)
   {
       ChangeCamera();
   }

   void ChangeCamera()
   {
       Camera Head=GameObject.Find("Head").GetComponent<Camera>();
       Camera Left=GameObject.Find("Left").GetComponent<Camera>();;
       Camera Right=GameObject.Find("Right").GetComponent<Camera>();;
       Head.fieldOfView = 40;
       Head.clearFlags = CameraClearFlags.Skybox;

       SelectAllLayers(Head,"Head","Head");
       SelectAllLayers(Left,"Left","Head");
       SelectAllLayers(Right,"Right","Head");
   }

   void SelectAllLayers(Camera camera,string excludeLayer1,string excludeLayer2)
   {
       // Fetch all layers
       string[] allLayers = GetAllLayerNames();
        
       // Get the layer index to exclude
       int excludedLayerIndex1 = LayerMask.NameToLayer(excludeLayer1);
       int excludedLayerIndex2 = LayerMask.NameToLayer(excludeLayer2);
        
       // Create a layer mask excluding the "Head" layer
       int layerMask = 0;
       for (int i = 0; i < allLayers.Length; i++)
       {
           if (i != excludedLayerIndex1)
               layerMask |= 1 << i;
           else if (i != excludedLayerIndex2)
               layerMask |= 1 << i;
       }
        
       // Assign the layer mask to the camera
       Camera.main.cullingMask = layerMask;
   }
   
   string[] GetAllLayerNames()
   {
       string[] allLayers = new string[32]; // Unity supports 32 layers
        
       for (int i = 0; i < allLayers.Length; i++)
       {
           allLayers[i] = LayerMask.LayerToName(i);
       }
        
       return allLayers;
   }
}
