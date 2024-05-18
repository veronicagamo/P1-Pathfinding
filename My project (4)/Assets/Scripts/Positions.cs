using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Positions 
{
    public List<CharacterPosittion> positions;

    public Positions()
    {
        positions = new List<CharacterPosittion>();
    }
}
