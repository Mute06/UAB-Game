using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyManager : MonoBehaviour
{
    public float startingMoney = 100f;
    public float moneyPerMeter = 5f;
    public float earnMoneyPerConnectedBuildingsInTime = 5f;
    public float earnMoneyPerSeconds;
    public float HubCost = 25f;
    public TextMeshProUGUI moneyText;

    [SerializeField] private GameObject notEnoughMoneyPanel;
    private float totalCableLenght;
    private float currentMoney;
    private float totalSpendMoney;
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

    public bool BuildLine(float length)
    {
        float totalCost = moneyPerMeter * length;
        if (CurrentMoney - totalCost >= 0f)
        {
            CurrentMoney -= totalCost;
            totalCableLenght += length;
            Debug.Log(totalCost);
            return true;
        }
        else
        {
            notEnoughMoneyPanel.SetActive(true);
            return false;
        }
    }

    public bool BuildHub()
    {
        if (CurrentMoney - HubCost >= 0f)
        {
            CurrentMoney -= HubCost;
            return true;
        }
        else
        {
            notEnoughMoneyPanel.SetActive(true);
            return false;
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
