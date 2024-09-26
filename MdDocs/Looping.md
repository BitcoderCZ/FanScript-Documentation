# Looping
In FanScript, there are 2 main ways of creating loops:
- while and do while loops (if the condition is false, while doesn't execute at all, but do while executes once)
``` fcs
float i = 0

while (i < 10)
{
    i++
}

i = 0

do
{
    i++
} while (i < 10)
```
- Loop event
``` fcs
on Loop(0, 10, out inline float i)
{

}
```

## Which one should you use

### while loop
- break and continue can be used
- if the loop count isn't know at the start

### on Loop
- faster(?)
- doesn't require an index variable (inline var can be used)