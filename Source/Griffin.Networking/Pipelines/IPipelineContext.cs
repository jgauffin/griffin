namespace Griffin.Core.Net.Pipelines
{
    /// <summary>
    /// Context valid for a specific pipeline invocation
    /// </summary>
    /// <remarks>
    /// <para>
    /// The context is used to know which handler to invoke next. The context is 
    /// therefore unique for a specific event in a specific channel.
    /// </para>
    /// <para>
    /// Processing can switch from upstream to down stream (and vice versa) at any time.
    /// Do note that switching means that the topmost handler will be invoked,
    /// so you need to make sure that each handler checks if it can used the currently
    /// attached object in the message event. If not, it should just pass the
    /// event to the next handler.
    /// </para>
    /// </remarks>
    public interface IPipelineContext
    {
    }
}