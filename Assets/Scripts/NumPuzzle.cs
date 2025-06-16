using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class NumPuzzle : MonoBehaviour
{
    public XRSocketInteractor[] sockets;
    [SerializeField] GameObject[] attachedPlates = new GameObject[5];

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
        // 소켓이 비면 결과 지움
        resultText.text = "";
    }

    private void TryCheckAnswer()
    {
        // 5개 모두 들어갔는지 확인
        foreach (var plate in attachedPlates)
        {
            resultText.text = "";
            if (plate == null) return;
        }

        var pv0 = attachedPlates[0].GetComponent<PlateValue>();
        var pv1 = attachedPlates[1].GetComponent<PlateValue>();
        var pv2 = attachedPlates[2].GetComponent<PlateValue>();
        var pv3 = attachedPlates[3].GetComponent<PlateValue>();
        var pv4 = attachedPlates[4].GetComponent<PlateValue>();

        if (pv0.plateType != PlateType.Number) return;
        if (pv1.plateType != PlateType.Symbol) return;
        if (pv2.plateType != PlateType.Number) return;
        if (pv3.plateType != PlateType.Symbol) return;
        if (pv4.plateType != PlateType.Number) return;

        // 1, 3소켓 속 플레이트의 symbol 값이 올바른지 체크
        if (pv1.symbolValue != '+') return;
        if (pv3.symbolValue != '=') return;

        int answer = pv0.numberValue + pv2.numberValue;
        if (answer == pv4.numberValue)
        {
            resultText.text = "<color=Green>정답!</color>";
        }
        else
        {
            resultText.text = "<color=red>오답!</color>";
        }
    }
}
