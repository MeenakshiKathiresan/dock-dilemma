using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diverter : MonoBehaviour {
    
    [SerializeField]
    List<Direction> stateDirection = new List<Direction>();

    [HideInInspector]
    public Direction currentState;

    int stateIndex = 0;

    private void Start()
    {
        currentState = stateDirection[0];    
    }
    public void ChangeState()
    {
        if(currentState == stateDirection[0]){
            currentState = stateDirection[1];
        }else{
  currentState = stateDirection[0];
        }
    }
}
