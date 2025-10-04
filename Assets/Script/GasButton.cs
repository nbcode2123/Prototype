using UnityEngine;

public class GasButton : MonoBehaviour, IClickable
{
    public GasStove _gasStove;

    public void OnClick()
    {
        _gasStove.TurnOnGas();

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
