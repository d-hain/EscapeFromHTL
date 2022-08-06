using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
class DoorDirection : MonoBehaviour {
    public enum Direction {
        HorizontalRight,
        HorizontalLeft,
        VerticalUpLeft,
        VerticalUpRight,
        VerticalDownLeft,
        VerticalDownRight
    }
    public Direction direction;
    
    private SpriteRenderer _spriteRenderer;

    public void Start() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        switch(direction) {
            case Direction.HorizontalRight:
                SpriteRendererChangeSprite("green", "door-green-left");
                break;
            case Direction.HorizontalLeft:
                SpriteRendererChangeSprite("green", "door-green-right");
                break;
            case Direction.VerticalUpLeft:
                SpriteRendererChangeSprite("green", "door-green-right-up-flipped");
                break;
            case Direction.VerticalUpRight:
                SpriteRendererChangeSprite("green", "door-green-left-up");
                break;
            case Direction.VerticalDownLeft:
                SpriteRendererChangeSprite("green", "door-green-left-up-flipped");
                break;
            case Direction.VerticalDownRight:
                SpriteRendererChangeSprite("green", "door-green-right-up");
                break;
            default:
                _spriteRenderer.sprite = Resources.Load<Sprite>("Sprites/CustomRuleTile/Nothing");
                break;
        }
    }

    private void SpriteRendererChangeSprite(string color, string fileName) {
        _spriteRenderer.sprite =
            Resources.Load<Sprite>("Sprites/tiles/school/doors/" + color + "/" + fileName);
    }
}