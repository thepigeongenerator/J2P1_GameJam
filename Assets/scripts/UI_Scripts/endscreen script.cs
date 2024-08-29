using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endscreenscript : MonoBehaviour
{
    [SerializeField] TMP_Text EndScreenScore;// for the endscreenscore text
    int endscore;// the score
    // Start is called before the first frame update
    void Start()
    {
        endscore = PlayerPrefs.GetInt("Scoreint");//gets the in from the playerpref scoreint
    }

    // Update is called once per frame
    void Update()
    {
        EndScreenScore.text = "score: " +endscore.ToString();//sets score plus the int that is set to int on the text
    }
    
}
