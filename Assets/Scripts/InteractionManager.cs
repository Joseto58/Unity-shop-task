using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class InteractionManager : MonoBehaviour
{

  [SerializeField] GameObject _interactionPanel;
  [SerializeField]TextMeshProUGUI _interactionPanelText;
  InteractionEventSubject _interactionSubject;


  public void InteractionObserver(Interactions interaction) {
    switch (interaction) {
      case
        Interactions.Shop:
        _interactionPanelText.text = "Sorry buddy, I'm still setting up shop.\nGo for a walk or something, I'll be done soon.";
        _interactionPanel.SetActive(true);
        print("Shop interaction");

        break;
      case
        Interactions.Pot:
        _interactionPanelText.text = "I suddenly feel an enourmous urge to break this pot.\nMy character controller could use an attack or two...";
        _interactionPanel.SetActive(true);
        print("Pot interaction");
        break;
      case
        Interactions.Chest:
        _interactionPanelText.text = "It's an empty chest. How exciting!";
        _interactionPanel.SetActive(true);
        print("Chest interaction");
        break;
      case
        Interactions.Door:
        _interactionPanelText.text = "It's locked shut, I wonder what's on the other side...";
        _interactionPanel.SetActive(true);
        print("Door interaction");
        break;
      default:
        Debug.Log("Unexpected Interaction!");
        break;
    }
  }

  private void Awake() {
    _interactionSubject = GetComponent<InteractionEventSubject>();
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
