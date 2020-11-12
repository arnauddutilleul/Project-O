using UnityEngine;
using UnityEngine.Audio;

namespace Sounds
{
    public class AudioMixerManager : MonoBehaviour {

        public AudioMixerSnapshot pauseSnapshot;
        public AudioMixerSnapshot playSnapshot;
        public AudioMixerSnapshot loseSnapshot;
        public AudioMixerSnapshot winSnapshot;
        [SerializeField] private AudioMixer masterMixer;

        public float defaultTransitionTime = 0.2f;

        public enum MusicMode {Pause, Play, Win, Lose};

        public void Transition(MusicMode newMode, float transitionTime) {
            switch (newMode) {
                case MusicMode.Pause:
                    pauseSnapshot.TransitionTo (transitionTime);
                    break;
                case MusicMode.Play:
                    playSnapshot.TransitionTo (transitionTime);
                    break;
                case MusicMode.Lose:
                    loseSnapshot.TransitionTo (transitionTime);
                    break;
                case MusicMode.Win:
                    winSnapshot.TransitionTo (transitionTime);
                    break;
                default:
                    break;
            }
        }

        public void Transition(MusicMode newMode) {
            Transition (newMode, defaultTransitionTime);
        }

        public void SetMasterVol(float masterVol)
        {
            masterMixer.SetFloat("masterVol", Mathf.Log10(masterVol) * 20);
        }
        
        public void SetSfxVol(float sfxVol)
        {
            masterMixer.SetFloat("sfxVol", Mathf.Log10(sfxVol) * 20);
        }
        
        public void SetMenuVol(float menuVol)
        {
            masterMixer.SetFloat("menuVol", Mathf.Log10(menuVol) * 20);
        }
        
        public void SetMusicVol(float musicVol)
        {
            masterMixer.SetFloat("musicVol", Mathf.Log10(musicVol) * 20);
        }
    }
}