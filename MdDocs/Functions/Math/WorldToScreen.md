# WorldToScreen(vec3, float, float)

Gets at what screen position is [worldPos](#worldPos) located.

```
void worldToScreen(vec3 worldPos, out float screenX, out float screenY)
```

## Parameters

#### `worldPos`
Type: [vec3](/MdDocs/Types/Vec3.md)

#### `screenX`
Type: [float](/MdDocs/Types/Float.md)

#### `screenY`
Type: [float](/MdDocs/Types/Float.md)

## Remarks

Due to a technical issue, still to be fixed, the output lags by one frame. I.e. if you Set Camera on frame N, then this block's output will change on frame N+1 - the function will not work on the first frame.

## Related

 - [worldToScreen(vec3)](/MdDocs/Functions/Math/WorldToScreen2.md)
 - [screenToWorld(float,float,vec3,vec3)](/MdDocs/Functions/Math/ScreenToWorld.md)

