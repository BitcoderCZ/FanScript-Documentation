

# LineVsPlane(vec3, vec3, vec3, vec3)

Returns the point at which a line intersects a plane.

```
vec3 lineVsPlane(vec3 lineFrom, vec3 lineTo, vec3 planePoint, vec3 planeNormal)
```

## Parameters

#### `lineFrom`
Type: [vec3](/MdDocs/Types/Vec3.md)

Line's starting position.

#### `lineTo`
Type: [vec3](/MdDocs/Types/Vec3.md)

Line's end position.

#### `planePoint`
Type: [vec3](/MdDocs/Types/Vec3.md)

A point on the plane.

#### `planeNormal`
Type: [vec3](/MdDocs/Types/Vec3.md)

A vector perpendicular to the plane (the up direction from the plane's surface).

## Returns

[vec3](/MdDocs/Types/Vec3.md)

The intersection of a line and a plane.

## Remarks

 - The line is not a line segment, so the intersection will be found even if it's not in-between [lineFrom](#lineFrom)/[lineTo](#lineTo).


