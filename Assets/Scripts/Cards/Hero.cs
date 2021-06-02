using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Card
{
    public Faction faction;
    public int cost;
    public int honor;

    public virtual void PlayEffect() {
        Debug.Log("To be implemented in subclasses");
    }
}
