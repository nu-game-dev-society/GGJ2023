using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pointsDisplay;
    [SerializeField]
    private TextMeshProUGUI popupDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        pointsDisplay.text = GameManager.Instance.Points.ToString("C", GameManager.CurrencyFormat);
        popupDisplay.text = InteractionController.Instance.currentInteractable != null ? InteractionController.Instance.currentInteractable.PopupText() : "";
    }
}
