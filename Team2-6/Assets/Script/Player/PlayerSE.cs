using UnityEngine;

public class PlayerSE : MonoBehaviour
{
    [Header("基本SE")]
    public AudioClip damageClip;
    public AudioClip jumpClip;
    public AudioClip deathClip;
    public AudioClip footstepClip;

    [Header("能力SE")]
    public AudioClip genericAbilityClip;
    public AudioClip freezeClip;
    public AudioClip dashClip;
    public AudioClip shotClip;
    public AudioClip fireShotClip;

    private AudioSource seSource;
    private AudioSource footstepSource;

    public float DeathClipLength => deathClip != null ? deathClip.length : 0f;

    void Awake()
    {
        seSource = gameObject.AddComponent<AudioSource>();
        seSource.playOnAwake = false;
        seSource.loop = false;
        seSource.spatialBlend = 0f;

        footstepSource = gameObject.AddComponent<AudioSource>();
        footstepSource.playOnAwake = false;
        footstepSource.loop = true;
        footstepSource.spatialBlend = 0f;
        footstepSource.clip = footstepClip;
    }

    public void PlayDamage()
    {
        PlaySE(damageClip);
    }

    public void PlayJump()
    {
        PlaySE(jumpClip);
    }

    public void PlayDeath()
    {
        PlaySE(deathClip);
    }

    public void PlayGenericAbility()
    {
        PlaySE(genericAbilityClip);
    }

    public void PlayFreeze()
    {
        PlaySE(freezeClip);
    }

    public void PlayDash()
    {
        PlaySE(dashClip);
    }

    public void PlayShot()
    {
        PlaySE(shotClip);
    }

    public void PlayFireShot()
    {
        PlaySE(fireShotClip);
    }

    public void SetFootsteps(bool isWalking)
    {
        if (footstepClip == null)
        {
            return;
        }

        if (isWalking)
        {
            if (!footstepSource.isPlaying)
            {
                footstepSource.Play();
            }
        }
        else
        {
            if (footstepSource.isPlaying)
            {
                footstepSource.Stop();
            }
        }
    }

    private void PlaySE(AudioClip clip)
    {
        if (clip != null)
        {
            seSource.PlayOneShot(clip);
        }
    }
}
