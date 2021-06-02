using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/******************************************************************
* Container subclass for containers that will be updated per player
******************************************************************/
public class PlayerContainer : Container
{
    void Awake() {
        cards = new List<GameObject>();
    }

    public override void Add(GameObject card, int position = -1)
    {
        base.Add(card, position);
    }
    public override void Remove(GameObject card)
    {
        base.Remove(card);
    }
    public override GameObject RemoveAt(int idx)
    {
        return base.RemoveAt(idx);
    }
    public override void Replace(GameObject oldCard, GameObject newCard)
    {
        base.Replace(oldCard, newCard);
    }
    public void UpdateSelf(List<GameObject> newList) {
        // Remove currently showing cards first
        for (int i = 0; i < cards.Count; i++) {
            cards[i].transform.SetParent(null);
            cards[i].SetActive(false);
        }
        this.cards = newList;
        for (int i = 0; i < cards.Count; i++) {
            cards[i].GetComponent<Card>().SetParentContainer(this.gameObject);
        }
        this.UpdateSelf();
    }
}
