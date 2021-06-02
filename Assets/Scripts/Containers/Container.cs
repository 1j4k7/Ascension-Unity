using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    public List<GameObject> cards;
    public float displayRatio;

    public static void Shuffle(List<GameObject> list) {
        System.Random rng = new System.Random();
        GameObject temp;
        int n = list.Count;
        for (int i = 0; i < n; i++) {
            int r = i + (int)(rng.NextDouble()*(n-i));
            temp = list[r];
            list[r] = list[i];
            list[i] = temp;
        }
    }

    void Awake() {
        cards = new List<GameObject>();
    }
    public virtual void Add(GameObject card, int position = -1) {
        //Debug.Log($"Adding to {this.name}");
        if (position == -1) {
            cards.Add(card);
        } else {
            cards.Insert(position, card);
        }
        card.GetComponent<Card>().SetParentContainer(this.gameObject);
        UpdateSelf();
    }
    // When moving from one place to another, remove first, then add
    public virtual void Remove(GameObject card) {
        //Debug.Log($"Removing from {this.name}");
        cards.Remove(card);
        card.GetComponent<Card>().SetParentContainer(null);
        UpdateSelf();
    }
    public virtual GameObject RemoveAt(int idx) {
        GameObject card = cards[idx];
        cards.RemoveAt(idx);
        card.GetComponent<Card>().SetParentContainer(null);
        UpdateSelf();
        return card;
    }
    public virtual void Replace(GameObject oldCard, GameObject newCard) {
        cards[cards.IndexOf(oldCard)] = newCard;
        oldCard.GetComponent<Card>().SetParentContainer(null);
        newCard.GetComponent<Card>().SetParentContainer(this.gameObject);
        UpdateSelf();
    }
    public virtual void Clear() {
        for (int i = 0; i < cards.Count; i++) {
            cards[i].transform.SetParent(null);
            cards[i].GetComponent<Card>().SetParentContainer(null);
            cards[i].SetActive(false);
        }
        cards.Clear();
    }
    public virtual void UpdateSelf() {
        for (int i = 0; i < cards.Count; i++) {
            cards[i].transform.SetParent(this.transform);
            cards[i].transform.SetSiblingIndex(i);
            float resizeFactor = this.GetComponent<BoxCollider2D>().size.y / cards[i].GetComponent<BoxCollider2D>().size.y * displayRatio;
            cards[i].transform.localScale = new Vector3(resizeFactor, resizeFactor, 1);
            cards[i].SetActive(true);
        }
    }
}
