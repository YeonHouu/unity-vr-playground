using System.Linq;
using UnityEngine;

public class Marker : MonoBehaviour
{
    [SerializeField] private Transform tip;
    [SerializeField] private int penSize = 5;

    private Renderer _renderer;
    private Color[] colors;
    private float tipHeight;

    private RaycastHit touch;
    private Board board;
    private Vector2 touchPos, lastTouchPos;
    private bool touchedLastFrame;
    private Quaternion lastTouchRot;

    private void Start()
    {
        _renderer = tip.GetComponent<Renderer>();
        colors = Enumerable.Repeat(_renderer.material.color, penSize * penSize).ToArray();
        tipHeight = tip.localScale.y;
    }

    private void Update()
    {
        Draw();
    }

    private void Draw()
    {
        if (Physics.Raycast(tip.position, transform.up, out touch, tipHeight))
        {
            if (touch.transform.CompareTag("Board"))
            {
                if (board == null)
                {
                    board = touch.transform.GetComponent<Board>();
                }

                touchPos = new Vector2(touch.textureCoord.x, touch.textureCoord.y);

                var x = (int)(touchPos.x * board.textureSize.x - (penSize / 2));
                var y = (int)(touchPos.y * board.textureSize.y - (penSize / 2));

                if (y < 0 || y > board.textureSize.y || x < 0 || x > board.textureSize.x)
                    return;

                if (touchedLastFrame)
                {
                    board.texture.SetPixels(x, y, penSize, penSize, colors);

                    for (float f = 0.01f; f < 1.00f; f += 0.001f)
                    {
                        var lerpX = (int)Mathf.Lerp(lastTouchPos.x, x, f);
                        var lerpY = (int)Mathf.Lerp(lastTouchPos.y, y, f);

                        if (lerpX >= 0 && lerpX <= board.textureSize.x - penSize &&
                            lerpY >= 0 && lerpY <= board.textureSize.y - penSize)
                        {
                            board.texture.SetPixels(lerpX, lerpY, penSize, penSize, colors);
                        }
                    }
                    transform.rotation = lastTouchRot;
                    board.texture.Apply();
                }
                lastTouchPos = new Vector2(x, y);
                lastTouchRot = transform.rotation;
                touchedLastFrame = true;
                return;
            }
        }

        board = null;
        touchedLastFrame = false;
    }
}
