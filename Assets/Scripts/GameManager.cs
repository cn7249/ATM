using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header ("Text UI")]
    [SerializeField] private TextMeshProUGUI balanceTxt;
    [SerializeField] private TextMeshProUGUI cashTxt;

    [Header ("Menu UI")]
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject depositPanel;
    [SerializeField] private GameObject withdrawPanel;
    [SerializeField] private GameObject popupPanel;

    [Header ("Popup UI")]
    [SerializeField] private Button popupBtn;
    [SerializeField] private TextMeshProUGUI popupTxt;

    [Header ("Set Money")]
    [SerializeField] private int balance;
    [SerializeField] private int cash;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        UpdateMoney();
        ShowMenu();
        popupBtn.onClick.AddListener(() => ClosePopup());
    }

    void Update()
    {
        
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
        popupPanel.SetActive(false);
    }

    public void ShowDeposit()
    {
        menuPanel.SetActive(false);
        depositPanel.SetActive(true);
        withdrawPanel.SetActive(false);
        popupPanel.SetActive(false);
    }

    public void ShowWithdraw()
    {
        menuPanel.SetActive(false);
        depositPanel.SetActive(false);
        withdrawPanel.SetActive(true);
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
            ShowPopup("�ܾ��� �����մϴ�.");
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
            ShowPopup("�ܾ��� �����մϴ�.");
        }
    }
}