# ArraySegment

Represents a segment of values.

```
arraySegment<>
```

## How to create

Array segment isn't a runtime type - variables of the type cannot be created.

Array segment can only be used to:
- create [array](/MdDocs/Types/Array.md)s
- as argument in functions [setRange<>(array<>,float,arraySegment<>)](/MdDocs/Functions/SetRange.md)
``` fcs
array<float> arr = [1, 2, 3]
arr.setRange(3, [4, 5, 6])
```

## Related

 - [array](/MdDocs/Types/Array.md)

