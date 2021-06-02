using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCardsContainer : Container
{
    public override void Remove(GameObject card) {
        //Debug.Log($"Removing from {this.name}");
        GameObject newCard = Instantiate(card, card.transform, true);
        Replace(card, newCard);
    }
}
