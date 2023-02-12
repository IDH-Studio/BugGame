using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOUND = CESCO.SOUND;
using SCREEN = CESCO.SCREEN;
using TOOL = CESCO.TOOL;

public class SoundManager : MonoBehaviour
{
    private AudioSource BGMSound;
    public AudioSource BGM
    {
        get { return BGMSound; }
    }
    private AudioSource EffectSound;
    public AudioSource Effect
    {
        get { return EffectSound; }
    }

    public AudioClip[] BGMSounds;
    public AudioClip[] EffectSounds;

    private List<AudioClip> BGMAudioClips;
    private List<AudioClip> EffectAudioClips;

    private List<AudioSource> Sounds;

    private int GetSoundIndex(SOUND sound) { return (int)sound; }

    private void Awake()
    {
        // 오디오 재생 AudioSource
        BGMSound = GameObject.Find("BGM").GetComponent<AudioSource>();
        EffectSound = GameObject.Find("Effect").GetComponent<AudioSource>();

        // 오디오 소스 AudioClip
        BGMAudioClips = new List<AudioClip>();
        EffectAudioClips = new List<AudioClip>();

        foreach (var bgm in BGMSounds)
        {
            BGMAudioClips.Add(bgm);
        }

        foreach(var effect in EffectSounds)
        {
            EffectAudioClips.Add(effect);
        }

        Sounds = new List<AudioSource>();
        Sounds.Insert(GetSoundIndex(SOUND.BGM), BGMSound);
        Sounds.Insert(GetSoundIndex(SOUND.EFFECT), EffectSound);

        BGMPlay();
    }

    public void BGMPlay()
    {
        BGMSound.clip = BGMAudioClips[0];
        BGMSound.loop = true;
        BGMSound.Play();
    }

    public void BGMPlay(SCREEN screen)
    {
        BGMSound.clip = BGMAudioClips[(int)screen];
        BGMSound.loop = true;
        BGMSound.Play();
    }
    public void BGMPlay(SCREEN screen, bool isLoop)
    {
        BGMSound.clip = BGMAudioClips[(int)screen];
        BGMSound.loop = isLoop;
        BGMSound.Play();
    }
    public void BGMPlay(SCREEN screen, float delay)
    {
        BGMSound.clip = BGMAudioClips[(int)screen];
        BGMSound.loop = true;
        //BGMSound.Play();
        BGMSound.PlayDelayed(delay);
    }
    public void BGMPlay(SCREEN screen, bool isLoop, float delay)
    {
        BGMSound.clip = BGMAudioClips[(int)screen];
        BGMSound.loop = isLoop;
        //BGMSound.Play();
        BGMSound.PlayDelayed(delay);
    }

    public void BGMStop()
    {
        BGMSound.Stop();
    }

    public void ChangeVolume(SOUND sound, float volume)
    {
        Sounds[GetSoundIndex(sound)].volume = volume;
    }

    public void EffectPlay(TOOL tool)
    {
        EffectSound.clip = EffectAudioClips[(int)tool];
        EffectSound.Play();
    }
}
