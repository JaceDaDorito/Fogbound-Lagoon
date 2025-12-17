# LocationsOfPrecipitation
A stage modding toolkit that streamlines addressable asset usability in a scene.
## Features
- ``InstantiateAddressablePrefab`` script that instantiates a RoR2 prefab into the Scene at runtime. ``Set Position and Rotation To Zero`` makes the prefab have no offset; some RoR2 assets have offset, making it hard to set position or rotation. ``Use Local Position And Rotation`` would make the prefab's position and rotation relative to its parent object. ``Refresh In Editor`` makes the prefab visible in editor; the prefab isn't saved to the scene to prevent GUID errors.
![ ](https://i.imgur.com/NHdbdyj.png)
- ``AddressableInjector`` script that injects a RoR2 asset into a desired field at runtime. Select the component in ``Target Component``, pick the ``Target Field``, then fill in the ``Key`` with any address of that type. It doesn't show the injected asset to prevent GUID errors, but it will be injected at runtime. A log will be produced when the asset is successfully injected.
![ ](https://i.imgur.com/8621jVm.png)
![ ](https://i.imgur.com/sGoGYqJ.png)
- ``CameraInstantiator`` is an editor only script that injects the RoR2 camera into your scene. This can be used to preview post processing. Building the scene with this script will have no effect in runtime.
![ ](https://i.imgur.com/4IcbNNF.png)
- ``SurfaceDefInjector`` quickly injects ``SurfaceDefs`` into any ``SurfaceDefProviders`` attatched to any children.
- A Geyser Menu item under ``Risk of Rain 2``.
- ``InstantiateGeyserPrefab`` which allows you to instantiate and configure most of RoR2's jump pads.
![](https://i.imgur.com/XZqDqxS.png)
- ``InstantiateLogbookPrefab`` which allows you to instantiate and configure a logbook pickup prefab.
- ``SetupDLC3AccessNode`` which sets up the DLC3 access node event automatically. Needs Points of Interest on the map.
![](https://i.imgur.com/ZJj07nv.png)
## Credits
- Initial scripts by IDeathHD / Quentin
- Polish and cleaner editor functionality by Nebby
- Testing and implementation by JaceDaDorito
- Icon art by JaceDaDorito and IbanPlay
- Bug fixes by viliger
