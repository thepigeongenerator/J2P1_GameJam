using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Score : MonoBehaviour
{
    [SerializeField] TMP_Text score;
    private int scoreInt;
    GameObject food;
    bool FoodDeleted;
    // Start is called before the first frame update
    void Start()
    {
        food = GameObject.Find("Food");
        scoreInt = -1;
        FoodDeleted = false;
        if(food != null)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if(food != null && FoodDeleted == false)
        {
            FoodDeleted=true;
            scoreInt++;
            FoodDeleted = false;
        }
        score.text = "score:" + scoreInt.ToString();
        PlayerPrefs.SetInt("Scoreint", scoreInt);
    }
}
