using Terraria.ModLoader;

namespace Timberlands;

public class Timberlands : Mod
{
    public static Timberlands Instance { get; private set; }

    #region Hooks
    public override void Load()
    {
        Instance = this;

        Log("Hello, Timberlands World!", "Mod.Load");
    }

    public override void Unload()
    {
        Instance = null;
        // nothing should be unloaded past this line. everything should be unloaded BEFORE the instance.
    }
    #endregion

    #region Methods
    /// <summary>
    ///     Outputs message to the console using Mod.Logger.Info in format [context] message. e.g.: "[Mod.Load]: Hello
    ///     World!".
    /// </summary>
    public void Log(string message, string context)
    {
        Instance.Logger.Info($"[{context}] {message}");
    }
    #endregion
}