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
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    RecipeManager recipeManager;

    private JsonData furnitureJson;
    private BagManager bagManager;


    public void SaveGame()
    {
        SaveSetupData();
        SaveBagData();
    }

    public void LoadGame()
    {
        LoadSetupData();
        LoadBagData();
    }


    //////////////////// Saving and Loading Furniture Data ////////////////////

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

        if(dataList == null)
        {
            return;
        }

        FurnitureData furnitureData = new FurnitureData();

        for (int i = 0; i < dataList.listFurnitureData.Count; i++)
        {

            placementManager.PlaceObject(dataList.listFurnitureData[i].pos, dataList.listFurnitureData[i].name, dataList.listFurnitureData[i].rotation, CellType.Furniture);
        }
    }

    //////////////////// Saving and Loading Inventory Data ////////////////////

    [System.Serializable]
    public class BagData
    {
        public Dictionary<string, Item> itemDict = new Dictionary<string, Item>();
    }

    public void SaveBagData()
    {
        bagManager = gameManager.bagManager;
        string filePath = Application.dataPath + "/Resources/BagTest.json";
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }

        BagData bagData = new BagData();
        bagData.itemDict = bagManager.ItemDict;

        string savingData = JsonMapper.ToJson(bagData);
        File.WriteAllText(filePath, savingData);

    }

    public void LoadBagData()
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Resources/BagTest.json");
        BagData bagData = new BagData();
        bagData = JsonMapper.ToObject<BagData>(jsonString);

        gameManager.bagManager.UpdateItemList(bagData.itemDict);
        gameManager.uiBagManager.RefreshBagItems();
    }

    //////////////////// Saving and Loading Store Data ////////////////////
    
    [System.Serializable]

    public class StoreData
    {
        public Item item;
    }

    public void SaveStoreData()
    {

    }

    //////////////////// Saving and Loading Recipe Data //////////////////// 

    [System.Serializable]
    public class RecipeData
    {
        public Dictionary<string, RecipeManager.Recipe> itemDict = new Dictionary<string, RecipeManager.Recipe>();
    }

    public void SaveRecipeData()
    {
        bagManager = gameManager.bagManager;
        string filePath = Application.dataPath + "/Resources/BagTest.json";
        if (!File.Exists(filePath))
        {
            File.Create(filePath);
        }

        RecipeData recipeData = new RecipeData();
        recipeData.itemDict = recipeManager.recipeDict;

        string savingData = JsonMapper.ToJson(recipeData);
        File.WriteAllText(filePath, savingData);

    }


    public void LoadRecipeData()
    {
        string jsonString = File.ReadAllText(Application.dataPath + "/Resources/Recipe.json");
        RecipeData recipeData = new RecipeData();
        recipeData = JsonMapper.ToObject<RecipeData>(jsonString);

        recipeManager.LoadRecipes(recipeData.itemDict);
    }


}
