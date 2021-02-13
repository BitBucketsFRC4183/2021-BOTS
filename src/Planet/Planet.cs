using Godot;
using Godot.Collections;
using System.Collections.Generic;
using System.Linq;

public class Planet : Node2D
{
	Array<TileMap> tileMaps = new Array<TileMap>();

    public override void _Ready()
    {
        CheckChildren(GetChildren());
        
        foreach (TileMap map in tileMaps) {
            MakeTileCollisionShapes(map);
        }
    }

	// go through each tilemap and add it to list if we want to generate collisions for it
	// determined by whether it has a collision layer and mask
	// if it's a Node2D, check its children too

	// note: for tilemapslike rocks and rockets, because they share the same tileset,
	// even if the nodes themselves dont have collision layers, polygons will be made
	// they wont collide, but will be made nonetheless
	// to address this, you must make the tileset "Make Unique"
	void CheckChildren(Array children) {
		foreach (Node child in children) {
			if (child is TileMap) {
				TileMap tileChild = (TileMap) child;
				if (tileChild.CollisionLayer > 0 && tileChild.CollisionMask > 0) {
					tileMaps.Add(tileChild);
				}
			} else if (child is Node2D) {
				CheckChildren(child.GetChildren());
			}
		}

	}

	void MakeTileCollisionShapes(TileMap map) {
		TileSet tileSet = map.TileSet;

		foreach (int tileId in tileSet.GetTilesIds()) {
			// Load image and calculate collison polygon
			// https://github.com/godotengine/godot/blob/2abe996414b8b551e69e29461de3ff1bcaf5a28f/editor/plugins/sprite_2d_editor_plugin.cpp#L160
			// https://github.com/godotengine/godot/blob/2abe996414b8b551e69e29461de3ff1bcaf5a28f/editor/plugins/sprite_2d_editor_plugin.cpp#L255

			Image tileImage = tileSet.TileGetTexture(tileId).GetData();

			Rect2 rect = new Rect2();
			rect.Size = new Vector2(tileImage.GetWidth(), tileImage.GetHeight());
			
			BitMap imageBitMap = new BitMap();
			imageBitMap.CreateFromImageAlpha(tileImage);
			
			Array lines = imageBitMap.OpaqueToPolygons(rect);

			Array<Array<Vector2>> OutlineLines = new Array<Array<Vector2>>();
			OutlineLines.Resize(lines.Count);

			Array<Array<Vector2>> computedOutlineLines = new Array<Array<Vector2>>();
			computedOutlineLines.Resize(lines.Count);

			for (int pi = 0; pi < lines.Count; pi++) {
				Array<Vector2> ol = new Array<Vector2>();
				Array<Vector2> col = new Array<Vector2>();

				IList<Vector2> linesLines = (IList<Vector2>)lines[pi];

				ol.Resize(linesLines.Count);
				col.Resize(linesLines.Count);

				for (int i = 0; i < linesLines.Count; i++) {
					Vector2 vtx = linesLines[i];

					ol[i] = vtx;
					vtx -= rect.Position;

					// Flipping logic is not implemented since idk how that would
					// translate from a Sprite to an Image
					// Don't flip any sprites horizontally or vertically

					// this assumes the texture is centered in the image (which it is)
					vtx -= rect.Size / 2;

					col[i] = vtx;
				}

				OutlineLines[pi] = ol;
				computedOutlineLines[pi] = col;
			}

			
			// Now that we've calculated the collision polygon, we need to set it
			// https://github.com/godotengine/godot/blob/2abe996414b8b551e69e29461de3ff1bcaf5a28f/editor/plugins/sprite_2d_editor_plugin.cpp#L400

			if (computedOutlineLines.Count == 0) {
				GD.PrintErr("Error, couldn't make some geometry - tileId is " + tileId.ToString());
				continue;
			}

			ConvexPolygonShape2D newShape = new ConvexPolygonShape2D();
			for (int i = 0; i < computedOutlineLines.Count; i++) {
				var outline = computedOutlineLines[i];
				newShape.Points = outline.ToArray<Vector2>();
				tileSet.TileSetShape(tileId, 0, newShape);
				tileSet.TileSetShapeOffset(tileId, 0, new Vector2(256, 256)); // needs to be offset by this amount for some reason
			}
		}
	}

}
