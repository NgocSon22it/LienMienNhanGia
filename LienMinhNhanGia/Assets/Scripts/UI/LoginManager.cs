using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoginManager : MonoBehaviour
{
    [SerializeField] TMP_InputField Username;
    [SerializeField] TMP_InputField Password;
    [SerializeField] TextMeshProUGUI LoginMessage;
    [SerializeField] TextMeshProUGUI UsernameMessage;
    [SerializeField] TextMeshProUGUI PasswordMessage;

    [SerializeField] GameObject FormMenuUI;
    [SerializeField] GameObject AllSettingMenuUI;
    public void Login()
    {
        if (Username.text.Length == 0)
        {
            UsernameMessage.text = "Bạn cần điền tài khoản!";
        }
        else
        {
            UsernameMessage.text = "";
        }
        if (Password.text.Length == 0)
        {
            PasswordMessage.text = "Bạn cần điền mật khẩu!";
        }
        else
        {
            PasswordMessage.text = "";
        }

        var CheckLogin = new AccountDAO().CheckLogin(Username.text, Password.text);

        if (CheckLogin != null)
        {
            AccountManager.AccountID = CheckLogin.AccountID;
            AccountManager.Account = CheckLogin;
            LoginMessage.text = "";
            FormMenuUI.SetActive(false);
            AllSettingMenuUI.SetActive(true);
            Password.text = "";
            Username.text = "";
            LoginMessage.text = "";
            PasswordMessage.text = "";
            MainMenuUI.Instance.SetUpPlayerInformation();
        }
        else
        {
            LoginMessage.text = "Tài khoản và Mật khẩu không đúng";
        }

        
    }
}


