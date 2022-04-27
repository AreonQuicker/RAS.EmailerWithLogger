using System.Threading.Tasks;
using E.S.Data.Query.DataAccess.Interfaces;

namespace RAS.EmailerWithLogger.Extensions
{
    internal static class AppExtensions
    {
        internal static async Task CreateSchemaAndTableAsync(this IDataAccessQuery dataAccessQuery)
        {
            if (dataAccessQuery != null)
            {
                var schemaCount = await dataAccessQuery.FirstOrDefaultQueryAsync<int>(
                    "SELECT COUNT(*) FROM sys.schemas WHERE name = 'Emailer'");

                if (schemaCount == 0) await dataAccessQuery.FirstOrDefaultQueryAsync<dynamic>("CREATE SCHEMA Emailer");

                var tableCount = await dataAccessQuery.FirstOrDefaultQueryAsync<int>(
                    "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'Emailer' AND TABLE_NAME = 'EmailLog'");

                if (tableCount == 0)
                    await dataAccessQuery.FirstOrDefaultQueryAsync<dynamic>(
                        "CREATE TABLE [Emailer].[EmailLog]( [Id] [INT] IDENTITY(1,1) NOT NULL, [CreatedDate] [DATETIME] NOT NULL, [Year] [INT] NOT NULL, [Month] [INT] NOT NULL, [Day] [INT] NOT NULL, [TemplateId] [VARCHAR](100) NOT NULL, [Subject] [VARCHAR](500) NOT NULL, [ToEmails] [VARCHAR](500) NOT NULL, [Status] [VARCHAR](50) NOT NULL, [LoggedInUser] [VARCHAR](100) NULL, CONSTRAINT [PK_EmailLog] PRIMARY KEY CLUSTERED ( [Id] ASC )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY] ) ON [PRIMARY]");
            }
        }
    }
}