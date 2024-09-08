# PlaySound(float, float, float, bool, float)

Plays the [SOUND](#SOUND).

```
void playSound(float volume, float pitch, out float channel, bool LOOP, float SOUND)
```

## Parameters

#### `volume`
Type: float

Volume of the sound (0-1).

#### `pitch`
Type: float

Pitch of the sound (0-4).

#### `channel`
Type: float

The channel at which the sound is playing (0-9, or -1 if all other channels are used).

#### `LOOP`
Type: bool

If the sound should loop (Must be constant).

#### `SOUND`
Type: float

Which sound to play, must be one of [SOUND](Constants/SOUND.md).

## Related

 - [stopSound(float)](Functions/Sound/StopSound.md)
 - [setVolumePitch(float,float,float)](Functions/Sound/SetVolumePitch.md)

