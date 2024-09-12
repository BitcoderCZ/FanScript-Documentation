# Array

A array/list of a type.

```
array<>
```

## How to create

``` fcs
// using arraySegment
array<float> a = [1, 2, 3]
// using setRange function
array<bool> b
b.setRange(0, [true, false, true])
// using multiple set's
array<vec3> c
c.set(0, vec3(1, 2, 3))
c.set(1, vec3(4, 5, 6))
c.set(2, vec3(7, 8, 9))
```

## Related

 - [arraySegment](/MdDocs/Types/ArraySegment.md)

