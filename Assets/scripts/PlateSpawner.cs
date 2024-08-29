using System;
using System.Collections;
using UnityEngine;

public class PlateSpawner : MonoBehaviour
{
    [SerializeField] Table table;
    [SerializeField] float timer;
    bool coroutineDone = true;
    void Update()
    {
        SpawnCheck();
        FixTimer();
    }
    IEnumerator Delay(Action action, float time)
    {
        yield return new WaitForSeconds(time);//after the time in secconds hit 0 the next lines will run
        coroutineDone = true;//coroutine is done so the boolean will set true to let the script start an other coroutine
        action();//the given action will run
    }
    void SpawnCheck()//check of there can spawn a other plate
    {
        if (!coroutineDone) return;//if the coroutine is stil going (the timer of it is not yet 0) it will go back imediatly
        coroutineDone = false;//the coroutine will start and the boolean will set false to make sure the coroutine will start every frame
        StartCoroutine(Delay(table.AddPlate, timer));//start a coroutine with a given action (a method in this case) and a time that it waits in secconds
    }
    private void FixTimer()//here the time between spawning each plate will be set based on the currend score
    {
        int score = PlayerPrefs.GetInt("Scoreint");//get the score
        timer = MathF.Pow(0.99F, score);//the timer is 0.99 to the power of score 
    }
}