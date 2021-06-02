using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCourierX99 : Construct
{
    public override void PlayEffect()
    {
        // Once per turn, when you acquire another Mechana Construct, you may put it directly into play.
        // Not sure if this has to be the first one or if player gets to choose.
    }
    public override void PerTurnEffect() {
    }
}
