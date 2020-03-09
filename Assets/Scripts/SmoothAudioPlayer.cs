using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SmoothAudioPlayer : MonoBehaviour
{
    [SerializeField] [Range(0,0.01f)] private float _smooth = 0.01f;

    private AudioSource _audio;
    private WaitForSeconds _pause;
    private Coroutine _play, _stop;

    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _pause = new WaitForSeconds(_smooth);
        _play = _stop = null;
    }

    public void Play()
    {
        if (_audio.isPlaying)
        {
            if (_stop == null)
                return;
            StopCoroutine(_stop);
            _stop = null;
        }

        _play = StartCoroutine(SmoothPlay());
    }

    public void Stop()
    {
        if (_play != null)
        {
            StopCoroutine(_play);
            _play = null;
        }

        _stop = StartCoroutine(SmoothStop());
    }

    private IEnumerator SmoothPlay()
    {
        _audio.Play();
        while (_audio.volume < 1)
        {
            _audio.volume += 0.01f;
            yield return _pause;
        }
        _play = null;
    }

    private IEnumerator SmoothStop()
    {
        while (_audio.volume > 0)
        {
            _audio.volume -= 0.01f;
            yield return _pause;
        }
        _audio.Stop();
        _stop = null;
    }
}
