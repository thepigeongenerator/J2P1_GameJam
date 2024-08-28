using System;
using System.Collections;
using UnityEngine;

public class PlateSpawner : MonoBehaviour
{
    Table table = FindAnyObjectByType<Table>();
    [SerializeField] int timer;
    bool coroutineDone = true;
    void Update()
    {
        SpawnCheck();
    }
    IEnumerator Delay(Action action, int time, Action action2 = null)
    {
        yield return new WaitForSeconds(time);
        coroutineDone = true;
        action();
        if (action2 != null) action2();
    }
    void SpawnCheck()
    {
        if (!coroutineDone) return;
        coroutineDone = false;
        StartCoroutine(Delay(table.AddPlate, timer));
    }
}