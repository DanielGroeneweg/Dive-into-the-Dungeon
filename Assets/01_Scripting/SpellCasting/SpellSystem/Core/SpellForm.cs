using System.Collections.Generic;
using UnityEngine;
public abstract class SpellForm : SpellComponent
{
    [Tooltip("These effects and amplifiers are allowed to come after this component")]
    public List<SpellComponent> allowedFollowUps;
    public abstract void Execute(SpellContext context);
}