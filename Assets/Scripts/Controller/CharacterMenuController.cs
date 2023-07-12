using Domain;
using Extensions;
using TMPro;
using UnityEngine;
using Controller;

public class CharacterMenuController : MonoBehaviour
{
    [field: SerializeField] private TextMeshProUGUI NameText { get; set; }
    [field: SerializeField] private TextMeshProUGUI CurrentAction { get; set; }
    [field: SerializeField] private Transform BarViewContent { get; set; }
    [field: SerializeField] private BarController BarControllerPrefab { get; set; }

    private Character CurrentCharacter { get; set; }
    
    public void Show(Character character)
    {
        CurrentCharacter = character;
        NameText.SetText(character.Name);
        
        CurrentAction.SetText(character.CurrentAdvertisement?.Name ?? " ");

        BarViewContent.Clear();

        foreach (var (_, motive) in character.Motives)
        {
            Instantiate(BarControllerPrefab, BarViewContent).Init(motive);
        }
    }

    private void UpdateCurrentAction()
    {
        CurrentAction.SetText(CurrentCharacter.CurrentAdvertisement?.Name ?? " ");
    }
}