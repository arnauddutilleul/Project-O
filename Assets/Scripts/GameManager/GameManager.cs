using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameManager
{
    public class GameManager : SingletonMb<GameManager>
    {
        protected override void Cleanup()
        {
        }

        protected override void Initialize()
        {
        }
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
            //var asyncload = SceneManager.LoadSceneAsync(sceneName);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}