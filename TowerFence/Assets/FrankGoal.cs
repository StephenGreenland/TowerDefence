using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ReGoap.Unity;

public class FrankGoal :  ReGoapGoal<KeyVales.GoapEnum,KeyVales.GoapVariables>
{
    protected override void Awake()
    {
        base.Awake();
        
        goal.Set(KeyVales.GoapEnum.BlowUpwalls, new KeyVales.GoapVariables() { Boolean = true });
    }
}
