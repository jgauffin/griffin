namespace Griffin.Specification
{
	/// <summary>
	/// Base classes for configuration.
	/// </summary>
	/// <remarks>
	/// It's appended by extension helpers for each block in Fadd.
	/// </remarks>
	public class Configure
	{
		private static Configure Instance = new Configure();

		private Configure()
		{
		}

		/// <summary>
		/// Gets fluent fadd configuration.
		/// </summary>
		public static Configure Griffin
		{
			get { return Instance; }
		}
	}
}