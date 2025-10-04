using UnityEngine;

public class TrashStorage : MonoBehaviour, IDropStorage
{
    public IDraggable _currentDraggable { get; set; }

    public void EmptySlot()
    {
    }

    public void OnDrop(IDraggable draggable)
    {
        if (draggable is SkinRemove)
        {
            draggable.GetTransform().position = gameObject.transform.position;
            draggable.SetDefaultPos(gameObject.transform.position);
            Destroy(draggable.gameObject);
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
