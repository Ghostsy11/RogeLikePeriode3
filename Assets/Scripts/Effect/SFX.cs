using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SFX", menuName = "Audio Clip", order = 1)]
public class SFX : ScriptableObject
{
    [Header("Shooting")]
    [SerializeField] AudioClip Clip;
    [SerializeField][Range(0f, 1f)] float ClipVolume = 1f;

    public void PlayThisSound()
    {
        PlayClip(Clip, ClipVolume);
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
