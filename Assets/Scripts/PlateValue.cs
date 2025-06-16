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
            throw new InvalidOperationException("���� �÷���Ʈ �ƴ�");
        return numberValue;
    }

    public char GetSymbolValue()
    {
        if (plateType != PlateType.Symbol)
            throw new InvalidOperationException("��ȣ �÷���Ʈ �ƴ�");
        return symbolValue;
    }
}
