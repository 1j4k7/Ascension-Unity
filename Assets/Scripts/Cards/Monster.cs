using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Card
{
    public int power;

    public virtual void RewardOnDefeat() {
        Debug.Log("To be implemented in subclasses");
    }
}
