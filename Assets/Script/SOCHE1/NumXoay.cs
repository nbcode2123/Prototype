using UnityEngine;

public class NumXoay : IDraggable
{

    public override void OnDragStart()
    {
        ObserverManager.Notify("TurnOnWater");
    }
    public override void OnDrag(Vector3 position)
    {
    }


}
