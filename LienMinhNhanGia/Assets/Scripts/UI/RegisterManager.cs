using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RegisterManager : MonoBehaviour
{
    [SerializeField] TMP_InputField Fullname;
    [SerializeField] TMP_InputField Username;
    [SerializeField] TMP_InputField Password;
    [SerializeField] TMP_InputField ConfirmPassword;

    [SerializeField] TextMeshProUGUI UsernameMessage;
    [SerializeField] TextMeshProUGUI FullnameMessage;
    [SerializeField] TextMeshProUGUI PasswordMessage;
    [SerializeField] TextMeshProUGUI ConfirmPasswordMessage;
    [SerializeField] TextMeshProUGUI RegisterMessage;

    private bool CheckAllData;
    private bool CheckFullnameData;
    private bool CheckUsernameData;
    private bool CheckPasswordData;
    private bool CheckConfirmPasswordData;
    // Start is called before the first frame update
    void Start()
    {
        UsernameMessage.text = "";
        PasswordMessage.text = "";
        FullnameMessage.text = "";
        ConfirmPasswordMessage.text = "";
        RegisterMessage.text = "";
        CheckAllData = false;
    }

    public void CheckUsername()
    {
        var CheckUserNameExist = new AccountDAO().GetAccountByUsername(Username.text);
        if (CheckUserNameExist != null)
        {
            UsernameMessage.text = "Tài khoản tồn tại!";
            CheckUsernameData = false;
        }
        else
        {
            if (Username.text.Length < 8)
            {
                UsernameMessage.text = "Tài khoản cần 8 kí tự trở lên!";
                CheckUsernameData = false;
            }
            else
            {
                UsernameMessage.text = "";
                CheckUsernameData = true;
            }
        }

    }

    public void CheckPassword()
    {
        if (Password.text.Length < 8)
        {
            PasswordMessage.text = "Mật khẩu cần 8 kí tự trở lên!";
            CheckPasswordData = false;
        }
        else
        {
            PasswordMessage.text = "";
            CheckPasswordData = true;
        }
    }
    public void CheckFullname()
    {
        if (Fullname.text.Length == 0)
        {
            FullnameMessage.text = "Họ và tên không được để trống!";
            CheckFullnameData = false;
        }
        else
        {
            FullnameMessage.text = "";
            CheckFullnameData = true;
        }
    }
    public void CheckConfirmPassword()
    {
        if (!ConfirmPassword.text.Equals(Password.text))
        {
            ConfirmPasswordMessage.text = "Nhập lại mật khẩu không đúng!";
            CheckConfirmPasswordData = false;
        }
        else
        {
            ConfirmPasswordMessage.text = "";
            CheckConfirmPasswordData = true;
        }
    }
    public void Register()
    {
        CheckAllData = CheckFullnameData && CheckUsernameData && CheckPasswordData && CheckConfirmPasswordData;
        if (CheckAllData)
        {
            AccountEntity accountEntity = new AccountEntity();
            accountEntity.Name = Fullname.text;
            accountEntity.Username = Username.text;
            accountEntity.Password = Password.text;
            new AccountDAO().CreateAccount(accountEntity);
            Fullname.text = "";
            Username.text = "";
            Password.text = "";
            ConfirmPassword.text = "";
            UsernameMessage.text = "";
            PasswordMessage.text = "";
            FullnameMessage.text = "";
            ConfirmPasswordMessage.text = "";
            RegisterMessage.text = "Đăng kí thành công";
        }
        else
        {
            RegisterMessage.text = "Bạn cần điền đúng thông tin";
            CheckFullname();
            CheckPassword();
            CheckConfirmPassword();
            CheckUsername();
        }
    }
}
