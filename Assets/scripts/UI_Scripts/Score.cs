using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Score : MonoBehaviour
{
    [SerializeField] TMP_Text score;// tmp text for the score
    private int scoreInt;// int for the score
    // Start is called before the first frame update
    void Start()
    {
        scoreInt = 0; // sets the int to 0 
    }

    // Update is called once per frame
    void Update()
    {
      
        score.text = "score:" + scoreInt.ToString();// sets the int to a string and sets it on the text
        PlayerPrefs.SetInt("Scoreint", scoreInt);//saves the int to a playerpref
    }
   public void ScorePlus()
    {
        scoreInt++;// higers the scoreint by 1
    }
}
