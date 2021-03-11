using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    #region Singleton 

    public static EquipmentManager instance;

    void Awake() {
      if(instance != null)
        {
            Debug.Log("More than one instance found!");
        }

        instance = this;
    }

    #endregion

    Equipment[] currentEquipment;
    public GameObject target;
    public delegate void OnEquipmentChanged (Equipment newEquipment, Equipment oldEquipment);
    public OnEquipmentChanged onEquipmentChanged;

    Inventory inventory;

    void Start() {
        inventory = Inventory.instance;

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip(Equipment newEquipment) {
        int slotIndex = (int)newEquipment.equipSlot;

        Equipment oldEquipment = null;

       if(currentEquipment[slotIndex] != null) {
            Equipment oldItem = currentEquipment[slotIndex];
            inventory.Add(oldItem);
            currentEquipment[slotIndex] = null;
        }

        if(onEquipmentChanged != null) {
            onEquipmentChanged.Invoke(newEquipment, oldEquipment);
        }

        currentEquipment[slotIndex] = newEquipment;

        GameObject newMesh = Instantiate<GameObject>(newEquipment.gameObject);
        newMesh.transform.parent = target.transform;
        newMesh.transform.localPosition = new Vector3(0,0,0);
    }

    public void Unequip(int slotIndex) {
        if(currentEquipment[slotIndex] != null) {
            Equipment oldEquipment = currentEquipment[slotIndex];
            inventory.Add(oldEquipment);
            currentEquipment[slotIndex] = null;

            if(onEquipmentChanged != null) {
                onEquipmentChanged.Invoke(null, oldEquipment);
            }      
        }
    }
}
