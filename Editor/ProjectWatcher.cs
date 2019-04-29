//============================================================
// Project: ProjectWatcher
// Author: Zoranner@ZORANNER
// Datetime: 2018-07-18 22:55:37
//============================================================

#if WATCH_DOG
namespace Zoranner.ProjectWatcher
{
    public enum MainDirectory
    {
        Arts,
        Plugins,
        Resources,
        Scenes,
        Scripts,
        StreamingAssets
    }

    public enum ArtsDirectory
    {
        Animations,
        Animators,
        Audios,
        Materials,
        Meshes,
        Models,
        Particles,
        Prefabs,
        Products,
        Profiles,
        Shaders,
        Skyboxes,
        Terrains,
        Textures,
        Timelines,
        Videos,
    }

    public enum ResourcesDirectory
    {
        Animations,
        Audios,
        Materials,
        Prefabs,
        Profiles,
        Skyboxes,
        Textures,
        Others
    }

    public enum ScriptsDirectory
    {
        Editor,
        Kernel,
        Master
    }

    public enum MasterDirectory
    {
        Connect,
        Coupler,
        Function,
        Interface
    }

    public enum StreamingAssetsDirectory
    {
        Bundles,
        Configs
    }
}
#endif