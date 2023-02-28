using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Level : MonoBehaviour
{

    [SerializeField]
    public Direction startDirection;

    [SerializeField]
    List<Target> patternList = new List<Target>();

    [SerializeField]
    Shape goldenBall;

    public int levelBest = 0;

    [SerializeField]
    public Transform startPos;

    [SerializeField]
    float interval = 3f;

    public float startTime;

    int correctAnsStreakCount = 0;

    [SerializeField]
    int goldenBallCount = 3;

    bool goldenBallTime = false;

    bool gameover;

    [SerializeField]
    public string ID;

    [SerializeField]
    public float shapeSpeed = 1;

    private void OnEnable()
    {
        GameManager.OnCorrectAnswer += OnCorrectAnswer;
         GameManager.OnWrongAnswer += OnWrongAnswer;
    }

    private void OnDisable()
    {
        GameManager.OnCorrectAnswer -= OnCorrectAnswer;
               GameManager.OnWrongAnswer -= OnWrongAnswer;

    }
    private void Start()
    {
        startTime = Time.time;


    }

    private void Update()
    {
        if (Time.time - startTime > interval && !gameover && !goldenBallTime && !GameManager.freeze)
        {
            if (correctAnsStreakCount >= goldenBallCount && patternList.Count > 1 )
            {
                correctAnsStreakCount = 0;
                Instantiate(goldenBall, startPos.position, Quaternion.identity);
                startTime = Time.time;
                goldenBallTime = true;

            }

            else
            { 
                int patternNo = Random.Range(0, patternList.Count);
                if (patternList[patternNo].pattern.Count > 0)
                {
                    EnableShape(patternNo);
                    startTime = Time.time;
                }   
            }

        }


    }

    public void RemoveTarget(Target pattern)
    {
      
        patternList.Remove(pattern);
        if (patternList.Count == 0)
        {
            gameover = true;
            if (PlayerPrefs.GetFloat(ID) < levelBest)
            {
                PlayerPrefs.SetFloat(ID, levelBest);
            }
            Invoke("EnableNextStage", 1f);
            
        }
    }

    void EnableNextStage()
    {
        GameManager.Instance.EnableNextLevel();
    }
    void EnableShape(int index)
    {
        patternList[index].pattern[0].gameObject.SetActive(true);
        patternList[index].pattern[0].gameObject.transform.position = startPos.position;
        patternList[index].pattern.RemoveAt(0);
    }

    void OnCorrectAnswer(Shape shape)
    {
        if (shape is GoldenBall)
        {
            goldenBallTime = false;


        }

        else
        {
            levelBest++;
            correctAnsStreakCount++;
            if (correctAnsStreakCount == goldenBallCount && !gameover)
            {  
              StartCoroutine(GlobalGratification());
            }

        }


    }
    void OnWrongAnswer(Shape shape,int id){
        correctAnsStreakCount = 0;
    }

    IEnumerator GlobalGratification()
    {
        AudioManager.Instance.PlayAudio(5);
        GameManager.Instance.gratificationText.DOColor(new Color(255, 255, 255, 1),.3f);
        yield return new WaitForSeconds(2f);
        GameManager.Instance.gratificationText.DOColor(new Color(255, 255, 255, 0), .3f);
    }

}


