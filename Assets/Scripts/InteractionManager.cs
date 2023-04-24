using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InteractionManager : MonoBehaviour
{

  InteractionEventSubject _interactionSubject;

  public void InteractionObserver(Interactions interaction) {
    switch (interaction) {
      case
        Interactions.Shop:
        print("Shop interaction");
        break;
      case
        Interactions.Pot:
        print("Pot interaction");
        break;
      case
        Interactions.Chest:
        print("Chest interaction");
        break;
      case
        Interactions.Door:
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
