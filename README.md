#AR manipulations
This package is an adaptation of the
[arcore-unity-sdk](https://github.com/google-ar/arcore-unity-sdk/tree/master/Assets/GoogleARCore/Examples)
example AR setup for ARFoundation.

It allows to easily build a simple yet visually pleasing and flexible AR experience having a few lines of code.

![](Documentation~/demo.gif)


## Supported Manipulations
- Select\Deselect - tap on an AR object
- Translate - drag with finger
- Scale - pinch to scale
- Rotate - rotation with two fingers
- Elevate - move tree fingers up and down
- Spawn - tap on a plane to spawn new object
- Place on a plane - object projects on a plane, tap to fix it in place

## Installation
manifest.json
```json
"dependencies": {
  ...
  "com.kasfan.ar-manipulations": "https://github.com/Kasfan/ar-manipulations.git",
  ...
}
```
Or you can install it through [Unity Package Manager](https://docs.unity3d.com/Manual/upm-ui-giturl.html)
```
https://github.com/Kasfan/ar-manipulations.git
```
### Supported platforms:
- Android
- iOS

### Use example:
- https://github.com/Kasfan/ARFoundation-Tools-Example
