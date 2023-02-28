using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour {

    [SerializeField]
    List<Image> sliders = new List<Image>();
    // Use this for initialization

[SerializeField]
Button back;

    float total = 18;
    private void OnEnable()
    {
        for(int i = 1; i < sliders.Count; i++)
        {          
            sliders[i].fillAmount =  PlayerPrefs.GetFloat(GameManager.Instance.levels[i].ID) / total;
        }
        back.onClick.AddListener(EnableMainMenu);
    }

    void OnDisable(){
back.onClick.RemoveAllListeners();
    }
    void Start () {
		
	}
	
    void EnableMainMenu(){
        MenuManager.Instance.mainMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
           AudioManager.Instance.PlayAudio(8);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
