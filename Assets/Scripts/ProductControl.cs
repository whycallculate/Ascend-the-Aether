using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Products
{
    public class ProductControl : MonoBehaviour
    {
        [SerializeField] private ProductEnum productEnum;
        public ProductEnum ProductEnum {get { return productEnum;} set { productEnum =  value;}}

        [SerializeField] private int price;
        public int Price { get { return price;} set {price = value;}}


       
        public void SetProductPrice(int productPrice)
        {
            price = productPrice;;
        }

    }

}
