# Events

Events are like functions, but they also execute a block of code.

They are called like so:

``` fcs
on EventName(arg0, arg1)
{
    // some code that gets executed when the condition of this event is true, depending on the event this can even be executed multiplle times
}
```


If the event doesn't have any parameters, the parentheses can be ignored:

``` fcs
on EventWithoutParameters
{
    
}
```


Ref and out parameters work the same way as in functions.

## Contents

- [BoxArt](BoxArt.md)
- [Button](Button.md)
- [Collision](Collision.md)
- [LateUpdate](LateUpdate.md)
- [Loop](Loop.md)
- [Play](Play.md)
- [Swipe](Swipe.md)
- [Touch](Touch.md)
