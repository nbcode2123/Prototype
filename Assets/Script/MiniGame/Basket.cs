using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Basket : MonoBehaviour
{
    [SerializeField] public List<BasketPos> _basket = new List<BasketPos>();
    public void PutItemToBasket(GameObject item)
    {
        for (int i = 0; i < _basket.Count; i++)
        {
            if (_basket[i].isEmpty == true)
            {
                item.gameObject.transform.DOMove(_basket[i].pos.transform.position, 1f);

                _basket[i].isEmpty = false;
                return;

            }
        }
    }

}
[Serializable]
public class BasketPos
{
    public bool isEmpty;
    public GameObject pos;


}
