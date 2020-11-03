using UnityEngine;

namespace GameManager
{
    public class LevelLoader : MonoBehaviour
    {
        public void Load(string levelName)
        {
            GameManager.Instance.LoadScene(levelName);
        }
    }
}
