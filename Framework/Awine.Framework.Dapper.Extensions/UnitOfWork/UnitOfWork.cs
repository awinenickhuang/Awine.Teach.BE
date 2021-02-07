using Awine.Framework.Dapper.Extensions.Options;
using Awine.Framework.Dapper.Extensions.UnitOfWork;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Awine.Framework.Dapper.Extensions
{
    public class DapperUnitOfWork : IDapperUnitOfWork
    {
        private IDbConnection _connection;

        private IDbTransaction _transaction;

        private bool _disposed;

        private readonly MySQLProviderOptions _mySqlProviderOptions;

        private readonly ILogger<DapperUnitOfWork> _logger;

        public DapperUnitOfWork(MySQLProviderOptions mySqlProviderOptions)
        {
            _mySqlProviderOptions = mySqlProviderOptions ?? throw new ArgumentNullException(nameof(mySqlProviderOptions));
            _connection = new MySqlConnection(_mySqlProviderOptions.ConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
            }
        }

        public void Dispose()
        {
            dispose(true);
            GC.SuppressFinalize(this);
        }

        private void dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                        _transaction = null;
                    }
                    if (_connection != null)
                    {
                        _connection.Dispose();
                        _connection = null;
                    }
                }
                _disposed = true;
            }
        }

        ~DapperUnitOfWork()
        {
            dispose(false);
        }
    }
}
