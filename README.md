# Signal Tower Logic 3D

Signal Tower Logic 3D is a mobile-first 3D puzzle strategy game built in Unity. The player routes coloured signal beams across a futuristic board by rotating reflectors, activating power nodes, timing pulse gates, and solving colour-matching logic.

The project is designed for Android and iPhone in landscape orientation. It uses a manually assembled Unity scene, clean modular C# scripts, Unity primitives, TextMeshPro UI, and generated license-free placeholder audio.

## Game Overview

In each level, signal sources emit coloured beams across a grid. The goal is to power the required receivers by guiding the correct signal colour into them. The player interacts with existing scene objects using touch controls:

- Rotate reflectors to redirect beams.
- Activate power nodes to unlock gated receivers.
- Avoid blockers and invalid colour routes.
- Use splitters to branch one signal into multiple paths.
- Pass through colour gates only with matching signals.
- Time pulse gates that open or close after player actions.
- Solve within the action and power budget for higher stars.

The game currently includes 12 manually built levels with increasing difficulty.

## Core Features

- 12 playable manually assembled levels.
- Touch-first controls for Android and iPhone.
- Landscape mobile layout with safe-area handling.
- Manual Unity scene hierarchy, not runtime-generated levels.
- Colour signal routing for red, blue, green, and yellow.
- Reflectors, receivers, sources, splitters, gates, blockers, power nodes, and pulse gates.
- Undo system for reflector rotations and power node activations.
- Hint system with star-rating impact.
- Tutorial messages for guided onboarding.
- Star rating and PlayerPrefs save progress.
- Level select with lock and star state.
- Pause, settings, credits, win, and loss panels.
- Generated license-free placeholder SFX and background music.
- Unity MCP package included under `Packages/com.anklebreaker.unity-mcp`.

## How The Gameplay Works

The scene contains all main objects before Play Mode:

- `GameScene/Levels/Level_01` through `Level_12`
- `Environment`
- `SharedGameplayObjects`
- `Effects`
- `Audio`
- `Managers`
- `UI`
- `Main Camera`
- `Directional Light`

When a level starts, `LevelManager` enables the selected level parent and disables the others. It does not generate level objects. Each level has a `LevelConfig` component with Inspector-assigned references to its sources, receivers, reflectors, gates, splitters, power nodes, pulse gates, blockers, beam renderers, and level effects.

After every valid player action, `SignalPathCalculator` recalculates the beam route from each active source. It walks tile-by-tile through the manually placed grid and applies the level rules:

- Stop at blockers.
- Stop at grid boundaries.
- Stop at closed pulse gates.
- Stop at wrong-colour gates.
- Reflect at reflectors.
- Split at splitters.
- Power receivers only when the signal colour matches.
- Prevent infinite loops by tracking visited tile-direction-colour states.

`SignalBeamRenderer` then updates existing `LineRenderer` objects assigned in the scene. It does not create the level or beam holders at runtime.

## Controls

Mobile:

- Tap an interactable object to select it.
- Tap `Rotate` to rotate a selected reflector.
- Tap `Activate` to activate a selected power node or special object.
- Tap `Undo` to revert the last supported action.
- Tap `Restart` to reset the current level.
- Tap `Pause` to pause the game.
- Tap `Hint` to show the current level hint.

Editor testing:

- `R` rotates the selected reflector.
- `A` activates the selected object.
- `U` performs undo.
- `Escape` pauses.

## Project Structure

```text
Assets/
  Audio/GeneratedLicenseFree/   Generated placeholder music and SFX
  Materials/Stage2/             Mobile-friendly puzzle materials
  Scenes/SampleScene.unity      Main manually assembled game scene
  Scripts/Gameplay/             Core gameplay object scripts
  Scripts/Managers/             Game flow, UI, input, routing, save, audio systems
  Settings/                     URP and project render settings
  TextMesh Pro/                 TMP resources

Packages/
  com.anklebreaker.unity-mcp/   Unity MCP editor plugin
  manifest.json                 Unity package dependencies

ProjectSettings/                Unity project and build settings
CODEX.md                        Development notes and validation history
```

## Important Scene Rules

This project intentionally avoids runtime generation for core content:

- No runtime level generation.
- No runtime UI generation.
- No runtime camera creation.
- No runtime environment generation.
- No `Instantiate` for core level layouts.
- No `GameObject.CreatePrimitive` for core gameplay objects.
- Core references use serialized fields and Inspector assignments.
- All main objects exist in the Unity Scene Hierarchy before pressing Play.

These rules are documented in `CODEX.md` and should stay intact for future development.

## Requirements

- Unity `6000.4.6f1` or compatible Unity 6 version.
- Android or iOS build modules for mobile publishing.
- TextMeshPro package.
- Unity Input System package.
- Universal Render Pipeline package.

The project is configured for:

- Android minimum API level 25.
- Android ARM64.
- iPhone and Android landscape orientation.
- Screen Space Overlay UI with Canvas Scaler reference resolution `1920 x 1080`, match `0.5`.

## How To Open

1. Open Unity Hub.
2. Add this folder as an existing Unity project.
3. Open the project with Unity `6000.4.6f1` or a compatible Unity 6 editor.
4. Open `Assets/Scenes/SampleScene.unity`.
5. Press Play.

## How To Build

Android:

1. Open `File > Build Profiles`.
2. Select Android.
3. Confirm landscape orientation and minimum API level 25 or higher.
4. Build an APK or AAB.

iOS:

1. Open `File > Build Profiles`.
2. Select iOS.
3. Confirm landscape orientation.
4. Build the Xcode project.

## Audio License Notes

The current audio clips in `Assets/Audio/GeneratedLicenseFree` are generated placeholder WAV files. They were created for this project, contain no third-party samples, and are documented as free for commercial use without attribution in:

```text
Assets/Audio/GeneratedLicenseFree/LICENSE.md
```

Before final publishing, the sounds can be replaced with verified CC0 or free-for-commercial-use audio if a stronger production sound direction is desired.

## Current Validation

Latest local validation:

- Unity compilation reports zero errors.
- Unity console reports zero errors after clearing and rechecking.
- All 12 levels start unsolved with zero powered receivers.
- All 12 levels have a validated intended solution state.
- The scene remains manually assembled.
- No runtime level, UI, camera, or environment generation was added.

## Repository Notes

Unity-generated folders such as `Library`, `Temp`, `Logs`, `UserSettings`, and build outputs are ignored. The repository tracks the actual project source, assets, package manifest, embedded MCP Unity package, project settings, development notes, and this README.

Use Git LFS for future large binary additions such as release builds or video captures.
