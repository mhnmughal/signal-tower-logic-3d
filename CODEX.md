# Signal Tower Logic 3D - Development Notes

## Global Rules
- Manual scene hierarchy only.
- No runtime scene generation.
- No runtime UI generation.
- No runtime camera creation.
- Mobile touch first.
- Android and iPhone landscape support.
- The project must remain a manually assembled Unity scene, not a runtime scene generator.
- Do not generate the full environment, UI, camera, lights, or level layouts from code.
- All main objects must exist in the Unity Scene Hierarchy before pressing Play.
- Scripts may only control, activate, deactivate, update, animate, reset, or read existing objects.
- Do not use runtime UI creation.
- Do not use runtime level generation.
- Do not use runtime camera creation.
- Do not use GameObject.CreatePrimitive for core scene objects.
- Do not use Instantiate for core level layouts.
- Avoid GameObject.Find, FindObjectOfType, and Resources.Load for core references.
- Use serialized fields and Inspector-assigned references.
- Target Android and iPhone in landscape orientation.
- Game must work with touch controls.
- Keyboard shortcuts are allowed only for Unity Editor testing.
- No ads, no IAP, no internet requirement, and no analytics SDK.
- Use only Unity primitives, TextMeshPro, free-license placeholder audio, and clean modular scripts.

## Stage Progress
- Stage 0 completed: Created the root project documentation file before gameplay work.
- Stage 0 completed: Added all global project rules and constraints.
- Stage 0 completed: Added the required final scene hierarchy checklist for future manual scene assembly.
- Stage 0 completed: No gameplay, runtime generation scripts, runtime UI creation, runtime camera creation, or level generation were built in this stage.
- Stage 1 completed: Created the complete manual Unity scene hierarchy under GameScene in Assets/Scenes/SampleScene.unity.
- Stage 1 completed: Added Level_01 through Level_12 under Levels, with matching child groups for every level.
- Stage 1 completed: Set only Level_01 active by default; Level_02 through Level_12 are inactive.
- Stage 1 completed: Configured Main Camera as a manually placed orthographic isometric camera at position 8, 12, -8 and rotation 55, 45, 0.
- Stage 1 completed: Added the manually placed Directional Light, UI Canvas, EventSystem, and audio source placeholders.
- Stage 1 completed: Confirmed no runtime hierarchy generation, runtime UI creation, runtime camera creation, gameplay logic, or level generator scripts were added.
- Stage 2 completed: Created the mobile-friendly visual foundation using Unity primitive scene objects and simple material assets.
- Stage 2 completed: Created all requested materials under Assets/Materials/Stage2.
- Stage 2 completed: Added manually placed environment objects under Environment, including floor platform, boundary walls, background panels, futuristic buildings, antenna decorations, cable decorations, and lighting decorations.
- Stage 2 completed: Added simple Level_01 board styling with grid tiles, signal-colored visual placeholders, receivers, reflectors, gates, blockers, power nodes, and beam previews.
- Stage 2 completed: Created shared gameplay visual objects under SharedGameplayObjects and set SelectionRing, ValidTargetHighlight, and InvalidActionMarker inactive by default.
- Stage 2 completed: Confirmed no gameplay logic, runtime environment generation, runtime UI creation, runtime camera creation, or level generator scripts were added.
- Stage 3 completed: Created clean modular C# scripts for core gameplay objects under Assets/Scripts/Gameplay.
- Stage 3 completed: Added serialized Inspector fields for interactables, tiles, sources, receivers, reflectors, gates, splitters, power nodes, pulse gates, blockers, and level configuration.
- Stage 3 completed: Added smooth reflector rotation control that only rotates an existing assigned Transform.
- Stage 3 completed: Confirmed no runtime scene hierarchy generation, runtime UI creation, runtime camera creation, level generation, GameObject.Find, Resources.Load, or Instantiate usage was added.
- Stage 4 completed: Created core game flow manager scripts for game state, level activation/reset, and local PlayerPrefs saves.
- Stage 4 completed: Attached GameManager, LevelManager, and SaveManager scripts to the existing Managers hierarchy objects.
- Stage 4 completed: Added LevelConfig components to Level_01 through Level_12 and assigned them to LevelManager alongside the existing level parent references.
- Stage 4 completed: Confirmed no runtime level generation, scene generation, UI generation, camera generation, GameObject.Find, Resources.Load, or Instantiate usage was added.
- Stage 5 completed: Implemented signal grid lookup, signal path calculation, and reusable LineRenderer beam updates.
- Stage 5 completed: Wired SignalGridManager, SignalPathCalculator, and SignalBeamRenderer to existing Managers hierarchy objects.
- Stage 5 completed: Added Level_01 SignalTile, source, receiver, reflector, gate, splitter, power node, pulse gate, blocker, and LineRenderer references through existing scene objects.
- Stage 5 completed: Confirmed no runtime level generation, scene generation, UI generation, camera generation, GameObject.Find, Resources.Load, or Instantiate usage was added.
- Stage 6 completed: Implemented mobile-first tap selection, selected object actions, and undo.
- Stage 6 completed: Attached TouchInputController and UndoManager to the existing Managers hierarchy.
- Stage 6 completed: Wired touch, undo, camera, feedback, and shared selection visual references through serialized Inspector fields.
- Stage 6 completed: Added colliders to existing Level_01 interactable gameplay objects so touch raycasts can hit them.
- Stage 6 completed: Confirmed no runtime hierarchy generation, runtime UI creation, runtime camera creation, level generation, GameObject.Find, Resources.Load, or Instantiate usage was added.
- Stage 7 completed: Completed all manually placed UI panels under UI/Canvas.
- Stage 7 completed: Added UIManager, LevelSelectUI, and SafeAreaHandler scripts.
- Stage 7 completed: Attached UIManager and LevelSelectUI to existing Managers hierarchy objects.
- Stage 7 completed: Created and wired 12 existing level select button objects with number text, lock overlays, and 3 star icons each.
- Stage 7 completed: Added safe area support to every required UI panel.
- Stage 7 completed: Confirmed no runtime UI generation, runtime panel/button/TextMeshPro creation, runtime camera creation, level generation, GameObject.Find, Resources.Load, or Instantiate usage was added.
- Stage 8 completed: Added tutorial, hint, star rating, and temporary feedback systems.
- Stage 8 completed: Attached TutorialManager, HintManager, StarRatingManager, and FeedbackTextUI scripts to the existing Managers hierarchy objects.
- Stage 8 completed: Wired Stage 8 systems to existing UIManager, LevelManager, GameManager, SaveManager, UndoManager, TutorialPanel, and FeedbackTextPanel references.
- Stage 8 completed: Confirmed no runtime UI generation, runtime scene generation, runtime camera creation, or runtime level generation was added.
- Stage 9 completed: Added a complete AudioManager using the existing manually placed MusicSource, SFXSource, and UISFXSource objects.
- Stage 9 completed: Wired AudioManager to SaveManager, UIManager, and FeedbackTextUI.
- Stage 9 completed: Added serialized placeholder clip fields for all requested music, UI, gameplay, completion, and reward sounds.
- Stage 9 completed: Confirmed no AudioSource objects, UI objects, scene objects, cameras, or level objects are created at runtime.
- Stage 10 completed: Authored all 12 playable levels under the existing Level_01 through Level_12 hierarchy.
- Stage 10 completed: Added manually placed grid tiles, sources, receivers, reflectors, gates, splitters, power nodes, pulse gates, blockers, signal beam holders, and level effect placeholders as scene objects.
- Stage 10 completed: Assigned LevelConfig lists, objectives, budgets, action limits, receiver requirements, star limits, hints, and tutorial messages for all 12 levels.
- Stage 10 completed: Confirmed no runtime level generator, runtime UI generator, runtime camera generator, or runtime core object instantiation was added.
- Stage 11 completed: Added mobile-friendly polish, feedback effects, camera shake, UI pulses, and panel show animation.
- Stage 11 completed: Wired existing Effects hierarchy placeholders with lightweight ParticleSystem components.
- Stage 11 completed: Attached CameraShakeOnly to the existing manually placed Main Camera.
- Stage 11 completed: Confirmed no runtime scene generation, runtime UI generation, runtime camera creation, runtime level generation, ads, IAP, internet, analytics, or expensive post-processing was added.
- Stage 12 completed: Performed final scene, UI, camera, gameplay, level, audio, save, mobile, and build settings validation.
- Stage 12 completed: Added Assets/Scenes/SampleScene.unity to Build Settings.
- Stage 12 completed: Configured Android and iOS publishing readiness settings for landscape mobile play.
- Stage 12 completed: Confirmed no missing scripts, missing references, compile errors, runtime UI generation, runtime environment generation, runtime level generation, or runtime camera generation.

## Scene Hierarchy Checklist
- GameScene
  - Environment
    - Floor
    - BackgroundDecorations
    - BoundaryWalls
    - FuturisticBuildings
    - AntennaDecorations
    - CableDecorations
    - LightingDecorations
  - Levels
    - Level_01
      - SignalGrid
      - Sources
      - Receivers
      - Reflectors
      - Gates
      - Splitters
      - PowerNodes
      - PulseGates
      - Blockers
      - SignalBeams
      - LevelEffects
    - Level_02 through Level_12
      - SignalGrid
      - Sources
      - Receivers
      - Reflectors
      - Gates
      - Splitters
      - PowerNodes
      - PulseGates
      - Blockers
      - SignalBeams
      - LevelEffects
  - SharedGameplayObjects
    - SelectionRing
    - ValidTargetHighlight
    - InvalidActionMarker
  - Effects
    - ReceiverActivatedParticles
    - GateBlockedParticles
    - PowerNodeParticles
    - LevelCompleteParticles
    - WrongActionParticles
  - Audio
    - MusicSource
    - SFXSource
    - UISFXSource
  - Managers
    - GameManager
    - LevelManager
    - SignalGridManager
    - SignalPathCalculator
    - TouchInputController
    - UIManager
    - LevelSelectUI
    - AudioManager
    - SaveManager
    - TutorialManager
    - StarRatingManager
    - HintManager
    - UndoManager
    - FeedbackTextUI
  - UI
    - Canvas
      - TitleScreen
      - MainMenuPanel
      - LevelSelectPanel
      - GameplayHUD
      - MobileControlsPanel
      - PausePanel
      - SettingsPanel
      - LevelCompletePanel
      - GameOverPanel
      - CreditsPanel
      - TutorialPanel
      - FeedbackTextPanel
    - EventSystem
  - Main Camera
  - Directional Light

## Scripts Created
- None in Stage 0.
- None in Stage 1.
- None in Stage 2.
- Stage 3: Assets/Scripts/Gameplay/SignalTypes.cs
- Stage 3: Assets/Scripts/Gameplay/InteractableObject.cs
- Stage 3: Assets/Scripts/Gameplay/SignalTile.cs
- Stage 3: Assets/Scripts/Gameplay/SignalSource.cs
- Stage 3: Assets/Scripts/Gameplay/SignalReceiver.cs
- Stage 3: Assets/Scripts/Gameplay/SignalReflector.cs
- Stage 3: Assets/Scripts/Gameplay/SignalGate.cs
- Stage 3: Assets/Scripts/Gameplay/SignalSplitter.cs
- Stage 3: Assets/Scripts/Gameplay/PowerNode.cs
- Stage 3: Assets/Scripts/Gameplay/PulseGate.cs
- Stage 3: Assets/Scripts/Gameplay/BlockerTile.cs
- Stage 3: Assets/Scripts/Gameplay/LevelConfig.cs
- Stage 4: Assets/Scripts/Managers/GameManager.cs
- Stage 4: Assets/Scripts/Managers/LevelManager.cs
- Stage 4: Assets/Scripts/Managers/SaveManager.cs
- Stage 5: Assets/Scripts/Gameplay/SignalPathData.cs
- Stage 5: Assets/Scripts/Managers/SignalGridManager.cs
- Stage 5: Assets/Scripts/Managers/SignalPathCalculator.cs
- Stage 5: Assets/Scripts/Managers/SignalBeamRenderer.cs
- Stage 6: Assets/Scripts/Managers/TouchInputController.cs
- Stage 6: Assets/Scripts/Managers/UndoManager.cs
- Stage 7: Assets/Scripts/Managers/UIManager.cs
- Stage 7: Assets/Scripts/Managers/LevelSelectUI.cs
- Stage 7: Assets/Scripts/Managers/SafeAreaHandler.cs
- Stage 8: Assets/Scripts/Managers/TutorialManager.cs
- Stage 8: Assets/Scripts/Managers/HintManager.cs
- Stage 8: Assets/Scripts/Managers/StarRatingManager.cs
- Stage 8: Assets/Scripts/Managers/FeedbackTextUI.cs
- Stage 9: Assets/Scripts/Managers/AudioManager.cs
- Stage 10: No new runtime level generator scripts were created.
- Stage 11: Assets/Scripts/Managers/CameraShakeOnly.cs
- Stage 11: Assets/Scripts/Managers/PolishFeedbackController.cs
- Stage 11: Assets/Scripts/Managers/PanelShowAnimator.cs
- Stage 12: No new scripts were created; existing manager references and mobile build settings were finalized.

## Inspector References Required
- Future scripts must expose core scene references through serialized fields.
- Game systems must reference manually placed scene objects through the Inspector.
- UI controllers must reference existing TextMeshPro and UI objects from the scene.
- Camera controllers must reference the manually placed camera rig and targets.
- Level controllers must reference manually assembled level roots, towers, paths, goals, blockers, and related scene objects.
- Audio controllers must reference existing AudioSource components and free-license placeholder audio clips.
- Future manager scripts must be attached manually to the existing Manager objects.
- Future gameplay scripts must reference the existing Level_01 through Level_12 child groups through serialized Inspector fields.
- Future UI scripts must reference existing panels under UI/Canvas and must not create UI at runtime.
- Future effects scripts must reference existing particle placeholder objects under Effects or LevelEffects.
- SignalTile components require gridPosition, isBlocked, and currentOccupant values to be set per manually placed tile.
- SignalSource components require signalColour, outputDirection, sourceTile, and isActive values.
- SignalReceiver components require requiredColour, receiverTile, isPowered, requiresPowerNode, and requiredPowerNode values.
- SignalReflector components require currentDirection, reflectorTile, rotationCost, selectedVisual, and optional visualRoot assignment.
- SignalGate components require acceptedColour, gateTile, and isOpen values.
- SignalSplitter components require inputDirection and outputDirections values.
- PowerNode components require activationCost, isActivated, and nodeTile values.
- PulseGate components require isOpen, startsOpen, and pulseAfterEachAction values.
- BlockerTile components should be attached to manually placed blocker objects.
- LevelConfig components require all level metadata, object lists, signalBeamRenderers, and levelEffects to be assigned through the Inspector.
- GameManager requires LevelManager, SaveManager, and optional existing UIManager GameObject references.
- LevelManager requires Level_01 through Level_12 parent GameObjects assigned in order.
- LevelManager requires matching LevelConfig components assigned in the same order as the level parents.
- SaveManager does not require scene object references, but must be attached to the existing Managers/SaveManager object.
- LevelConfig object lists still need final source, receiver, reflector, gate, splitter, power node, pulse gate, blocker, signal beam renderer, and level effect references after gameplay pieces are fully authored.
- SignalGridManager requires LevelManager and the active LevelConfig or assigned tile/object lists.
- SignalPathCalculator requires LevelManager, SignalGridManager, SignalBeamRenderer, and optional FeedbackTextUI reference.
- SignalBeamRenderer requires existing LineRenderer components assigned in its beamRenderers list.
- LevelConfig requires signalTiles to be assigned so routing can resolve grid boundaries.
- PulseGate requires pulseGateTile assignment.
- BlockerTile requires blockerTile assignment.
- TouchInputController requires Main Camera, EventSystem, SelectionRing, ValidTargetHighlight, InvalidActionMarker, GameManager, UndoManager, SignalPathCalculator, and optional FeedbackTextUI references.
- TouchInputController requires interactableMask to include placed gameplay object layers.
- UndoManager requires LevelManager, SignalPathCalculator, and optional FeedbackTextUI references.
- GameManager requires UndoManager assignment so undo is disabled after level complete or game over.
- LevelManager requires UndoManager assignment so undo state resets when a level restarts or changes.
- UI buttons must call TouchInputController.RotateSelectedButton, ActivateSelectedButton, UndoButton, RestartButton, and PauseButton.
- Interactable gameplay objects must have colliders sized for mobile touch targets.
- UIManager requires references to GameManager, LevelManager, SaveManager, LevelSelectUI, all existing UI panels, TMP text fields, sliders, toggles, star icons, and power bar fill image.
- LevelSelectUI requires GameManager, SaveManager, and all 12 manually created level button views.
- Each LevelSelectUI level button view requires a Button, level number TMP text, lock overlay, and 3 star Image references.
- SafeAreaHandler should remain attached to all top-level UI panels under Canvas.
- Feedback senders should target Managers/UIManager so ShowFeedback(string) updates the existing feedback text.
- SignalSplitter requires splitterTile and outputDirections assignment.
- TutorialManager requires LevelManager, SaveManager, and UIManager references.
- Tutorial levels require tutorialMessages to be authored in LevelConfig.
- HintManager requires LevelManager, SaveManager, and feedback target references.
- StarRatingManager requires LevelManager, SaveManager, UndoManager, HintManager, and UIManager references.
- FeedbackTextUI requires the existing FeedbackTextPanel GameObject, existing FeedbackText TMP text, and optional StarRatingManager reference.
- GameManager requires TutorialManager, HintManager, and StarRatingManager references for level start, restart, and completion flow.
- LevelManager requires HintManager, StarRatingManager, and FeedbackTextUI references so attempt state resets with the active level.
- AudioManager requires the existing Audio/MusicSource AudioSource reference.
- AudioManager requires the existing Audio/SFXSource AudioSource reference.
- AudioManager requires the existing Audio/UISFXSource AudioSource reference.
- AudioManager requires SaveManager for PlayerPrefs-backed volume saving.
- UIManager requires AudioManager so settings sliders update live music and SFX volume.
- FeedbackTextUI requires AudioManager if feedback messages should trigger matching SFX.
- AudioManager clip slots must be assigned in the Inspector with CC0 or clearly free-for-commercial-use clips before publishing.
- All Level_01 through Level_12 LevelConfig object lists are assigned.
- Level_01 through Level_12 each have assigned signalTiles, sources, receivers, reflectors, gates, splitters, powerNodes, pulseGates, blockers, signalBeamRenderers, and levelEffects lists.
- SignalBeamRenderer now reads the active LevelConfig signalBeamRenderers list before rendering paths.
- No Stage 10 level Inspector references are intentionally left unassigned.
- Main Camera has CameraShakeOnly attached and must remain the existing manually placed camera.
- Managers/FeedbackTextUI has PolishFeedbackController attached and references CameraShakeOnly plus all five existing global effect ParticleSystems.
- FeedbackTextUI references PolishFeedbackController so feedback messages can trigger polish effects.
- UI panels under UI/Canvas have CanvasGroup and PanelShowAnimator for simple show animation.
- Receiver glow uses existing receiver child renderers; final material tuning can be polished later.

## Known Issues
- Gameplay has not been implemented yet.
- Stage 2 created visual placeholders and low-poly foundation art only; final production art polish, animation, VFX tuning, and UI graphics are still pending.
- Inspector references will be defined as future scripts are created.
- The active Unity scene is currently Assets/Scenes/SampleScene.unity; it contains the GameScene root hierarchy.

## Final Validation Checklist
- Confirm all main gameplay objects exist in the Scene Hierarchy before pressing Play.
- Confirm no core scene objects are created with GameObject.CreatePrimitive at runtime.
- Confirm no core level layouts are created with Instantiate at runtime.
- Confirm no runtime UI creation is used.
- Confirm no runtime camera creation is used.
- Confirm no runtime level generation is used.
- Confirm core references are assigned through serialized fields in the Inspector.
- Confirm no core systems depend on GameObject.Find, FindObjectOfType, or Resources.Load.
- Confirm the game runs in landscape orientation on Android and iPhone.
- Confirm all gameplay can be completed with touch controls.
- Confirm keyboard shortcuts are limited to Unity Editor testing.
- Confirm there are no ads, IAP, internet requirements, or analytics SDKs.
- Confirm only Unity primitives, TextMeshPro, free-license placeholder audio, and clean modular scripts are used.
- Confirm CODEX.md has been updated after every completed stage.

## Stage 0 File Changes
- Created CODEX.md.

## Stage 1 File Changes
- Updated Assets/Scenes/SampleScene.unity with the complete GameScene hierarchy.
- Updated CODEX.md with Stage 1 progress and future setup requirements.

## Stage 1 Validation
- Full requested hierarchy exists under GameScene.
- Level_01 is active by default.
- Level_02 through Level_12 are inactive by default.
- Every level contains SignalGrid, Sources, Receivers, Reflectors, Gates, Splitters, PowerNodes, PulseGates, Blockers, SignalBeams, and LevelEffects.
- Canvas exists under UI and uses Screen Space Overlay.
- Canvas Scaler uses Scale With Screen Size, reference resolution 1920 x 1080, and match 0.5.
- EventSystem exists under UI.
- Main Camera exists under GameScene, uses orthographic projection, and is placed at position 8, 12, -8 with rotation 55, 45, 0.
- Directional Light exists under GameScene.
- Scene validation found no missing references.
- No runtime hierarchy generation was added.
- No gameplay logic was added.

## Stage 2 File Changes
- Updated Assets/Scenes/SampleScene.unity with manually placed primitive visual foundation objects.
- Created Assets/Materials/Stage2 material assets.
- Updated CODEX.md with Stage 2 progress, validation, and pending art polish notes.

## Stage 2 Materials Created
- Floor_Dark
- Grid_Normal
- Grid_Selected
- Grid_Highlight
- Source_Red
- Source_Blue
- Source_Green
- Source_Yellow
- Receiver_Inactive
- Receiver_Active_Red
- Receiver_Active_Blue
- Receiver_Active_Green
- Receiver_Active_Yellow
- Reflector_Metal
- Reflector_Selected
- Gate_Red
- Gate_Blue
- Gate_Green
- Gate_Yellow
- Blocker_Dark
- PowerNode_Active
- PowerNode_Inactive
- Beam_Red
- Beam_Blue
- Beam_Green
- Beam_Yellow

## Stage 2 Environment Objects Created
- Floor/Floor_Platform
- Floor/Board_Recess
- BackgroundDecorations/Distant_Backdrop_North
- BackgroundDecorations/Signal_Strip_North_Red
- BackgroundDecorations/Signal_Strip_North_Blue
- BackgroundDecorations/Distant_Backdrop_East
- BackgroundDecorations/Signal_Strip_East_Green
- BackgroundDecorations/Signal_Strip_East_Yellow
- BoundaryWalls/North_Wall
- BoundaryWalls/South_Wall
- BoundaryWalls/East_Wall
- BoundaryWalls/West_Wall
- FuturisticBuildings/LowPoly_Building_01 through LowPoly_Building_10
- FuturisticBuildings/Building_LightStrip_01 through Building_LightStrip_10
- AntennaDecorations/Antenna_Mast_01 through Antenna_Mast_04
- AntennaDecorations/Antenna_Node_01 through Antenna_Node_04
- CableDecorations/Cable_Backbone_X
- CableDecorations/Cable_Backbone_Z
- CableDecorations/Cable_Glow_Red
- CableDecorations/Cable_Glow_Blue
- LightingDecorations/Glow_Pylon_01 through Glow_Pylon_08

## Stage 2 Shared Gameplay Visuals Created
- SharedGameplayObjects/SelectionRing with four primitive ring segments; inactive by default.
- SharedGameplayObjects/ValidTargetHighlight with a flat highlight pad; inactive by default.
- SharedGameplayObjects/InvalidActionMarker with two crossing slash primitives; inactive by default.

## Stage 2 Pending Art Polish Notes
- Replace placeholder board pieces with final low-poly proportions once gameplay rules are locked.
- Add final TextMeshPro UI styling in a later UI stage.
- Tune material brightness and emission after device testing.
- Add lightweight particle systems later using the existing Effects and LevelEffects hierarchy.
- Keep lighting mobile friendly: one Directional Light, ambient lighting, and no expensive post-processing.

## Stage 2 Validation
- All requested Stage 2 materials exist in Assets/Materials/Stage2.
- Environment objects exist in the Scene Hierarchy before Play Mode.
- Shared gameplay visual objects exist and start inactive.
- Level_01 has visual board styling using manually placed primitive objects.
- Scene uses one Directional Light and ambient lighting.
- Scene validation found no missing references.
- No runtime environment generation was added.
- No gameplay logic was added.
- No new C# scripts were created for Stage 2.

## Stage 3 File Changes
- Created Assets/Scripts/Gameplay folder.
- Created modular gameplay object scripts for manually placed scene objects.
- Created SignalTypes.cs for shared SignalColour and SignalDirection enums.
- Updated CODEX.md with Stage 3 progress, scripts, Inspector requirements, and validation.

## Stage 3 Scripts Created
- InteractableObject: base selectable object with objectName, canSelect, canActivate, canRotate, OnSelected, OnDeselected, OnActivate, and OnRotate.
- SignalTile: grid tile data with gridPosition, isBlocked, and currentOccupant.
- SignalSource: signal source data with signalColour, outputDirection, sourceTile, and isActive.
- SignalReceiver: receiver state with requiredColour, receiverTile, isPowered, requiresPowerNode, requiredPowerNode, SetPowered, and ResetReceiver.
- SignalReflector: reflector state with currentDirection, reflectorTile, rotationCost, selectedVisual, RotateReflector, ResetReflector, and smooth rotation animation.
- SignalGate: colour gate with acceptedColour, gateTile, isOpen, and CanPass.
- SignalSplitter: splitter data with inputDirection, outputDirections, and GetOutputDirections.
- PowerNode: power node data with activationCost, isActivated, nodeTile, ActivateNode, and ResetNode.
- PulseGate: pulse gate state with isOpen, startsOpen, pulseAfterEachAction, TogglePulseState, and ResetPulseGate.
- BlockerTile: simple blocker marker component.
- LevelConfig: level metadata, budgets, star limits, hints, tutorial messages, gameplay object lists, signalBeamRenderers, and levelEffects.
- SignalTypes: shared SignalColour and SignalDirection enums plus direction helper methods.

## Stage 3 Validation
- Unity compilation completed with zero reported C# errors.
- Unity console reported zero errors after script import.
- New scripts contain no GameObject.Find, FindObjectOfType, Resources.Load, Instantiate, GameObject.CreatePrimitive, runtime UI creation, or runtime camera creation.
- Scripts only expose data and control existing assigned scene objects.
- No scene hierarchy generation was added.
- No gameplay managers or level generators were implemented in this stage.

## Stage 4 File Changes
- Created Assets/Scripts/Managers/GameManager.cs.
- Created Assets/Scripts/Managers/LevelManager.cs.
- Created Assets/Scripts/Managers/SaveManager.cs.
- Updated Assets/Scenes/SampleScene.unity by attaching manager scripts to existing manager GameObjects.
- Added LevelConfig components to existing Level_01 through Level_12 parent GameObjects.
- Updated CODEX.md with Stage 4 progress, save keys, Inspector requirements, and testing notes.

## Stage 4 Game Flow Scripts Created
- GameManager: tracks Title, MainMenu, LevelSelect, Playing, Paused, LevelComplete, and GameOver states; starts levels; pauses/resumes; restarts; completes levels; triggers game over; notifies an assigned UIManager object without creating UI.
- LevelManager: stores serialized Level_01 through Level_12 parent references; stores serialized LevelConfig references; activates one level and deactivates the rest; resets existing receivers, reflectors, power nodes, pulse gates, signal beams, and level effects.
- SaveManager: stores offline progress and settings with PlayerPrefs.

## Stage 4 Save Keys Used
- SignalTower.HighestUnlockedLevel
- SignalTower.MusicVolume
- SignalTower.SFXVolume
- SignalTower.Vibration
- SignalTower.TutorialSeen
- SignalTower.Level.{level}.Stars
- SignalTower.Level.{level}.HintUsed

## Stage 4 Inspector References Needed
- Managers/GameManager: assign LevelManager, SaveManager, and existing UIManager GameObject.
- Managers/LevelManager: assign Level_01 through Level_12 parent GameObjects in order.
- Managers/LevelManager: assign Level_01 through Level_12 LevelConfig components in the same order.
- LevelConfig on each level: fill final objective text, budgets, star limits, hint text, tutorial messages, and gameplay object lists.
- LevelConfig on each level: assign final signalBeamRenderers and levelEffects once those objects are finalized.

## Stage 4 Testing Notes
- Unity compilation completed with zero reported C# errors.
- Scene validation found no missing references.
- GameManager, LevelManager, and SaveManager are attached to existing manager GameObjects.
- LevelManager reports 12 level parent references and 12 LevelConfig components.
- Current level starts at Level_01 and highest unlocked level starts at 1.
- New scripts contain no GameObject.Find, FindObjectOfType, Resources.Load, Instantiate, GameObject.CreatePrimitive, runtime UI creation, runtime camera creation, or runtime level generation.

## Stage 5 File Changes
- Created Assets/Scripts/Gameplay/SignalPathData.cs.
- Created Assets/Scripts/Managers/SignalGridManager.cs.
- Created Assets/Scripts/Managers/SignalPathCalculator.cs.
- Created Assets/Scripts/Managers/SignalBeamRenderer.cs.
- Updated SignalTypes.cs with grid direction offsets.
- Updated LevelConfig.cs with signalTiles.
- Updated PulseGate.cs with pulseGateTile.
- Updated BlockerTile.cs with blockerTile reference.
- Updated SignalSplitter.cs with splitterTile.
- Updated Assets/Scenes/SampleScene.unity with Stage 5 manager components, Level_01 routing component assignments, and reusable SignalLine_01 through SignalLine_32 LineRenderer objects.

## Stage 5 Signal Calculation Rules Implemented
- Recalculate signal paths from every active source.
- Support Red, Blue, Green, and Yellow signal colours.
- Support multiple simultaneous sources.
- Move signals tile by tile using SignalDirection grid offsets.
- Stop signals at missing tiles and grid boundaries.
- Stop signals at blocked tiles and assigned BlockerTile references.
- Stop signals at closed PulseGate objects.
- Stop signals at closed gates.
- Stop signals at wrong-colour gates.
- Reflect signals using SignalReflector currentDirection.
- Split signals using SignalSplitter outputDirections.
- Activate receivers only when signal colour matches requiredColour.
- Never activate a receiver with the wrong signal colour.
- Track visited tile, direction, and colour pairs to stop loops safely.
- Report feedback messages for Receiver powered, Wrong colour, Signal blocked, Gate locked, Gate opened, and Signal loop detected.

## Stage 5 Beam Renderer Behaviour
- SignalBeamRenderer uses existing assigned LineRenderer objects.
- Unused beams are disabled before each render.
- Active beams are enabled, positioned between existing tile world positions, and coloured by signal colour.
- Beam pulse effect adjusts width only on active assigned LineRenderer objects.
- Stage 5 added 32 reusable LineRenderer objects under Level_01/SignalBeams for the starter level.
- No LineRenderer objects are created at runtime.

## Stage 5 Inspector References Required
- Managers/SignalGridManager: assign LevelManager.
- Managers/SignalPathCalculator: assign LevelManager, SignalGridManager, SignalBeamRenderer, and optional FeedbackTextUI.
- Managers/SignalPathCalculator: tune maxStepsPerBranch if larger levels require it.
- Managers/SignalPathCalculator: SignalBeamRenderer component must keep existing LineRenderer references assigned.
- LevelConfig: assign signalTiles, sources, receivers, reflectors, gates, splitters, powerNodes, pulseGates, blockers, signalBeamRenderers, and levelEffects.
- SignalSource: assign signalColour, outputDirection, sourceTile, and isActive.
- SignalReceiver: assign requiredColour, receiverTile, optional requiredPowerNode, and requiresPowerNode.
- SignalReflector: assign currentDirection, reflectorTile, rotationCost, selectedVisual, and optional visualRoot.
- SignalGate: assign acceptedColour, gateTile, and isOpen.
- SignalSplitter: assign inputDirection, splitterTile, and outputDirections.
- PowerNode: assign activationCost and nodeTile.
- PulseGate: assign startsOpen, pulseAfterEachAction, and pulseGateTile.
- BlockerTile: assign blockerTile.

## Stage 5 Known Limitations
- Level_01 has starter routing references wired for validation; Level_02 through Level_12 still need final tile, object, and beam assignments after their manual layouts are authored.
- Reflectors currently redirect to their currentDirection directly; advanced mirror-angle behaviour can be expanded later if puzzle rules require it.
- SignalSplitter does not yet validate inputDirection before splitting.
- Feedback is sent through optional SendMessage to an existing feedback object until the full UI feedback system is implemented.

## Stage 5 Testing Notes
- Unity compilation completed with zero reported C# errors.
- Scene validation found no missing references.
- Forbidden-call scan found no GameObject.Find, FindObjectOfType, Resources.Load, Instantiate, GameObject.CreatePrimitive, runtime UI creation, runtime camera creation, or runtime level generation in Assets/Scripts.
- In-editor path calculation test on Level_01 returned 16 path segments.
- Test feedback included Receiver powered, Signal blocked, and Wrong colour.
- All Stage 5 LineRenderer beam objects were returned to disabled state after validation.

## Stage 6 File Changes
- Created Assets/Scripts/Managers/TouchInputController.cs.
- Created Assets/Scripts/Managers/UndoManager.cs.
- Updated Assets/Scripts/Gameplay/SignalReflector.cs with SetDirection for undo restoration.
- Updated Assets/Scripts/Gameplay/PowerNode.cs with activation override and SetActivated for undo restoration.
- Updated Assets/Scripts/Gameplay/SignalGate.cs with activation toggle support.
- Updated Assets/Scripts/Gameplay/PulseGate.cs with activation toggle support and SetOpen for undo restoration.
- Updated Assets/Scripts/Managers/GameManager.cs to enable undo during play and disable undo after level complete or game over.
- Updated Assets/Scripts/Managers/LevelManager.cs to reset undo state when the current level resets.
- Updated Assets/Scripts/Managers/SignalPathCalculator.cs so signal recalculation no longer auto-activates PowerNode objects.
- Updated Assets/Scenes/SampleScene.unity by attaching TouchInputController and UndoManager to existing manager objects, wiring serialized references, and adding colliders to existing Level_01 interactables.

## Stage 6 Touch Input Implemented
- TouchInputController uses the existing manually placed Main Camera for screen-to-world raycasts.
- TouchInputController supports Android and iPhone tap input through Unity touch events.
- Unity Editor mouse clicks are supported only as an editor test fallback.
- TouchInputController selects existing InteractableObject components hit by raycast.
- SelectionRing and ValidTargetHighlight are existing shared scene objects moved and shown on selection.
- InvalidActionMarker is an existing shared scene object moved and shown on invalid taps or invalid actions.
- Feedback is sent to the optional existing FeedbackTextUI object through ShowFeedback(string).
- The controller does not create cameras, UI, hierarchy objects, or level objects.

## Stage 6 Selected Object Actions
- RotateSelectedButton rotates the selected SignalReflector through UndoManager.
- ActivateSelectedButton activates the selected PowerNode through UndoManager.
- ActivateSelectedButton can call existing SignalGate and PulseGate activation behaviour for special interactables.
- UndoButton calls UndoManager.UndoLastAction.
- RestartButton calls GameManager.RestartCurrentLevel.
- PauseButton calls GameManager.PauseGame.
- Editor-only keyboard shortcuts were added: R rotates, A activates, U undoes, and Escape pauses.

## Stage 6 Undo Implemented
- UndoManager supports undo for reflector rotations.
- UndoManager supports undo for power node activations.
- Undo restores reflector direction.
- Undo restores power node activation state.
- Undo restores action count and power spent count.
- Undo captures and restores pulse gate open states around reversible actions.
- Undo recalculates signal paths after restoration so receiver powered state and beams refresh from the restored board state.
- Undo is cleared and disabled after level complete or game over.
- Undo state resets when the active level is reset or changed.

## Stage 6 Inspector References Required
- Managers/TouchInputController: assign Main Camera, EventSystem, SelectionRing, ValidTargetHighlight, InvalidActionMarker, GameManager, UndoManager, SignalPathCalculator, and optional FeedbackTextUI.
- Managers/TouchInputController: verify interactableMask includes the layers used by gameplay objects.
- Managers/UndoManager: assign LevelManager, SignalPathCalculator, and optional FeedbackTextUI.
- Managers/GameManager: assign UndoManager.
- Managers/LevelManager: assign UndoManager.
- UI button OnClick events must be manually connected to TouchInputController button methods.
- Future manually placed interactables in Level_02 through Level_12 must receive colliders and Inspector references.

## Stage 6 Testing Notes
- Unity compilation completed with zero reported C# errors.
- Scene validation found no missing references.
- All 14 current InteractableObject components in Level_01 have colliders.
- In-editor undo test rotated a reflector from North to East, then restored it to North.
- The same undo test restored action count to 0 and power spent to 0.
- Forbidden-call scan found no GameObject.Find, FindObjectOfType, Resources.Load, Instantiate, GameObject.CreatePrimitive, runtime UI creation, runtime camera creation, or runtime level generation in Assets/Scripts.

## Stage 7 File Changes
- Created Assets/Scripts/Managers/UIManager.cs.
- Created Assets/Scripts/Managers/LevelSelectUI.cs.
- Created Assets/Scripts/Managers/SafeAreaHandler.cs.
- Updated Assets/Scenes/SampleScene.unity with completed UI panel contents, UIManager and LevelSelectUI components, serialized UI references, button OnClick connections, level select button references, feedback target rewiring, and SafeAreaHandler components.
- Updated CODEX.md with Stage 7 progress, UI status, references, and validation notes.

## Stage 7 UI Panels Completed
- TitleScreen: title, subtitle, and Start button.
- MainMenuPanel: Play, Level Select, Settings, Credits, and editor Quit buttons.
- LevelSelectPanel: 12 manually placed level buttons, Back button, level number text, lock overlays, and 3 star icons per level.
- GameplayHUD: current level text, objective text, power bar, power number text, action count text, active/required receiver counts, signal colour indicator, hint, undo, restart, pause, feedback text, and HUD star icons.
- MobileControlsPanel: Rotate Selected, Activate Selected, Undo, Restart, Pause, and Hint buttons.
- PausePanel: Resume, Restart, and Main Menu buttons.
- SettingsPanel: Music volume slider, SFX volume slider, vibration toggle placeholder, Reset Progress, and Back.
- LevelCompletePanel: 3 star display, power remaining, actions used, receivers powered, Next Level, Retry, and Main Menu.
- GameOverPanel: failure reason, Retry, and Main Menu.
- CreditsPanel: includes required credits text confirming Unity default primitives, placeholder/free commercial-use fonts/sounds/music, and no paid or copyrighted assets.
- TutorialPanel: tutorial message text, Continue, and Skip.
- FeedbackTextPanel: shared feedback TMP text area.

## Stage 7 UIManager Status
- UIManager shows and hides existing panels only.
- UIManager updates assigned TMP text fields.
- UIManager updates assigned sliders, power bar fill, star icons, current level text, objective text, action count text, receiver count text, and feedback text.
- UIManager exposes button methods for Start, Play, Level Select, Settings, Credits, Back/Main Menu, Resume, Restart, Next Level, Retry, Quit, Hint, tutorial continue/skip, reset progress, music volume, SFX volume, and vibration.
- Button OnClick events for non-level-select buttons are connected to UIManager or TouchInputController methods.
- Feedback targets for TouchInputController, UndoManager, and SignalPathCalculator are rewired to Managers/UIManager.

## Stage 7 Level Select Status
- LevelSelectUI references 12 manually created level buttons.
- Each level button has level number TMP text, a lock overlay, and 3 star Image references.
- LevelSelectUI refreshes lock state from SaveManager.GetHighestUnlockedLevel.
- LevelSelectUI refreshes earned stars from SaveManager.GetStars.
- LevelSelectUI opens unlocked levels through GameManager.StartLevel.
- No level select buttons are created at runtime.

## Stage 7 Safe Area Support Status
- SafeAreaHandler is attached to all 12 top-level Canvas panels.
- SafeAreaHandler applies Screen.safeArea to keep UI away from iPhone notches, iPhone home indicator, Android cutouts, and rounded screen corners.
- SafeAreaHandler updates when safe area or screen size changes.

## Stage 7 Testing Notes
- Unity compilation completed with zero reported C# errors.
- Scene validation found no missing references.
- UI audit confirmed all 12 required panels have child UI content.
- UI audit confirmed 12 SafeAreaHandler components exist on required panels.
- UI audit confirmed 12 manually placed LevelButton_01 through LevelButton_12 objects exist.
- UI audit found 42 Button components, all with persistent listeners or serialized LevelSelectUI handling.
- Forbidden-call scan found no GameObject.Find, FindObjectOfType, Resources.Load, Instantiate, GameObject.CreatePrimitive, runtime UI creation, runtime camera creation, or runtime level generation in Assets/Scripts.

## Stage 8 File Changes
- Created Assets/Scripts/Managers/TutorialManager.cs.
- Created Assets/Scripts/Managers/HintManager.cs.
- Created Assets/Scripts/Managers/StarRatingManager.cs.
- Created Assets/Scripts/Managers/FeedbackTextUI.cs.
- Updated Assets/Scripts/Managers/UIManager.cs to delegate hint, tutorial, and feedback calls to the Stage 8 systems.
- Updated Assets/Scripts/Managers/GameManager.cs to reset Stage 8 attempt state on level start/restart and calculate completion stars.
- Updated Assets/Scripts/Managers/LevelManager.cs to reset hint, star, and feedback state with the active level.
- Updated Assets/Scripts/Managers/UndoManager.cs with specific feedback messages for rotation, power activation, undo, no undo, and not enough power.
- Updated Assets/Scripts/Managers/TouchInputController.cs with selection feedback.
- Updated Assets/Scenes/SampleScene.unity by attaching Stage 8 scripts to existing manager GameObjects and wiring Inspector references.
- Updated Level_01 LevelConfig with starter tutorial messages.

## Stage 8 Tutorial System Completed
- TutorialManager reads tutorialMessages from the active LevelConfig.
- TutorialManager shows the existing TutorialPanel through UIManager.SetTutorialMessage.
- Continue advances to the next authored tutorial message.
- Skip closes the tutorial.
- Tutorial seen status is saved through SaveManager using PlayerPrefs key SignalTower.TutorialSeen.
- Tutorial UI is not created from code.

## Stage 8 Hint System Completed
- HintManager reads hintText from the active LevelConfig.
- HintManager shows hints through the existing FeedbackTextPanel and FeedbackTextUI route.
- Hint use is tracked per current attempt and saved through SaveManager for the current level.
- Hint use caps the maximum star rating at 2 when hintCapsStarsAtTwo is enabled.
- Hint UI is not created from code.

## Stage 8 Star Rating Rules
- StarRatingManager calculates stars after level completion from LevelConfig star action limits, remaining power, hint use, and tracked mistakes.
- 3 stars require the solution to meet star3ActionLimit, keep good power remaining, avoid major/minor mistakes, and avoid hint use.
- 2 stars require the solution to meet star2ActionLimit with no more than one major mistake; hint use can still allow up to 2 stars.
- 1 star is awarded for solved levels with high action count, low power, several mistakes, or performance outside the 2-star threshold.
- Star results are shown on LevelCompletePanel through UIManager and saved with SaveManager.SaveStars.

## Stage 8 Feedback Messages Implemented
- Reflector selected
- Reflector rotated
- Receiver powered
- Wrong colour
- Signal blocked
- Gate locked
- Gate opened
- Power node active
- Not enough power
- Undo complete
- No action to undo
- Hint used
- Level complete
- FeedbackTextUI shows temporary messages using the existing FeedbackTextPanel and TMP text.

## Stage 8 Testing Notes
- Unity compilation completed with zero reported C# errors after Stage 8 script import.
- Stage 8 manager reference validation confirmed TutorialManager, HintManager, StarRatingManager, FeedbackTextUI, UIManager, GameManager, and LevelManager references are assigned.
- Forbidden-call scan found no GameObject.Find, FindObjectOfType, Resources.Load, Instantiate, GameObject.CreatePrimitive, runtime UI creation, runtime camera creation, or runtime level generation in Assets/Scripts.
- Stage 8 did not add runtime UI generation, runtime hierarchy generation, or runtime level generation.

## Stage 9 File Changes
- Created Assets/Scripts/Managers/AudioManager.cs.
- Updated Assets/Scripts/Managers/UIManager.cs so existing settings sliders update AudioManager music and SFX volume, with SaveManager fallback if AudioManager is absent.
- Updated Assets/Scripts/Managers/UIManager.cs so existing UI button handlers play the assigned UI click sound.
- Updated Assets/Scripts/Managers/FeedbackTextUI.cs so feedback messages can trigger matching AudioManager SFX cues.
- Updated Assets/Scenes/SampleScene.unity by attaching AudioManager to Managers/AudioManager and wiring existing AudioSource references.
- Updated CODEX.md with Stage 9 progress, references, licence notes, and placeholder audio list.

## Stage 9 AudioManager Completed
- AudioManager references the existing Audio/MusicSource AudioSource.
- AudioManager references the existing Audio/SFXSource AudioSource.
- AudioManager references the existing Audio/UISFXSource AudioSource.
- AudioManager exposes Inspector clip fields for UI button click, reflector select, reflector rotate, signal update pulse, receiver activated, wrong colour blocked, gate open, gate blocked, power node activated, not enough power, undo action, hint opened, level complete, game over, star reward, and background music.
- AudioManager controls separate music and SFX volume.
- AudioManager loads and saves music/SFX volume through SaveManager PlayerPrefs methods.
- AudioManager plays feedback-linked cues from FeedbackTextUI using existing feedback message strings.
- AudioManager does not create AudioSource objects at runtime.

## Stage 9 Audio Clip Licence Notes
- No real audio files were added in Stage 9 because no local clip files with verified licence metadata were present.
- Before publishing, assign only CC0 or clearly free-for-commercial-use audio clips.
- Preferred sources are Kenney CC0 audio packs, OpenGameArt assets marked CC0 or clearly free for commercial use, or Freesound clips explicitly marked CC0.
- Do not use copyrighted songs, paid audio, or random internet files without licence confirmation.
- Keep the source URL, licence type, author if required, and download date in project notes when final clips are assigned.

## Stage 9 Missing Placeholder Audio List
- Background music loop.
- UI button click.
- Reflector select.
- Reflector rotate.
- Signal update pulse.
- Receiver activated.
- Wrong colour blocked.
- Gate open.
- Gate blocked.
- Power node activated.
- Not enough power.
- Undo action.
- Hint opened.
- Level complete.
- Game over.
- Star reward.

## Stage 9 Testing Notes
- Unity compilation completed with zero reported C# errors.
- AudioManager required reference validation confirmed MusicSource, SFXSource, UISFXSource, and SaveManager are assigned.
- UIManager and FeedbackTextUI are wired to AudioManager.
- Forbidden-call scan found no GameObject.Find, FindObjectOfType, Resources.Load, Instantiate, GameObject.CreatePrimitive, new AudioSource, runtime UI creation, runtime camera creation, or runtime level generation in Assets/Scripts.
- Stage 9 did not add runtime audio source generation or any runtime scene hierarchy generation.

## Stage 10 File Changes
- Updated Assets/Scenes/SampleScene.unity with authored scene objects for Level_01 through Level_12.
- Updated Assets/Scripts/Managers/SignalBeamRenderer.cs so it uses existing LineRenderer references from the active LevelConfig.
- Updated Assets/Scripts/Managers/SignalPathCalculator.cs so editor validation can recalculate paths without sending Play Mode feedback messages.
- Updated Assets/Scripts/Gameplay/SignalReflector.cs so editor validation resets do not start coroutines or overwrite authored reflector directions.
- Updated Assets/Scripts/Managers/FeedbackTextUI.cs so editor validation does not start feedback hide coroutines outside Play Mode.
- Updated CODEX.md with Stage 10 progress, objectives, notes, and validation status.

## Stage 10 Level Objective List
- Level 1: Tutorial level; one red source powers one red receiver in a straight line.
- Level 2: Introduces one reflector; rotate it to route the red signal north.
- Level 3: Introduces blockers; redirect the signal around a blocker wall.
- Level 4: Introduces colour matching; red source must power the red receiver while the blue receiver stays inactive.
- Level 5: Introduces splitter; one red source powers two red receivers.
- Level 6: Introduces colour gate; red signal passes through a red gate.
- Level 7: Introduces limited actions; solve with two reflector rotations.
- Level 8: Introduces power node; activate a node before the receiver can hold charge.
- Level 9: Introduces two signal colours; red and blue signals solve separate lanes.
- Level 10: Introduces locked receiver and key node; activate the key node to unlock the receiver.
- Level 11: Introduces pulse gate; one action opens the pulse gate and routes the signal.
- Level 12: Final challenge with larger grid, multiple colours, reflectors, receivers, blockers, colour gates, splitter routing, limited power, and a key power node.

## Stage 10 Level-Specific Notes
- Level_01 is active by default; Level_02 through Level_12 are inactive by default.
- Level_01 includes tutorial messages for source and receiver basics.
- Level_02 includes tutorial messages for reflector selection and rotation.
- Level_03 uses a three-block wall and three reflectors for around-the-wall routing.
- Level_04 intentionally places a wrong-colour receiver in the red path to confirm wrong-colour receivers do not activate.
- Level_05 uses one splitter with north and south outputs.
- Level_06 uses an open red colour gate.
- Level_07 uses a power/action budget of 2 to enforce the two-rotation solution.
- Level_08 and Level_10 use receiver requiredPowerNode references.
- Level_11 uses a pulse gate that starts closed and toggles after player action.
- Level_12 uses 80 existing LineRenderer holders to support the larger final routing layout.

## Stage 10 Inspector References Still Missing
- No Stage 10 LevelConfig gameplay object lists are missing.
- No Stage 10 AudioSource, camera, UI, or manager references were intentionally changed.
- Final production art, final CC0 audio clips, and later balance tuning remain future polish work.

## Stage 10 Test Status
- Level_01: ActivateLevel succeeded; intended solution powered 1/1 required receivers.
- Level_02: ActivateLevel succeeded; intended solution powered 1/1 required receivers.
- Level_03: ActivateLevel succeeded; intended solution powered 1/1 required receivers.
- Level_04: ActivateLevel succeeded; intended solution powered 1/1 required receivers.
- Level_05: ActivateLevel succeeded; intended solution powered 2/2 required receivers.
- Level_06: ActivateLevel succeeded; intended solution powered 1/1 required receivers.
- Level_07: ActivateLevel succeeded; intended solution powered 1/1 required receivers.
- Level_08: ActivateLevel succeeded; intended solution powered 1/1 required receivers.
- Level_09: ActivateLevel succeeded; intended solution powered 2/2 required receivers.
- Level_10: ActivateLevel succeeded; intended solution powered 1/1 required receivers.
- Level_11: ActivateLevel succeeded; intended solution powered 1/1 required receivers.
- Level_12: ActivateLevel succeeded; intended solution powered 4/4 required receivers.
- Default activation pass after editor feedback guard succeeded for all 12 levels with no Unity console errors.
- Unity compilation completed with zero reported C# errors.
- Unity console reported zero errors after final validation.
- Forbidden-call scan found no GameObject.Find, FindObjectOfType, Resources.Load, Instantiate, GameObject.CreatePrimitive, new GameObject, new AudioSource, runtime UI creation, runtime camera creation, or runtime level generation in Assets/Scripts.

## Stage 11 File Changes
- Created Assets/Scripts/Managers/CameraShakeOnly.cs.
- Created Assets/Scripts/Managers/PolishFeedbackController.cs.
- Created Assets/Scripts/Managers/PanelShowAnimator.cs.
- Updated Assets/Scripts/Managers/FeedbackTextUI.cs to forward feedback messages to PolishFeedbackController.
- Updated Assets/Scripts/Gameplay/SignalReceiver.cs with receiver activation glow and pulse feedback.
- Updated Assets/Scripts/Managers/UIManager.cs with low power UI pulse and level complete star reward pulse.
- Updated Assets/Scenes/SampleScene.unity by attaching CameraShakeOnly, PolishFeedbackController, PanelShowAnimator, CanvasGroup, and lightweight ParticleSystem components to existing scene objects.
- Updated CODEX.md with Stage 11 progress, camera behaviour, performance notes, and remaining issues.

## Stage 11 Polish Features Added
- Smooth reflector rotation animation remains active for Play Mode.
- Signal beam pulse animation remains active through SignalBeamRenderer.
- Receiver activation glow and small scale pulse were added to SignalReceiver.
- Gate block feedback now routes through existing GateBlockedParticles and tiny camera shake.
- Power node activation feedback routes through existing PowerNodeParticles.
- Wrong action feedback routes through existing WrongActionParticles and tiny camera shake.
- Selected object highlight remains handled through the existing SelectionRing and selectedVisual objects.
- Valid interactable highlight remains handled through the existing ValidTargetHighlight object.
- Low power UI pulse was added to the existing power bar fill.
- Level complete celebration uses existing LevelCompleteParticles and tiny camera shake.
- Star reward animation pulses existing LevelCompletePanel star images.
- Simple panel show animation was added to existing UI panels using CanvasGroup and PanelShowAnimator.

## Stage 11 Camera Shake Behaviour
- CameraShakeOnly is attached to the existing manually placed Main Camera.
- CameraShakeOnly stores the camera original local position on Start.
- Invalid actions, wrong colour, blocked/gate-locked feedback, not enough power, and no undo trigger tiny short shake.
- Level complete triggers a slightly longer tiny shake.
- CameraShakeOnly always returns the camera to its original local position.
- CameraShakeOnly never changes base camera angle, zoom, follow target, projection, or hardcoded transform values.

## Stage 11 Mobile Performance Notes
- Polish uses simple primitives, existing UI objects, existing scene objects, and lightweight ParticleSystems only.
- The scene still uses one main camera and one directional light.
- No expensive post-processing was added.
- No unnecessary packages were added.
- No internet requirement, ads, IAP, or analytics were added.
- UI panels retain SafeAreaHandler support and landscape layout.
- Existing buttons remain large touch targets from Stage 7.

## Stage 11 Remaining Issues
- Particle effects are placeholder bursts and should be art-tuned after device testing.
- Receiver glow uses existing renderer material colour changes; final shader/emission tuning can be polished later.
- Real CC0/free-commercial audio clips are still missing from Stage 9.
- Final mobile aspect-ratio QA on physical iPhone and Android devices is still pending.

## Stage 11 Testing Notes
- Unity compilation completed with zero reported C# errors.
- Unity console reported zero errors after final validation.
- Stage 11 reference validation confirmed CameraShakeOnly, PolishFeedbackController, five ParticleSystems, FeedbackTextUI polish reference, CanvasGroup, and PanelShowAnimator setup.
- All 12 top-level UI panels have CanvasGroup and PanelShowAnimator.
- Forbidden-call scan found no GameObject.Find, FindObjectOfType, Resources.Load, Instantiate, GameObject.CreatePrimitive, new GameObject, new AudioSource, runtime UI creation, runtime camera creation, or runtime level generation in Assets/Scripts.

## Stage 12 File Changes
- Updated Assets/Scenes/SampleScene.unity by wiring final manager references for signal completion, UI action/power updates, tutorial close recalculation, and game over checks.
- Updated Project Settings through Unity PlayerSettings for landscape Android and iOS readiness.
- Updated Build Settings to include Assets/Scenes/SampleScene.unity.
- Updated Assets/Scripts/Managers/GameManager.cs to recalculate signal paths after level start/restart when no tutorial is open.
- Updated Assets/Scripts/Managers/TutorialManager.cs to recalculate signal paths after tutorial close.
- Updated Assets/Scripts/Managers/SignalPathCalculator.cs to update receiver counts and trigger level completion when required receivers are powered.
- Updated Assets/Scripts/Managers/UndoManager.cs to update HUD action/power counts and trigger game over when action limit is exhausted without solving.
- Updated CODEX.md with final validation results.

## Final Validation Results
- Scene hierarchy status: Passed. GameScene, Environment, UI, Main Camera, Directional Light, EventSystem, Levels, Level_01 through Level_12, Effects, Audio, and Managers exist before Play.
- UI status: Passed. Canvas, all panels, all non-level buttons, and all 12 level select buttons are manually created. Canvas is Screen Space Overlay. Canvas Scaler uses Scale With Screen Size, 1920 x 1080 reference resolution, and Match 0.5.
- Camera status: Passed. Main Camera is manually placed, orthographic, and uses CameraShakeOnly for temporary local-position shake only. No runtime camera creation, hardcoded camera reset, camera follow, base angle change, or base zoom change exists.
- Gameplay status: Passed. Main menu, level select, pause/resume, restart, settings, hints, undo, star rating, level complete, game over, signal routing, receiver colour matching, gates, splitters, pulse gates, power nodes, power budget, and action count are backed by existing scene objects and modular scripts.
- Level status: Passed. Level_01 through Level_12 are manually built in the hierarchy, activate through LevelManager by enabling existing level parents, and have complete LevelConfig lists. Intended solution validation powered all required receivers on every level.
- Audio status: Passed with placeholders. AudioManager references existing MusicSource, SFXSource, and UISFXSource. Separate music and SFX volume are saved through SaveManager. Real CC0/free-commercial audio clips still need assignment before publishing.
- Save system status: Passed. PlayerPrefs keys cover highest unlocked level, stars per level, music volume, SFX volume, vibration placeholder, tutorial seen, and hint used.
- Mobile readiness status: Passed for project configuration. Build Settings include Assets/Scenes/SampleScene.unity. Android and iOS identifiers are configured. Android minimum SDK is API 25 and target SDK is automatic. Landscape left/right are enabled and portrait orientations are disabled. UI safe area support is present. TouchInputController uses touch raycasts from the existing Main Camera.
- Missing references fixed: Passed. Final audit found zero missing scripts and zero missing object references.
- Compile status: Passed. Unity compilation reports zero errors and Unity console reports zero errors.
- Known issues: Physical-device QA is still pending; real CC0/free-commercial audio clips are still placeholders; final art and particle tuning are still polish tasks.
- Publishing notes: No paid or copyrighted assets, ads, IAP, analytics SDKs, internet requirement, runtime UI generation, runtime environment generation, runtime level generation, runtime camera generation, or runtime core AudioSource creation were added. Keep future changes Inspector-assigned and scene-authored.

## Future Stage Rules
- Keep the scene manually assembled.
- Add main objects to the Scene Hierarchy before Play Mode.
- Use scripts only to control existing scene objects.
- Assign core references through serialized fields in the Inspector.
- Do not add runtime generation for scenes, levels, UI, cameras, or core objects.
- Update CODEX.md after every completed stage.

## Post-Stage Visibility and Audio Fix
- Completed after Stage 12 in response to signal ray visibility and missing SFX/music feedback.
- Updated Assets/Scripts/Managers/SignalBeamRenderer.cs so rendered signal beams are wider, view-aligned, shadow-free, capped, and lifted slightly above tile centers to avoid being hidden by the board.
- Updated Assets/Scripts/Managers/SignalPathCalculator.cs so both public recalculation entry points update the existing LineRenderer beam holders.
- Created Assets/Materials/Stage2/SignalBeam_Unlit_Visible.mat and assigned it to existing manually placed LineRenderer objects.
- Created synthesized placeholder audio clips under Assets/Audio/GeneratedLicenseFree.
- Added Assets/Audio/GeneratedLicenseFree/LICENSE.md documenting that these clips were procedurally generated for this project, contain no third-party samples, and are free for commercial use without attribution.
- Assigned generated background music, UI click, hint, gameplay, win/loss, and star reward clips to the existing AudioManager clip fields.
- Confirmed no runtime level generation, runtime UI generation, runtime camera creation, or runtime AudioSource creation was added.
- Validation: Unity compilation reports zero errors, Unity console reports zero errors, Level_01 signal path produces 4 visible enabled LineRenderer beams after recalculation, and AudioManager has generated music/SFX clips assigned.

## Post-Stage Difficulty and UI Cleanup
- Completed after Stage 12 in response to auto-completing levels, duplicated gameplay buttons, and weak level difficulty.
- Updated Assets/Scripts/Managers/SignalPathCalculator.cs with an Inspector-assigned UndoManager reference so level completion requires at least one player action or power activation.
- Reworked Level_01, Level_04, Level_05, Level_06, Level_09, and Level_12 so they no longer start solved or partially completed.
- Added manually placed reflector objects to affected level hierarchies where needed; these objects exist in the scene before Play and are referenced by LevelConfig.
- Tightened objectives, hints, action limits, power budgets, and star thresholds across the level set.
- Final validation result: all 12 levels start with zero powered receivers and zero auto-solved levels.
- Solution validation result: all 12 levels can still power their required receivers in the intended solved state.
- Cleaned GameplayHUD duplication by hiding the HUD copies of Hint, Undo, Restart, and Pause. These controls now live in MobileControlsPanel with Rotate and Activate.
- Refreshed MobileControlsPanel button sizing, labels, and colours for clearer mobile touch use.
- Refreshed SettingsPanel layout, button colours, label sizes, and spacing.
- Confirmed the changes keep manual-scene rules intact: no runtime level generation, runtime UI generation, runtime camera creation, or runtime environment generation was added.
- Validation: Unity compilation reports zero errors after the cleanup.
