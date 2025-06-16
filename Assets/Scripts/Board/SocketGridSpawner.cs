using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocketGridSpawner : MonoBehaviour
{
    public GameObject socketPrefab;
    public Transform whiteboard;   
    public int countX = 20;          // ĥ�� ���ο� �� ���� (Plane�� z����)
    public int countY = 5;          // ĥ�� ���ο� �� ���� (Plane�� x����)
    public float spacing = 0.02f;
    public Vector3 boardOffset = Vector3.zero;

    private void Start()
    {
        SpawnAndResizeSockets();
    }

    public void SpawnAndResizeSockets()
    {
        var scale = whiteboard.localScale;
        float boardWidth = 10f * scale.z;  // Plane�� ����(z����)
        float boardHeight = 10f * scale.x; // Plane�� ����(x����)

        float socketWidth = (boardWidth - (countX - 1) * spacing) / countX;
        float socketHeight = (boardHeight - (countY - 1) * spacing) / countY;

        Vector3 boardCenter = whiteboard.position;
        Vector3 boardForward = whiteboard.forward; // ����(z)
        Vector3 boardRight = whiteboard.right;     // ����(x)

        for (int x = 0; x < countX; x++)
        {
            for (int y = 0; y < countY; y++)
            {
                float px = -boardWidth / 2 + socketWidth / 2 + x * (socketWidth + spacing);
                float py = -boardHeight / 2 + socketHeight / 2 + y * (socketHeight + spacing);

                Vector3 localOffset = boardForward * px + boardRight * py;
                Vector3 socketPos = boardCenter + localOffset + boardOffset;
                Quaternion socketRot = whiteboard.rotation;

                var socketObj = Instantiate(socketPrefab, socketPos, socketRot, whiteboard);

                var col = socketObj.GetComponent<BoxCollider>();
                if (col != null)
                {
                    col.size = new Vector3(Mathf.Abs(socketWidth), Mathf.Abs(socketHeight), Mathf.Abs(col.size.z));
                }
                else
                {
                    socketObj.transform.localScale = new Vector3(Mathf.Abs(socketWidth), Mathf.Abs(socketHeight), Mathf.Abs(socketObj.transform.localScale.z));
                }
            }
        }
    }
}
