# Parametric 3D Point Cloud Stimulus Generator

A Unity (C#) tool that generates rotating 3D dot-cloud shapes (cube, sphere, cylinder) and renders them on a 2D UI canvas using a **custom rotation, translation, and perspective-projection pipeline**, instead of Unity's built-in 3D camera.

Built as a foundation for parametric visual stimuli (for example, depth and motion perception tasks) in the Human Perception & Visualization Lab at UAB.

![Rotating point cloud demo](docs/demo.gif)
<!-- Replace docs/demo.gif with a GIF or screenshot of the cube, sphere, and cylinder rotating -->

## Features

- **Three procedurally generated shapes:** cube, sphere, and cylinder
- **From-scratch 3D-to-2D pipeline:** Y-axis rotation, depth translation, and perspective division, with no reliance on Unity's camera projection
- **Frame-rate-independent rotation:** speed is set in RPM and scaled by `Time.deltaTime`, so motion stays consistent across hardware
- **Fully configurable from the Inspector:** shape, dot count, size, radius, height, rotation speed, and an optional center point
- **Modular design:** data generation, rotation, translation, and projection live in separate classes

## How it works

Every frame, each point goes through the same three steps before being drawn:

```
3D point  ->  rotate (Y axis)  ->  translate (depth)  ->  project  ->  2D canvas position
```

**1. Rotation about the Y axis** (`Rotation.cs`)

```
x' = x * cos(theta) - z * sin(theta)
y' = y
z' = x * sin(theta) + z * cos(theta)
```

The angle advances each frame by `PI * deltaTime * (RPM / 30)`, which equals `RPM` full turns per minute.

**2. Depth translation** (`ZTranslation.cs`)

The shape is pushed away from the viewer along Z by a fixed offset plus `dz`, so every point sits in front of the projection plane.

**3. Perspective projection** (`Projection.cs`)

```
x_screen = x / z
y_screen = y / z
```

Points farther away (larger `z`) land closer to the center, which produces the perspective depth cue. `z` is clamped to a small positive value to avoid division by zero.

### Shape generation (`PointCloudData.cs`)

| Shape | Method |
|---|---|
| **Cube** | The 8 corner vertices are always included; the remaining points are sampled uniformly at random on the six faces |
| **Sphere** | Fibonacci (golden-ratio) spiral, which gives near-even coverage of the surface |
| **Cylinder** | Stacked parametric circles spaced 1 unit apart, centered vertically |

## Inspector parameters

| Parameter | Default | Description |
|---|---|---|
| `shape` | Cube | Cube, Sphere, or Cylinder |
| `RPM` | 15 | Rotation speed in revolutions per minute |
| `numOfDots` | 150 | Total number of points |
| `size` | 3 | Edge length (cube) |
| `radius` | 3 | Radius (sphere and cylinder) |
| `height` | 3 | Number of stacked rings (cylinder), spaced 1 unit apart |
| `hasCenterPoint` | off | Spawns a fixation/center marker on the canvas |

## Getting started

**Requirements:** Unity [YOUR UNITY VERSION HERE]

1. Clone the repository:
   ```bash
   git clone https://github.com/marcos_gorro/point-cloud-stimulus-generator.git
   ```
2. Open the project folder in Unity Hub.
3. Open the sample scene in `Assets/Scenes/`.
4. Select the GameObject with the `PointSpawner` component and choose a shape and parameters in the Inspector.
5. Press Play.

To set it up in your own scene:

1. Create a UI **Canvas**.
2. Create a small UI prefab (for example an `Image`) to use as the dot, and assign it to **Point**.
3. Optionally create a marker prefab and assign it to **Center Point**.
4. Add the `PointSpawner` component to a GameObject and assign the **Canvas** transform.

## Project structure

```
Assets/Scripts/
  PointSpawner.cs     MonoBehaviour: builds the shape, runs the per-frame pipeline, spawns the dots
  PointCloudData.cs   Point generation for cube, sphere, and cylinder
  Rotation.cs         Y-axis rotation
  ZTranslation.cs     Depth translation
  Projection.cs       Perspective projection
```

## Limitations and future work

This is a working prototype. The current renderer instantiates and destroys one UI object per point every frame, which is simple but not efficient at high dot counts. Planned improvements:

- Replace per-frame instantiate/destroy with **object pooling** or a single mesh-based renderer
- Add rotation about additional axes
- Add per-dot controls (size, brightness, lifetime) and motion coherence parameters
- Log stimulus parameters and timing for use in experiments

## Author

**Marcos Gorrochategui** - [GitHub](https://github.com/marcos_gorro)

## License

[MIT](LICENSE)
