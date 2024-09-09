# LineVsPlane(vec3, vec3, vec3, vec3)

Returns the point at which a line intersects a plane.

```
vec3 lineVsPlane(vec3 lineFrom, vec3 lineTo, vec3 planePoint, vec3 planeNormal)
```

## Parameters

#### `lineFrom`
Type: vec3

Line's starting position.

#### `lineTo`
Type: vec3

Line's end position.

#### `planePoint`
Type: vec3

A point on the plane.

#### `planeNormal`
Type: vec3

A vector perpendicular to the plane (the up direction from the plane's surface).

## Returns

vec3

The intersection of a line and a plane.

## Remarks

The line is not a line segment, so the intersection will be found even if it's not in-between [lineFrom](#lineFrom)/[lineTo](#lineTo).

