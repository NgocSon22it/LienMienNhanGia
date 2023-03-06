using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using UnityEngine;

public class Account_MissionDAO
{
    string ConnectionStr = new LienMinhNhanGiaConnect().GetConnectLienMinhNhanGia();
    public void AddMissionToAccount(int AccountID, string MissionID)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "Insert into Account_Mission values(@accountid, @missionid, 0, 0, 0)";
            cmd.Parameters.AddWithValue("@missionid", MissionID);
            cmd.Parameters.AddWithValue("@accountid", AccountID);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
    public List<AccountMissionEntity> GetAllMissionForAccount(int AccountID)
    {
        List<AccountMissionEntity> list = new List<AccountMissionEntity>();
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Account_Mission where Account_ID = " + AccountID;
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    list.Add(new AccountMissionEntity
                    {
                        AccountID = Convert.ToInt32(dr["Account_ID"]),
                        MissionID = dr["Mission_ID"].ToString(),
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
    public void UpdateAccountMissionState(int AccountID, string MissionID, int State)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE Account_Mission Set [State] = @state where Account_ID = @accountid and Mission_ID = '" + MissionID + "'";
            cmd.Parameters.AddWithValue("@state", State);
            cmd.Parameters.AddWithValue("@accountid", AccountID);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
    public void UpdateAccountMissionCurrent(int AccountID, string MissionID)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandText = "exec [IncreaseCurrentMission] @accountid, '" + MissionID + "'";
            cmd.Parameters.AddWithValue("@accountid", AccountID);
            connection.Open();
            cmd.ExecuteNonQuery();
            connection.Close();
        }
    }
    public AccountMissionEntity GetAccountMissionByMissionID(int AccountID, string MissionID)
    {
        using (SqlConnection connection = new SqlConnection(ConnectionStr))
        {
            try
            {
                connection.Open();
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "Select * from Account_Mission where Account_ID = @accountid AND Mission_ID = @missionid";
                cmd.Parameters.AddWithValue("@accountid", AccountID);
                cmd.Parameters.AddWithValue("@missionid", MissionID);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);


                foreach (DataRow dr in dataTable.Rows)
                {
                    AccountMissionEntity a = new AccountMissionEntity
                    {
                        AccountID = Convert.ToInt32(dr["Account_ID"]),
                        MissionID = dr["Mission_ID"].ToString(),
                        State = Convert.ToInt32(dr["State"]),
                        Current = Convert.ToInt32(dr["Current"]),
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
