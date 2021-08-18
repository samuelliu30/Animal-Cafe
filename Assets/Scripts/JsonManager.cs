using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using System.IO;

public class JsonManager : MonoBehaviour
{
    [SerializeField]
    FurnitureManager furnitureManager;
    [SerializeField]
    PlacementManager placementManager;

    JsonData furnitureJson;

    public class FurnitureData
    {
        public string name;
        public List<Vector3Int> positions = new List<Vector3Int>();
    }


    public void SaveSetupData()
    {

        Dictionary<CellType, List<Vector3Int>> furniturePositionDic = furnitureManager.FurniturePositionDic;
        List<Vector3Int> furnitureList = furniturePositionDic[CellType.Furniture];
        Vector3Int pos = furniturePositionDic[CellType.Furniture][0];
        List<JsonData> jsonDatalist = new List<JsonData>();

        FurnitureData tmp = new FurnitureData();
        tmp.name = "table";

        for (int i = 0; i < furnitureList.Count; i++)
        {

            tmp.positions.Add(furnitureList[i]);
        }

        string jsonData = JsonUtility.ToJson(tmp);
        File.WriteAllText(Application.dataPath + "/Resources/test.json", jsonData.ToString());

        LoadSetupData();
        //Debug.Log(furnitureJson);
    }

    public void LoadSetupData()
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Resources/test.json");

        FurnitureData setUpData = new FurnitureData();

        setUpData = JsonUtility.FromJson<FurnitureData>(jsonString);

        Debug.Log(setUpData.name + ": " + setUpData.positions[0]);

        //for(int i = 0; i < setUpData.positions.Count; i++)
        //{
        //    placementManager.PlaceTemporaryStructure(setUpData.positions, , CellType.Furniture)
        //}

    }

}
