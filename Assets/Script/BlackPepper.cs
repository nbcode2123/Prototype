using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BlackPepper : FoodIngredient
{

    public override void Start()
    {
        _ingredient = Ingredient.Pepper;
        _defaultPosition = gameObject.transform.position;
    }
    // Update is called once per frame
    void Update()
    {

    }
    public override void OnDragEnd(IDropStorage storage)
    {

        base.OnDragEnd(storage);
    }
    public void RunAnimation()
    {
        Vector3 direction = Quaternion.Euler(0, 0, 120f) * Vector3.up; // quay vector hướng lên 120°

        Sequence sequence = DOTween.Sequence();
        Sequence seq = DOTween.Sequence();

        // Bước 1: Quay 120 độ
        seq.Append(transform.DORotate(new Vector3(0, 0, 120f), 0.1f));

        // Bước 2: Di chuyển theo hướng vừa quay
        seq.Append(transform.DOMove(transform.position + direction * 0.5f, 0.1f).SetLoops(4, LoopType.Yoyo));
        seq.Append(gameObject.transform.DORotate(Vector3.zero, 0.1f));
        seq.Append(gameObject.transform.DOMove(_defaultPosition, 0.1f));
        seq.OnComplete(() =>
        {
            seq.Kill();
        });





    }
}
