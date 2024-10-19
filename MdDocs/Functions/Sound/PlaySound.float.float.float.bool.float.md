

# PlaySound(float, float, float, bool, float)

Plays the [SOUND](#SOUND).

```
void playSound(float volume, float pitch, out float channel, bool LOOP, float SOUND)
```

## Parameters

#### `volume`
Type: [float](/MdDocs/Types/Float.md)

Volume of the sound (0 - 1).

#### `pitch`
Type: [float](/MdDocs/Types/Float.md)

Pitch of the sound (0 - 4).

#### `channel`
Type: [float](/MdDocs/Types/Float.md)

The channel at which the sound is playing (0 - 9, or -1 if all other channels are used).

#### `LOOP`
Type: [bool](/MdDocs/Types/Bool.md)

If the sound should loop, must be constant.

#### `SOUND`
Type: [float](/MdDocs/Types/Float.md)

Which sound to play, one of [SOUND](/MdDocs/Constants/SOUND.md), must be constant.

## Related

 - [stopSound(float)](/MdDocs/Functions/StopSound.float.md)
 - [setVolumePitch(float,float,float)](/MdDocs/Functions/SetVolumePitch.float.float.float.md)


