using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPlacementController : MonoBehaviour
{
    [SerializeField]
    private List<PlaceableItem> placeableObjectPrefabs = new List<PlaceableItem>();

    private GameObject currentPlaceableObject;

    private float mouseWheelRotation;
    private int currentPrefabIndex = -1;

    private static GroundPlacementController instance;

    private void Awake() {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one Placement Manager in the scene");
        }
        instance = this;
    }

    public static GroundPlacementController Instance(){
        return instance;
    }

    private void Update()
    {
        HandleNewObjectHotkey();

        if (currentPlaceableObject != null)
        {
            MoveCurrentObjectToMouse();
            RotateFromMouseWheel();
            ReleaseIfClicked();
        }
    }

    private void HandleNewObjectHotkey()
    {
        for (int i = 0; i < placeableObjectPrefabs.Count; i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
            {
                InventoryController.Instance().ChangeCurrentChosen(i);
                if (PressedKeyOfCurrentPrefab(i))
                {
                    InventoryController.Instance().ChangeCurrentChosen(-1);
                    Destroy(currentPlaceableObject);
                    currentPrefabIndex = -1;
                }
                else
                {
                    if (currentPlaceableObject != null)
                    {
                        Destroy(currentPlaceableObject);
                    }

                    currentPlaceableObject = Instantiate(placeableObjectPrefabs[i].itemPrefab);
                    currentPrefabIndex = i;
                }

                break;
            }
        }
    }

    private bool PressedKeyOfCurrentPrefab(int i)
    {
        return currentPlaceableObject != null && currentPrefabIndex == i;
    }

    private void MoveCurrentObjectToMouse()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            // Debug.Log("point info: " + hitInfo.point);
            currentPlaceableObject.transform.position = hitInfo.point;
            currentPlaceableObject.transform.rotation = Quaternion.FromToRotation(Vector3.up, hitInfo.normal);
        }
    }

    private void RotateFromMouseWheel()
    {
        // Debug.Log(Input.mouseScrollDelta);
        mouseWheelRotation += Input.mouseScrollDelta.y;
        currentPlaceableObject.transform.Rotate(Vector3.up, mouseWheelRotation * 10f);
    }

    private void ReleaseIfClicked()
    {
        if (Input.GetMouseButtonDown(0))
        {
            InventoryController.Instance().DeactivateStroke();
            Debug.Log("place item");
            currentPlaceableObject = null;
        }
    }

    public List<PlaceableItem> GetPlaceableItems(){
        return placeableObjectPrefabs;
    }
}