using Unity.VisualScripting;
using UnityEngine;

public class VoiNuoc : MonoBehaviour
{
    [SerializeField] private GameObject _water;
    [SerializeField] private bool isTurnOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _water.SetActive(false);
        ObserverManager.AddListener("TurnOnWater", WaterController);

    }
    private void OnDestroy()
    {
        ObserverManager.AddListener("TurnOnWater", WaterController);

    }
    private void OnDisable()
    {
        ObserverManager.AddListener("TurnOnWater", WaterController);

    }
    private void WaterController()
    {
        if (isTurnOn == false)
        {
            _water.SetActive(true);
            isTurnOn = true;
            return;


        }
        else
        {
            _water.SetActive(false);
            isTurnOn = false;
            return;


        }

    }



    // Update is called once per frame
    void Update()
    {

    }
}
