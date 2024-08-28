using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class aantalborden : MonoBehaviour
{
    [SerializeField] TMP_Text plates;
    [SerializeField] TMP_Text timer;
    int PlatesCount;
    int TimerCount = 3;
    bool TimerOn = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        plates.text = "amount of plates:" + PlatesCount.ToString();
        timer.text = "Time: " + TimerCount.ToString();
        if (Input.GetKeyUp(KeyCode.Space))
        {
            PlatesCount++;
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            PlatesCount--;
        }
        if (PlatesCount >= 10)
        {
            timer.enabled = true;
            if (TimerOn == false)
            {
                TimerOn = true;
                Invoke("WaitTimeTimer", 1);
            }
        }
        if(PlatesCount < 10) 
        {
            TimerCount = 3;
            timer.enabled = false;
        }
    }
    void WaitTimeTimer()
    {
        TimerOn = false;
        TimerCount--;
        if (TimerCount == 0)
        {
            TimerOn = true;
        }
    }
}
