
# Swipe(vec3)

Executes when a swipe is detected.

```
on swipe(out vec3 direction) { }
```

## Parameters

#### `direction`
Type: [vec3](/MdDocs/Types/Vec3.md)

Direction of the swipe (a cardinal direction in the XZ plane).

## Remarks

 - If the player holds the finger after swiping, swipe is executed every 15 frames.


