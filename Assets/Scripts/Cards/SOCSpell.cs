using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSpellSpellCard", menuName = "Card/Spell/SpellCard")]
public class SOCSpell : SOCard
{
    public List<SOC_Effect> allEffects = new List<SOC_Effect>();
    public List<SOC_Requirement> allRequirement = new List<SOC_Requirement>();

    public void PlayCard()
    {
        foreach (SOC_Effect effect in allEffects)
        {
            effect.ApplyEffect(null);
        }
    }

    public bool Requirement()
    {
        bool current = true;
        foreach (SOC_Requirement req in allRequirement)
        {
            if (!req.ApplyRequirement(null))
            {
                current = false;
            }
        }
        return current;
    }
}
