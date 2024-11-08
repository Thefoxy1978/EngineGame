using Aiv.Audio;
using System.Collections.Generic;

namespace Aiv.Fast2D.Component {
    public static class AudioManager {

        private static float[] volumes;
        private static Dictionary<string, AudioClip> clips;
        private static AudioSource[] audioSourcePlayOneShot;
        private static int maxPlayOneShotSource;

        static AudioManager () {
            clips = new Dictionary<string, AudioClip>();
        }

        public static void Init (int numberOfLayer = 1, int maxPlayOneShotSource = 10) {
            volumes = new float[numberOfLayer];
            for (int i = 0; i < volumes.Length; i++) {
                volumes[i] = 1;
            }
            audioSourcePlayOneShot = new AudioSource[1];
            for (int i = 0; i < audioSourcePlayOneShot.Length; i++) {
                audioSourcePlayOneShot[i] = new AudioSource();
            }
        }

        public static AudioClip AddClip (string name, string path) {
            if (clips.ContainsKey(name)) return null;
            AudioClip clip = new AudioClip(path);
            clips.Add(name, clip);
            return clip;
        }

        public static AudioClip GetClip (string name) {
            if (!clips.ContainsKey(name)) return null;
            return clips[name];
        }

        public static void ClearAll () {
            clips.Clear();
        }

        public static void SetVolume (int layer, float value) {
            if (layer < 0 || layer > volumes.Length) return;
            volumes[layer] = value < 0 ? 0 : value  > 1 ? 1 : value;
        }

        public static float GetVolumes (int layer) {
            if (layer < 0 || layer > volumes.Length) return 0;
            return volumes[layer];
        }

        public static void PlayOneShot (AudioClip clip, float volume) {
            for (int i = 0; i < audioSourcePlayOneShot.Length;i++) {
                if (!audioSourcePlayOneShot[i].IsPlaying) {
                    audioSourcePlayOneShot[i].Volume = volume;
                    audioSourcePlayOneShot[i].Play(clip);
                    return;
                }
            }
            if (audioSourcePlayOneShot.Length >= maxPlayOneShotSource) return;
            int newLength = audioSourcePlayOneShot.Length * 2 < maxPlayOneShotSource ? 
                audioSourcePlayOneShot.Length * 2 : maxPlayOneShotSource;
            AudioSource[] tempAudioSource = new AudioSource[newLength];
            int j = 0;
            for (; j < audioSourcePlayOneShot.Length; j++) {
                tempAudioSource[j] = audioSourcePlayOneShot[j];
            }
            for (; j < tempAudioSource.Length; j++) {
                tempAudioSource[j] = new AudioSource();
            }
            PlayOneShot(clip, volume);
        }
    }
}
