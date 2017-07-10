﻿using System;
using System.Data.SqlClient;
using System.Text;
using OfficeLocationMicroservice.Core.SharedContext;

namespace OfficeLocationMicroservice.Database
{
    public abstract class BaseDataTableGateway
    {
        protected ISystemLog SystemLog;

        protected BaseDataTableGateway(
            ISystemLog systemLog) 
        {
            SystemLog = systemLog;
        }

        //protected override string DefaultConnectionString
        protected abstract string DefaultConnectionString { get; }

        protected void ConnectionExecuteWithLog(
            Action<SqlConnection> connectionAction,
            string logSql)
        {
            using (var sqlConnection = new SqlConnection(DefaultConnectionString))
            {
                SystemLog.Info("Executing: " + logSql);

                sqlConnection.Open();
                connectionAction(sqlConnection);
                sqlConnection.Close();
            }
        }


        protected void ConnectionExecuteWithLog(
            Action<SqlConnection> connectionAction,
            string logSql,
            object values)
        {
            var propertyInfos = values.GetType().GetProperties();

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("Executing:");
            stringBuilder.AppendLine(logSql);
            stringBuilder.AppendLine("Values:");

            foreach (var info in propertyInfos)
            {
                stringBuilder.Append("@" + info.Name + " = ");
                var value = info.GetValue(values, null);

                if (value == null)
                    stringBuilder.AppendLine("NULL,");
                else
                    stringBuilder.AppendLine(value.ToString());
            }

            SystemLog.Info(stringBuilder.ToString());

            using (var sqlConnection = new SqlConnection(DefaultConnectionString))
            {
                sqlConnection.Open();
                connectionAction(sqlConnection);
                sqlConnection.Close();
            }
        }
    }
}