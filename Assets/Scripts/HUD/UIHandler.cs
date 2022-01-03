using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Image energyImage;
    [SerializeField] private Image regenImage;
    [SerializeField] private TextMeshProUGUI distanceText;

    [SerializeField] private EnergyManager energyManager;
    [SerializeField] private DistanceHandler distanceHandler;

    private bool tweening;
    private float energyFill;
    // Start is called before the first frame update
    private void OnEnable()
    {
        EnergyManager.EnergyUpAction += EnergyUp;
    }
    private void OnDisable()
    {
        EnergyManager.EnergyUpAction -= EnergyUp;
    }
    private void FixedUpdate()
    {
        UpdateEnergy();
        distanceText.text = distanceHandler.GetDistance() + " M";
    }

    private void UpdateEnergy()
    {
        regenImage.fillAmount = energyManager.GetEnergy() / 100;
        if (!tweening)
        {
            energyImage.fillAmount = regenImage.fillAmount;
        }
        else
        {
            energyImage.fillAmount = energyFill;
        }
        regenImage.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 360 * regenImage.fillAmount);
        energyImage.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 360 * energyImage.fillAmount);
    }

    private void EnergyUp(float value)
    {
        tweening = true;
        energyFill = energyImage.fillAmount;

        float fillTarget = regenImage.fillAmount + (value / 100) - energyManager.GetEnergyCost()/100;
        regenImage.fillAmount = fillTarget;

        Tween t = DOTween.To(() => energyFill, x => energyFill = x,  fillTarget, 1);
        t.OnComplete(() => tweening = false);
        t.Play();

    }
}
