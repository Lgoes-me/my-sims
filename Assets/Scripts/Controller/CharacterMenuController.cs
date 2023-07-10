using Domain;
using Extensions;
using TMPro;
using UnityEngine;
using View;

public class CharacterMenuController : MonoBehaviour
{
    [field: SerializeField] private TextMeshProUGUI NameText { get; set; }
    [field: SerializeField] private TextMeshProUGUI CurrentAction { get; set; }
    [field: SerializeField] private Transform BarViewContent { get; set; }
    [field: SerializeField] private BarView BarViewPrefab { get; set; }

    private Character CurrentCharacter { get; set; }
    
    public void Show(Character character)
    {
        CurrentCharacter = character;
        CurrentCharacter.OnCurrentInteractionChanged += UpdateCurrentAction;
        NameText.SetText(character.Name);
        
        CurrentAction.SetText(character.CurrentInteraction?.Name ?? " ");

        BarViewContent.Clear();

        foreach (var (_, motive) in character.Motives)
        {
            Instantiate(BarViewPrefab, BarViewContent).Init(motive);
        }
    }

    private void UpdateCurrentAction()
    {
        CurrentAction.SetText(CurrentCharacter.CurrentInteraction?.Name ?? " ");
    }
}