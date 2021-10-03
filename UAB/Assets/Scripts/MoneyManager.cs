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

    IEnumerator EarnMoney(float waitingSeconds)
    {
        while (true)
        {
            yield return new WaitForSeconds(waitingSeconds);
            CurrentMoney += earnMoneyPerConnectedBuildingsInTime * cityNetChecker.TotalConnectedBuildings;
        }
    }
}
