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

    // used for waiting
    private readonly Dictionary<HashSet<char>, float> punctuations = new Dictionary<HashSet<char>, float>()
    {
        { new HashSet<char>() { '.', '!', '?' }, 0.6f },
        { new HashSet<char>() { ',', ';', ':' }, 0.3f },
    };

    private Coroutine typingCoroutine;
    
    public void Run(string textToType, TMP_Text textLabel)
    {
        typingCoroutine = StartCoroutine(TypeText(textToType, textLabel));
    }

    public void Stop()
    {
        StopCoroutine(typingCoroutine);
        IsRunning = false;
    }

    private IEnumerator TypeText(string textToType, TMP_Text textLabel)
    {
        IsRunning = true;
        float t = 0;
        int charIndex = 0;
        textLabel.text = string.Empty;

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
            }

            yield return null;
        }

        IsRunning = false;
    }

    private bool IsPunctuation(char character, out float waitTime)
    {
        foreach (KeyValuePair<HashSet<char>, float> punctCat in punctuations)
        {
            if (punctCat.Key.Contains(character))
            {
                waitTime = punctCat.Value;
                return true;
            }
        }

        waitTime = default;
        return false;
    }
}
