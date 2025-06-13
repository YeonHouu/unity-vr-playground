using System.Linq;
using UnityEngine;

public class Marker2 : MonoBehaviour
{
    [SerializeField] private Transform tip;
    [SerializeField] private int penSize = 30;

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
        if (colors == null || colors.Length != penSize * penSize)
        {
            colors = Enumerable.Repeat(_renderer.material.color, penSize * penSize).ToArray();
        }

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

                int texWidth = board.texture.width;
                int texHeight = board.texture.height;
                int x = (int)(touchPos.x * texWidth - (penSize / 2));
                int y = (int)(touchPos.y * texHeight - (penSize / 2));

                if (x < 0 || x > texWidth - penSize || y < 0 || y > texHeight - penSize)
                    return;

                if (touchedLastFrame)
                {
                    board.texture.SetPixels(x, y, penSize, penSize, colors);

                    for (float f = 0.01f; f < 1.00f; f += 0.01f)
                    {
                        int lerpX = (int)Mathf.Lerp(lastTouchPos.x, x, f);
                        int lerpY = (int)Mathf.Lerp(lastTouchPos.y, y, f);

                        if (lerpX >= 0 && lerpX <= texWidth - penSize &&
                            lerpY >= 0 && lerpY <= texHeight - penSize)
                        {
                            board.texture.SetPixels(lerpX, lerpY, penSize, penSize, colors);
                        }
                    }
                    transform.rotation = lastTouchRot;
                    board.texture.Apply();
                    //Debug.Log($"SetPixels({x},{y},{penSize},{penSize}), colors.Length={colors.Length}, tex:{board.texture.width}x{board.texture.height}");
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
