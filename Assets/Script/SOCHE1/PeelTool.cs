using System;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class PeelTool : IDraggable
{
    public GameObject _pivot;
    private event Action OnPeel;
    public Collider2D collision;

    private void OnTriggerStay2D(Collider2D other)
    {
        PeelController peelController = other.GetComponent<PeelController>();
        if (peelController != null && peelController._isOnCutBroad && peelController._foodIngredient.CookProcess.Peek() == IngredientProcess.Peel)
        {
            Vector2 contactPos = other.ClosestPoint(transform.position);
            peelController.PeelAt(contactPos);
            peelController.PeelingSkin.SetActive(true);
            peelController.PeelingSkin.transform.position = gameObject.transform.position;


        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        PeelController peelController = other.GetComponent<PeelController>();
        if (peelController != null)
        {
            peelController.PeelingSkin.SetActive(false);
            peelController.PeelingSkin.transform.position = peelController.transform.position;




        }
    }
    public override void OnDragStart()
    {
        base.OnDragStart();
        ObserverManager.Notify("PickUpPeelTool");

    }
    public override void OnDragCancel()
    {
        base.OnDragCancel();
        ObserverManager.Notify("PutDownPeelTool");

    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();

        ObserverManager.AddListener("PeelComplete", ResetPosition);



    }
    public void ResetPosition()
    {
        gameObject.transform.DOMove(_defaultPosition, 0.5f);
    }
    private void OnDestroy()
    {
        ObserverManager.RemoveListener("PeelComplete", ResetPosition);
    }
    private void OnDisable()
    {
        ObserverManager.RemoveListener("PeelComplete", ResetPosition);
    }

    // Update is called once per frame
    void Update()
    {
        OnPeel?.Invoke();

    }

}
