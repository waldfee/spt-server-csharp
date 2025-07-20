using SPTarkov.Server.Core.Models.Logging;
using LogLevel = SPTarkov.Server.Core.Models.Spt.Logging.LogLevel;

namespace SPTarkov.Server.Core.Models.Utils;

public abstract class SptLoggerBase<T> : ISptLogger<T>
{
    public abstract void LogWithColorInternal(
        string data,
        LogTextColor? textColor = null,
        LogBackgroundColor? backgroundColor = null,
        Exception? ex = null
    );

    public void LogWithColor(
        StringOrFormattableString data,
        LogTextColor? textColor = null,
        LogBackgroundColor? backgroundColor = null,
        Exception? ex = null
    )
    {
        LogWithColorInternal(
            data.StringValue ?? data.FormattableValue!.Format,
            textColor,
            backgroundColor,
            ex
        );
    }

    protected abstract void SuccessInternal(string data, Exception? ex = null);

    public void Success(StringOrFormattableString data, Exception? ex = null)
    {
        SuccessInternal(data.StringValue ?? data.FormattableValue!.Format, ex);
    }

    protected abstract void ErrorInternal(string data, Exception? ex = null);

    public void Error(StringOrFormattableString data, Exception? ex = null)
    {
        if (!IsLogEnabled(LogLevel.Error))
        {
            return;
        }

        ErrorInternal(data.StringValue ?? data.FormattableValue!.Format, ex);
    }

    protected abstract void WarningInternal(string data, Exception? ex = null);

    public void Warning(StringOrFormattableString data, Exception? ex = null)
    {
        if (!IsLogEnabled(LogLevel.Warn))
        {
            return;
        }

        WarningInternal(data.StringValue ?? data.FormattableValue!.Format, ex);
    }

    protected abstract void InfoInternal(string data, Exception? ex = null);

    public void Info(StringOrFormattableString data, Exception? ex = null)
    {
        if (!IsLogEnabled(LogLevel.Info))
        {
            return;
        }

        InfoInternal(data.StringValue ?? data.FormattableValue!.Format, ex);
    }

    protected abstract void DebugInternal(string data, Exception? ex = null);

    public void Debug(StringOrFormattableString data, Exception? ex = null)
    {
        if (!IsLogEnabled(LogLevel.Debug))
        {
            return;
        }

        DebugInternal(data.StringValue ?? data.FormattableValue!.Format, ex);
    }

    protected abstract void CriticalInternal(string data, Exception? ex = null);

    public void Critical(StringOrFormattableString data, Exception? ex = null)
    {
        if (!IsLogEnabled(LogLevel.Fatal))
        {
            return;
        }

        CriticalInternal(data.StringValue ?? data.FormattableValue!.Format, ex);
    }

    protected abstract void LogInternal(
        LogLevel level,
        string data,
        LogTextColor? textColor = null,
        LogBackgroundColor? backgroundColor = null,
        Exception? ex = null
    );

    public void Log(
        LogLevel level,
        StringOrFormattableString data,
        LogTextColor? textColor = null,
        LogBackgroundColor? backgroundColor = null,
        Exception? ex = null
    )
    {
        if (!IsLogEnabled(level))
        {
            return;
        }
        LogInternal(
            level,
            data.StringValue ?? data.FormattableValue!.Format,
            textColor,
            backgroundColor,
            ex
        );
    }

    protected abstract bool IsLogEnabled(LogLevel level);

    public abstract void DumpAndStop();
}
