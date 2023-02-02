
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
namespace ZSerializer {

[System.Serializable]
public sealed class AnimatorZSerializer : ZSerializer.Internal.ZSerializer {
    public UnityEngine.Vector3 rootPosition;
    public UnityEngine.Quaternion rootRotation;
    public System.Boolean applyRootMotion;
    public UnityEngine.AnimatorUpdateMode updateMode;
    public UnityEngine.Vector3 bodyPosition;
    public UnityEngine.Quaternion bodyRotation;
    public System.Boolean stabilizeFeet;
    public System.Single feetPivotActive;
    public System.Single speed;
    public UnityEngine.AnimatorCullingMode cullingMode;
    public System.Single playbackTime;
    public System.Single recorderStartTime;
    public System.Single recorderStopTime;
    public UnityEngine.RuntimeAnimatorController runtimeAnimatorController;
    public UnityEngine.Avatar avatar;
    public System.Boolean layersAffectMassCenter;
    public System.Boolean logWarnings;
    public System.Boolean fireEvents;
    public System.Boolean keepAnimatorStateOnDisable;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public AnimatorZSerializer (string ZUID, string GOZUID) : base(ZUID, GOZUID) {
        var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID] as UnityEngine.Animator;
        rootPosition = instance.rootPosition;
        rootRotation = instance.rootRotation;
        applyRootMotion = instance.applyRootMotion;
        updateMode = instance.updateMode;
        bodyPosition = instance.bodyPosition;
        bodyRotation = instance.bodyRotation;
        stabilizeFeet = instance.stabilizeFeet;
        feetPivotActive = instance.feetPivotActive;
        speed = instance.speed;
        cullingMode = instance.cullingMode;
        playbackTime = instance.playbackTime;
        recorderStartTime = instance.recorderStartTime;
        recorderStopTime = instance.recorderStopTime;
        runtimeAnimatorController = instance.runtimeAnimatorController;
        avatar = instance.avatar;
        layersAffectMassCenter = instance.layersAffectMassCenter;
        logWarnings = instance.logWarnings;
        fireEvents = instance.fireEvents;
        keepAnimatorStateOnDisable = instance.keepAnimatorStateOnDisable;
        enabled = instance.enabled;
        hideFlags = instance.hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.Animator))?.OnSerialize?.Invoke(this, instance);
    }
    public override void RestoreValues(UnityEngine.Component component)
    {
        var instance = (UnityEngine.Animator)component;
        instance.rootPosition = rootPosition;
        instance.rootRotation = rootRotation;
        instance.applyRootMotion = applyRootMotion;
        instance.updateMode = updateMode;
        instance.bodyPosition = bodyPosition;
        instance.bodyRotation = bodyRotation;
        instance.stabilizeFeet = stabilizeFeet;
        instance.feetPivotActive = feetPivotActive;
        instance.speed = speed;
        instance.cullingMode = cullingMode;
        instance.playbackTime = playbackTime;
        instance.recorderStartTime = recorderStartTime;
        instance.recorderStopTime = recorderStopTime;
        instance.runtimeAnimatorController = runtimeAnimatorController;
        instance.avatar = avatar;
        instance.layersAffectMassCenter = layersAffectMassCenter;
        instance.logWarnings = logWarnings;
        instance.fireEvents = fireEvents;
        instance.keepAnimatorStateOnDisable = keepAnimatorStateOnDisable;
        instance.enabled = enabled;
        instance.hideFlags = hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.Animator))?.OnDeserialize?.Invoke(this, instance);
    }
}
[System.Serializable]
public sealed class AudioSourceZSerializer : ZSerializer.Internal.ZSerializer {
    public System.Single volume;
    public System.Single pitch;
    public System.Single time;
    public System.Int32 timeSamples;
    public UnityEngine.AudioClip clip;
    public UnityEngine.Audio.AudioMixerGroup outputAudioMixerGroup;
    public UnityEngine.GamepadSpeakerOutputType gamepadSpeakerOutputType;
    public System.Boolean loop;
    public System.Boolean ignoreListenerVolume;
    public System.Boolean playOnAwake;
    public System.Boolean ignoreListenerPause;
    public UnityEngine.AudioVelocityUpdateMode velocityUpdateMode;
    public System.Single panStereo;
    public System.Single spatialBlend;
    public System.Boolean spatialize;
    public System.Boolean spatializePostEffects;
    public System.Single reverbZoneMix;
    public System.Boolean bypassEffects;
    public System.Boolean bypassListenerEffects;
    public System.Boolean bypassReverbZones;
    public System.Single dopplerLevel;
    public System.Single spread;
    public System.Int32 priority;
    public System.Boolean mute;
    public System.Single minDistance;
    public System.Single maxDistance;
    public UnityEngine.AudioRolloffMode rolloffMode;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public AudioSourceZSerializer (string ZUID, string GOZUID) : base(ZUID, GOZUID) {
        var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID] as UnityEngine.AudioSource;
        volume = instance.volume;
        pitch = instance.pitch;
        time = instance.time;
        timeSamples = instance.timeSamples;
        clip = instance.clip;
        outputAudioMixerGroup = instance.outputAudioMixerGroup;
        gamepadSpeakerOutputType = instance.gamepadSpeakerOutputType;
        loop = instance.loop;
        ignoreListenerVolume = instance.ignoreListenerVolume;
        playOnAwake = instance.playOnAwake;
        ignoreListenerPause = instance.ignoreListenerPause;
        velocityUpdateMode = instance.velocityUpdateMode;
        panStereo = instance.panStereo;
        spatialBlend = instance.spatialBlend;
        spatialize = instance.spatialize;
        spatializePostEffects = instance.spatializePostEffects;
        reverbZoneMix = instance.reverbZoneMix;
        bypassEffects = instance.bypassEffects;
        bypassListenerEffects = instance.bypassListenerEffects;
        bypassReverbZones = instance.bypassReverbZones;
        dopplerLevel = instance.dopplerLevel;
        spread = instance.spread;
        priority = instance.priority;
        mute = instance.mute;
        minDistance = instance.minDistance;
        maxDistance = instance.maxDistance;
        rolloffMode = instance.rolloffMode;
        enabled = instance.enabled;
        hideFlags = instance.hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.AudioSource))?.OnSerialize?.Invoke(this, instance);
    }
    public override void RestoreValues(UnityEngine.Component component)
    {
        var instance = (UnityEngine.AudioSource)component;
        instance.volume = volume;
        instance.pitch = pitch;
        instance.time = time;
        instance.timeSamples = timeSamples;
        instance.clip = clip;
        instance.outputAudioMixerGroup = outputAudioMixerGroup;
        instance.gamepadSpeakerOutputType = gamepadSpeakerOutputType;
        instance.loop = loop;
        instance.ignoreListenerVolume = ignoreListenerVolume;
        instance.playOnAwake = playOnAwake;
        instance.ignoreListenerPause = ignoreListenerPause;
        instance.velocityUpdateMode = velocityUpdateMode;
        instance.panStereo = panStereo;
        instance.spatialBlend = spatialBlend;
        instance.spatialize = spatialize;
        instance.spatializePostEffects = spatializePostEffects;
        instance.reverbZoneMix = reverbZoneMix;
        instance.bypassEffects = bypassEffects;
        instance.bypassListenerEffects = bypassListenerEffects;
        instance.bypassReverbZones = bypassReverbZones;
        instance.dopplerLevel = dopplerLevel;
        instance.spread = spread;
        instance.priority = priority;
        instance.mute = mute;
        instance.minDistance = minDistance;
        instance.maxDistance = maxDistance;
        instance.rolloffMode = rolloffMode;
        instance.enabled = enabled;
        instance.hideFlags = hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.AudioSource))?.OnDeserialize?.Invoke(this, instance);
    }
}
[System.Serializable]
public sealed class MeshFilterZSerializer : ZSerializer.Internal.ZSerializer {
    public UnityEngine.Mesh sharedMesh;
    public UnityEngine.HideFlags hideFlags;
    public MeshFilterZSerializer (string ZUID, string GOZUID) : base(ZUID, GOZUID) {
        var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID] as UnityEngine.MeshFilter;
        sharedMesh = instance.sharedMesh;
        hideFlags = instance.hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.MeshFilter))?.OnSerialize?.Invoke(this, instance);
    }
    public override void RestoreValues(UnityEngine.Component component)
    {
        var instance = (UnityEngine.MeshFilter)component;
        instance.sharedMesh = sharedMesh;
        instance.hideFlags = hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.MeshFilter))?.OnDeserialize?.Invoke(this, instance);
    }
}
[System.Serializable]
public sealed class MeshRendererZSerializer : ZSerializer.Internal.ZSerializer {
    public UnityEngine.Mesh additionalVertexStreams;
    public UnityEngine.Mesh enlightenVertexStream;
    public UnityEngine.Bounds bounds;
    public UnityEngine.Bounds localBounds;
    public System.Boolean enabled;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public System.Boolean receiveShadows;
    public System.Boolean forceRenderingOff;
    public System.Boolean staticShadowCaster;
    public UnityEngine.MotionVectorGenerationMode motionVectorGenerationMode;
    public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public System.UInt32 renderingLayerMask;
    public System.Int32 rendererPriority;
    public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode;
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean allowOcclusionWhenDynamic;
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.HideFlags hideFlags;
    public MeshRendererZSerializer (string ZUID, string GOZUID) : base(ZUID, GOZUID) {
        var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID] as UnityEngine.MeshRenderer;
        additionalVertexStreams = instance.additionalVertexStreams;
        enlightenVertexStream = instance.enlightenVertexStream;
        bounds = instance.bounds;
        localBounds = instance.localBounds;
        enabled = instance.enabled;
        shadowCastingMode = instance.shadowCastingMode;
        receiveShadows = instance.receiveShadows;
        forceRenderingOff = instance.forceRenderingOff;
        staticShadowCaster = instance.staticShadowCaster;
        motionVectorGenerationMode = instance.motionVectorGenerationMode;
        lightProbeUsage = instance.lightProbeUsage;
        reflectionProbeUsage = instance.reflectionProbeUsage;
        renderingLayerMask = instance.renderingLayerMask;
        rendererPriority = instance.rendererPriority;
        rayTracingMode = instance.rayTracingMode;
        sortingLayerName = instance.sortingLayerName;
        sortingLayerID = instance.sortingLayerID;
        sortingOrder = instance.sortingOrder;
        allowOcclusionWhenDynamic = instance.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = instance.lightProbeProxyVolumeOverride;
        probeAnchor = instance.probeAnchor;
        lightmapIndex = instance.lightmapIndex;
        realtimeLightmapIndex = instance.realtimeLightmapIndex;
        lightmapScaleOffset = instance.lightmapScaleOffset;
        realtimeLightmapScaleOffset = instance.realtimeLightmapScaleOffset;
        sharedMaterials = instance.sharedMaterials;
        hideFlags = instance.hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.MeshRenderer))?.OnSerialize?.Invoke(this, instance);
    }
    public override void RestoreValues(UnityEngine.Component component)
    {
        var instance = (UnityEngine.MeshRenderer)component;
        instance.additionalVertexStreams = additionalVertexStreams;
        instance.enlightenVertexStream = enlightenVertexStream;
        instance.bounds = bounds;
        instance.localBounds = localBounds;
        instance.enabled = enabled;
        instance.shadowCastingMode = shadowCastingMode;
        instance.receiveShadows = receiveShadows;
        instance.forceRenderingOff = forceRenderingOff;
        instance.staticShadowCaster = staticShadowCaster;
        instance.motionVectorGenerationMode = motionVectorGenerationMode;
        instance.lightProbeUsage = lightProbeUsage;
        instance.reflectionProbeUsage = reflectionProbeUsage;
        instance.renderingLayerMask = renderingLayerMask;
        instance.rendererPriority = rendererPriority;
        instance.rayTracingMode = rayTracingMode;
        instance.sortingLayerName = sortingLayerName;
        instance.sortingLayerID = sortingLayerID;
        instance.sortingOrder = sortingOrder;
        instance.allowOcclusionWhenDynamic = allowOcclusionWhenDynamic;
        instance.lightProbeProxyVolumeOverride = lightProbeProxyVolumeOverride;
        instance.probeAnchor = probeAnchor;
        instance.lightmapIndex = lightmapIndex;
        instance.realtimeLightmapIndex = realtimeLightmapIndex;
        instance.lightmapScaleOffset = lightmapScaleOffset;
        instance.realtimeLightmapScaleOffset = realtimeLightmapScaleOffset;
        instance.sharedMaterials = sharedMaterials;
        instance.hideFlags = hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.MeshRenderer))?.OnDeserialize?.Invoke(this, instance);
    }
}
[System.Serializable]
public sealed class TransformZSerializer : ZSerializer.Internal.ZSerializer {
    public UnityEngine.Vector3 position;
    public UnityEngine.Vector3 localPosition;
    public UnityEngine.Vector3 eulerAngles;
    public UnityEngine.Vector3 localEulerAngles;
    public UnityEngine.Vector3 right;
    public UnityEngine.Vector3 up;
    public UnityEngine.Vector3 forward;
    public UnityEngine.Quaternion rotation;
    public UnityEngine.Quaternion localRotation;
    public UnityEngine.Vector3 localScale;
    public UnityEngine.Transform parent;
    public System.Boolean hasChanged;
    public System.Int32 hierarchyCapacity;
    public UnityEngine.HideFlags hideFlags;
    public TransformZSerializer (string ZUID, string GOZUID) : base(ZUID, GOZUID) {
        var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID] as UnityEngine.Transform;
        position = instance.position;
        localPosition = instance.localPosition;
        eulerAngles = instance.eulerAngles;
        localEulerAngles = instance.localEulerAngles;
        right = instance.right;
        up = instance.up;
        forward = instance.forward;
        rotation = instance.rotation;
        localRotation = instance.localRotation;
        localScale = instance.localScale;
        parent = instance.parent;
        hasChanged = instance.hasChanged;
        hierarchyCapacity = instance.hierarchyCapacity;
        hideFlags = instance.hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.Transform))?.OnSerialize?.Invoke(this, instance);
    }
    public override void RestoreValues(UnityEngine.Component component)
    {
        var instance = (UnityEngine.Transform)component;
        instance.position = position;
        instance.localPosition = localPosition;
        instance.eulerAngles = eulerAngles;
        instance.localEulerAngles = localEulerAngles;
        instance.right = right;
        instance.up = up;
        instance.forward = forward;
        instance.rotation = rotation;
        instance.localRotation = localRotation;
        instance.localScale = localScale;
        instance.parent = parent;
        instance.hasChanged = hasChanged;
        instance.hierarchyCapacity = hierarchyCapacity;
        instance.hideFlags = hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.Transform))?.OnDeserialize?.Invoke(this, instance);
    }
}
[System.Serializable]
public sealed class SpriteRendererZSerializer : ZSerializer.Internal.ZSerializer {
    public UnityEngine.Sprite sprite;
    public UnityEngine.SpriteDrawMode drawMode;
    public UnityEngine.Vector2 size;
    public System.Single adaptiveModeThreshold;
    public UnityEngine.SpriteTileMode tileMode;
    public UnityEngine.Color color;
    public UnityEngine.SpriteMaskInteraction maskInteraction;
    public System.Boolean flipX;
    public System.Boolean flipY;
    public UnityEngine.SpriteSortPoint spriteSortPoint;
    public UnityEngine.Bounds bounds;
    public UnityEngine.Bounds localBounds;
    public System.Boolean enabled;
    public UnityEngine.Rendering.ShadowCastingMode shadowCastingMode;
    public System.Boolean receiveShadows;
    public System.Boolean forceRenderingOff;
    public System.Boolean staticShadowCaster;
    public UnityEngine.MotionVectorGenerationMode motionVectorGenerationMode;
    public UnityEngine.Rendering.LightProbeUsage lightProbeUsage;
    public UnityEngine.Rendering.ReflectionProbeUsage reflectionProbeUsage;
    public System.UInt32 renderingLayerMask;
    public System.Int32 rendererPriority;
    public UnityEngine.Experimental.Rendering.RayTracingMode rayTracingMode;
    public System.String sortingLayerName;
    public System.Int32 sortingLayerID;
    public System.Int32 sortingOrder;
    public System.Boolean allowOcclusionWhenDynamic;
    public UnityEngine.GameObject lightProbeProxyVolumeOverride;
    public UnityEngine.Transform probeAnchor;
    public System.Int32 lightmapIndex;
    public System.Int32 realtimeLightmapIndex;
    public UnityEngine.Vector4 lightmapScaleOffset;
    public UnityEngine.Vector4 realtimeLightmapScaleOffset;
    public UnityEngine.Material[] sharedMaterials;
    public UnityEngine.HideFlags hideFlags;
    public SpriteRendererZSerializer (string ZUID, string GOZUID) : base(ZUID, GOZUID) {
        var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID] as UnityEngine.SpriteRenderer;
        sprite = instance.sprite;
        drawMode = instance.drawMode;
        size = instance.size;
        adaptiveModeThreshold = instance.adaptiveModeThreshold;
        tileMode = instance.tileMode;
        color = instance.color;
        maskInteraction = instance.maskInteraction;
        flipX = instance.flipX;
        flipY = instance.flipY;
        spriteSortPoint = instance.spriteSortPoint;
        bounds = instance.bounds;
        localBounds = instance.localBounds;
        enabled = instance.enabled;
        shadowCastingMode = instance.shadowCastingMode;
        receiveShadows = instance.receiveShadows;
        forceRenderingOff = instance.forceRenderingOff;
        staticShadowCaster = instance.staticShadowCaster;
        motionVectorGenerationMode = instance.motionVectorGenerationMode;
        lightProbeUsage = instance.lightProbeUsage;
        reflectionProbeUsage = instance.reflectionProbeUsage;
        renderingLayerMask = instance.renderingLayerMask;
        rendererPriority = instance.rendererPriority;
        rayTracingMode = instance.rayTracingMode;
        sortingLayerName = instance.sortingLayerName;
        sortingLayerID = instance.sortingLayerID;
        sortingOrder = instance.sortingOrder;
        allowOcclusionWhenDynamic = instance.allowOcclusionWhenDynamic;
        lightProbeProxyVolumeOverride = instance.lightProbeProxyVolumeOverride;
        probeAnchor = instance.probeAnchor;
        lightmapIndex = instance.lightmapIndex;
        realtimeLightmapIndex = instance.realtimeLightmapIndex;
        lightmapScaleOffset = instance.lightmapScaleOffset;
        realtimeLightmapScaleOffset = instance.realtimeLightmapScaleOffset;
        sharedMaterials = instance.sharedMaterials;
        hideFlags = instance.hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.SpriteRenderer))?.OnSerialize?.Invoke(this, instance);
    }
    public override void RestoreValues(UnityEngine.Component component)
    {
        var instance = (UnityEngine.SpriteRenderer)component;
        instance.sprite = sprite;
        instance.drawMode = drawMode;
        instance.size = size;
        instance.adaptiveModeThreshold = adaptiveModeThreshold;
        instance.tileMode = tileMode;
        instance.color = color;
        instance.maskInteraction = maskInteraction;
        instance.flipX = flipX;
        instance.flipY = flipY;
        instance.spriteSortPoint = spriteSortPoint;
        instance.bounds = bounds;
        instance.localBounds = localBounds;
        instance.enabled = enabled;
        instance.shadowCastingMode = shadowCastingMode;
        instance.receiveShadows = receiveShadows;
        instance.forceRenderingOff = forceRenderingOff;
        instance.staticShadowCaster = staticShadowCaster;
        instance.motionVectorGenerationMode = motionVectorGenerationMode;
        instance.lightProbeUsage = lightProbeUsage;
        instance.reflectionProbeUsage = reflectionProbeUsage;
        instance.renderingLayerMask = renderingLayerMask;
        instance.rendererPriority = rendererPriority;
        instance.rayTracingMode = rayTracingMode;
        instance.sortingLayerName = sortingLayerName;
        instance.sortingLayerID = sortingLayerID;
        instance.sortingOrder = sortingOrder;
        instance.allowOcclusionWhenDynamic = allowOcclusionWhenDynamic;
        instance.lightProbeProxyVolumeOverride = lightProbeProxyVolumeOverride;
        instance.probeAnchor = probeAnchor;
        instance.lightmapIndex = lightmapIndex;
        instance.realtimeLightmapIndex = realtimeLightmapIndex;
        instance.lightmapScaleOffset = lightmapScaleOffset;
        instance.realtimeLightmapScaleOffset = realtimeLightmapScaleOffset;
        instance.sharedMaterials = sharedMaterials;
        instance.hideFlags = hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.SpriteRenderer))?.OnDeserialize?.Invoke(this, instance);
    }
}
[System.Serializable]
public sealed class RigidbodyZSerializer : ZSerializer.Internal.ZSerializer {
    public UnityEngine.Vector3 velocity;
    public UnityEngine.Vector3 angularVelocity;
    public System.Single drag;
    public System.Single angularDrag;
    public System.Single mass;
    public System.Boolean useGravity;
    public System.Single maxDepenetrationVelocity;
    public System.Boolean isKinematic;
    public System.Boolean freezeRotation;
    public UnityEngine.RigidbodyConstraints constraints;
    public UnityEngine.CollisionDetectionMode collisionDetectionMode;
    public UnityEngine.Vector3 centerOfMass;
    public UnityEngine.Quaternion inertiaTensorRotation;
    public UnityEngine.Vector3 inertiaTensor;
    public System.Boolean detectCollisions;
    public UnityEngine.Vector3 position;
    public UnityEngine.Quaternion rotation;
    public UnityEngine.RigidbodyInterpolation interpolation;
    public System.Int32 solverIterations;
    public System.Single sleepThreshold;
    public System.Single maxAngularVelocity;
    public System.Int32 solverVelocityIterations;
    public UnityEngine.HideFlags hideFlags;
    public RigidbodyZSerializer (string ZUID, string GOZUID) : base(ZUID, GOZUID) {
        var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID] as UnityEngine.Rigidbody;
        velocity = instance.velocity;
        angularVelocity = instance.angularVelocity;
        drag = instance.drag;
        angularDrag = instance.angularDrag;
        mass = instance.mass;
        useGravity = instance.useGravity;
        maxDepenetrationVelocity = instance.maxDepenetrationVelocity;
        isKinematic = instance.isKinematic;
        freezeRotation = instance.freezeRotation;
        constraints = instance.constraints;
        collisionDetectionMode = instance.collisionDetectionMode;
        centerOfMass = instance.centerOfMass;
        inertiaTensorRotation = instance.inertiaTensorRotation;
        inertiaTensor = instance.inertiaTensor;
        detectCollisions = instance.detectCollisions;
        position = instance.position;
        rotation = instance.rotation;
        interpolation = instance.interpolation;
        solverIterations = instance.solverIterations;
        sleepThreshold = instance.sleepThreshold;
        maxAngularVelocity = instance.maxAngularVelocity;
        solverVelocityIterations = instance.solverVelocityIterations;
        hideFlags = instance.hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.Rigidbody))?.OnSerialize?.Invoke(this, instance);
    }
    public override void RestoreValues(UnityEngine.Component component)
    {
        var instance = (UnityEngine.Rigidbody)component;
        instance.velocity = velocity;
        instance.angularVelocity = angularVelocity;
        instance.drag = drag;
        instance.angularDrag = angularDrag;
        instance.mass = mass;
        instance.useGravity = useGravity;
        instance.maxDepenetrationVelocity = maxDepenetrationVelocity;
        instance.isKinematic = isKinematic;
        instance.freezeRotation = freezeRotation;
        instance.constraints = constraints;
        instance.collisionDetectionMode = collisionDetectionMode;
        instance.centerOfMass = centerOfMass;
        instance.inertiaTensorRotation = inertiaTensorRotation;
        instance.inertiaTensor = inertiaTensor;
        instance.detectCollisions = detectCollisions;
        instance.position = position;
        instance.rotation = rotation;
        instance.interpolation = interpolation;
        instance.solverIterations = solverIterations;
        instance.sleepThreshold = sleepThreshold;
        instance.maxAngularVelocity = maxAngularVelocity;
        instance.solverVelocityIterations = solverVelocityIterations;
        instance.hideFlags = hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.Rigidbody))?.OnDeserialize?.Invoke(this, instance);
    }
}
[System.Serializable]
public sealed class MeshColliderZSerializer : ZSerializer.Internal.ZSerializer {
    public UnityEngine.Mesh sharedMesh;
    public System.Boolean convex;
    public UnityEngine.MeshColliderCookingOptions cookingOptions;
    public System.Boolean enabled;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public System.Boolean hasModifiableContacts;
    public UnityEngine.HideFlags hideFlags;
    public MeshColliderZSerializer (string ZUID, string GOZUID) : base(ZUID, GOZUID) {
        var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID] as UnityEngine.MeshCollider;
        sharedMesh = instance.sharedMesh;
        convex = instance.convex;
        cookingOptions = instance.cookingOptions;
        enabled = instance.enabled;
        isTrigger = instance.isTrigger;
        contactOffset = instance.contactOffset;
        hasModifiableContacts = instance.hasModifiableContacts;
        hideFlags = instance.hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.MeshCollider))?.OnSerialize?.Invoke(this, instance);
    }
    public override void RestoreValues(UnityEngine.Component component)
    {
        var instance = (UnityEngine.MeshCollider)component;
        instance.sharedMesh = sharedMesh;
        instance.convex = convex;
        instance.cookingOptions = cookingOptions;
        instance.enabled = enabled;
        instance.isTrigger = isTrigger;
        instance.contactOffset = contactOffset;
        instance.hasModifiableContacts = hasModifiableContacts;
        instance.hideFlags = hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.MeshCollider))?.OnDeserialize?.Invoke(this, instance);
    }
}
[System.Serializable]
public sealed class BoxColliderZSerializer : ZSerializer.Internal.ZSerializer {
    public UnityEngine.Vector3 center;
    public UnityEngine.Vector3 size;
    public System.Boolean enabled;
    public System.Boolean isTrigger;
    public System.Single contactOffset;
    public System.Boolean hasModifiableContacts;
    public UnityEngine.HideFlags hideFlags;
    public BoxColliderZSerializer (string ZUID, string GOZUID) : base(ZUID, GOZUID) {
        var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID] as UnityEngine.BoxCollider;
        center = instance.center;
        size = instance.size;
        enabled = instance.enabled;
        isTrigger = instance.isTrigger;
        contactOffset = instance.contactOffset;
        hasModifiableContacts = instance.hasModifiableContacts;
        hideFlags = instance.hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.BoxCollider))?.OnSerialize?.Invoke(this, instance);
    }
    public override void RestoreValues(UnityEngine.Component component)
    {
        var instance = (UnityEngine.BoxCollider)component;
        instance.center = center;
        instance.size = size;
        instance.enabled = enabled;
        instance.isTrigger = isTrigger;
        instance.contactOffset = contactOffset;
        instance.hasModifiableContacts = hasModifiableContacts;
        instance.hideFlags = hideFlags;
        ZSerializerSettings.Instance.unityComponentDataList.FirstOrDefault(data => data.Type == typeof(UnityEngine.BoxCollider))?.OnDeserialize?.Invoke(this, instance);
    }
}
}