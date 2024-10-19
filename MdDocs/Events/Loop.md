
# Loop(float, float, float)

Executes multiple times from start to stop.

```
on loop(float start, float stop, out float counter) { }
```

## Parameters

#### `start`
Type: [float](/MdDocs/Types/Float.md)

The start value (inclusive).

#### `stop`
Type: [float](/MdDocs/Types/Float.md)

The end value (exclusive).

#### `counter`
Type: [float](/MdDocs/Types/Float.md)

The current value.

## Remarks

 - The counter always steps by 1 (or -1, if start is greater than stop).
 - The counter does not output stop.
 - If a non-integer value is provided for start, it's rounded down to the next smallest integer.
 - If a non-integer value is provided for stop, it's rounded up to the next biggest integer.

## Examples

``` fcs
on Loop(0, 5, out inline float i)
{
    inspect(i) // [0, 1, 2, 3, 4]
}
```

``` fcs
on Loop(5, 0, out inline float i)
{
    inspect(i) // [5, 4, 3, 2, 1]
}
```


