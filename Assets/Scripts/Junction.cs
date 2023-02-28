using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Junction : MonoBehaviour
{

    [SerializeField]
    Diverter diverter;

    [SerializeField]
    Transform knob;

    [SerializeField]
    SpriteRenderer knobSprite;

    [SerializeField]
    int rotationDirection = 1;

    bool canSwitch = true;

    private void OnMouseDown()
    {
        if (canSwitch)
        {
      
             //DOTween.KillAll(knob);

            if (knob.localEulerAngles.z == 0)
            {
                knob.DOLocalRotate(new Vector3(0, 0, rotationDirection * 90), 0.2f).SetEase(Ease.OutBack);
                      diverter.ChangeState();
            }
            else if(knob.localEulerAngles.z == 90 || knob.localEulerAngles.z == -90)
            {
                knob.DOLocalRotate(new Vector3(0, 0, 0), 0.2f).SetEase(Ease.OutBack);
                      diverter.ChangeState();
            }
            AudioManager.Instance.PlayAudio(4);
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Shape>())
        {
            knobSprite.DOColor(new Color(255, 255, 255, 0.75f), 0.2f);
            canSwitch = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<Shape>())
        {
         
        knobSprite.DOColor(new Color(255, 255, 255, 1f), 0.2f);
        canSwitch = true;
        }
    }

}
