# Loop(float, float, float)

Executes multiple times from [start](#start) to [stop](#stop).

```
on loop(float start, float stop, out float counter) { }
```

## Parameters

#### `start`
Type: float

The start value (inclusive).

#### `stop`
Type: float

The end value (exclusive).

#### `counter`
Type: float

The current value.

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

## Remarks

- The counter always steps by 1 (or -1, if [start](#start) is greater than [stop](#stop)).
- The counter does not output [stop](#stop).
- If a non-integer value is provided for [start](#start), it's rounded down to the next smallest integer.
- If a non-integer value is provided for [stop](#stop), it's rounded up to the next biggest integer.

