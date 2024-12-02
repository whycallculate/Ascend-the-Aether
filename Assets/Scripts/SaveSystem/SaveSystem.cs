using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    #region  Veri Kaydetme
    public static void DataSave(string dataType,string dataValu)
    {
        PlayerPrefs.SetString(dataType,dataValu);
    }

    public static void DataSave(string dataType,float dataValu)
    {
        PlayerPrefs.SetFloat(dataType,dataValu);
    }

    public static void DataSave(string dataType,int dataValu)
    {
        PlayerPrefs.SetInt(dataType,dataValu);
    }

    #endregion


    #region  Veri Çekme

    public static string DataExtraction(string dataType,string dataValueType)
    {
        return PlayerPrefs.GetString(dataType);
    }

    public static float DataExtraction(string dataType,float dataValueType)
    {
        return PlayerPrefs.GetFloat(dataType);
    }

    public static int DataExtraction(string dataType,int dataValueType)
    {
        return PlayerPrefs.GetInt(dataType);
    }

    #endregion

    #region  Veri Sorgulama

    public static bool DataQuery(string dataType)
    {
        return PlayerPrefs.HasKey(dataType);
    }

    #endregion


    #region  Veri Silme

    public static void DataRemove(string dataType)
    {
        PlayerPrefs.DeleteKey(dataType);
    }

    #endregion
}
