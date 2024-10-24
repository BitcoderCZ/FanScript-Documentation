

# SetCamera(vec3, rot, float, bool)

Sets the [position](#position), [rotation](#rotation), [range](#range) and mode of the camera.

```
void setCamera(vec3 position, rot rotation, float range, const bool PERSPECTIVE)
```

## Parameters

#### `position`
Type: [vec3](/MdDocs/Types/Vec3.md)

The new position of the camera.

#### `rotation`
Type: [rot](/MdDocs/Types/Rot.md)

The new rotation of the camera.

#### `range`
Type: [float](/MdDocs/Types/Float.md)

- If in orthographic (isometric) mode, determines how wide the view frustum is.
 - If in perspective mode specifies half of the field of view.

#### `PERSPECTIVE`
Modifiers: [const](/MdDocs/Modifiers/Constant.md)

Type: [bool](/MdDocs/Types/Bool.md)

If true, the camera will be in perspective mode, otherwise it will be in orthographic mode.


