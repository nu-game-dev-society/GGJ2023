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
    [SerializeField]
    private Image deathOverlay;

    void Start()
    {
        GameManager.Instance.PlayerController.PerksChanged += this.OnPlayerPerksChanged;
    }

    private void FixedUpdate()
    {
        pointsDisplay.text = GameManager.Instance.Points.ToString("C", GameManager.CurrencyFormat);
        popupDisplay.text = InteractionController.Instance.currentInteractable?.PopupText() ?? "";
        deathOverlay.color = new Color(deathOverlay.color.r, deathOverlay.color.g, deathOverlay.color.b, (1f - (GameManager.Instance.PlayerController.GetHealth() / GameManager.Instance.PlayerController.maxHealth)) * 0.5f);
    }

    private void OnPlayerPerksChanged(PlayerController.PerksChangedEventArgs args)
    {
        GameObject newPerkIcon = Instantiate(blankPerkIconPrefab, perkIconGroup.transform);
        GameObject juiceBoxPortionOfIcon = newPerkIcon.transform.Find($"JuiceBox").gameObject;
        RawImage juiceBoxImage = juiceBoxPortionOfIcon.GetComponent<RawImage>();
        juiceBoxImage.color = args.NewPerk.Color;
    }
}
