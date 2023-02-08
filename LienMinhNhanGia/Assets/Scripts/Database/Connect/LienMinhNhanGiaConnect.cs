using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LienMinhNhanGiaConnect 
{
    string Server = "(local)";
    string id = "sa";
    string password = "123456";
    string database = "LienMinhNhanGia";
    public string GetConnectLienMinhNhanGia()
    {
        return $"Server = {Server}; uid = {id}; pwd = {password}; Database = {database}; Trusted_Connection = False;";
    }
}
