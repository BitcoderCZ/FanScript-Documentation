

# PlaySound(float, float, float, bool, float)

Plays the [SOUND](#SOUND).

```
void playSound(float volume, float pitch, out float channel, const bool LOOP, const float SOUND)
```

## Parameters

#### `volume`
Type: [float](/MdDocs/Types/Float.md)

Volume of the sound (0 - 1).

#### `pitch`
Type: [float](/MdDocs/Types/Float.md)

Pitch of the sound (0 - 4).

#### `channel`
Modifiers: [out](/MdDocs/Modifiers/Out.md)

Type: [float](/MdDocs/Types/Float.md)

The channel at which the sound is playing (0 - 9, or -1 if all other channels are used).

#### `LOOP`
Modifiers: [const](/MdDocs/Modifiers/Constant.md)

Type: [bool](/MdDocs/Types/Bool.md)

If the sound should loop.

#### `SOUND`
Modifiers: [const](/MdDocs/Modifiers/Constant.md)

Type: [float](/MdDocs/Types/Float.md)

Which sound to play, one of [SOUND](/MdDocs/Constants/SOUND.md).

## Related

 - [stopSound(float)](/MdDocs/Functions/StopSound.float.md)
 - [setVolumePitch(float,float,float)](/MdDocs/Functions/SetVolumePitch.float.float.float.md)


