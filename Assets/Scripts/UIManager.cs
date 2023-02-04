using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private PlayerController playerController;

    [SerializeField]
    private GameObject perkIconGroup;

    [SerializeField]
    private GameObject blankPerkIconPrefab;

    void Awake()
    {
        this.playerController.PerksChanged += this.OnPlayerPerksChanged;
    }

    void Update()
    {
        
    }

    private void OnPlayerPerksChanged(PlayerController.PerksChangedEventArgs args)
    {
        GameObject newPerkIcon = Instantiate(blankPerkIconPrefab, perkIconGroup.transform);
        GameObject juiceBoxPortionOfIcon = newPerkIcon.transform.Find($"JuiceBox").gameObject;
        RawImage juiceBoxImage = juiceBoxPortionOfIcon.GetComponent<RawImage>();
        juiceBoxImage.color = args.NewPerk.Color;
    }
}
