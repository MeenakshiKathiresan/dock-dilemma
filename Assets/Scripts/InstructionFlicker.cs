using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class InstructionFlicker : MonoBehaviour
{

    [SerializeField]
    float startDelay;

    [SerializeField]
    int loop;

    [SerializeField]
    float durationTween;

    void GameStart()
    {
        StartCoroutine(TextTween());
    }

    IEnumerator TextTween()
    {
        yield return new WaitForSeconds(startDelay);

        if (GetComponent<SpriteRenderer>())
        {
            GetComponent<SpriteRenderer>().DOColor(new Color(255, 255, 255, 1), durationTween).SetLoops(loop,LoopType.Yoyo);
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
