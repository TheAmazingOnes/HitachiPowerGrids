﻿using Common.Interfaces;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using Common.Enum;

namespace Common.Classes
{
    public class DataAccess : IDataAccess
    {
        public List<T> GetPointsOfAction<T, U>(string sql, U parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                List<T> rows = connection.Query<T>(sql, parameters).ToList();

                return rows;
            }
        }

        public void SavePointOfAction<T>(string sql, T parameters, string connectionString)
        {
            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                connection.Execute(sql, parameters);
            }
        }
    }
}
