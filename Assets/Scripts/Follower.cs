using System.Collections;
using JMRSDK;
using JMRSDK.InputModule;
using UnityEngine;

public class Follower : MonoBehaviour
{
    enum FollowType{ DontFollow, StrictFollow, LazyFollow,DockFollow }
    //enum FollowAxis{ XY, X, Y }

    [SerializeField] private FollowType followType = FollowType.DockFollow;

    //[Space] [Tooltip("Axis that will be tracked for the object")] 
    //[SerializeField] private FollowAxis followAxis;
    
    [Tooltip("Override the follow distance by the starting distance.")]
    [SerializeField] private bool autoFollowDistance = true;
    
    [Tooltip("Set a default forward offset from the head")] 
    public float followDistance;
    
    [Tooltip("Additional offset given to the object, while being tracked.")]
    public Vector3 offset;
    public bool faceCamera=true;
    public Vector3 faceCameraOffset = new Vector3(0, 180, 0);

    [Header("Lazy Follow")] 
    public float followSpeed = 12f;
    
    Transform HeadObject => JMRTrackerManager.Instance.GetHeadTransform();
    public Vector3 additionalOffset = new Vector3();
    public bool stickYLock = true;

    public bool startFollow = false;
    public float valueRotation = 0.6f;
    private bool onmove = false;
    private float tillCenter = 1f;
    private float tempCounter = 1f;
    private void Start()
    {
        startFollow = false;
        if (autoFollowDistance) followDistance = Vector3.Distance(HeadObject.position, transform.localPosition);
        tempCounter = tillCenter;
    }

    void LateUpdate()
    {
        if (followType == FollowType.StrictFollow)
        {
            transform.localPosition = HeadObject.forward * (followDistance + offset.z) + HeadObject.right * offset.x +
                                 HeadObject.up * offset.y;
        }
        else if(followType == FollowType.LazyFollow)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,HeadObject.forward * (followDistance + offset.z) + HeadObject.right * offset.x + HeadObject.up * offset.y, followSpeed / 360);
        }
        else if (followType == FollowType.DockFollow && stickYLock && startFollow)
        {
            //print("Head Position="+HeadObject.position);
            tempCounter -= Time.deltaTime;
            Ray r = new Ray( HeadObject.position, HeadObject.forward);
            Debug.DrawRay(HeadObject.position, HeadObject.forward);
            Vector3 dif=r.GetPoint(2.4f);
            dif = new Vector3(dif.x, 0, dif.z);
            float dist = Vector3.Distance(dif, transform.localPosition);
            print("Distance=" + dist);
            if ( dist > valueRotation || tempCounter >0)
            {
                if (dist > valueRotation)
                {
                    tempCounter = tillCenter;
                }
                Vector3 newPos = HeadObject.forward * (followDistance + offset.z) + HeadObject.right * offset.x + HeadObject.up * offset.y;
                newPos.y = additionalOffset.y;
                transform.localPosition = Vector3.Lerp(transform.localPosition,newPos, followSpeed / 360);
            }
            
            transform.localPosition =new Vector3(transform.localPosition.x,0, transform.localPosition.z);
            
        }
        else { /*dont follow */ }

        if (faceCamera && startFollow)
        {
            transform.LookAt(HeadObject);
            transform.eulerAngles +=faceCameraOffset;
        }
    }
}
