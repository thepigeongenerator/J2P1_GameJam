using System;
using System.Collections;
using UnityEngine;

public class PlateSpawner : MonoBehaviour
{
    [SerializeField] Table table;
    [SerializeField] float timer;
    bool coroutineDone = true;
    void Start()
    {
        coroutineDone = false;
        StartCoroutine(Delay(table.AddPlate, timer));
    }
    void Update()
    {
        SpawnCheck();
        FixTimer();
    }
    IEnumerator Delay(Action action, float time)
    {
        yield return new WaitForSeconds(time);
        coroutineDone = true;
        action();
    }
    void SpawnCheck()
    {
        if (!coroutineDone) return;
        coroutineDone = false;
        StartCoroutine(Delay(table.AddPlate, timer));
    }
    private void FixTimer()
    {
        int score = PlayerPrefs.GetInt("Scoreint");
        timer = MathF.Pow(0.99F, score);
    }
}