using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreCounter : MonoBehaviour
{

    bool counterStop;
    public Text counter;
    float currentTime;
    // Start is called before the first frame update
    void Start()
    {
        counterStop = false;
        currentTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(!counterStop)
        {
            currentTime = currentTime + Time.deltaTime;
        }

        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        counter.text = time.Minutes.ToString() + ":" + time.Seconds.ToString() + ":" + time.Milliseconds.ToString();    
    }
}
