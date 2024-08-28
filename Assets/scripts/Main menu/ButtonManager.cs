using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main_menu
{
    public class ButtonManager : MonoBehaviour
    {
        [SerializeField] private Scenes scene;

        public void SwitchScene()
        {
            SceneManager.LoadScene((int)scene);
        }
    }
}
