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

    [System.Serializable]
    public class FurnitureData
    {
        public Vector3Int pos;
        public string name;
        public Quaternion rotation;

        public FurnitureData() { }
        public FurnitureData(Vector3Int pos, FurnitureManager.FurnitureData data)
        {
            this.pos = pos;
            this.name = data.name;
            this.rotation = data.rotation;
        }
    }

    [System.Serializable]
    public class ListFurnitureData
    {
        public List<FurnitureData> listFurnitureData = new List<FurnitureData>();
    }

    public void SaveSetupData()
    {
        string filePath = Application.dataPath + "/Resources/test.json";
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }

        Dictionary<Vector3Int, FurnitureManager.FurnitureData> positionFurnitureDic = furnitureManager.PositionFurnitureDic;

        FurnitureManager.FurnitureData tmpData = new FurnitureManager.FurnitureData();
        // Use ListFurnitureData class to serialize furnituredata
        ListFurnitureData dataList = new ListFurnitureData();

        foreach (var item in positionFurnitureDic)
        {
            //tmpData = item.Value;
            FurnitureData storeData = new FurnitureData(item.Key, item.Value);
            dataList.listFurnitureData.Add(storeData);
        }

        string jsonData = JsonUtility.ToJson(dataList, true);
        File.WriteAllText(filePath, jsonData);


    }

    public void LoadSetupData()
    {
        // Read back from Json. The steps are reversed version of saving
        ListFurnitureData dataList = new ListFurnitureData();
        string jsonString = File.ReadAllText(Application.dataPath + "/Resources/test.json");
        dataList = JsonUtility.FromJson<ListFurnitureData>(jsonString);


        FurnitureData furnitureData = new FurnitureData();

        for (int i = 0; i < dataList.listFurnitureData.Count; i++)
        {

            placementManager.PlaceObject(dataList.listFurnitureData[i].pos, dataList.listFurnitureData[i].name, dataList.listFurnitureData[i].rotation, CellType.Furniture);
        }
    }

}
