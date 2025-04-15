using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    [SerializeField] private float writeSpeed = 50;
    public bool IsRunning { get; private set; }

    public AudioSource _audioSource;

    // used for waiting
    private readonly List<Punctuation> punctuations = new List<Punctuation>()
    {
        new Punctuation(new HashSet<char>() { '.', '!', '?' }, 0.6f),
        new Punctuation(new HashSet<char>() { ',', ';', ':' }, 0.3f),
    };

    private Coroutine typingCoroutine;

    public void Run(string textToType, TMP_Text textLabel, AudioClip voice, float pitch)
    {
        typingCoroutine = StartCoroutine(TypeText(textToType, textLabel, voice, pitch));
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel, AudioClip voice, float pitch)
    {
        IsRunning = true;
        float t = 0;
        int charIndex = 0;
        textLabel.text = string.Empty;
        // TODO: would be nice if we could detect if word is going to wrap and start it from a new line so it doesn't jump.

        while (charIndex < textToType.Length)
        {
            int lastCharIndex = charIndex;
            t += Time.deltaTime * writeSpeed;
            charIndex = Mathf.FloorToInt(t);
            charIndex = Mathf.Clamp(charIndex, 0, textToType.Length);

            for (int i = lastCharIndex; i < charIndex; i++)
            {
                bool isLast = i >= textToType.Length - 1;


                textLabel.text = textToType.Substring(0, i + 1);

                if (IsPunctuation(textToType[i], out float waitTime) && !isLast && !IsPunctuation(textToType[i + 1], out _))
                {
                    yield return new WaitForSeconds(waitTime);
                }

                if (_audioSource != null && !_audioSource.isPlaying)
                {
                    _audioSource.clip = voice;
                    _audioSource.pitch = pitch;
                    _audioSource.Play();
                }
            }

            yield return null;
        }

        IsRunning = false;
    }

    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach (Punctuation punctCat in punctuations)
        {
            if (punctCat.punctuations.Contains(character))
            {
                waitTime = punctCat.waitTime;
                return true;
            }
        }

        waitTime = default;
        return false;
    }

    private readonly struct Punctuation
    {
        public readonly HashSet<char> punctuations;
        public readonly float waitTime;

        public Punctuation(HashSet<char> punctuations, float waitTime)
        {
            this.punctuations = punctuations;
            this.waitTime = waitTime;
        }
    }
}
