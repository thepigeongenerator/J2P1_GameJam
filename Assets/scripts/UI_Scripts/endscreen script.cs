using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class endscreenscript : MonoBehaviour
{
    [SerializeField] TMP_Text EndScreenScore;
    int endscore;
    // Start is called before the first frame update
    void Start()
    {
        endscore = PlayerPrefs.GetInt("Scoreint");
    }

    // Update is called once per frame
    void Update()
    {
        EndScreenScore.text = "score: " +endscore.ToString();
    }
    public void returnscene()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
