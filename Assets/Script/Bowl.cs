using UnityEngine;

public class Bowl : MonoBehaviour, IDropStorage
{
    public Transform _pivotTransform;
    public FinalIngredient _finalIngredient;

    public IDraggable _currentDraggable { get; set; }

    public void EmptySlot()
    {

    }

    public void OnDrop(IDraggable draggable)
    {
        if (_currentDraggable == null)
        {
            if (draggable is FinalIngredient finalIngredient)
            {
                draggable.GetTransform().position = _pivotTransform.position;
                _currentDraggable = draggable;
                _currentDraggable.GetComponent<Collider2D>().enabled = false;
                _currentDraggable.gameObject.layer = LayerMask.NameToLayer("Default");
                ObserverManager.Notify("ChangeDropStorage", finalIngredient.foodIngredient);

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
