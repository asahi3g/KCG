//imports UnityEngine

using UnityEngine;

namespace Audio
{
    public class AudioSystem
    {
        private AudioSource audioSource;

        public void InitStage1()
        {
        }

        public void InitStage2(AudioSource _audioSource)
        {
            audioSource = _audioSource;
        }

        public void PlayOneShot(AudioClip clip)
        {
            if (audioSource == null)
                return;

            audioSource.clip = clip;
            audioSource.PlayOneShot(clip);
        }

        public void Play()
        {
            if (audioSource == null)
                return;

            audioSource.Play();
        }

        public void PlayOneShot(string clipPath)
        {
            if (audioSource == null)
                return;

            AudioClip tempClip = Resources.Load(clipPath, typeof(AudioClip)) as AudioClip;
            audioSource.clip = tempClip;
            audioSource.PlayOneShot(tempClip);
        }

        public void SetAudioClip(AudioClip clip)
        {
            if (audioSource == null)
                return;

            audioSource.clip = clip;
        }

        public void SetAudioClip(string clipPath)
        {
            if (audioSource == null)
                return;

            AudioClip tempClip = Resources.Load(clipPath, typeof(AudioClip)) as AudioClip;
            audioSource.clip = tempClip;
        }

        public void SetAudioSource(AudioSource source)
        {
            if (source == null)
                return;

            audioSource = source;
        }

        public void Stop()
        {
            if (audioSource == null)
                return;

            audioSource.Stop();
        }
    }
}

