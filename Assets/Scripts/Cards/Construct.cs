using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Construct : Card
{
    public Faction faction;
    public int cost;
    public int honor;

    public virtual void PlayEffect() {
        Debug.Log("To be implemented in subclasses");
    }
    public virtual void PerTurnEffect() {
        Debug.Log("To be implemented in subclasses");
    }
}
