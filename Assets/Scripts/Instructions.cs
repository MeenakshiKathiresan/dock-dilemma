using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Instructions : MonoBehaviour {

    // Use this for initialization

[SerializeField]
Button back;

    float total = 18;
    private void OnEnable()
    {
        back.onClick.AddListener(EnableMainMenu);
    }

    void OnDisable(){
		back.onClick.RemoveAllListeners();
    }
	
    void EnableMainMenu(){
        MenuManager.Instance.mainMenu.gameObject.SetActive(true);
        gameObject.SetActive(false);
		   AudioManager.Instance.PlayAudio(8);
    }


}
