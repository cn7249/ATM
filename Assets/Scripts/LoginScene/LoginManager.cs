using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    [Header("Login UI")]
    [SerializeField] private TMP_InputField idInput;
    [SerializeField] private TMP_InputField passwordInput;
    [SerializeField] private Button loginButton;
    [SerializeField] private Button registerButton;

    [Header("Register UI")]
    [SerializeField] private GameObject registerPanel;
    [SerializeField] private TMP_InputField registerID;
    [SerializeField] private TMP_InputField registerName;
    [SerializeField] private TMP_InputField registerPassword;
    [SerializeField] private TMP_InputField registerConfirm;
    [SerializeField] private Button registerConfirmBtn;
    [SerializeField] private Button closeButton;

    [Header("User Data")]
    [SerializeField] private GameObject saveDataObject;

    [Header("Popup Panel")]
    [SerializeField] private GameObject popupPanel;
    [SerializeField] private TextMeshProUGUI popupText;
    [SerializeField] private Button popupCloseBtn;

    private SaveData _saveData;

    private void Awake()
    {
        _saveData = saveDataObject.GetComponent<SaveData>();
    }

    void Start()
    {
        loginButton.onClick.AddListener(() => Login());
        registerButton.onClick.AddListener(() => OpenRegister());
        closeButton.onClick.AddListener(() => CloseRegister());
        registerConfirmBtn.onClick.AddListener(() => CheckRegister());
        popupCloseBtn.onClick.AddListener(()=> ClosePopup());
    }

    private void OpenRegister()
    {
        registerPanel.SetActive(true);
    }

    private void CloseRegister()
    {
        registerPanel.SetActive(false);
    }

    private void OpenPopup(string text)
    {
        popupText.text = text;
        popupPanel.SetActive(true);
    }

    private void ClosePopup()
    {
        popupPanel.SetActive(false);
    }

    private void Login()
    {
        if (CheckUserID(idInput.text))
        {
            if (CheckUserPassword(idInput.text, passwordInput.text))
            {
                SceneManager.LoadScene("MainScene");
                Debug.Log("�α��� ����!");
            }
            else
            {
                OpenPopup("ID Ȥ�� ��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
            }
        }
        else
        {
            OpenPopup("�ش� ȸ�� ������ �������� �ʽ��ϴ�.");
        }
    }

    private bool CheckUserID(string userid)
    {
        foreach (var item in _saveData.saveUserData.user)
        {
            if (item.id == userid)
            {
                Debug.Log("ID�� �����մϴ�.");
                return true;
            }
        }
        Debug.Log("ID�� �������� �ʽ��ϴ�.");
        return false;
    }

    private bool CheckUserPassword(string userid, string input)
    {
        foreach (var item in _saveData.saveUserData.user)
        {
            if (item.id == userid)
            {
                if (item.password == input)
                {
                    Debug.Log("��й�ȣ�� ��ġ�մϴ�!");
                    return true;
                }
            }
        }
        Debug.Log("��й�ȣ�� ��ġ���� �ʽ��ϴ�.");
        return false;
    }

    private void CheckRegister()
    {
        if (!CheckUserID(registerID.text)) // ID�� �����Ϳ� �����ϴ� �� Ȯ��
        {
            if (registerID.text.Length > 2 && registerID.text.Length < 11) // ID ���ڼ� Ȯ��
            {
                if (registerName.text.Length > 1 && registerName.text.Length < 6) // Name ���ڼ� Ȯ��
                {
                    if (registerPassword.text == registerConfirm.text) // ��й�ȣ ��ġ Ȯ��
                    {
                        AddUserData();
                    }
                    else
                    {
                        OpenPopup("��й�ȣ�� ���� ��ġ���� �ʽ��ϴ�.");
                    }
                }
                else
                {
                    OpenPopup("Name�� 2���� �̻� 5���� ���Ͽ��� �մϴ�.");
                }
            }
            else
            {
                OpenPopup("ID�� ����, ���� 3���� �̻� 10���� ���Ͽ��� �մϴ�.");
            }
        }
        else
        {
            OpenPopup("ID�� �̹� �����մϴ�. �ٸ� ID�� ������ �õ��ϼ���.");
        }
    }

    private void AddUserData()
    {
        var data = new UserData();
        data.id = registerID.text;
        data.name = registerName.text;
        data.password = registerPassword.text;
        data.balance = 50000;

        _saveData.saveUserData.user.Add(data);
        OpenPopup("���������� ȸ�������� �Ϸ��߽��ϴ�.");
        SaveUserData();
    }

    private void SaveUserData()
    {
        _saveData.SaveUserData();
    }

}
