using System.Collections;
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

    private Coroutine UpdateCoroutine { get; set; }

    public void Show(Character character)
    {
        NameText.SetText(character.Name);

        BarViewContent.Clear();

        foreach (var (_, motive) in character.Motives)
        {
            Instantiate(BarControllerPrefab, BarViewContent).Init(motive);
        }

        if (UpdateCoroutine != null)
            StopCoroutine(UpdateCoroutine);

        UpdateCoroutine = StartCoroutine(UpdateCurrentAction(character));
    }

    private IEnumerator UpdateCurrentAction(Character character)
    {
        while (gameObject.activeInHierarchy)
        {
            CurrentAction.SetText(character.CurrentInteraction?.Advertisement.Name ?? string.Empty);

            yield return new WaitUntil(() =>
                CurrentAction.text != (character.CurrentInteraction?.Advertisement.Name ?? string.Empty));
        }
    }
}