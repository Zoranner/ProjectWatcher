//===================================
// Project: ProjectWatcher
// Author: Zoranner@ZORANNER
// Datetime: 2018-07-18 22:55:37
//===================================

namespace Zoranner.ProjectWatcher
{
#if WATCH_DOG
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
        Animates,
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
        Coupler,
        Function,
        Interface
    }

    public enum StreamingAssetsDirectory
    {
        Bundles,
        Configs
    }
#endif
}