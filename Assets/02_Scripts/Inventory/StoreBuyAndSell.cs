using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreBuyAndSell : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    [SerializeField] MaterialData materialData;
    ItemManger itemManger;
    private void Start()
    {
       itemManger = ItemManger.itemInst;
    }
    //************** 검얻기****************//
    public void BuySword01()
    {
        if(playerData.GoldValue >= 100)
        {
            playerData.GoldValue -= 100;
            itemManger.GetSword01();
            itemManger.GoldUpdate();
        }
    }
    public void BuySword02()
    {
        if (playerData.GoldValue >= 5000)
        {
            playerData.GoldValue -= 5000;
            itemManger.GetSword02();
            itemManger.GoldUpdate();
        }
    }
    public void BuySword03()
    {
        if (playerData.GoldValue >= 10000)
        {
            playerData.GoldValue -= 10000;
            itemManger.GetSword03();
            itemManger.GoldUpdate();
        }
    }
    //************** 방패얻기****************//
    public void BuyShield01()
    {
        if (playerData.GoldValue >= 100)
        {
            playerData.GoldValue -= 100;
            itemManger.GetShield01();
            itemManger.GoldUpdate();
        }
    }
    public void BuyShield02()
    {
        if (playerData.GoldValue >= 2000)
        {
            playerData.GoldValue -= 2000;
            itemManger.GetShield02();
            itemManger.GoldUpdate();
        }
    }
    public void BuyShield03()
    {
        if (playerData.GoldValue >= 5000)
        {
            playerData.GoldValue -= 5000;
            itemManger.GetShield03();
            itemManger.GoldUpdate();
        }
    }
    public void BuyShield04()
    {
        if (playerData.GoldValue >= 10000)
        {
            playerData.GoldValue -= 10000;
            itemManger.GetShield04();
            itemManger.GoldUpdate();
        }
    }
    //************** 헬멧얻기****************//
    public void BuyHat01()
    {
        if (playerData.GoldValue >= 1000)
        {
            playerData.GoldValue -= 1000;
            itemManger.GetHat01();
            itemManger.GoldUpdate();
        }
    }
    public void BuyHat02()
    {
        if (playerData.GoldValue >= 2000)
        {
            playerData.GoldValue -= 2000;
            itemManger.GetHat02();
            itemManger.GoldUpdate();
        }
    }
    public void BuyHat03()
    {
        if (playerData.GoldValue >= 5000)
        {
            playerData.GoldValue -= 5000;
            itemManger.GetHat03();
            itemManger.GoldUpdate();
        }
    }
    public void BuyHat04()
    {
        if (playerData.GoldValue >= 10000)
        {
            playerData.GoldValue -= 10000;
            itemManger.GetHat04();
            itemManger.GoldUpdate();
        }
    }
    //************** 옷얻기****************//
    public void BuyCloth01()
    {
        if (playerData.GoldValue >= 100)
        {
            playerData.GoldValue -= 100;
            itemManger.GetCloth01();
            itemManger.GoldUpdate();
        }
    }
    public void BuyCloth02()
    {
        if (playerData.GoldValue >= 5000)
        {
            playerData.GoldValue -= 5000;
            itemManger.GetCloth02();
            itemManger.GoldUpdate();
        }
    }
    public void BuyCloth03()
    {
        if (playerData.GoldValue >= 10000)
        {
            playerData.GoldValue -= 10000;
            itemManger.GetCloth03();
            itemManger.GoldUpdate();
        }
    }
    //************** 바지얻기****************//
    public void BuyPants01()
    {
        if (playerData.GoldValue >= 100)
        {
            playerData.GoldValue -= 100;
            itemManger.GetPants01();
            itemManger.GoldUpdate();
        }
    }
    public void BuyPants02()
    {
        if (playerData.GoldValue >= 5000)
        {
            playerData.GoldValue -= 5000;
            itemManger.GetPants02();
            itemManger.GoldUpdate();
        }
    }
    public void BuyPants03()
    {
        if (playerData.GoldValue >= 10000)
        {
            playerData.GoldValue -= 10000;
            itemManger.GetPants03();
            itemManger.GoldUpdate();
        }
    }
    //************** 신발얻기****************//
    public void BuyShoes01()
    {
        if (playerData.GoldValue >= 100)
        {
            playerData.GoldValue -= 100;
            itemManger.GetShoes01();
            itemManger.GoldUpdate();
        }
    }
    public void BuyShoes02()
    {
        if (playerData.GoldValue >= 5000)
        {
            playerData.GoldValue -= 5000;
            itemManger.GetShoes02();
            itemManger.GoldUpdate();
        }
    }
    public void BuyShoes03()
    {
        if (playerData.GoldValue >= 10000)
        {
            playerData.GoldValue -= 10000;
            itemManger.GetShoes03();
            itemManger.GoldUpdate();
        }
    }
    //************** 망토얻기****************//
    public void BuyKloak01()
    {
        if (playerData.GoldValue >= 5000)
        {
            playerData.GoldValue -= 5000;
            itemManger.GetKloak01();
            itemManger.GoldUpdate();
        }
    }
    public void BuyKloak02()
    {
        if (playerData.GoldValue >= 7500)
        {
            playerData.GoldValue -= 7500;
            itemManger.GetKloak02();
            itemManger.GoldUpdate();
        }
    }
    public void BuyKloak03()
    {
        if (playerData.GoldValue >= 10000)
        {
            playerData.GoldValue -= 10000;
            itemManger.GetKloak03();
            itemManger.GoldUpdate();
        }
    }

    //************** 목걸이얻기****************//
    public void BuyNeck01()
    {
        if (playerData.GoldValue >= 2000)
        {
            playerData.GoldValue -= 2000;
            itemManger.GetNeck01();
            itemManger.GoldUpdate();
        }
    }
    public void BuyNeck02()
    {
        if (playerData.GoldValue >= 5000)
        {
            playerData.GoldValue -= 5000;
            itemManger.GetNeck02();
            itemManger.GoldUpdate();
        }
    }
    public void BuyNeck03()
    {
        if (playerData.GoldValue >= 10000)
        {
            playerData.GoldValue -= 10000;
            itemManger.GetNeck03();
            itemManger.GoldUpdate();
        }
    }
    public void BuyNeck04()
    {
        if (playerData.GoldValue >= 15000)
        {
            playerData.GoldValue -= 15000;
            itemManger.GetNeck04();
            itemManger.GoldUpdate();
        }
    }
    //************** 반지얻기****************//
    public void BuyRing01()
    {
        if (playerData.GoldValue >= 30000)
        {
            playerData.GoldValue -= 30000;
            itemManger.GetRing01();
            itemManger.GoldUpdate();
        }
    }

    public void BuyRing02()
    {
        if (playerData.GoldValue >= 50000)
        {
            playerData.GoldValue -= 50000;
            itemManger.GetRing02();
            itemManger.GoldUpdate();
        }
    }

    //************** HP포션얻기****************//
    public void BuyHPpotion01()
    {
        if (playerData.GoldValue >= 100)
        {
            playerData.GoldValue -= 100;
            itemManger.GetHPpotion01();
            itemManger.GoldUpdate();
        }
    }
    public void BuyHPpotion02()
    {
        if (playerData.GoldValue >= 500)
        {
            playerData.GoldValue -= 500;
            itemManger.GetHPpotion02();
            itemManger.GoldUpdate();
        }
    }
    public void BuyHPpotion03()
    {
        if (playerData.GoldValue >= 1000)
        {
            playerData.GoldValue -= 1000;
            itemManger.GetHPpotion03();
            itemManger.GoldUpdate();
        }
    }
    //************** MP포션얻기****************//
    public void BuyMPpotion01()
    {
        if (playerData.GoldValue >= 100)
        {
            playerData.GoldValue -= 100;
            itemManger.GetMPpotion01();
            itemManger.GoldUpdate();
        }
    }
    public void BuyMPpotion02()
    {
        if (playerData.GoldValue >= 500)
        {
            playerData.GoldValue -= 500;
            itemManger.GetMPpotion02();
            itemManger.GoldUpdate();
        }
    }
    public void BuyMPpotion03()
    {
        if (playerData.GoldValue >= 1000)
        {
            playerData.GoldValue -= 1000;
            itemManger.GetMPpotion03();
            itemManger.GoldUpdate();
        }
    }
    //************* 재료 얻기 *******************/
    public void SellMaterial01()
    {
        if(materialData.material01Count > 0)
        {
            playerData.GoldValue += materialData.material01Price;
            materialData.material01Count--;
            itemManger.GoldUpdate();
        }
    }
    public void SellMaterial02()
    {
        if (materialData.material01Count > 0)
        {
            playerData.GoldValue += materialData.material02Price;
            materialData.material01Count--;
            itemManger.GoldUpdate();
        }
    }
    public void SellMaterial03()
    {
        if (materialData.material01Count > 0)
        {
            playerData.GoldValue += materialData.material03Price;
            materialData.material01Count--;
            itemManger.GoldUpdate();
        }
    }
    public void SellMaterial04()
    {
        if (materialData.material01Count > 0)
        {
            playerData.GoldValue += materialData.material04Price;
            materialData.material01Count--;
            itemManger.GoldUpdate();
        }
    }
    public void SellMaterial05()
    {
        if (materialData.material01Count > 0)
        {
            playerData.GoldValue += materialData.material05Price;
            materialData.material01Count--;
            itemManger.GoldUpdate();
        }
    }
    public void SellMaterial06()
    {
        if (materialData.material01Count > 0)
        {
            playerData.GoldValue += materialData.material06Price;
            materialData.material01Count--;
            itemManger.GoldUpdate();
        }
    }
    public void SellMaterial07()
    {
        if (materialData.material01Count > 0)
        {
            playerData.GoldValue += materialData.material07Price;
            materialData.material01Count--;
            itemManger.GoldUpdate();
        }
    }
    public void SellMaterial08()
    {
        if (materialData.material01Count > 0)
        {
            playerData.GoldValue += materialData.material08Price;
            materialData.material01Count--;
            itemManger.GoldUpdate();
        }
    }
}
