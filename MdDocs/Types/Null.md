# Null

If used as an argument or in a constant operation - gets converted to the default value (0, vec30, 0, 0), otherwise, when emited, no block gets placed.

```
null
```

## How to create

``` fcs
float a = null // emits just the set variable block
float b = 5 + null // emits b = 5

// loops from 0 to 5, nothing gets assigned to the start input of the loop block
on Loop(null, 5)
{

}
```

