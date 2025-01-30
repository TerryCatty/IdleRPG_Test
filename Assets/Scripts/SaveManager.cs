using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private List<ISaveableObject> saveableObjects = new List<ISaveableObject>();

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void AddSaveableObject(ISaveableObject saveableObject)
    {
        if (saveableObjects.Contains(saveableObject)) return;

        saveableObjects.Add(saveableObject);
    }

    public void SaveAll()
    {
        foreach (ISaveableObject saveable in saveableObjects)
        {
            saveable.SaveData();
        }
    }
}


public interface ISaveableObject
{
    public void SaveData();
    public void LoadData();
}