using UnityEngine;

namespace Sounds
{
    public class AudioManager : MonoBehaviour
    {

        public void EnableSound(AudioSource audio)
        {
            audio.Play();
        }

    }
}
