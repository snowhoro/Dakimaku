using UnityEngine;
using System.Collections;

public class BaseStatusEffect : ScriptableObject
{

    public string _name;
    public string _description;

    public Types.StatusEffects _statusID;

    public float _power;
    public int _duration;

    public GameObject _prefabFX;

    public virtual void Effect(BaseCharacter affected)
    {
        RunFX(affected);
        Damage(affected);
        Duration();
        if (_duration == 0)
            RemoveEffect(affected);    
    }
    public virtual void Damage(BaseCharacter affected)
    {
         
    }
    public virtual void RunFX(BaseCharacter affected)
    {
        if(_prefabFX != null)
            Destroy(Instantiate(_prefabFX, affected.transform.position, Quaternion.identity), 2.0f);
    }
    public virtual void Duration()
    {
        _duration--;
    }
    public virtual void RemoveEffect(BaseCharacter affected)
    {
        affected._statusEffects.Remove(this);
    }
    public virtual void AddEffect(BaseCharacter affected)
    {

    }
}