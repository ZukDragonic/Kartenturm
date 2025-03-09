using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSpellSpellCard", menuName = "Card/Spell/SpellCard")]
public class SOCSpell : SOCard
{
    public List<SOC_Effect> allEffects = new List<SOC_Effect>();
    public List<SOCSRequirement> allRequirement = new List<SOCSRequirement>();

    public void PlayCard()
    {
        foreach (SOC_Effect effect in allEffects)
        {
            effect.ApplyEffect(null, null);
        }
    }

    public bool Requirement()
    {
        bool current = true;
        foreach (SOCSRequirement req in allRequirement)
        {
            if (req.ApplyRequirement())
            {
                current = false;
            }
        }
        return current;
    }
}
