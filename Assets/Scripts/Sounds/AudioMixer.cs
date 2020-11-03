using UnityEngine;
using UnityEngine.Audio;

namespace Sounds
{
    public class AudioMixer : MonoBehaviour {

        public AudioMixerSnapshot pauseSnapshot;
        public AudioMixerSnapshot playSnapshot;
        public AudioMixerSnapshot loseSnapshot;
        public AudioMixerSnapshot winSnapshot;

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
    }
}