using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class InteractionManager : MonoBehaviour
{
  [SerializeField] PlayerController _playerController;
  [SerializeField] GameObject _interactionPanel;
  [SerializeField] TextMeshProUGUI _interactionPanelText;
  [SerializeField] Button _closeButton;
  InteractionEventSubject _interactionSubject;
  //When switching over to UI, prevent instant pressing
  bool _requireNewPress = true;

  //Temporary Chest Outfit interaction
  bool _outfitChanged;
  [SerializeField] GameObject _closedChest;

  private void Awake() {
    _interactionSubject = GetComponent<InteractionEventSubject>();
  }

  //I don't like switching INput action maps here, but for this short implementation I'll let it fly.
  public void CloseInteractionPanel() {
    if (_requireNewPress) {
    _requireNewPress = false;
      return;
    }
    _interactionPanel.SetActive(false);
    _playerController.SwitchActionMap(ActionMaps.Movement);
    _requireNewPress = true;
  }

  //Placeholder chest interaction since I have not implementedd the chest feature.
  void ClosedChestInteraction() {
    _playerController.ChangeOutfit();
    _outfitChanged = true;
    _closedChest.GetComponent<SpriteRenderer>().enabled = false;
    
  }

  public void InteractionObserver(Interactions interaction) {
    switch (interaction) {
      case
        Interactions.Shop:
        if (_outfitChanged) {
        _interactionPanelText.text = "You do realize that what you're doing is straight up theft right?";
        } else {
        _interactionPanelText.text = "Sorry buddy, I'm still setting up shop.\nGo for a walk or something, I'll be done soon.";
        }
        _interactionPanel.SetActive(true);
        _closeButton.Select();
        print("Shop interaction");

        break;
      case
        Interactions.Pot:
        _interactionPanelText.text = "I suddenly feel an enourmous urge to break this pot.\nMy character controller could use an attack or two...";
        _interactionPanel.SetActive(true);
        _closeButton.Select();
        print("Pot interaction");
        break;
      case
        Interactions.Chest:
        _interactionPanelText.text = "It's an empty chest. How exciting!";
        _interactionPanel.SetActive(true);
        _closeButton.Select();
        print("Chest interaction");
        break;
      case
        Interactions.Door:
        _interactionPanelText.text = "It's locked shut, I wonder what's on the other side...";
        _interactionPanel.SetActive(true);
        _closeButton.Select();
        print("Door interaction");
        break;
      case
        Interactions.ClosedChest:
        _interactionPanelText.text = "Hey! There's some clothes in this chest\nThe wig feels a bit itchy ...";
        ClosedChestInteraction();
        _interactionPanel.SetActive(true);
        _closeButton.Select();
        print("Chest interaction");
        break;
      default:
        Debug.Log("Unexpected Interaction!");
        break;
    }
  }


  private void OnEnable() {
    if (_interactionSubject != null) {
      _interactionSubject.Interaction += InteractionObserver;
    }
  }

  private void OnDisable() {
    if (_interactionSubject != null) {
      _interactionSubject.Interaction -= InteractionObserver;
    }
  }



}
