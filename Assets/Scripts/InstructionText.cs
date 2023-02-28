using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class InstructionText : MonoBehaviour {

    [SerializeField]
    float startDelay;

    [SerializeField]
    float waitTime;

    [SerializeField]
    float durationTween;

   void GameStart()
    {
        StartCoroutine(TextTween());
    }

    IEnumerator TextTween()
    {
        yield return new WaitForSeconds(startDelay);

        if (GetComponent<TextMeshPro>())
        {
            GetComponent<TextMeshPro>().DOColor(new Color(255, 255, 255, 1), durationTween);
            yield return new WaitForSeconds(waitTime);
            GetComponent<TextMeshPro>().DOColor(new Color(255, 255, 255, 0), durationTween);
        }   
    }

    private void Awake()
    {
        GameManager.OnGameStart += GameStart;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= GameStart;
    }
}
