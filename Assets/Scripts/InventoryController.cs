using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryController : MonoBehaviour
{
    [SerializeField] GameObject inventoryContainer;
    [SerializeField] GameObject[] inventorySpaces;
    [SerializeField] Color32 inventoryStrokeChosen = new Color32(255, 0, 0, 255);
    [SerializeField] Color32 inventoryStrokeDefault = new Color32(255, 255, 255, 255);
    public List<PlaceableItem> inventoryItems = new List<PlaceableItem>();
    int currentPlaceableObjectIndex = -1;

    private static InventoryController instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Inventory Manager in the scene");
        }
        instance = this;
    }

    public static InventoryController Instance()
    {
        return instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        List<PlaceableItem> tempList = GroundPlacementController.Instance().GetPlaceableItems();
        foreach (PlaceableItem item in tempList)
        {
            inventoryItems.Add(item);
        }
        int i = 0;
        foreach (PlaceableItem item in inventoryItems)
        {
            Image tempImage = inventorySpaces[i].transform.GetChild(0).GetComponent<Image>();
            Debug.Log(tempImage);
            tempImage.sprite = item.UIsprite;
            tempImage.color = new Color32(255, 255, 255, 255);
            i++;
        }
    }

    public void ChangeCurrentChosen(int chosen)
    {
        Debug.Log(currentPlaceableObjectIndex);
        if (currentPlaceableObjectIndex != -1)
        {
            Debug.Log("should disactive");
            DeactivateStroke();
        }

            currentPlaceableObjectIndex = chosen;
            if (chosen != -1)
                inventorySpaces[chosen].GetComponent<Image>().color = inventoryStrokeChosen;
        }


        public void DeactivateStroke()
        {
            inventorySpaces[currentPlaceableObjectIndex].GetComponent<Image>().color = inventoryStrokeDefault;
        }


    }
