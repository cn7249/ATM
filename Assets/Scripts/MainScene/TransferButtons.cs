using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransferButtons : MonoBehaviour
{
    [Header("Transfer Panel")]
    [SerializeField] private Button goBackBtn;
    [SerializeField] private Button transferBtn;
    [SerializeField] private TMP_InputField destination;
    [SerializeField] private TMP_InputField amount;

    private void Start()
    {
        goBackBtn.onClick.AddListener(() => GoMenu());
        transferBtn.onClick.AddListener(() => Transfer());
    }

    private void GoMenu()
    {
        GameManager.instance.ShowMenu();
    }

    private void Transfer()
    {
        if (destination.text == "" || amount.text == "")
        {
            GameManager.instance.ShowPopup("�Է� ������ Ȯ�����ּ���.");
        }
        else
        {
            GameManager.instance.Transfer(GetDestination(), GetAmount());
        }
    }

    private string GetDestination()
    {
        return destination.text;
    }

    private int GetAmount()
    {
        bool isExpected = int.TryParse(amount.text, out int num);

        if (isExpected)
        {
            return num;
        }
        else
        {
            GameManager.instance.ShowPopup("���� ���ڸ� �Է����ּ���.");
            return 0;
        }
    }
}
