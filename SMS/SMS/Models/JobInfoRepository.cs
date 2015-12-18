using SMS.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SMS.Models
{
    public class JobInfoRepository
    {
       

        public IEnumerable<FinalVoting> GetData(string Band)
        {

            using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                connection.Open();
                //string a = @"SELECT COUNT(*) as Votes,Keyword  FROM [dbo].[MstSMS] group by Keyword order by Votes desc";
                string a = @"SELECT Votes FROM [dbo].[EmpKeywordMap] where Band='"+Band +"' order by Votes desc";

                using (SqlCommand command = new SqlCommand(a, connection))
                {
                    command.Notification = null;
                    SqlDependency dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    using (var reader = command.ExecuteReader())
                        return reader.Cast<IDataRecord>()
                            .Select(x => new FinalVoting()
                            {
                              
                               TotalVotes = x.GetInt32(0)
                                //Keyword = x.GetString(1)
                            }).ToList();



                }
            }
        }
      
        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            FinalVotingHub.Show();
        }


    }

    public class JobInfo
    {
        public int JobID { get; set; }
        public int Name { get; set; }
        public string LastExecutionDate { get; set; }
        public string Status { get; set; }
    }
    public class FinalVoting
    {
        public string Keyword  { get; set; }
        public int TotalVotes { get; set; }
        //public string EmpCode { get; set; }
    }

}