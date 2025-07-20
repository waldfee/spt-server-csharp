using SPTarkov.Server.Core.Models.Logging;
using LogLevel = SPTarkov.Server.Core.Models.Spt.Logging.LogLevel;

namespace SPTarkov.Server.Core.Models.Utils;

public interface ISptLogger<T>
{
    void LogWithColor(
        StringOrFormattableString data,
        LogTextColor? textColor = null,
        LogBackgroundColor? backgroundColor = null,
        Exception? ex = null
    );
    void Success(StringOrFormattableString data, Exception? ex = null);
    void Error(StringOrFormattableString data, Exception? ex = null);
    void Warning(StringOrFormattableString data, Exception? ex = null);
    void Info(StringOrFormattableString data, Exception? ex = null);
    void Debug(StringOrFormattableString data, Exception? ex = null);
    void Critical(StringOrFormattableString data, Exception? ex = null);
    void Log(
        LogLevel level,
        StringOrFormattableString data,
        LogTextColor? textColor = null,
        LogBackgroundColor? backgroundColor = null,
        Exception? ex = null
    );
    void DumpAndStop();
}

// workaround for string/Formattablestring overloads
// https://stackoverflow.com/a/68299792
// is a struct to avoid allocations
public readonly record struct StringOrFormattableString
{
    internal string? StringValue { get; }
    internal FormattableString? FormattableValue { get; }

    internal StringOrFormattableString(string str)
    {
        StringValue = str;
    }

    internal StringOrFormattableString(FormattableString formattable)
    {
        FormattableValue = formattable;
    }

    public static implicit operator StringOrFormattableString(string str)
    {
        return new StringOrFormattableString(str);
    }

    public static implicit operator StringOrFormattableString(FormattableString formattable)
    {
        return new StringOrFormattableString(formattable);
    }
}
