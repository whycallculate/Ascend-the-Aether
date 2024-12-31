using System.Collections;
using System.Collections.Generic;
using ShopItemDegreeEnums;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    [SerializeField] private bool isProduct = false;
    public bool IsProduct {get {return isProduct;} set {isProduct = value;} }
    [SerializeField] private ShopItemDegreeEnum shopItemDegree;
    public ShopItemDegreeEnum ShopItemDegree { get {return shopItemDegree;} set {shopItemDegree = value;} }

}
