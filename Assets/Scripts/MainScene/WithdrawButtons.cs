using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WithdrawButtons : MonoBehaviour
{
    [Header("Withdraw Panel")]
    [SerializeField] private Button withdraw10k;
    [SerializeField] private Button withdraw30k;
    [SerializeField] private Button withdraw50k;
    [SerializeField] private Button goMenuBtn;

    [Header("Input Field")]
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button withdrawBtn;

    void Start()
    {
        withdraw10k.onClick.AddListener(() => Withdraw(10000));
        withdraw30k.onClick.AddListener(() => Withdraw(30000));
        withdraw50k.onClick.AddListener(() => Withdraw(50000));
        withdrawBtn.onClick.AddListener(() => CheckInputField());
        goMenuBtn.onClick.AddListener(() => GoMenu());
    }

    void Withdraw(int amount)
    {
        GameManager.instance.Withdraw(amount);
    }

    void CheckInputField()
    {
        bool isExpected = int.TryParse(inputField.text, out int amount);

        if (isExpected)
        {
            Withdraw(amount);
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
