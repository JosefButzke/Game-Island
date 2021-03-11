using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region Singleton  

    public static Inventory instance;

    void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than one instance found!");
        }

        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public List<Item> items = new List<Item>();
    public int space = 32;

    public bool Add(Item item)
    {
        if(items.Count >= space)
        {
            Debug.Log("Inventory full");
            return false;
        }

        items.Add(item);

        if(onItemChangedCallback != null)
        {
           onItemChangedCallback.Invoke();
        }

        return true;
    }

    public void Remove(Item item)
    {
        items.Remove(item);

        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }

    }
}
