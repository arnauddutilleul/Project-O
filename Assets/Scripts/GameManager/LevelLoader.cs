using UnityEngine;

namespace GameManager
{
    public class LevelLoader : MonoBehaviour
    {
        public void Load(string levelName)
        {
            global::GameManager.GameManager.Instance.LoadScene(levelName);
        }
    }
}
