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
    [SerializeField] private bool autoFollowDistance = false;
    
    [Tooltip("Set a default forward offset from the head")] 
    public float followDistance=2.4f;
    
    [Tooltip("Additional offset given to the object, while being tracked.")]
    public Vector3 offset;
    public bool faceCamera=true;
    public Vector3 faceCameraOffset = new Vector3(0, 180, 0);

    [Header("Lazy Follow")] 
    [Range(0,1f)]
    public float lazyFollow = 0.6f;

    private float followSpeed = 12;
    
    Transform HeadObject
    {
        get
        {
            return JMRTrackerManager.Instance.GetHeadTransform();
        }
    }

    public bool stickYLock = true;

    public bool startFollow = false;
    public float valueRotation = 0.6f;
    private float tillCenter = 0.5f;
    private float _tempCounter = 0.5f;
    private Vector3 _velocity = Vector3.zero;
    private Ray _ray;

    private float _tempLazy = 0.5f;
    private Transform _JRMTransform=null;
    private Vector3 _additionalOffset = new Vector3();
    private void Start()
    {
        _JRMTransform = transform.parent.parent;
        _tempLazy = lazyFollow;
        startFollow = false;
        JMRInputManager.Instance.AddGlobalListener(gameObject);
        if (autoFollowDistance) followDistance = Vector3.Distance(HeadObject.position, transform.localPosition);
        _tempCounter = tillCenter;
        transform.Find("UIPanel").eulerAngles += faceCameraOffset;
    }

    void Update()
    {
        if (followType == FollowType.StrictFollow)
        {
            transform.localPosition = HeadObject.forward * (followDistance + offset.z) + HeadObject.right * offset.x + HeadObject.up * offset.y;
        }
        else if(followType == FollowType.LazyFollow)
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition,HeadObject.forward * (followDistance + offset.z) + HeadObject.right * offset.x + HeadObject.up * offset.y, followSpeed / 360);
        }
        else if (followType == FollowType.DockFollow && stickYLock && startFollow)
        {
            _tempCounter -= Time.unscaledDeltaTime;
            
            _ray.origin = HeadObject.position;
            _ray.direction = HeadObject.forward;
            //Debug.DrawRay(HeadObject.position,HeadObject.forward);
            Vector3 dif=_ray.GetPoint(followDistance-(_additionalOffset.y));
            dif.y = HeadObject.position.y;
            float dist = Vector3.Distance(dif, transform.position);
            float headRayDistance = Vector3.Distance(HeadObject.position, dif);
            //print("dist="+dist+"|headRayDistance="+(headRayDistance));
            if ( (dist > valueRotation || _tempCounter>0 ) && headRayDistance>1.5f  )
            {
                if (dist > valueRotation)
                {
                    _tempCounter = tillCenter;
                }

                if (dist > followDistance -_additionalOffset.y)
                {
                    lazyFollow = 0f;
                }
                else
                {
                    lazyFollow = _tempLazy;
                }

                transform.position = Vector3.SmoothDamp(transform.position, getTargetPosition(), ref _velocity, lazyFollow);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, HeadObject.rotation, 35 * Time.deltaTime);
            }
            else if(dist>(valueRotation*2))
            {
                lazyFollow = 0f;
                _ray.origin = HeadObject.position;
                _ray.direction = _JRMTransform.forward;
                dif=_ray.GetPoint(followDistance-_additionalOffset.y);
                transform.position = Vector3.SmoothDamp(transform.position, getTargetPosition(), ref _velocity, lazyFollow);
            }
        }
        else { /*dont follow */ }

        if (faceCamera && startFollow)
        {
            transform.LookAt(HeadObject);
        }
    }
    
    Vector3 getTargetPosition()
    {
        Vector3 targetPosition = HeadObject.TransformPoint(new Vector3(0, 0, followDistance -_additionalOffset.y));
        targetPosition.y = HeadObject.position.y;
        _additionalOffset.y = (Mathf.Tan(_JRMTransform.eulerAngles.x * Mathf.Deg2Rad) * followDistance)/2;
        return targetPosition + offset - _additionalOffset;
    }
}
