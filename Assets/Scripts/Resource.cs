using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{
    public Item itemDropped;
    int amountTotal = 10;

    public bool Harvest(int amountToHarvest) {  
        bool canDestroy = false;

        amountTotal -= amountToHarvest;
        
        if(amountTotal == 0) {
            canDestroy = true;
        } 

         if((amountTotal - amountToHarvest) < 0) {
             canDestroy = true;
            return canDestroy;
        }

        Inventory.instance.Add(itemDropped);

        return canDestroy;
    }
}
