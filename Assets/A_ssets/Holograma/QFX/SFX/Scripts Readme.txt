---------------------------------------------------------------------------------------------------------
Animation
---------------------------------------------------------------------------------------------------------
_______________
AnimationModule
Allows you to specify a curved animation with the scale of the value scaling and time
_______________
DynamicShaderParameter
Allows you to specify a AnimationModule with name of the shader parameter
_______________
LightAnimator
Allows to animate the light intensity
______________
ShaderAnimator
Allows to animate shader parameters (float types)
______________
ShaderParameterSetter
Allows to set shader parameter just once (float types)

---------------------------------------------------------------------------------------------------------
Weapon
---------------------------------------------------------------------------------------------------------
______________
Beam Weapon
Creates a beam effect: LineRenderer between StartPosition and EndPosition with launch and impact effects
______________
PhysicsHomingMissile
The missile will be launched towards the launch position, and after a delay, it will follow the target (or coordinates).
______________
PhysicsHomingMissileLauncher
Launches PhysicsHomingMissiles
If you want to launch missile to the "marked" object, you need to select option "FollowTarget";otherwise, you need to
unselect it and set Mode of the TargetMarker to "Position."
______________
SimpleProjectile
The projectile just follows the forward direction.
______________
SimpleProjectileWeapon
Launches SimpleProjectiles with FireRate and LaunchEffect
______________
ProjectileLauncher
Just LaunchesProjectiles

---------------------------------------------------------------------------------------------------------
Collisions
---------------------------------------------------------------------------------------------------------
______________
ColliderCollisionDetector
Detects collisions by OnCollisionEnter event (Requires Collider)
______________
DistanceCollisionDetector
Casts a ray and checks the distance to object

---------------------------------------------------------------------------------------------------------
Fx
---------------------------------------------------------------------------------------------------------
FxObject
FxRotationType

Stub to specify GameObject with rotation type in order to instantiate it by FxObjectInstancer

---------------------------------------------------------------------------------------------------------
Instancer
---------------------------------------------------------------------------------------------------------
______________
CollisionBasedFxInstancer
Instantiates FxObjects on collision event

IEmitterKeeper
If you want to specify FxObjectRotationType to "LookAtEmitter", you need to inherit "projectile" of this interface and set EmitterTransform when you launch it.

To see an example, you can look up at SimpleProjectile and SimpleProjectileLauncher.

---------------------------------------------------------------------------------------------------------
Manager
---------------------------------------------------------------------------------------------------------
______________
ControlledObjects
It's a base object to allow you to specify a delay before running the object, and it won't execute the code if object is disabled.

______________
ComponentsStartupController
Allows to specify order of ControlledObjectsExecution
Too see and example, you can look up at Hologram3 prefab.

---------------------------------------------------------------------------------------------------------
Motion
---------------------------------------------------------------------------------------------------------
______________
PhysicsMotion
Launches the objects toward to direction (requires rigidbody) and have an option to destroy on collision

---------------------------------------------------------------------------------------------------------
Mouse
---------------------------------------------------------------------------------------------------------
______________
LookAtMouse
Rotates object to the mouse position

MouseControlledObjectLauncher
Run or Stop "ControlledObject" by mouse click

---------------------------------------------------------------------------------------------------------
Utils
---------------------------------------------------------------------------------------------------------
______________
MaterialAdder
Adds material to the all renderers of the objects
______________
MaterialReplacer
Replaces all materials of the all object renderers by one material
______________
GroundAttacher
Attaches the object to the ground
______________
SelfDestroyer
Destroys the object after a delay
______________
Rotator
Rotates the object
______________
TargetMarker
Allows to "mark" objects or positions and provides "marked" objects. For example, it uses in the HomingMissileLauncher.

---------------------------------------------------------------------------------------------------------
---------------------------------------------------------------------------------------------------------
______________
AreaProtectorController
Finds a object with "Enemy" class and starts to shoot him.
______________
EngineController
Changes the size of particles depended on speed power
______________
HealingAreaController
Finds a object wiith "Healable" class and attaches particles with it
______________
MotionCloner
Makes a copy of the object
______________
BeamScannerController
Finds object with "BeamScannable" class and changes color if the object is close to the target area
______________
TopographicScanner
Adds the post effect to the camera and TopographicScannable material to the object that touches scan
