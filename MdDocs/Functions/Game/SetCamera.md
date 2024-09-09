# SetCamera(vec3, rot, float, bool)

Sets the [position](#position), [rotation](#rotation), [range](#range) and mode of the camera.

```
void setCamera(vec3 position, rot rotation, float range, bool PERSPECTIVE)
```

## Parameters

#### `position`
Type: vec3

The new position of the camera.

#### `rotation`
Type: rot

The new rotation of the camera.

#### `range`
Type: float

- If in orthographic (isometric) mode, determines how wide the view frustum is
- If in perspective mode specifies half of the field of view.

#### `PERSPECTIVE`
Type: bool

If true, the camera will be in perspective mode, otherwise it will be in orthographic mode.

