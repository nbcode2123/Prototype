using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ClayPot : IDraggable, IDropStorage
{
    public bool isHaveWater;
    public bool isInGasStove;
    public bool isGasTurnOn;
    public GameObject _water;
    public GameObject _broth;
    public GameObject _beef;
    public GameObject _beefProduct;
    public GameObject _pepper;
    public GameObject _leafLemon;
    public GameObject _potObj;
    public GameObject _chickenProduct;
    public Collider2D _collider;
    public Stack<CookProcess> _CookProcess;
    public List<Ingredient> _boilBeefProcessIngredient;
    public List<Ingredient> _boilBeefWithChickenIngredient;
    public List<IngredientAndGamObject> _listIngredientAndGameObject;
    public float _cookProcessTimer;






    // public GameObject 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        _CookProcess = new Stack<CookProcess>(new List<CookProcess>() { CookProcess.BoilBeefWithChicken, CookProcess.BoilBeef });


        Color c = _broth.GetComponent<SpriteRenderer>().color;
        c.a = 0f;
        _beef.GetComponent<SpriteRenderer>().sortingOrder = 11;
        _beefProduct.GetComponent<SpriteRenderer>().sortingOrder = 11;
        _pepper.GetComponent<SpriteRenderer>().sortingOrder = 11;
        _leafLemon.GetComponent<SpriteRenderer>().sortingOrder = 11;
        Color b = _beef.GetComponent<SpriteRenderer>().color;
        b.a = 0f;
        isHaveWater = false;
        isInGasStove = true;




    }


    // Update is called once per frame
    void Update()
    {

    }

    public void OnDrop(IDraggable draggable)
    {
        if (isGasTurnOn == true && isInGasStove == true && isHaveWater == true)
        {
            if (draggable is FoodIngredient foodIngredient)
            {
                PutIngredientToPot(foodIngredient);

            }
            else
            {
                draggable.OnDragCancel();
            }

        }

        else
        {
            draggable.OnDragCancel();
        }

    }
    public override void OnDrag(Vector3 position)
    {
        base.OnDrag(position);
    }
    public override void OnDragCancel()
    {
        base.OnDragCancel();


    }



    public void PutWaterToPot()
    {
        _collider.enabled = false;

        Color c = _water.GetComponent<SpriteRenderer>().color;
        c.a = 0f;
        _water.SetActive(true);
        _water.GetComponent<SpriteRenderer>().DOFade(1f, 0.3f).OnComplete(() =>
        {
            _collider.enabled = true;
            isHaveWater = true;
        });




    }

    public void PutIngredientToPot(FoodIngredient ingredient)
    {

        if (_CookProcess.Peek() == CookProcess.BoilBeef)
        {

            if (_boilBeefProcessIngredient.Contains(ingredient._ingredient))
            {
                Debug.Log("Cần nguyên liệu này");

                if (ingredient is BlackPepper blackPepper)
                {
                    blackPepper.RunAnimation();

                }
                else
                {
                    ingredient.gameObject.SetActive(false);

                }

                AnimationIngredientInPot(_listIngredientAndGameObject.Find(x => x._ingredient == ingredient._ingredient)._prefab);
                _boilBeefProcessIngredient.Remove(ingredient._ingredient);
                if (_boilBeefProcessIngredient.Count == 0)
                {
                    Debug.Log("Đủ nguyên liệu process 1 ");
                    Process1();
                }


            }
            else
            {
                ingredient.OnDragCancel();
            }


        }


    }
    public void AnimationIngredientInPot(GameObject obj)
    {
        if (obj.activeSelf == false)
        {
            var y = obj.transform.localScale.y;
            var x = obj.transform.localScale.x;

            Sequence sequence = DOTween.Sequence();
            sequence.AppendCallback(() =>
            {
                obj.GetComponent<SpriteRenderer>().sortingOrder = _water.GetComponent<SpriteRenderer>().sortingOrder + 1;
                obj.SetActive(true);
                obj.transform.DOScaleY(y - 0.1f, 0.2f).SetLoops(2, LoopType.Yoyo);
                obj.transform.DOScaleX(x - 0.1f, 0.2f).SetLoops(2, LoopType.Yoyo);


            });
            sequence.AppendInterval(0.2f);
            sequence.AppendCallback(() =>
            {
                obj.GetComponent<SpriteRenderer>().sortingOrder = _broth.GetComponent<SpriteRenderer>().sortingOrder - 1;
            });
            sequence.OnComplete(() =>
            {
                sequence.Kill();
            });
        }




    }
    public void Process1()
    {
        Debug.Log(_cookProcessTimer);
        _water.transform.DOMoveY(_water.gameObject.transform.position.y + 0.05f, 1).SetLoops(10, LoopType.Yoyo);

        _water.GetComponent<SpriteRenderer>().DOFade(0, _cookProcessTimer).OnComplete(() =>
        {
            _water.SetActive(false);

        });

        _broth.SetActive(true);
        _broth.GetComponent<SpriteRenderer>().DOFade(1, _cookProcessTimer).SetDelay(2f);
        _broth.transform.DOMoveY(_broth.gameObject.transform.position.y + 0.05f, 1).SetLoops(10, LoopType.Yoyo);

        _beef.GetComponent<SpriteRenderer>().DOFade(0, _cookProcessTimer).OnComplete(() =>
        {
            _beef.SetActive(false);
        });
        _beef.transform.DOMoveY(_beef.gameObject.transform.position.y + 0.1f, 1).SetLoops(10, LoopType.Yoyo);

        _leafLemon.GetComponent<SpriteRenderer>().DOFade(0, _cookProcessTimer).OnComplete(() =>
        {
            _leafLemon.SetActive(false);
        });
        _leafLemon.transform.DOMoveY(_leafLemon.gameObject.transform.position.y + 0.15f, 1).SetLoops(10, LoopType.Yoyo);

        _pepper.GetComponent<SpriteRenderer>().DOFade(0, _cookProcessTimer).OnComplete(() =>
        {
            _pepper.SetActive(false);
        });
        _pepper.transform.DOMoveY(_pepper.gameObject.transform.position.y + 0.1f, 1).SetLoops(10, LoopType.Yoyo);
        _beefProduct.SetActive(true);
        _beefProduct.GetComponent<SpriteRenderer>().DOFade(1, _cookProcessTimer);
        DOVirtual.DelayedCall(_cookProcessTimer, () =>
       {
           _CookProcess.Pop();
           Debug.Log("Đã nấu xong Process 1 ");
       });

    }
    public enum CookProcess
    {
        BoilBeef,
        BoilBeefWithChicken

    }
    [Serializable]
    public class IngredientAndGamObject
    {
        public Ingredient _ingredient;
        public GameObject _prefab;


    }
}
