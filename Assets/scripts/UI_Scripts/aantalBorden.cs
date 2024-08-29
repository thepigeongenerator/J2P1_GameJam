using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AantalBorden : MonoBehaviour
{
    [SerializeField] TMP_Text plates;// for the plates text
    [SerializeField] TMP_Text timer;// for the timer
    [SerializeField] Table table;// a reference to table script
    int TimerCount = 3;// interger for the time that you have if there are to many plates on the table
    bool TimerOn = false;// bool for if the timer is on or off
    // Start is called before the first frame update
    void Start()
    {
        timer.enabled = false;//set the timer text to be not seen
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        plates.text = "plates:" + table.PlateCount.ToString();// set the everything to text and sets platecount to string
        timer.text = "Time: " + TimerCount.ToString();// is the same as the one above
        if (table.PlateCount != 0)//checks if plate count isn't 0
        {
            if (table.PlateCount >= 15)// checks if platecount is same or higher then 15
            {

                if (TimerOn == false)//checks if timeron bool on false is
                {
                    timer.enabled = true;// make the timer visible
                    TimerOn = true;//sets the bool to true
                    Invoke("WaitTimeTimer", 1);// invoke the WaitTimeTimer method after 1 second
                }
            }
            if (table.PlateCount < 15)// checks if table count is lower then 15
            {
                TimerCount = 3;//set the timercount back to 3
                timer.enabled = false;//makes the timer invisible again
            }
        }
    }
    void WaitTimeTimer()
    {
        TimerOn = false;//set timeron to false
        TimerCount--;// removes 1 from timercount
        if (TimerCount == 0)// checks if timercount is 0
        {
            SceneManager.LoadScene("End Screen");//loads end screen scene
        }
    }
}
