using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XeronDukeOfLies : Monster
{
    public override void RewardOnDefeat()
    {
        // Gain 3 honor. Take a card at random from each opponent's hand and add that card to your hand (make sure that hands are draw at the end of a turn).
    }
}
