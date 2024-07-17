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

    // 아이템 구매를 위한 공통 메서드
    private void BuyItem(int cost, System.Action getItemAction)
    {
        if (playerData.GoldValue >= cost)
        {
            playerData.GoldValue -= cost;
            getItemAction.Invoke();
            itemManger.GoldUpdate();
        }
    }

    // 검 구매 메서드
    public void BuySword01() => BuyItem(100, itemManger.GetSword01);
    public void BuySword02() => BuyItem(5000, itemManger.GetSword02);
    public void BuySword03() => BuyItem(10000, itemManger.GetSword03);

    // 방패 구매 메서드
    public void BuyShield01() => BuyItem(100, itemManger.GetShield01);
    public void BuyShield02() => BuyItem(2000, itemManger.GetShield02);
    public void BuyShield03() => BuyItem(5000, itemManger.GetShield03);
    public void BuyShield04() => BuyItem(10000, itemManger.GetShield04);

    // 헬멧 구매 메서드
    public void BuyHat01() => BuyItem(1000, itemManger.GetHat01);
    public void BuyHat02() => BuyItem(2000, itemManger.GetHat02);
    public void BuyHat03() => BuyItem(5000, itemManger.GetHat03);
    public void BuyHat04() => BuyItem(10000, itemManger.GetHat04);

    // 옷 구매 메서드
    public void BuyCloth01() => BuyItem(100, itemManger.GetCloth01);
    public void BuyCloth02() => BuyItem(5000, itemManger.GetCloth02);
    public void BuyCloth03() => BuyItem(10000, itemManger.GetCloth03);

    // 바지 구매 메서드
    public void BuyPants01() => BuyItem(100, itemManger.GetPants01);
    public void BuyPants02() => BuyItem(5000, itemManger.GetPants02);
    public void BuyPants03() => BuyItem(10000, itemManger.GetPants03);

    // 신발 구매 메서드
    public void BuyShoes01() => BuyItem(100, itemManger.GetShoes01);
    public void BuyShoes02() => BuyItem(5000, itemManger.GetShoes02);
    public void BuyShoes03() => BuyItem(10000, itemManger.GetShoes03);

    // 망토 구매 메서드
    public void BuyKloak01() => BuyItem(5000, itemManger.GetKloak01);
    public void BuyKloak02() => BuyItem(7500, itemManger.GetKloak02);
    public void BuyKloak03() => BuyItem(10000, itemManger.GetKloak03);

    // 목걸이 구매 메서드
    public void BuyNeck01() => BuyItem(2000, itemManger.GetNeck01);
    public void BuyNeck02() => BuyItem(5000, itemManger.GetNeck02);
    public void BuyNeck03() => BuyItem(10000, itemManger.GetNeck03);
    public void BuyNeck04() => BuyItem(15000, itemManger.GetNeck04);

    // 반지 구매 메서드
    public void BuyRing01() => BuyItem(30000, itemManger.GetRing01);
    public void BuyRing02() => BuyItem(50000, itemManger.GetRing02);

    // HP포션 구매 메서드
    public void BuyHPpotion01() => BuyItem(100, itemManger.GetHPpotion01);
    public void BuyHPpotion02() => BuyItem(500, itemManger.GetHPpotion02);
    public void BuyHPpotion03() => BuyItem(1000, itemManger.GetHPpotion03);

    // MP포션 구매 메서드
    public void BuyMPpotion01() => BuyItem(100, itemManger.GetMPpotion01);
    public void BuyMPpotion02() => BuyItem(500, itemManger.GetMPpotion02);
    public void BuyMPpotion03() => BuyItem(1000, itemManger.GetMPpotion03);

    // 재료 판매 메서드
    private void SellMaterial(int materialIndex, int materialPrice, ref int materialCount)
    {
        if (materialCount > 0)
        {
            playerData.GoldValue += materialPrice;
            materialCount--;
            itemManger.GoldUpdate();
            if (materialCount == 0)
            {
                itemManger.ClearMaterial(materialIndex);
            }
        }
    }

    public void SellMaterial01() => SellMaterial(materialData.material01Idx, materialData.material01Price, ref materialData.material01Count);
    public void SellMaterial02() => SellMaterial(materialData.material02Idx, materialData.material02Price, ref materialData.material02Count);
    public void SellMaterial03() => SellMaterial(materialData.material03Idx, materialData.material03Price, ref materialData.material03Count);
    public void SellMaterial04() => SellMaterial(materialData.material04Idx, materialData.material04Price, ref materialData.material04Count);
    public void SellMaterial05() => SellMaterial(materialData.material05Idx, materialData.material05Price, ref materialData.material05Count);
    public void SellMaterial06() => SellMaterial(materialData.material06Idx, materialData.material06Price, ref materialData.material06Count);
    public void SellMaterial07() => SellMaterial(materialData.material07Idx, materialData.material07Price, ref materialData.material07Count);
    public void SellMaterial08() => SellMaterial(materialData.material08Idx, materialData.material08Price, ref materialData.material08Count);
}
