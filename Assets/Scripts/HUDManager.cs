using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI pointsDisplay;
    [SerializeField]
    private TextMeshProUGUI popupDisplay;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private GameObject perkIconGroup;
    [SerializeField]
    private GameObject blankPerkIconPrefab;

    void Awake()
    {
        GameManager.Instance.PlayerController.PerksChanged += this.OnPlayerPerksChanged;
    }

    private void FixedUpdate()
    {
        pointsDisplay.text = GameManager.Instance.Points.ToString("C", GameManager.CurrencyFormat);
        popupDisplay.text = InteractionController.Instance.currentInteractable?.PopupText() ?? "";
    }

    private void OnPlayerPerksChanged(PlayerController.PerksChangedEventArgs args)
    {
        GameObject newPerkIcon = Instantiate(blankPerkIconPrefab, perkIconGroup.transform);
        GameObject juiceBoxPortionOfIcon = newPerkIcon.transform.Find($"JuiceBox").gameObject;
        RawImage juiceBoxImage = juiceBoxPortionOfIcon.GetComponent<RawImage>();
        juiceBoxImage.color = args.NewPerk.Color;
    }
}
