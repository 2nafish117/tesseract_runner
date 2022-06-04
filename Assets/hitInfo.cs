using UnityEngine;
using JMRSDK.InputModule;

public class hitInfo : MonoBehaviour
{
	[SerializeField]
	GameObject bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

	public void Update()
	{
		HitObjectInfo();
		FocusedObjectInfo();
		CursorInfo();
	}

	/// <summary>
	/// Get JMRPointerObject and update it on UI.
	/// </summary>
	public void CursorInfo()
	{
		// get cursor transform
		Transform cursorTransform = JMRPointerManager.Instance.GetCursorTransform();
		if (cursorTransform == null)
		{
			Debug.Log("Cursor Object : null");
		}
		else
		{
			Debug.Log("Cursor Object : " + cursorTransform.name);
			bullet.transform.position = cursorTransform.position; 
		}
	}

	/// <summary>
	/// Get the currently focused object and update it on UI.
	/// </summary>
	public void FocusedObjectInfo()
	{
		// get current focus
		GameObject go = JMRPointerManager.Instance.GetCurrentFocusedObject();
		if (go == null)
		{
			Debug.Log("Focused Object : null");
		
		}
		else
		{
			Debug.Log("Focused Object: " + go.name);
		}
	}

	/// <summary>
	/// Get the currently hit object and update it on UI.
	/// </summary>
	public void HitObjectInfo()
	{
		// get current ray
		Ray ray = JMRPointerManager.Instance.GetCurrentRay();
		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			Debug.Log("Hit Object : " + hit.transform.name);
		}
		else
		{
			Debug.Log("Hit Object : null");
		}
	}
}
