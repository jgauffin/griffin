/*
using System;
using System.IO;
using System.Reflection;
using Autofac;
using HttpServer;
using NHibernate;
using NHibernate.Cfg;

namespace Griffin.Repository.NHibernate
{
    /// <summary>
    /// Returns nhibernate sessions (database connections)
    /// </summary>
    public class SessionProvider : IUnitOfWorkFactory, ISessionProvider
    {
        private static Configuration _configuration = new Configuration();
        private static ISessionFactory _factory;
        [ThreadStatic] private static ISession _session;
        [ThreadStatic] private static UnitOfWork _unitOfWork;
        private static bool _isStarted = false;

        public SessionProvider()
        {
            _configuration.Configure(Assembly.GetExecutingAssembly(), GetType().Namespace + ".hibernate.cfg.xml");
            AppDomain.CurrentDomain.AssemblyLoad += OnAddAssembly;
            UnitOfWorkFactory.Assign(this);
            LoadMappingsFromAllLoadedAssemblies();
        }

        private void LoadMappingsFromAllLoadedAssemblies()
        {
            var currentDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (!Path.GetDirectoryName(assembly.Location).StartsWith(currentDir))
                    continue;
                _configuration.AddAssembly(assembly);
            }
        }

        private void OnAddAssembly(object sender, AssemblyLoadEventArgs args)
        {
            Console.WriteLine("Adding assembly.");
            _configuration.AddAssembly(args.LoadedAssembly);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SessionProvider"/> class.
        /// </summary>
        static SessionProvider()
        {
            HttpContext.CurrentRequestCompleted += OnRequestCompleted;
        }

        /// <summary>
        /// Gets factory instance.
        /// </summary>
        public static ISessionFactory Factory
        {
            get
            {
                if (!_isStarted)
                {
                    Console.WriteLine("Configuring");
                    _configuration.Configure();
                    _isStarted = true;
                }

                if (_factory == null)
                    _factory = _configuration.BuildSessionFactory();
                return _factory;
            }
        }

        /// <summary>
        /// Get a new session.
        /// </summary>
        /// <returns>Created session</returns>
        public static ISession GetSession()
        {
            if (_session == null)
                _session = Factory.OpenSession();

            return _session;
        }

        public static void Setup(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>));
            builder.Register((ctx) => GetSession()).As(typeof(ISession));
            builder.RegisterType<FlexiRepository>().As<IFlexRepository>().SingleInstance();

            var instance = new SessionProvider();
            builder.RegisterInstance(instance)
                .As<IUnitOfWorkFactory>().SingleInstance()
                .As<ISessionProvider>().SingleInstance();
            UnitOfWorkFactory.Assign(instance);
        }

        private static void OnDisposal(object sender, EventArgs e)
        {
            _unitOfWork.Disposed -= OnDisposal;
            _unitOfWork = null;
            CloseSession();
        }

        private static void OnRequestCompleted(object sender, RequestEventArgs e)
        {
           CloseSession();
        }

        public static void CloseSession()
        {
            if (_session == null)
                return;
            _session.Close();
            _session = null;
        }

        public IUnitOfWork Create()
        {
            if (_unitOfWork != null)
                throw new InvalidOperationException("Unit of work have already been created (dispose it first).");

            if (_session == null)
                _session = Factory.OpenSession();
            _unitOfWork = new UnitOfWork(_session);
            _unitOfWork.Disposed += OnDisposal;
            return _unitOfWork;
        }

        public IUnitOfWork Current
        {
            get { return _unitOfWork; }
        }

        ISession ISessionProvider.GetSession()
        {
            return GetSession();
        }
    }


}*/