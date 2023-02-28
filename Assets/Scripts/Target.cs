using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class Target : MonoBehaviour
{

    [SerializeField]
    public int id;

    [SerializeField]
    int displayCount;

    [SerializeField]
    Direction directionToTarget;

    [SerializeField]
    SpriteRenderer door;

    int correctAnswerCount = 0;

    [SerializeField]
    public List<Shape> correctAnswers = new List<Shape>();

    [SerializeField]
    public List<Shape> pattern = new List<Shape>();

    [SerializeField]
    TextMeshPro gratificationText;

    int totalShapes;

    private void OnEnable()
    {
        GameManager.OnCorrectAnswer += OnCorrectAnswer;
        GameManager.OnWrongAnswer += OnWrongAnswer;
        totalShapes = pattern.Count;
    }

    private void OnDisable()
    {
        GameManager.OnCorrectAnswer -= OnCorrectAnswer;
        GameManager.OnWrongAnswer -= OnWrongAnswer;
    }

    void OnCorrectAnswer(Shape shape)
    {
        if (shape is GoldenBall && id == shape.targetID)
        {
            StartCoroutine(BringToTarget());
        }
        else if (id == shape.targetID)
        {
            ShiftShapes(shape);
            StartCoroutine(ChangeGratificationText(GameManager.Instance.gratifications[Random.Range(0, GameManager.Instance.gratifications.Length)], .5f));
        }
        AudioManager.Instance.PlayAudio(2);
    }

    void OnWrongAnswer(Shape shape, int wrongID)
    {
        if (id == shape.targetID)
        {
            AudioManager.Instance.PlayAudio(3);
            shape.JumpToHome(correctAnswers[correctAnswers.Count - 1].transform.position);
            ShiftShapes(shape, 0);
        }

        if (wrongID == id)
        {
            GetComponent<SpriteRenderer>().DOColor(new Color(0.8705882f, .135f, .1436f), 0.25f).SetLoops(2, LoopType.Yoyo);
            StartCoroutine(ChangeGratificationText("OOPS!!", 0.5f));
        }
    }

    IEnumerator BringToTarget()
    {
        if (pattern.Count > 0)
        {
            Shape shape = pattern[0];
            pattern.RemoveAt(0);
            shape.transform.DOScale(Vector2.zero, 0.2f).OnComplete(() =>
            {
                shape.transform.DOScale(new Vector2(.85f, .85f), 0.2f);
            });
            ShiftShapes(shape, 0);
            shape.gameObject.SetActive(true);
            shape.reachedTarget = true;

            yield return new WaitForSeconds(0.5f);


        }

    }

    public IEnumerator ChangeGratificationText(string text, float wait)
    {
        gratificationText.text = text;
        gratificationText.DOFade(1, 0.2f);
        gratificationText.transform.DOLocalMoveY(1.75f, .5f);
        yield return new WaitForSeconds(wait);
        gratificationText.DOFade(0, .3f).OnComplete(()=> gratificationText.transform.DOLocalMoveY(0,0));

    }
    public void CloseDoor()
    {
        door.enabled = false;
        GameManager.Instance.levels[GameManager.Instance.currentLevelIndex].RemoveTarget(this);
    }

    void ShiftShapes(Shape shape, float duration = 0.2f)
    {
        correctAnswers.Add(shape);

        for (int i = 0; i < correctAnswers.Count; i++)
        {

            if (i == 0)
            {
                correctAnswers[i].transform.DOScale(Vector2.zero, 0.2f);
                
            }
            else if (i == correctAnswers.Count - 1)
            {
                correctAnswers[i].transform.DOMove(correctAnswers[i - 1].transform.position, duration).OnComplete(() =>
                {
                    correctAnswers[correctAnswers.Count - 1].boat.gameObject.SetActive(false);
                    correctAnswers[correctAnswers.Count - 1].ball.transform.DOLocalMove(Vector2.zero, duration);
                    correctAnswers[correctAnswers.Count - 1].ball.transform.DOScale(Vector2.one, duration);
                    correctAnswerCount++;

                    if (correctAnswerCount == totalShapes)
                    {  
                        CloseDoor();
                    }
                });
            }
            else
            {
                correctAnswers[i].transform.DOMove(correctAnswers[i - 1].transform.position, 0.2f);
            }
        }
        if (correctAnswers.Count > displayCount)
        {
            correctAnswers.RemoveAt(0);
        }

    }
}
