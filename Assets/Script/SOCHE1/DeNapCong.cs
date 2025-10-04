using UnityEngine;

public class DeNapCong : MonoBehaviour, IDropStorage
{
    public IDraggable _currentDraggable { get; set; }

    public void EmptySlot()
    {
    }

    public void OnDrop(IDraggable draggable)
    {
        if (draggable.GetGameObject().CompareTag("CloseWater"))
        {
            draggable.GetTransform().position = gameObject.transform.position;
            draggable.SetDefaultPos(gameObject.transform.position);
            ObserverManager.Notify("OpenWater");
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
