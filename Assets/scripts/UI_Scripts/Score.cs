using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Score : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    private int scoreInt;
    // Start is called before the first frame update
    void Start()
    {
        scoreInt = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyUp(KeyCode.Space))
        {
            scoreInt++;
        }*/
        score.text = "score:" + scoreInt.ToString();
        PlayerPrefs.SetInt("Scoreint", scoreInt);
    }
}
