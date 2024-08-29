using System;
using System.Collections;
using UnityEngine;

public class PlateSpawner : MonoBehaviour
{
    [SerializeField] Table table;
    [SerializeField] int timer;
    bool coroutineDone = true;
    void Start()
    {
        //table = GetComponent<Table>();
        coroutineDone = false;
        StartCoroutine(Delay(table.AddPlate, timer));
    }
    void Update()
    {
        SpawnCheck();
    }
    IEnumerator Delay(Action action, int time)
    {
        yield return new WaitForSeconds(time);
        coroutineDone = true;
        action();
    }
    void SpawnCheck()
    {
        if (!coroutineDone || table.PlateCount >= 15) return;
        coroutineDone = false;
        StartCoroutine(Delay(table.AddPlate, timer));
    }
}