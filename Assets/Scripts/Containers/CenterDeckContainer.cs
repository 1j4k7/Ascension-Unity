using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterDeckContainer : Container
{
    public void Shuffle() {
        Container.Shuffle(this.cards);
        UpdateSelf();
    }
}
