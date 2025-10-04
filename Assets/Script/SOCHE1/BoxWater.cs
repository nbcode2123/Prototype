using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class BoxWater : MonoBehaviour, IDropStorage
{
    public GameObject _pivot;
    public GameObject _voinuoc;
    public ClayPot _clayPot;

    [SerializeField] private GameObject _waterInBox;
    [SerializeField] private bool isClose;
    void Start()
    {
        _waterInBox.SetActive(false);
        ObserverManager.AddListener("TurnOnWater", TurnOnWater);
        ObserverManager.AddListener("CloseWater", () => { isClose = true; });
        ObserverManager.AddListener("OpenWater", () => { isClose = false; });

        ObserverManager.AddListener<ClayPot>(ObserverEvent.ChangeDropStorage.ToString(), CheckChangeDrag);


    }
    public void CheckChangeDrag(ClayPot clayPot)
    {
        if (_clayPot != null && clayPot == _clayPot)
        {
            _clayPot = null;

        }

    }
    void Update()
    {
        if (isClose == false)
        {
            _waterInBox.SetActive(false);

        }
    }

    private void TurnOnWater()
    {
        if (isClose == true)
        {
            _waterInBox.SetActive(true);

        }
        if (_clayPot != null)
        {
            _clayPot.PutWaterToPot();
        }

    }
    private void OnDestroy()
    {
        ObserverManager.RemoveListener("TurnOnWater", TurnOnWater);
        ObserverManager.RemoveListener("CloseWater", () => { isClose = true; });
        ObserverManager.RemoveListener("OpenWater", () => { isClose = false; });
        ObserverManager.RemoveListener<ClayPot>(ObserverEvent.ChangeDropStorage.ToString(), CheckChangeDrag);


    }
    private void OnDisable()
    {
        ObserverManager.RemoveListener("TurnOnWater", TurnOnWater);
        ObserverManager.RemoveListener("CloseWater", () => { isClose = true; });
        ObserverManager.RemoveListener("OpenWater", () => { isClose = false; });
        ObserverManager.RemoveListener<ClayPot>(ObserverEvent.ChangeDropStorage.ToString(), CheckChangeDrag);


    }

    public void OnDrop(IDraggable draggable)
    {

        if (draggable is FoodIngredient foodIngredient)
        {
            if (foodIngredient.CookProcess.Count > 0 && foodIngredient.CookProcess.Peek() == IngredientProcess.Wash && isClose == true)
            {
                draggable.GetTransform().position = _pivot.transform.position;
                draggable.GetComponentInChildren<WashController>().RemoveSoil();
            }
            else
            {
                draggable.OnDragCancel();
            }
        }
        else if (draggable is ClayPot clayPot)
        {
            ObserverManager.Notify(ObserverEvent.ChangeDropStorage.ToString(), clayPot);
            _clayPot = clayPot;
            clayPot.transform.DOMove(_pivot.transform.position, 0.2f);
            clayPot.transform.DOScale(new Vector3(0.75f, 0.75f, 1f), 0.2f);
            clayPot.SetDefaultPos(_pivot.transform.position);
            clayPot._potObj.GetComponent<SpriteRenderer>().sortingOrder = _voinuoc.GetComponent<SpriteRenderer>().sortingOrder - 1;
            clayPot._potObj.GetComponent<SpriteRenderer>().sortingLayerName = _voinuoc.GetComponent<SpriteRenderer>().sortingLayerName;
            clayPot.isInGasStove = false;

        }
        else
        {
            draggable.OnDragCancel();
        }
    }

}
