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

    void Start()
    {
        loginButton.onClick.AddListener(() => Login());
        registerButton.onClick.AddListener(() => OpenRegister());
    }

    private void Login()
    {
        if (idInput.text == "admin" && passwordInput.text == "admin")
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    private void OpenRegister()
    {
        registerPanel.SetActive(true);
    }

    private void CloseRegister()
    {
        registerPanel.SetActive(false);
    }
}
