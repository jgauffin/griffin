namespace Griffin.Networking.Pipelines
{
    public interface IPipelineFactory
    {
        /// <summary>
        /// Create a new pipeline with all attached channel handlers.
        /// </summary>
        IPipeline CreatePipeline();
    }
}