using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private Button depositBtn;
    [SerializeField] private Button withdrawBtn;
    [SerializeField] private Button transferBtn;

    void Start()
    {
        depositBtn.onClick.AddListener(() => Deposit());
        withdrawBtn.onClick.AddListener(() => Withdraw());
        transferBtn.onClick.AddListener(() => Transfer());
    }

    void Deposit()
    {
        GameManager.instance.ShowDeposit();
    }

    void Withdraw()
    {
        GameManager.instance.ShowWithdraw();
    }

    void Transfer()
    {
        GameManager.instance.ShowTransfer();
    }
}
