using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

[SerializeField]
Button play;

    [SerializeField]
    Button stats;

    [SerializeField]
    GameObject statsWindow;

        [SerializeField]
    GameObject instructionsWindow;

        [SerializeField]
   public GameObject gameOverWindow;

    [SerializeField]
    Button instruction;

    [SerializeField]
    public GameObject mainMenu;

    void OnEnable(){
play.onClick.AddListener(Play);
stats.onClick.AddListener(ShowStats);
instruction.onClick.AddListener(ShowInstructions);
    }
void OnDisable(){
play.onClick.RemoveAllListeners();
stats.onClick.RemoveAllListeners();
instruction.onClick.RemoveAllListeners();
    }
public static MenuManager Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

   void Play(){

        AudioManager.Instance.PlayAudio(8);
        GameManager.Instance.StartGame();
        mainMenu.SetActive(false);	
    }

    void ShowStats()
    {
        AudioManager.Instance.PlayAudio(8);
        mainMenu.SetActive(false);
        statsWindow.SetActive(true);
    }

    void ShowInstructions(){
        AudioManager.Instance.PlayAudio(8);
        mainMenu.SetActive(false);
        instructionsWindow.SetActive(true);
    }

}
