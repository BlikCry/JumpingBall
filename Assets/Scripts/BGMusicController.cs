using UnityEngine;

public class BGMusicController : MonoBehaviour
{
    [SerializeField]
    private AudioClip startupClip;
    [SerializeField]
    private AudioClip loopClip;
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioSource sourceLoop;

    private void Start()
    {
        source.clip = startupClip;
        sourceLoop.clip = loopClip;
        sourceLoop.loop = true;
        source.Play();
        sourceLoop.PlayDelayed(startupClip.length);
    }
}
