using System;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NumPuzzle : MonoBehaviour
{
    public XRSocketInteractor[] sockets;
    [SerializeField] GameObject[] attachedPlates = new GameObject[6];

    public TextMeshProUGUI resultText;

    private void Awake()
    {
        for (int i = 0; i < sockets.Length; i++)
        {
            int index = i;
            sockets[i].selectEntered.AddListener(args => OnPlateInserted(index, args));
            sockets[i].selectExited.AddListener(args => OnPlateRemoved(index, args));
        }
    }

    private void OnPlateInserted(int index, SelectEnterEventArgs args)
    {
        attachedPlates[index] = args.interactableObject.transform.gameObject;
        TryCheckAnswer();
    }

    private void OnPlateRemoved(int index, SelectExitEventArgs args)
    {
        attachedPlates[index] = null;
        // ������ ��� ��� ����
        resultText.text = "";
    }

    private void TryCheckAnswer()
    {
        // 0~3�� ������ ������ �÷���Ʈ �־�� ��
        for (int i = 0; i < 4; i++)
            if (attachedPlates[i] == null)
                return;

        // ���� ���
        var pv0 = attachedPlates[0].GetComponent<PlateValue>();
        var pv1 = attachedPlates[1].GetComponent<PlateValue>();
        var pv2 = attachedPlates[2].GetComponent<PlateValue>();
        var pv3 = attachedPlates[3].GetComponent<PlateValue>();

        if (pv0.plateType != PlateType.Number) return;
        if (pv1.plateType != PlateType.Symbol || pv1.symbolValue != '+') return;
        if (pv2.plateType != PlateType.Number) return;
        if (pv3.plateType != PlateType.Symbol || pv3.symbolValue != '=') return;

        int answer = pv0.numberValue + pv2.numberValue;
        string answerStr = answer.ToString();

        // ������ 1�ڸ� ��
        if (answerStr.Length == 1)
        {
            bool slot5 = attachedPlates[4] != null;
            bool slot6 = attachedPlates[5] != null;
            
            // �� �� ������ ���� ó��
            if (slot5 && slot6)
            {
                resultText.text = "<color=red>����!</color>";
                return;
            }
            // �� �� �� ���� = ���� �Է� �� ��
            if (!slot5 && !slot6)
            {
                resultText.text = "";
                return;
            }

            int inputValue = -1;
            if (slot5)
            {
                inputValue = attachedPlates[4].GetComponent<PlateValue>().numberValue;
            }
            else if (slot6)
            {
                inputValue = attachedPlates[5].GetComponent<PlateValue>().numberValue;
            }

            resultText.text = (inputValue == answer)
                ? "<color=green>����!</color>"
                : "<color=red>����!</color>";
        }
        // ������ 2�ڸ� ��
        else if (answerStr.Length == 2)
        {
            // 5, 6�� �� �� �÷���Ʈ�� �� �־�� ��
            if (attachedPlates[4] == null || attachedPlates[5] == null) return;
            
            var pv4 = attachedPlates[4].GetComponent<PlateValue>();
            var pv5 = attachedPlates[5].GetComponent<PlateValue>();
            if (pv4.plateType != PlateType.Number) return;
            if (pv5.plateType != PlateType.Number) return;

            int tens = answerStr[0] - '0';
            int ones = answerStr[1] - '0';

            resultText.text = (pv4.numberValue == tens && pv5.numberValue == ones)
                ? "<color=green>����!</color>"
                : "<color=red>����!</color>";
        }
    }
}
