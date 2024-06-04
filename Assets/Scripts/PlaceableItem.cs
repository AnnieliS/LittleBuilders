using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlaceableItem", menuName = "PlaceableItem", order = 0)]
public class PlaceableItem : ScriptableObject {
    public GameObject itemPrefab;
    public Sprite UIsprite;
    
}
