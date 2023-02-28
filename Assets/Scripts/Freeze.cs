using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Freeze : MonoBehaviour
{

    bool canFreeze = true;

    [SerializeField]
    float freezeDuration = 2;

    [SerializeField]
    float freezeCoolDown = 2;

    [SerializeField]
    int freezeThreshold = 2;

    [SerializeField]
    SpriteRenderer freezeSprite;

    int currentFreezeCount = 0;
    private void OnMouseDown()
    {
        if (currentFreezeCount < freezeThreshold)
        {
            // if (canFreeze)
            {
                DisableButton(0.5f);
                currentFreezeCount++;
                GameManager.Instance.SetFreeze(true);
                canFreeze = false;
                StartCoroutine(DeFreeze());

            }
            AudioManager.Instance.PlayAudio(6);
        }
    }

    IEnumerator DeFreeze()
    {
        yield return new WaitForSeconds(freezeDuration);
        GameManager.Instance.SetFreeze(false);
        GameManager.Instance.currentLevel.startTime += freezeDuration;
        //  yield return new WaitForSeconds(freezeCoolDown);        
        //  canFreeze = true;
        if (currentFreezeCount != freezeThreshold)
        {
            EnableButton();
        }
        else
        {
            DisableButton(0f);
        }
    }
    void DisableButton(float alpha)
    {
        freezeSprite.DOColor(new Color(255, 255, 255, alpha), 0.2f);

    }

    void EnableButton()
    {
        freezeSprite.DOColor(new Color(255, 255, 255, 1f), 0.2f);
    }

    public void ResetButton()
    {
        EnableButton();
        currentFreezeCount = 0;
    }

}
