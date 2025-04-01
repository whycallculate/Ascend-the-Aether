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

        private bool selectable = false;
       
        public void SetProductPrice(int productPrice)
        {
            price = productPrice;;
        }

        public void SetSelectable()
        {
            selectable = !selectable;
        }
    }

}
