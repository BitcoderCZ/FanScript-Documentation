# ScreenToWorld(float, float, vec3, vec3)

Gets a ray going from ([screenX](#screenX),[screenY](#screenY)).

```
void screenToWorld(float screenX, float screenY, out vec3 worldNear, out vec3 worldFar)
```

## Parameters

#### `screenX`
Type: [float](/MdDocs/Types/Float.md)

The x screen coordinate.

#### `screenY`
Type: [float](/MdDocs/Types/Float.md)

The y screen coordinate.

#### `worldNear`
Type: [vec3](/MdDocs/Types/Vec3.md)

Position 2 units away from the camera.

#### `worldFar`
Type: [vec3](/MdDocs/Types/Vec3.md)

Position 400 units away from the camera.

## Remarks

Due to a technical issue, still to be fixed, the output lags by one frame. I.e. if you Set Camera on frame N, then this block's output will change on frame N+1 - the function will not work on the first frame.

## Related

 - [worldToScreen(vec3,float,float)](/MdDocs/Functions/Math/WorldToScreen.md)
 - [worldToScreen(vec3)](/MdDocs/Functions/Math/WorldToScreen2.md)

