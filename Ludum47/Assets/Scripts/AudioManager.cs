using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class AudioManager : Singleton<AudioManager>
{
    void Start()
    {
        float audioVolume = PlayerPrefs.GetInt(k_musicVolume, 1) == 0 ? 0 : 1;
        float sfxVolume = PlayerPrefs.GetInt(k_sfxVolume, 1) == 0 ? 0 : 1;

        // SetAudioVolume(audioVolume);
        // SetSfxVolume(sfxVolume);

        m_randomGenerator = new Random();
    }

    public void PlayAudio(int index, bool loop)
    {
        AudioClip file = m_audios[index];
        PlayAudio(file, loop);
    }

    public void PlayAudio(string name, bool loop)
    {
        AudioClip file = m_audios.Where(x => x.name == name).SingleOrDefault();
        PlayAudio(file, loop);
    }

    public void PlayAudio(AudioClip file, bool loop)
    {
        m_audioSource.Stop();

        if (file != null)
        {
            m_audioSource.loop = loop;
            m_audioSource.clip = file;
            m_audioSource.Play();
        }
    }

    public void PlaySFX(SFXType type, bool force = false)
    {
        AudioClip clip = null;

        switch (type)
        {
            case SFXType.Portal:
                clip = m_sfxPortal[m_randomGenerator.Next(0, m_sfxPortal.Count - 1)];
                break;
            case SFXType.Hit:
                clip = m_sfxHit[m_randomGenerator.Next(0, m_sfxHit.Count - 1)];
                break;
            case SFXType.Projectile:
                clip = m_sfxProjectile[m_randomGenerator.Next(0, m_sfxProjectile.Count - 1)];
                break;
            case SFXType.Pickup:
                clip = m_sfxPickup[m_randomGenerator.Next(0, m_sfxPickup.Count - 1)];
                break;
            case SFXType.Cat:
                clip = m_sfxCat[m_randomGenerator.Next(0, m_sfxCat.Count - 1)];
                break;
            default:
                break;
        }

        PlaySFX(clip, force);
    }

    public void PlaySFX(string name, bool force = false)
    {
        AudioClip clip = m_sfxs.Where(x => x.name == name).SingleOrDefault();

        PlaySFX(clip, force);
    }

    public void PlaySFX(AudioClip clip, bool force = false)
    {
        if (m_sfxSource.isPlaying && !force)
        {
            return;
        }

        if (clip != null)
        {
            m_sfxSource.clip = clip;
            m_sfxSource.Play();
        }
    }

    public void SetAudioVolume(float volume)
    {
        if (m_audioSource != null)
        {
            m_audioSource.volume = volume;
        }
    }

    public void SetSfxVolume(float volume)
    {
        if (m_sfxSource != null)
        {
            m_sfxSource.volume = volume;
        }
    }

    public enum SFXType
    {
        Portal,
        Hit,
        Pickup,
        Projectile,
        Cat
    }

    private const string k_musicVolume = "MusicVolume";
    private const string k_sfxVolume = "SFXVolume";

    private Random m_randomGenerator;

    [SerializeField]
    private bool m_enabled = false;
    [SerializeField]
    private AudioSource m_audioSource = default;
    [SerializeField]
    private AudioSource m_sfxSource = default;

    [SerializeField]
    private List<AudioClip> m_sfxs = default;
    [SerializeField]
    private List<AudioClip> m_audios = default;

    [SerializeField]
    private List<AudioClip> m_sfxPortal = default;
    [SerializeField]
    private List<AudioClip> m_sfxHit = default;
    [SerializeField]
    private List<AudioClip> m_sfxProjectile = default;
    [SerializeField]
    private List<AudioClip> m_sfxPickup = default;
    [SerializeField]
    private List<AudioClip> m_sfxCat = default;
}
