using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header ("Text UI")]
    [SerializeField] private TextMeshProUGUI balanceTxt;
    [SerializeField] private TextMeshProUGUI nameTxt;
    [SerializeField] private TextMeshProUGUI cashTxt;

    [Header ("Menu UI")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject depositPanel;
    [SerializeField] private GameObject withdrawPanel;
    [SerializeField] private GameObject transferPanel;
    [SerializeField] private GameObject popupPanel;

    [Header ("Popup UI")]
    [SerializeField] private Button popupBtn;
    [SerializeField] private TextMeshProUGUI popupTxt;

    [Header ("Set Money")]
    [SerializeField] private int balance;
    [SerializeField] private int cash;

    private SaveData _saveData;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            _saveData = GameObject.Find("SaveData").GetComponent<SaveData>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadData();
        UpdateMoney();
        ShowMenu();
        popupBtn.onClick.AddListener(() => ClosePopup());
    }

    void OnApplicationQuit()
    {
        SetStateLogout();
        _saveData.SaveUserData();
    }

    private void SetStateLogout()
    {
        foreach (var item in _saveData.saveUserData.user)
        {
            if (item.isLogined)
            {
                item.isLogined = false;
                item.balance = balance;
                item.cash = cash;
            }
        }
    }

    private void LoadData()
    {
        foreach (var item in _saveData.saveUserData.user)
        {
            if (item.isLogined)
            {
                balance = item.balance;
                cash = item.cash;
                nameTxt.text = "Welcome, " + item.name;
            }
        }
    }

    private void UpdateMoney()
    {
        balanceTxt.text = balance.ToString("N0");
        cashTxt.text = cash.ToString("N0");
    }

    public void ShowMenu()
    {
        menuPanel.SetActive(true);
        depositPanel.SetActive(false);
        withdrawPanel.SetActive(false);
        transferPanel.SetActive(false);
        popupPanel.SetActive(false);
    }

    public void ShowDeposit()
    {
        menuPanel.SetActive(false);
        depositPanel.SetActive(true);
        withdrawPanel.SetActive(false);
        transferPanel.SetActive(false);
        popupPanel.SetActive(false);
    }

    public void ShowWithdraw()
    {
        menuPanel.SetActive(false);
        depositPanel.SetActive(false);
        withdrawPanel.SetActive(true);
        transferPanel.SetActive(false);
        popupPanel.SetActive(false);
    }

    public void ShowTransfer()
    {
        menuPanel.SetActive(false);
        depositPanel.SetActive(false);
        withdrawPanel.SetActive(false);
        transferPanel.SetActive(true);
        popupPanel.SetActive(false);
    }

    public void ShowPopup(string input)
    {
        popupTxt.text = input;
        popupPanel.SetActive(true);
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
    }

    public void Deposit(int amount)
    {
        if (cash >= amount)
        {
            cash -= amount;
            balance += amount;
            UpdateMoney();
        }
        else
        {
            ShowPopup("잔액이 부족합니다.");
        }
    }

    public void Withdraw(int amount)
    {
        if (balance >= amount)
        {
            balance -= amount;
            cash += amount;
            UpdateMoney();
        }
        else
        {
            ShowPopup("잔액이 부족합니다.");
        }
    }

    public void Transfer(string destination, int amount)
    {
        foreach (var item in _saveData.saveUserData.user)
        {
            if (item.name == destination)
            {
                if (balance >= amount)
                {
                    item.balance += amount;
                    balance -= amount;
                    ShowPopup($"{destination}님께 {amount}원 안전하게 송금 완료!");
                    break;
                }
                else
                {
                    ShowPopup("잔액이 부족합니다.");
                    break;
                }
            }
            ShowPopup("송금 대상을 찾을 수 없습니다!");
        }
        UpdateMoney();
    }
}
