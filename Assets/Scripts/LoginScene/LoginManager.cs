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
                Debug.Log("로그인 성공!");
            }
            else
            {
                OpenPopup("ID 혹은 비밀번호가 일치하지 않습니다.");
            }
        }
        else
        {
            OpenPopup("해당 회원 정보가 존재하지 않습니다.");
        }
    }

    private bool CheckUserID(string userid)
    {
        foreach (var item in _saveData.saveUserData.user)
        {
            if (item.id == userid)
            {
                Debug.Log("ID가 존재합니다.");
                return true;
            }
        }
        Debug.Log("ID가 존재하지 않습니다.");
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
                    Debug.Log("비밀번호가 일치합니다!");
                    return true;
                }
            }
        }
        Debug.Log("비밀번호가 일치하지 않습니다.");
        return false;
    }

    private void CheckRegister()
    {
        if (!CheckUserID(registerID.text)) // ID가 데이터에 존재하는 지 확인
        {
            if (registerID.text.Length > 2 && registerID.text.Length < 11) // ID 글자수 확인
            {
                if (registerName.text.Length > 1 && registerName.text.Length < 6) // Name 글자수 확인
                {
                    if (registerPassword.text == registerConfirm.text) // 비밀번호 일치 확인
                    {
                        AddUserData();
                    }
                    else
                    {
                        OpenPopup("비밀번호가 서로 일치하지 않습니다.");
                    }
                }
                else
                {
                    OpenPopup("Name은 2글자 이상 5글자 이하여야 합니다.");
                }
            }
            else
            {
                OpenPopup("ID는 영문, 숫자 3글자 이상 10글자 이하여야 합니다.");
            }
        }
        else
        {
            OpenPopup("ID가 이미 존재합니다. 다른 ID로 가입을 시도하세요.");
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
        OpenPopup("성공적으로 회원가입을 완료했습니다.");
        SaveUserData();
    }

    private void SaveUserData()
    {
        _saveData.SaveUserData();
    }

}
