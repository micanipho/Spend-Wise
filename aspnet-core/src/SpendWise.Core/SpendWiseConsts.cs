using SpendWise.Debugging;

namespace SpendWise;

public class SpendWiseConsts
{
    public const string LocalizationSourceName = "SpendWise";

    public const string ConnectionStringName = "Default";

    public const bool MultiTenancyEnabled = true;


    /// <summary>
    /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
    /// </summary>
    public static readonly string DefaultPassPhrase =
        DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "8ec12efc251347e99da824a647c90c4b";
}
