using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;
using Unity.VisualScripting;

public class ItemDAO : MonoBehaviour
{
    string ConnectionStr = new LienMinhNhanGiaConnect().GetConnectLienMinhNhanGia();

    public List<ItemEntity> GetAllItem()
    {
        List<ItemEntity> list = new List<ItemEntity>();
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Item";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    list.Add(new ItemEntity
                    {
                        ItemID = dr["Item_ID"].ToString(),
                        ItemName = dr["Name"].ToString(),
                        ItemCoin = Convert.ToInt32(dr["Coin"]),
                        Description = dr["Description"].ToString(),
                        LinkImage = dr["Link_image"].ToString(),
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

    public void BuyItem(int AccountID, string ItemID, int Quantity)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "exec BuyMainItem @accountid, @itemid, @quantity";
            cmd.Parameters.AddWithValue("@accountid", AccountID);
            cmd.Parameters.AddWithValue("@itemid", ItemID);
            cmd.Parameters.AddWithValue("@quantity", Quantity);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }


    public ItemEntity GetItembyId(string ItemID)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Item where Item_ID = '" + ItemID + "'";
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    ItemEntity a = new ItemEntity
                    {
                        ItemID = dr["Item_ID"].ToString(),
                        ItemName = dr["Name"].ToString(),
                        ItemCoin = Convert.ToInt32(dr["Coin"]),
                        Description = dr["Description"].ToString(),
                        LinkImage = dr["Link_image"].ToString(),
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
