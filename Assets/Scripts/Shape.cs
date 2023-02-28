using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Shape : MonoBehaviour
{

    Direction currentDirection;

    [SerializeField]
    public int targetID;

    Vector2 directionVector = Vector2.down;

    [SerializeField]
    public bool reachedTarget;

    [SerializeField]
    public SpriteRenderer boat;

    public GameObject ball;

    // Use this for initialization
    public virtual void OnEnable()
    {
        if (!reachedTarget)
        {
            currentDirection = GameManager.Instance.currentLevel.startDirection;
            boat.sprite = Utils.Instance.GetSprite(currentDirection);
        }
    }

    // Update is called once per frame
    void Update()
    {


        directionVector = Utils.GetDirection(currentDirection);

        if (!reachedTarget && !GameManager.freeze)
        {
            transform.Translate(directionVector * GameManager.Instance.currentLevel.shapeSpeed * Time.deltaTime);
        }
    }


    public void JumpToHome(Vector2 targetPos)
    {
        GetComponentInChildren<TextMeshPro>().DOColor(Color.red, 0.3f);
        transform.DOScale(Vector3.zero, 0.2f).OnComplete(() =>
        {
            transform.position = targetPos;
            boat.enabled = false;
            transform.DOScale(new Vector2(.85F, .85F), 0.2f);
        }
        );

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Diverter>())
        {
            currentDirection = collision.gameObject.GetComponent<Diverter>().currentState;
            boat.sprite = Utils.Instance.GetSprite(currentDirection);

        }

        if (!reachedTarget)
        {
            if (collision.gameObject.GetComponent<Target>())
            {
                Target target = collision.gameObject.GetComponent<Target>();
                OnHittingTarget(target);
            }

        }
    }



    public virtual void OnHittingTarget(Target target)
    {

        GetComponent<Collider2D>().enabled = false;

        reachedTarget = true;
        if (targetID == target.id)
        {
            if (GameManager.OnCorrectAnswer != null)
                GameManager.OnCorrectAnswer(this);
        }
        else
        {
            if (GameManager.OnWrongAnswer != null)
                GameManager.OnWrongAnswer(this, target.id);
        }
    }

}
