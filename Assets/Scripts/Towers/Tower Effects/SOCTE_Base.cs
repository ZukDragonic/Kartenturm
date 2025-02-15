using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CardTowerEffectBase", menuName = "Card/TowerEffect/Base")]
public class SOCTE_Base : ScriptableObject
{
    public virtual void PassiveEffect(projectileScript script)
    {
    }
    public virtual void OnDestroyEffect(projectileScript script)
    {
    }
    public virtual void OnSummonEffect(baseTowerScript script)
    {
    }
    public virtual void OnPDestroyEffect(baseTowerScript script)
    {
    }
    public virtual void OnPHitEffect(baseTowerScript script)
    {
    }
}