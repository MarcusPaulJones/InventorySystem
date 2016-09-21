﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
public class ItemDatabase : MonoBehaviour
{
    private Dictionary<string, Item> database = new Dictionary<string, Item>();
    private JsonData itemData;
    private static ItemDatabase instance = null;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            string jsonFilePath = Application.dataPath + "/StreamingAssets/Items.json";
            string jsonText = File.ReadAllText(jsonFilePath);
            itemData = JsonMapper.ToObject(jsonText);
            ConstructData();
        }
        else
        {
            instance = null;
        }

    }

    void ConstructData()
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            JsonData data = itemData[i];
            Item newItem = new Item(data);
            database.Add(newItem.Title, newItem);
        }
    }
    public static Item GetItem(string itemName)
    {
        Dictionary<string, Item> database = instance.database;
        if (database.ContainsKey(itemName))
        {
            return database[itemName];
        }
        return null;
    }
    public static Dictionary<string, Item> GetDatabase()
    {
        return instance.database;
    }
}

public class Stat
{
    public int Power { get; set; }
    public int Defence { get; set; }
    public int Vitality { get; set; }
    public Stat(Stat stats)
    {
        this.Power = stats.Power;
        this.Defence = stats.Defence;
        this.Vitality = stats.Vitality;
    }
    public Stat(JsonData data)
    {
        Power = (int)data["power"];
        Defence = (int)data["defence"];
        Vitality = (int)data["vitality"];
    }
}
public class Item
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Value { get; set; }
    public Stat Stats { get; set; }
    public string Description { get; set; }
    public bool Stackable { get; set; }
    public int Rarity { get; set; }
    public Sprite Sprite { get; set; }
    public GameObject gameobject { get; set; }

    public Item()
    {
        ID = -1;
    }
    public Item(JsonData data)
    {
        ID = (int)data["id"];
        Title = data["title"].ToString();
        Value = (int)data["value"];
        Stats = new Stat(data);
        Description = data["description"].ToString();
        Stackable = (bool)data["stackable"];
        Rarity = (int)data["Rarity"];
        string fileName = data["sprite"].ToString();
        Sprite = Resources.Load<Sprite>("Sprites/Items/" + fileName);
    }
}