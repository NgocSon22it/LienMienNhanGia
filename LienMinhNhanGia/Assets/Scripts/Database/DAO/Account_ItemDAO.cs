using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using UnityEngine;

public class Account_ItemDAO : MonoBehaviour
{
    string ConnectionStr = new LienMinhNhanGiaConnect().GetConnectLienMinhNhanGia();
    public List<AccountItemEntity> GetAllItemForAccount(int AccountID)
    {
        List<AccountItemEntity> list = new List<AccountItemEntity>();
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Account_Item where Account_ID = " + AccountID;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    list.Add(new AccountItemEntity
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        AccountID = Convert.ToInt32(dr["Account_ID"]),
                        ItemID = dr["Item_ID"].ToString(),
                        Amount = Convert.ToInt32(dr["Amount"]),
                        Delete = Convert.ToBoolean(dr["Delete"])
                    });
                }

            }
            finally
            {
                connection.Close();

            }
        }

        return list;
    }
    public AccountItemEntity GetAccountItemByItemID(int AccountID, string ItemID)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Account_Item where Account_ID = @accountid AND Item_ID = @itemid";
                cmd.Parameters.AddWithValue("@accountid", AccountID);
                cmd.Parameters.AddWithValue("@itemid", ItemID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    AccountItemEntity a = new AccountItemEntity
                    {
                        ID = Convert.ToInt32(dr["ID"]),
                        AccountID = Convert.ToInt32(dr["Account_ID"]),
                        ItemID = dr["Item_ID"].ToString(),
                        Amount = Convert.ToInt32(dr["Amount"]),
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


}
