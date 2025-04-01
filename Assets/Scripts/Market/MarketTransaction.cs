using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Market
{
    public class MarketTransaction 
    {
        private int refCrystalCount= 0;
        
        public void ShopCrystalControl(int buyItemPrice)
        {
            refCrystalCount = GameManager.Instance.CrystalCount;
            GameManager.Instance.CrystalCoinLose(buyItemPrice);       
        }

        public void BagCrystalControl(int buyItemPrice)
        {
            refCrystalCount = GameManager.Instance.CrystalCount;
            GameManager.Instance.CrystalCoinWin(buyItemPrice);       
        }


        public void CancelTransaction()
        {
            GameManager.Instance.CrystalCount = refCrystalCount;
        }
        
    }

}
