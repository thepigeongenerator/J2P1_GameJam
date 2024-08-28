using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main_menu
{
    public class ButtonManager : MonoBehaviour
    {
        [SerializeField] private string sceneName;

        public void SwitchScene()
        {
            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogWarning("Scene name is not set!");
            }
            else
            {
                SceneManager.LoadScene(sceneName); 
            }
        }
    }
}
