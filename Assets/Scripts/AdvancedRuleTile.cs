using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu(menuName = "2D/Tiles/Advanced Rule Tile")]
public class AdvancedRuleTile : RuleTile<AdvancedRuleTile.Neighbor> {
    public bool alwaysConnect;
    public TileBase[] tilesToConnect;
    public bool checkSelf;

    public class Neighbor : RuleTile.TilingRule.Neighbor {
        // disable warning about hiding variables and using new keyword
#pragma warning disable 0108
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

    /// <summary>
    /// Check if the given tile is the same as the tile this rule is assigned to.
    /// </summary>
    /// <param name="tile">The tile to check</param>
    /// <returns>
    /// true ... The tiles are the same <br/>
    /// false ... The tiles are not the same
    /// </returns>
    private bool CheckThis(TileBase tile) {
        if(!alwaysConnect) {
            return tile == this;
        } else {
            return tilesToConnect.Contains(tile) || tile == this;
        }
    }

    /// <summary>
    /// Check if the given tile is not the same as the tile this rule is assigned to.
    /// </summary>
    /// <param name="tile">The tile to check</param>
    /// <returns>
    /// true ... The tiles are not the same <br/>
    /// false ... The tiles are the same
    /// </returns>
    private bool CheckNotThis(TileBase tile) {
        return tile != this;
    }

    /// <summary>
    /// Check if there is any tile next to this tile.
    /// </summary>
    /// <param name="tile">The tile to check</param>
    /// <returns>
    /// true ... There is a tile next to this tile <br/>
    /// false ... There is no tile next to this tile
    /// </returns>
    private bool CheckAny(TileBase tile) {
        if(checkSelf) {
            return tile != null;
        } else {
            return tile != null && tile != this;
        }
    }

    /// <summary>
    /// Check if the given tile is one of the tiles specified in the <see cref="tilesToConnect"/> array.
    /// </summary>
    /// <param name="tile">The tile to check</param>
    /// <returns>
    /// true ... The tile is one specified in the <see cref="tilesToConnect"/> array <br/>
    /// false ... The tile is not specified in the <see cref="tilesToConnect"/> array
    /// </returns>
    private bool CheckSpecified(TileBase tile) {
        return tilesToConnect.Contains(tile);
    }

    /// <summary>
    /// Check no tile is next to this tile.
    /// </summary>
    /// <param name="tile">The tile to check</param>
    /// <returns>
    /// true ... There is no tile next to this tile <br/>
    /// false ... There is a tile next to this tile
    /// </returns>
    private bool CheckNothing(TileBase tile) {
        return tile == null;
    }
}