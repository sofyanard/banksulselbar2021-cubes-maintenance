using System.Web;
using System.Data.SqlClient;
using System;
using System.Configuration;

namespace CuBES_Maintenance
{
	/// <summary>
	/// Summary description for MyConnection.
	/// </summary>
	public class MyConnection
	{
		private SqlConnection myConn;
		private SqlCommand myCMD;
		private SqlDataReader myReader;
		string koneksi = ConfigurationSettings.AppSettings["connME"];

		public MyConnection()
		{
			myConn = new SqlConnection(koneksi);
		}

		public string OpenConnection()
		{
			try
			{
				myConn.Open();
				return "Opening connection succeeded";
			}
			catch (Exception ex)
			{
				return ex.Message.ToString();
			}
		}

		public string CloseConnection()
		{
			try
			{
				myConn.Close();
				return "Connection closed successily";
			}
			catch (Exception ex)
			{
				return ex.Message.ToString();
			}
		}

		public int Execute(string SQL)
		{
			int rows;

			myCMD = new SqlCommand(SQL, myConn);
			rows = myCMD.ExecuteNonQuery();
			return rows;
		}

		public SqlDataReader Query(string SQL)
		{
			myCMD = new SqlCommand(SQL, myConn);
			myReader = myCMD.ExecuteReader();
			return myReader;
		}
	}
}