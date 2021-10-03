using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public float startingMoney = 100f;
    public float moneyPerMeter = 5f;
    public float earnMoneyPerConnectedBuildingsInTime = 5f;
    public float earnMoneyPerSeconds;
    public float HubCost = 25f;
    public TextMeshProUGUI moneyText;
    private float totalCableLenght;
    private float currentMoney;
    public float CurrentMoney
    {
        get
        {
            return currentMoney;
        }
        set
        {
            currentMoney = value;
            moneyText.text = Mathf.CeilToInt(currentMoney).ToString();
        }
    }
    private CityNetChecker cityNetChecker;
    private void Start()
    {
        cityNetChecker = FindObjectOfType<CityNetChecker>();
        StartCoroutine(EarnMoney(earnMoneyPerSeconds));
        currentMoney = startingMoney;
    }

    public void BuildLine(float length)
    {
        float totalCost = moneyPerMeter * length;
        if (CurrentMoney - totalCost >= 0f)
        {
            CurrentMoney -= totalCost;
            totalCableLenght += length;
        }
    }

    public void BuildHub()
    {
        if (CurrentMoney - HubCost >= 0f)
        {
            CurrentMoney -= HubCost;
        }
    }

    IEnumerator EarnMoney(float waitingSeconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitingSeconds);
            CurrentMoney += earnMoneyPerConnectedBuildingsInTime * cityNetChecker.TotalConnectedBuildings;
        }
    }
}
