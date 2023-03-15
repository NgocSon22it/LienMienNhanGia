using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;

public class AccountDAO
{

    string ConnectionStr = new LienMinhNhanGiaConnect().GetConnectLienMinhNhanGia();

    public AccountEntity GetAccountByID(int AccountID)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Account where Account_ID = " + AccountID;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    AccountEntity a = new AccountEntity
                    {
                        AccountID = Convert.ToInt32(dr["Account_ID"]),
                        Name = dr["Name"].ToString(),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString(),
                        Role = Convert.ToInt32(dr["Role"]),
                        Avatar = dr["Avatar"].ToString(),
                        Coin = Convert.ToInt32(dr["Coin"]),
                        Experience = Convert.ToInt32(dr["Experience"]),
                        Level = Convert.ToInt32(dr["Level"]),
                        CheckPoint = dr["Check_Point"].ToString(),
                        BossKill = Convert.ToInt32(dr["Boss_Kill"]),
                        AmountSlotSkill = Convert.ToInt32(dr["Amount_Slot_Skill"]),
                        PointSkill = Convert.ToInt32(dr["Point_Skill"]),
                        Delete = Convert.ToBoolean(dr["Delete"])

                    };
                    connection.Close();
                    return a;
                }
            }
            finally
            {
                connection.Close();
            }

        }

        return null;
    }

    public void UpdateAccountCheckPoint(int AccountID, string CheckPointID)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Update Account set Check_Point = @checkpointid where Account_ID = " + AccountID;
            cmd.Parameters.AddWithValue("@checkpointid", CheckPointID);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void UpdateAccountCoinNLevel(int AccountID, int CoinAmount, int ExperinenceAmount, int Level)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Update Account set Coin = Coin, Experience = Experience, Level = @level where Account_ID = " + AccountID;
            cmd.Parameters.AddWithValue("@coinamount", CoinAmount);
            cmd.Parameters.AddWithValue("@level", Level);
            cmd.Parameters.AddWithValue("@expamount", ExperinenceAmount);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public AccountEntity CheckLogin(string username, string password)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from Account where UserName = @username and PassWord = @password and [Delete] = 0";
                cmd.Parameters.AddWithValue("@username", username);
                cmd.Parameters.AddWithValue("@password", GetMD5(password));
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    AccountEntity account = new AccountEntity
                    {
                        AccountID = Convert.ToInt32(dr["Account_ID"]),
                        Name = dr["Name"].ToString(),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString(),
                        Role = Convert.ToInt32(dr["Role"]),
                        Avatar = dr["Avatar"].ToString(),
                        Coin = Convert.ToInt32(dr["Coin"]),
                        Experience = Convert.ToInt32(dr["Experience"]),
                        Level = Convert.ToInt32(dr["Level"]),
                        CheckPoint = dr["Check_Point"].ToString(),
                        BossKill = Convert.ToInt32(dr["Boss_Kill"]),
                        AmountSlotSkill = Convert.ToInt32(dr["Amount_Slot_Skill"]),
                        PointSkill = Convert.ToInt32(dr["Point_Skill"]),
                        Delete = Convert.ToBoolean(dr["Delete"])

                    };
                    connection.Close();

                    return account;
                }

            }
            finally
            {
                connection.Close();
            }
        }
        return null;
    }

    public void CreateAccount(AccountEntity account)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Insert into Account values(@name,@username,@password,0,'0',0,0,1,'map1_1',0,3,0,0)";
            cmd.Parameters.AddWithValue("@username", account.Username);
            cmd.Parameters.AddWithValue("@password", GetMD5(account.Password));
            cmd.Parameters.AddWithValue("@name", account.Name);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void SaveAccountData(AccountEntity account)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Update Account set Coin = @coin, Experience = @exp, [Level] = @level, Check_Point =@checkpoint where Account_ID = " + account.AccountID;
            cmd.Parameters.AddWithValue("@coin", account.Coin);
            cmd.Parameters.AddWithValue("@exp", account.Experience);
            cmd.Parameters.AddWithValue("@level", account.Level) ;
            cmd.Parameters.AddWithValue("@checkpoint", account.CheckPoint);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public AccountEntity GetAccountByUsername(string username)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();

                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "select * from Account where UserName = @username";
                cmd.Parameters.AddWithValue("@username", username);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    AccountEntity a = new AccountEntity
                    {
                        AccountID = Convert.ToInt32(dr["Account_ID"]),
                        Name = dr["Name"].ToString(),
                        Username = dr["Username"].ToString(),
                        Password = dr["Password"].ToString(),
                        Role = Convert.ToInt32(dr["Role"]),
                        Avatar = dr["Avatar"].ToString(),
                        Coin = Convert.ToInt32(dr["Coin"]),
                        Experience = Convert.ToInt32(dr["Experience"]),
                        Level = Convert.ToInt32(dr["Level"]),
                        CheckPoint = dr["Check_Point"].ToString(),
                        BossKill = Convert.ToInt32(dr["Boss_Kill"]),
                        AmountSlotSkill = Convert.ToInt32(dr["Amount_Slot_Skill"]),
                        PointSkill = Convert.ToInt32(dr["Point_Skill"]),
                        Delete = Convert.ToBoolean(dr["Delete"])
                    };
                    connection.Close();

                    return a;

                }

            }
            finally
            {
                connection.Close();

            }
        }
        return null;
    }

    public static string GetMD5(string str)
    {
        MD5 md5 = new MD5CryptoServiceProvider();
        byte[] fromData = Encoding.UTF8.GetBytes(str);
        byte[] targetData = md5.ComputeHash(fromData);
        string byte2String = null;

        for (int i = 0; i < targetData.Length; i++)
        {
            byte2String += targetData[i].ToString("x2");

        }
        return byte2String;
    }
}
