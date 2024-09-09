# WorldToScreen(vec3)

Returns at what screen position is [worldPos](#worldPos) located.

```
vec3 worldToScreen(vec3 worldPos)
```

## Parameters

#### `worldPos`
Type: vec3

## Returns

vec3

The screen position of [worldPos](#worldPos) (XY).

## Remarks

Due to a technical issue, still to be fixed, the output lags by one frame. I.e. if you Set Camera on frame N, then this block's output will change on frame N+1 - the function will not work on the first frame.

## Related

 - [worldToScreen(vec3,float,float)](/MdDocs/Functions/Math/WorldToScreen.md)
 - [screenToWorld(float,float,vec3,vec3)](/MdDocs/Functions/Math/ScreenToWorld.md)

