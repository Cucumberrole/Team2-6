using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GameAudioManager : MonoBehaviour
{
    public static GameAudioManager Instance;

    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("ゲーム中SE")]
    public AudioClip itemGetClip;
    public AudioClip unlockClip;

    [Header("UI SE")]
    public AudioClip selectClip;
    public AudioClip decideClip;
    public AudioClip cancelClip;

    [Header("クリア")]
    public AudioClip clearJingleClip;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;
        audioSource.loop = false;
        audioSource.spatialBlend = 0f;
    }

    public void PlayItemGet()
    {
        PlaySE(itemGetClip);
    }

    public void PlayUnlock()
    {
        PlaySE(unlockClip);
    }

    public void PlaySelect()
    {
        PlaySE(selectClip);
    }

    public void PlayDecide()
    {
        PlaySE(decideClip);
    }

    public void PlayCancel()
    {
        PlaySE(cancelClip);
    }

    public float PlayClearJingle()
    {
        if (clearJingleClip == null)
        {
            return 0f;
        }

        PlaySE(clearJingleClip);
        return clearJingleClip.length;
    }

    private void PlaySE(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
