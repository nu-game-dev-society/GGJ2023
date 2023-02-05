using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using UnityEngine.InputSystem;

public class HUDManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI roundDisplay;
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
    [SerializeField]
    private GameObject pauseScreen;

    void Start()
    {
        GameManager.Instance.PlayerController.PerksChanged += this.OnPlayerPerksChanged;
        GameManager.Instance.RoundChanged += this.OnRoundChanged;
        GameManager.Instance.Controls.controls.Gameplay.Pause.performed += Pause;
        this.OnRoundChanged();
    }

    private void OnRoundChanged()
    {
        this.roundDisplay.text = $"Round {ToRoman(GameManager.Instance.CurrentRound)}";
    }

    // 100% nicked from here: https://stackoverflow.com/a/11749642
    private static string ToRoman(int number)
    {
        if (number < 1) return string.Empty;
        if (number >= 1000) return "M" + ToRoman(number - 1000);
        if (number >= 900) return "CM" + ToRoman(number - 900);
        if (number >= 500) return "D" + ToRoman(number - 500);
        if (number >= 400) return "CD" + ToRoman(number - 400);
        if (number >= 100) return "C" + ToRoman(number - 100);
        if (number >= 90) return "XC" + ToRoman(number - 90);
        if (number >= 50) return "L" + ToRoman(number - 50);
        if (number >= 40) return "XL" + ToRoman(number - 40);
        if (number >= 10) return "X" + ToRoman(number - 10);
        if (number >= 9) return "IX" + ToRoman(number - 9);
        if (number >= 5) return "V" + ToRoman(number - 5);
        if (number >= 4) return "IV" + ToRoman(number - 4);
        if (number >= 1) return "I" + ToRoman(number - 1);
        return "WTF";
    }

    private void FixedUpdate()
    {
        pointsDisplay.text = GameManager.Instance.Points.ToString("C", GameManager.CurrencyFormat);
        popupDisplay.text = InteractionController.Instance.currentInteractable?.PopupText() ?? "";
        deathOverlay.color = new Color(deathOverlay.color.r, deathOverlay.color.g, deathOverlay.color.b, (1f - (GameManager.Instance.PlayerController.GetHealth() / GameManager.Instance.PlayerController.maxHealth)) * 0.5f);
        roundDisplay.color = GameManager.Instance.IsChangingRounds ? Color.red : Color.white;
    }

    private void OnPlayerPerksChanged(PlayerController.PerksChangedEventArgs args)
    {
        if (args.NewPerk == null)
        {
            for (int i = 0; i < this.perkIconGroup.transform.childCount; i++)
            {
                Destroy(this.perkIconGroup.transform.GetChild(i).gameObject);
            }
            return;
        }

        GameObject newPerkIcon = Instantiate(blankPerkIconPrefab, perkIconGroup.transform);
        GameObject juiceBoxPortionOfIcon = newPerkIcon.transform.Find($"JuiceBox").gameObject;
        RawImage juiceBoxImage = juiceBoxPortionOfIcon.GetComponent<RawImage>();
        juiceBoxImage.color = args.NewPerk.Color;
    }

    public void Pause(InputAction.CallbackContext ctx)
    {
        if (ctx.phase == InputActionPhase.Performed)
        {
            pauseScreen.SetActive(!pauseScreen.activeSelf);

            GameManager.Instance.PlayerController.enabled = !pauseScreen.activeSelf;
            InteractionController.Instance.enabled = !pauseScreen.activeSelf;
            Time.timeScale = pauseScreen.activeSelf ? 0 : 1;
        }
    }
}
