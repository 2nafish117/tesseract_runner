using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Camera cam;
    public GameObject cursor;
    public GameObject follow;
    public float speed = 1000.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        shoot();

        follow.transform.position = Vector3.Lerp(follow.transform.position, cursor.transform.position, Time.deltaTime * speed);


    }

    void shoot()
    {
        RaycastHit hit;
        Debug.DrawRay(cam.transform.position, cam.transform.forward*1000, Color.green);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit))
        {
            Debug.Log(hit.transform.name);
            cursor.transform.position = hit.point;
        }

    }
}
