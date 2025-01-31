using System;
using UnityEngine;

public class ArmorCellUI : CellUI
{
    public BodyPart armorType;


    public override void SetIndex(int value)
    {
        base.SetIndex(value);
    }

   
}

[Serializable]

public enum BodyPart
{
    Body,
    Head
}
