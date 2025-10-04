
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Knife : IDraggable
{
    public override void Start()
    {
        base.Start();


    }
    private void OnDestroy()
    {

    }
    private void OnDisable()
    {

    }
    public override void OnDragStart()
    {
        base.OnDragStart();
        ObserverManager.Notify("PickUpKnife");



    }
    public override void OnDragCancel()
    {
        base.OnDragCancel();
        ObserverManager.Notify("PutDownKnife");

    }
    public void ResetStatue()
    {
        gameObject.SetActive(true);
        OnDragCancel();

    }



}
