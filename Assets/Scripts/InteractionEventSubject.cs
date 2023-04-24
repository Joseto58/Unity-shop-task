using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Overkill for this simple implementation, but orderly and scalable
public class InteractionEventSubject : MonoBehaviour
{
  public System.Action<Interactions> Interaction;

  public void TriggerPatronAction(Interactions interaction) {
    Interaction?.Invoke(interaction);
  }
}

public enum Interactions
{
  Shop,
  Pot,
  Chest,
  Door
}