using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class EndAnimation : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private DistanceHandler distanceHandler;
    private CanvasGroup canvasGroup;
    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    private void OnEnable()
    {
        EnergyManager.GameOverAction += EndAction;
    }
    private void OnDisable()
    {
        EnergyManager.GameOverAction -= EndAction;
    }

    public void EndAction()
    {
        if (distanceHandler.GetDistance() >= PlayerPrefs.GetInt("highScore"))
            title.text = "parabéns! você quebrou o seu recorde!\nPercorreu " + distanceHandler.GetDistance() + "M";
        else 
            title.text = "Distância percorrida: " + distanceHandler.GetDistance() + "M\n" +
                "Recorde pessoal: "+ PlayerPrefs.GetInt("highScore")+"M";
        transform.GetChild(0).gameObject.SetActive(true);
        canvasGroup.blocksRaycasts = true;
        StartCoroutine(StartAnim());
    }
    private IEnumerator StartAnim()
    {
        yield return new WaitForSeconds(1);
        canvasGroup.DOFade(1, 1);
    }
}
