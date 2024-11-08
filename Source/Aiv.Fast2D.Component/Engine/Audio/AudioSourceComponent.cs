using Aiv.Audio;

namespace Aiv.Fast2D.Component {


    enum AudioSourceStatus {
        play,
        pause,
        stop
    }

    public class AudioSourceComponent : Component {

        private AudioSourceStatus myStatus;

        private AudioSource internalAudioSource;

        private int myType; //il canale audio, quindi l'indice dell'array di volumes che abbiamo inserito in AudioManger
        public int MyType {
            get { return myType; }
            set {
                myType = value;
                internalAudioSource.Volume = myVolume * AudioManager.GetVolumes(myType);
            }
        }

        private float myVolume;
        public float MyVolume {
            get { return myVolume; }
            set {
                myVolume = value < 0 ? 0 : value > 1 ? 1 : value;
                internalAudioSource.Volume = myVolume * AudioManager.GetVolumes(myType);
            }
        }

        private AudioClip myAudioClip;

        public bool Loop {
            get;
            set;
        }

        public AudioSourceComponent (GameObject owner) : base (owner) {
            myStatus = AudioSourceStatus.stop;
            internalAudioSource = new AudioSource();
            MyVolume = 1;
        }

        public void SetClip (AudioClip clip) {
            myAudioClip = clip;
        }

        public void Play () {
            switch (myStatus) {
                case AudioSourceStatus.pause:
                    internalAudioSource.Resume();
                    break;
                case AudioSourceStatus.stop:
                    internalAudioSource.Play(myAudioClip, Loop);
                    break;
            }
            myStatus = AudioSourceStatus.play;
        }

        public void Pause () {
            switch (myStatus) {
                case AudioSourceStatus.play:
                    internalAudioSource.Pause();
                    myStatus = AudioSourceStatus.pause;
                    break;
            }
        }

        public void Stop () {
            switch (myStatus) {
                case AudioSourceStatus.pause:
                case AudioSourceStatus.play:
                    internalAudioSource.Stop();
                    myStatus = AudioSourceStatus.stop;
                    break;
            }
        }

        public void PlayOneShot (AudioClip clipToPlay) {
            AudioManager.PlayOneShot(clipToPlay, MyVolume);
        }

    }
}
