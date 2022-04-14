using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

public class LoadSaveData : MonoBehaviour
{
    public static string PlayerOneSave = "/PlayerData1.dat";
    public static string PlayerTwoSave = "/PlayerData2.dat";
    public static string PlayerThreeSave = "/PlayerData3.dat";
    public static string ErrorSave = "/ErrorSave.dat";

    //
    public static void SaveData(string _path, PlayerData _temp)
    {
        XmlSerializer _serializer = new XmlSerializer(typeof(PlayerData));

        FileStream _file = File.Open(Application.persistentDataPath + _path, FileMode.Create);

        _serializer.Serialize(_file, _temp);
        _file.Close();
    }

    public static PlayerData LoadData(string _path)
    {
        if (!DoesFileExist(_path))
            return null;

        XmlSerializer _serializer = new XmlSerializer(typeof(PlayerData));

        FileStream _file = File.Open(Application.persistentDataPath + _path, FileMode.Open);

        PlayerData _temp = (PlayerData)_serializer.Deserialize(_file);
        _file.Close();

        return _temp;
    }

    public static bool DoesFileExist(string _path)
    {
        return File.Exists(Application.persistentDataPath + _path);
    }

}
