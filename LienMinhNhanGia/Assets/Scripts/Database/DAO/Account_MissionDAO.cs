using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class Account_MissionDAO : MonoBehaviour
{
    string ConnectionStr = new LienMinhNhanGiaConnect().GetConnectLienMinhNhanGia();

    public List<AccountMissionEntity> GetAllMissionForAccount(int AccountID)
    {
        List<AccountMissionEntity> list = new List<AccountMissionEntity>();
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Account_Mission where Acc_ID = " + AccountID;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    list.Add(new AccountMissionEntity
                    {
                        AccountID = Convert.ToInt32(dr["Acc_ID"]),
                        MissionID = Convert.ToInt32(dr["Mission_ID"]),
                        State = Convert.ToInt32(dr["State"]),
                        Current = Convert.ToInt32(dr["Current"]),
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

    public void UpdateAccountMissionState(int AccountID, int MissionID, int State)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE Account_Mission Set [State] = @state where Acc_ID = @accountid and Mission_ID = " + MissionID;
            cmd.Parameters.AddWithValue("@state", State);
            cmd.Parameters.AddWithValue("@accountid", AccountID);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }

    public void UpdateAccountMissionCurrent(int AccountID, int MissionID)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE Account_Mission Set [Current] = [Current] + 1 where Acc_ID = @accountid and Mission_ID = " + MissionID;
            cmd.Parameters.AddWithValue("@accountid", AccountID);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
}
