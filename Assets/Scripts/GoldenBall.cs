using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GoldenBall : Shape
{


    [SerializeField]
    List<Color> colours = new List<Color>();

    public override void OnHittingTarget(Target target)
    {
        GetComponent<Collider2D>().enabled = false;

        reachedTarget = true;

        targetID = target.id;
        if (GameManager.OnCorrectAnswer != null)
            GameManager.OnCorrectAnswer(this);

        gameObject.SetActive(false);
    }

    public override void OnEnable()
    {
        base.OnEnable();
        TweenColor();
    }

    void TweenColor()
    {

        ball.GetComponent<SpriteRenderer>().DOColor(colours[Random.Range(0, colours.Count)], 0.5f).SetLoops(2, LoopType.Yoyo).OnComplete(TweenColor);
    }
}
