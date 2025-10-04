using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class CuttingBoard : MonoBehaviour, IDropStorage
{
    [SerializeField] private FoodIngredient _currentIngredientInBoard;
    [SerializeField] private Transform _pivotSurface;
    [SerializeField] private Knife _knife;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _currentIngredientInBoard = null;
        ObserverManager.AddListener("SliceComplete", () => { _knife.gameObject.SetActive(true); _knife.ResetStatue(); });
        ObserverManager.AddListener("CutComplete", () => { _knife.gameObject.SetActive(true); _knife.ResetStatue(); });
        ObserverManager.AddListener<IDraggable>("ChangeDropStorage", CheckDraggable);






    }
    public void CheckDraggable(IDraggable draggable)
    {
        if (_currentIngredientInBoard != null)
        {
            if (_currentIngredientInBoard == draggable)
            {
                _currentIngredientInBoard = null;
            }
        }
    }
    private void OnDestroy()
    {

        ObserverManager.RemoveListener("SliceComplete", () => { _knife.gameObject.SetActive(true); _knife.ResetStatue(); });
        ObserverManager.RemoveListener("CutComplete", () => { _knife.gameObject.SetActive(true); _knife.ResetStatue(); });


    }
    private void OnDisable()
    {

        ObserverManager.RemoveListener("SliceComplete", () => { _knife.gameObject.SetActive(true); _knife.ResetStatue(); });
        ObserverManager.RemoveListener("CutComplete", () => { _knife.gameObject.SetActive(true); _knife.ResetStatue(); });


    }

    // Update is called once per frame
    void Update()
    {



    }




    public void OnDrop(IDraggable draggable)
    {

        if (_currentIngredientInBoard == null)
        {
            if (draggable is FoodIngredient foodIngredient)
            {
                if (foodIngredient.CookProcess.Count == 0)
                {
                    draggable.OnDragCancel();
                }
                else
                {
                    _currentIngredientInBoard = foodIngredient;
                    _currentIngredientInBoard.GetComponent<Collider2D>().enabled = false;
                    _currentIngredientInBoard.gameObject.layer = LayerMask.NameToLayer("Default");
                    _currentIngredientInBoard.transform.position = _pivotSurface.position;
                    _currentIngredientInBoard.SetDefaultPos(_pivotSurface.position);
                    _currentIngredientInBoard.LayDown();

                    PeelController peelController = _currentIngredientInBoard.GetComponentInChildren<PeelController>();
                    if (peelController != null)
                    {
                        peelController._isOnCutBroad = true;
                    }
                    CutController cutController = _currentIngredientInBoard.GetComponentInChildren<CutController>();
                    if (cutController != null)
                    {
                        cutController._isOnCutBroad = true;
                    }
                    SliceController sliceController = _currentIngredientInBoard.GetComponentInChildren<SliceController>();
                    if (sliceController != null)
                    {
                        sliceController._isOnCutBroad = true;
                        Debug.Log(draggable.name + 1);
                    }
                    BocVo bocvo = _currentIngredientInBoard.GetComponentInChildren<BocVo>();
                    if (bocvo != null)
                    {
                        bocvo._isOnCutBroad = true;
                    }
                }



            }

            else
            {
                draggable.OnDragCancel();
            }
        }
        else if (_currentIngredientInBoard != null)
        {
            if (draggable is Knife)
            {
                if (_currentIngredientInBoard.CookProcess.Peek() == IngredientProcess.Cut)
                {
                    CutController cutController = _currentIngredientInBoard.GetComponentInChildren<CutController>();
                    cutController.CutProcess();
                    _knife.gameObject.SetActive(false);

                }
                if (_currentIngredientInBoard.CookProcess.Peek() == IngredientProcess.Slice)
                {
                    SliceController sliceController = _currentIngredientInBoard.GetComponentInChildren<SliceController>();
                    sliceController.SliceProcess();
                    _knife.gameObject.SetActive(false);


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
        else
        {
            draggable.OnDragCancel();
        }


    }


    public void EmptySlot()
    {
        _currentIngredientInBoard = null;

    }
}
