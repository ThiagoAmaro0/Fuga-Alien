using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    
    // Start is called before the first frame update
    void Start()
    {
        EnergyManager.PauseAction.Invoke();
        StartCoroutine(ShowNumber(3));        
    }

    private IEnumerator ShowNumber(int num)
    {
        if (num == 0)
        {
            EnergyManager.ContinueAction.Invoke();
            gameObject.SetActive(false);

            yield return null;
        }
        yield return new WaitForSeconds(0.1f);
        text.text = num.ToString();
        text.color = new Color(1,1,1,1);
        text.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        Tween startTween = text.transform.DOScale(1,0.5f);
        startTween.Play();

        Tween endTween = text.DOFade(0,0.5f);
        endTween.Pause();
        startTween.OnComplete(() => endTween.Play());
        endTween.OnComplete(() => StartCoroutine(ShowNumber(num-1)));
        

    }
}
