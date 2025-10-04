using UnityEngine;

public class Cong : MonoBehaviour, IDropStorage
{
    public IDraggable _currentDraggable { get; set; }

    public void EmptySlot()
    {
    }

    public void OnDrop(IDraggable draggable)
    {
        if (draggable is FoodIngredient)
        {
            draggable.OnDragCancel();

        }
        else if (draggable.GetGameObject().CompareTag("CloseWater") || draggable is NapCong napCong)
        {
            draggable.GetTransform().position = gameObject.transform.position;
            draggable.SetDefaultPos(gameObject.transform.position);
            ObserverManager.Notify("CloseWater");
        }
        else
        {
            draggable.OnDragCancel();
        }

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
