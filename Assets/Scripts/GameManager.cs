using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField]
    public float unitWidth = 2;

    [SerializeField]
    public List<Level> levels = new List<Level>();

    [HideInInspector]
    public Level currentLevel;

    [HideInInspector]
    public int currentLevelIndex;  

    public static bool freeze;

    public static GameManager Instance;

    [SerializeField]
    Text scoreText;

    [SerializeField]
    public TextMeshProUGUI gratificationText;

    [SerializeField]
    public string[] gratifications;


    [SerializeField]
    GameObject canvas;


    int score;

    public void StartGame()
    {
        score = 0;     
        currentLevel = levels[0];
        currentLevelIndex = 0;
        levels[0].gameObject.SetActive(true);
        canvas.SetActive(true);
        AudioManager.Instance.PlayAudio(1);

        if (OnGameStart != null)
        {
            OnGameStart();
        }

      
    }

    private void Start()
    {

        if(!PlayerPrefs.HasKey(levels[0].ID))
        {
            for(int i = 1; i < levels.Count;i++)
            {
                PlayerPrefs.SetFloat(levels[i].ID, levels[i].levelBest);
            }
        }
        //BGM
        AudioManager.Instance.PlayAudio(0);
    }

    public void EnableNextLevel()
    {
        if(currentLevelIndex < levels.Count - 1)
        {
            for(int i = 0 ;i<levels.Count;i++){
                  levels[i].transform.DOMoveY(levels[i].transform.position.y + 10, 2);
            }
            
            currentLevel.transform.DOScale(new Vector3(0.5f,0.5f,0.5f),2).OnComplete(()=> {currentLevel.gameObject.SetActive(false);
                currentLevelIndex++;
                currentLevel = levels[currentLevelIndex];          
            });
            levels[currentLevelIndex + 1].gameObject.SetActive(true);           
    
            
            
        }else{
            MenuManager.Instance.gameOverWindow.SetActive(true);
        }
        
    }
    private void OnEnable()
    {
        GameManager.OnCorrectAnswer += IncreaseScore;
        
    }

    private void OnDisable()
    {
        GameManager.OnCorrectAnswer -= IncreaseScore;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void IncreaseScore(Shape shape)
    {
        if (shape is GoldenBall)
        {

            score += 20;

        }

        else
        {
            score += 10;
        }

        scoreText.text = "SCORE : " + score;
    }


    public void SetFreeze(bool isFreeze)
    {
        freeze = isFreeze;
    }


    public delegate void CorrectAnswerHandler(Shape shape);
    public delegate void WrongAnswerHandler(Shape shape, int wrongTargetID);
    public delegate void GameStartHandler();
    public static CorrectAnswerHandler OnCorrectAnswer;
    public static GameStartHandler OnGameStart;
    public static WrongAnswerHandler OnWrongAnswer;

}
