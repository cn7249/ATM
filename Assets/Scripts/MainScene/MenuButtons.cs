using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtons : MonoBehaviour
{
    [SerializeField] private Button depositBtn;
    [SerializeField] private Button withdrawBtn;

    void Start()
    {
        depositBtn.onClick.AddListener(() => Deposit());
        withdrawBtn.onClick.AddListener(() => Withdraw());
    }

    void Deposit()
    {
        GameManager.instance.ShowDeposit();
    }

    void Withdraw()
    {
        GameManager.instance.ShowWithdraw();
    }
}
