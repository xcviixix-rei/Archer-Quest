using System.Collections;
using UnityEngine;
using UnityEngine.Events;
public class CharacterEvents
{
    public static UnityAction<GameObject, int> OnCharacterTookDamage;
    public static UnityAction<GameObject, int> OnCharacterHealed;
}