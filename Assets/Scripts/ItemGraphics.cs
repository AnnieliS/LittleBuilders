using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGraphics : MonoBehaviour
{
    [SerializeField] Sprite UIsprite;

    public Sprite GetUISprite()
    {

        return UIsprite;
    }
}
