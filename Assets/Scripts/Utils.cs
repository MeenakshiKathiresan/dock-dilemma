using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Utils Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


    }
    [SerializeField]
    public List<Sprite> boatSprites = new List<Sprite>();
    public static Vector3 GetDirection(Direction direction)
    {
        Vector2 directionVector = Vector2.zero;

        switch (direction)
        {
            case Direction.Down: directionVector = Vector2.down; break;
            case Direction.Up: directionVector = Vector2.up; break;
            case Direction.Left: directionVector = Vector2.left; break;
            case Direction.Right: directionVector = Vector2.right; break;
        }

        return directionVector;
    }

    public Sprite GetSprite(Direction direction)
    {
        Sprite boatSprite = boatSprites[0];

        switch (direction)
        {
            case Direction.Down: boatSprite = boatSprites[0]; break;
            case Direction.Up: boatSprite = boatSprites[1]; break;
            case Direction.Left: boatSprite = boatSprites[2]; break;
            case Direction.Right: boatSprite = boatSprites[3]; break;
        }

        return boatSprite;
    }
}
