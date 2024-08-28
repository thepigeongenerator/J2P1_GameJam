using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class aantalborden : MonoBehaviour
{
    [SerializeField] TMP_Text plates;
    [SerializeField] TMP_Text timer;
    [SerializeField] Table table;
    int PlatesCount;
    int TimerCount = 3;
    bool TimerOn = false;
    // Start is called before the first frame update
    void Start()
    {
        timer.enabled = false;
    }
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            PlatesCount++;
        }
        if (Input.GetKeyUp(KeyCode.M))
        {
            PlatesCount--;
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        plates.text = "plates:" + PlatesCount.ToString();
        timer.text = "Time: " + TimerCount.ToString();
        if (table.PlateCount != 0)
        {
            if (table.PlateCount >= 10)
            {

                if (TimerOn == false)
                {
                    timer.enabled = true;
                    TimerOn = true;
                    Invoke("WaitTimeTimer", 1);
                }
            }
            if (table.PlateCount < 10)
            {
                TimerCount = 3;
                timer.enabled = false;
            }
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
