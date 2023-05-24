
using System.Collections;
using TMPro;
using UnityEngine;

public class CountDownLoading : MonoBehaviour
{
    public TextMeshProUGUI countDownLoading;
    public GameObject CountDownCanvas;
    
    IEnumerator Start()
    {
        CountDownCanvas.SetActive(true);
        countDownLoading.text = "GET";
        countDownLoading.color=Color.red;
        yield return new WaitForSecondsRealtime(1f);
        countDownLoading.text = "SET";
        countDownLoading.color=Color.yellow;
        yield return new WaitForSecondsRealtime(1f);
        countDownLoading.text = "GO!!";
        countDownLoading.color=Color.green;
        yield return new WaitForSecondsRealtime(1f);
        CountDownCanvas.SetActive(false);
    }

    
}
