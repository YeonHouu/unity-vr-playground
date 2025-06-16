using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlateType { Number, Symbol }

public class PlateValue : MonoBehaviour
{
    public PlateType plateType;
    public int numberValue;
    public char symbolValue;

    public int GetNumberValue()
    {
        if (plateType != PlateType.Number)
            throw new InvalidOperationException("숫자 플레이트 아님");
        return numberValue;
    }

    public char GetSymbolValue()
    {
        if (plateType != PlateType.Symbol)
            throw new InvalidOperationException("기호 플레이트 아님");
        return symbolValue;
    }
}
