using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardSpellSpellCard", menuName = "Card/Spell/SpellCard")]
public class SOCSpell : SOCard
{
    public List<SOCSEffect> allEffects = new List<SOCSEffect>();
    public List<SOCSRequirement> allRequirement = new List<SOCSRequirement>();

    public void PlayCard()
    {
        foreach (SOCSEffect effect in allEffects)
        {
            effect.ApplyEffect();
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
