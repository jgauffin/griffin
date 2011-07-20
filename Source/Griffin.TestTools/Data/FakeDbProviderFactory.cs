﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Griffin.TestTools.Data
{
    public class FakeDbProviderFactory : System.Data.Common.DbProviderFactory
    {
        private FakeDbConnection _currentConnection;
        public static readonly FakeDbProviderFactory Instance = new FakeDbProviderFactory();

        private FakeDbProviderFactory()
        {
            NextResult = new DataTable();
        }

        public static string ProviderName
        {
            get { return typeof(FakeDbProviderFactory).FullName; }
        }

        /// <summary>
        /// Setup the FakeDbFactory
        /// </summary>
        /// <remarks>
        /// <para>Will register the provider and add a connectionstring called "FakeDb" to <see cref="ConfigurationManager.ConnectionStrings"/>.
        /// </para>
        /// </remarks>
        public static void Setup()
        {
            var settings = ConfigurationManager.ConnectionStrings[0];
            var fi = typeof(ConfigurationElement).GetField("_bReadOnly", BindingFlags.Instance | BindingFlags.NonPublic);
            fi.SetValue(settings, false);
            settings.ConnectionString = "Data Source=FakeDb";
            settings.ProviderName = ProviderName;
            settings.Name = "FakeDb";

            try
            {
                var dataSet = (System.Data.DataSet)ConfigurationManager.GetSection("system.data");

                /*
                 *
                    * 0 Readable name for the data provider
                    * 1 eadable description of the data provider
                    * 2 Name that can be used programmatically to refer to the data provider
                    * 3 Fully qualified name of the factory class, which contains enough information to instantiate the object
                 * */
                dataSet.Tables[0].Rows.Add("Fake Data Provider"
                , "Amazing provider for databases"
                , "Griffin.TestTools.Data.FakeDbProviderFactory"
                , "Griffin.TestTools.Data.FakeDbProviderFactory, Griffin.TestTools");
            }
            catch (System.Data.ConstraintException)
            {

            }
            
        }

        public override System.Data.Common.DbConnection CreateConnection()
        {
            _currentConnection = new FakeDbConnection(NextResult.Clone());
            return CurrentConnection;
        }

        public override System.Data.Common.DbCommand CreateCommand()
        {
            return new FakeCommand(CurrentConnection, NextResult.Clone());

        }

        public override System.Data.Common.DbParameter CreateParameter()
        {
            return new FakeParameter();
        }

        /// <summary>
        /// Gets or sets the next result to be returned by a command.
        /// </summary>
        /// <remarks>
        /// The result will be cloned into a new datatable before being used by the command.
        /// </remarks>
        public DataTable NextResult { get; set; }

        public FakeDbConnection CurrentConnection
        {
            get { return _currentConnection; }
        }
    }

}
