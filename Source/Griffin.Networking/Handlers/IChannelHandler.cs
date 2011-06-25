namespace Griffin.Networking.Handlers
{
    /// <summary>
    /// All channel handlers MUST be thread safe.
    /// </summary>
    /// <remarks>
    /// The best way to achieve thread safety is to use the <see cref="IChannelHandlerContext.State"/> property
    /// to store context information. It will allow you to avoid using locks. Simply create a <c>[ThreadStatic]</c> field
    /// which you initialize with your context each time you receive a message.
    /// </remarks>
    public interface IChannelHandler
    {
    }
}