using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu(menuName = "2D/Tiles/Advanced Rule Tile")]
public class AdvancedRuleTile : RuleTile<AdvancedRuleTile.Neighbor> {
    public bool alwaysConnect;
    public TileBase[] tilesToConnect;
    public bool checkSelf;

    public class Neighbor : RuleTile.TilingRule.Neighbor {
        public const int This = 1;
        public const int NotThis = 2;
        public const int Any = 3;
        public const int Specified = 4;
        public const int Nothing = 5;
    }

    public override bool RuleMatch(int neighbor, TileBase tile) {
        switch(neighbor) {
            case Neighbor.This: return CheckThis(tile);
            case Neighbor.NotThis: return CheckNotThis(tile);
            case Neighbor.Any: return CheckAny(tile);
            case Neighbor.Specified: return CheckSpecified(tile);
            case Neighbor.Nothing: return CheckNothing(tile);
        }

        return base.RuleMatch(neighbor, tile);
    }

    private bool CheckThis(TileBase tile) {
        if(!alwaysConnect) {
            return tile == this;
        } else {
            return tilesToConnect.Contains(tile) || tile == this;
        }
    }

    private bool CheckNotThis(TileBase tile) {
        return tile != this;
    }

    private bool CheckAny(TileBase tile) {
        if(checkSelf) {
            return tile != null;
        } else {
            return tile != null && tile != this;
        }
    }

    private bool CheckSpecified(TileBase tile) {
        return tilesToConnect.Contains(tile);
    }
    
    private bool CheckNothing(TileBase tile) {
        return tile == null;
    }
}