using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class JsonManager : MonoBehaviour
{
    [SerializeField]
    FurnitureManager furnitureManager;
    JsonData furnitureJson;


    public void SaveSetupData()
    {
        Dictionary<CellType, List<Vector3Int>> furniturePositionDic = furnitureManager.FurniturePositionDic;
        List<Vector3Int> furnitureList = furniturePositionDic[CellType.Furniture];
        for (int i = 0; i < furnitureList.Count; i++)
        {
            //JsonData pos = JsonMapper.ToJson(Vector3.zero);
            JsonData pos = JsonMapper.ToJson(furniturePositionDic[CellType.Furniture][i]);
            JsonData item = JsonMapper.ToJson("table");
            Debug.Log(pos);
            //File.WriteAllText(Application.dataPath + "Resources/test.json", pos.ToString());
        }
        //Debug.Log(furnitureJson);
    }
}
