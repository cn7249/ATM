using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DepositButtons : MonoBehaviour
{
    [Header("Deposit Panel")]
    [SerializeField] private Button deposit10k;
    [SerializeField] private Button deposit30k;
    [SerializeField] private Button deposit50k;
    [SerializeField] private Button goMenuBtn;

    [Header("Input Field")]
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button depositBtn;

    void Start()
    {
        deposit10k.onClick.AddListener(() => Deposit(10000));
        deposit30k.onClick.AddListener(() => Deposit(30000));
        deposit50k.onClick.AddListener(() => Deposit(50000));
        depositBtn.onClick.AddListener(() => CheckInputField());
        goMenuBtn.onClick.AddListener(() => GoMenu());
    }

    void Deposit(int amount)
    {
        GameManager.instance.Deposit(amount);
    }

    void CheckInputField()
    {
        bool isExpected = int.TryParse(inputField.text, out int amount);

        if (isExpected)
        {
            Deposit(amount);
        }
        else
        {
            GameManager.instance.ShowPopup("허용된 숫자를 입력해주세요.");
        }
    }

    void GoMenu()
    {
        GameManager.instance.ShowMenu();
    }
}
