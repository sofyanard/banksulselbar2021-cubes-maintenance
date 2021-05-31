using System;
using System.Data.SqlClient;
using System.IO;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using DMS.DBConnection;

namespace CuBES_Maintenance.Parameter
{
	/// <summary>
	/// Summary description for ParamDownload.
	/// </summary>
	public partial class ParamDownload : System.Web.UI.Page
	{
		protected string strHasil, x, xTemp;
		protected int l1, l2, sudahCek;
		protected System.Web.UI.WebControls.Label Label1;
		protected Connection dmsConn;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		protected void Button1_Click(object sender, System.EventArgs e)
		{
			SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
			//SqlConnection conn = new SqlConnection("Data Source=(local);Initial Catalog=SME;uid=sa;pwd=;Pooling=true");
			SqlCommand cmd		= conn.CreateCommand(),
				cmdIn			= conn.CreateCommand();

			StreamReader sr3 = File.OpenText(@"C:\LCUCPARA");
			strHasil		 = "";
			l1				 = 0;
			l2				 = 0;
			x				 = "";
			xTemp			 = "";
			sudahCek		 = 0;
			
			conn.Open();
			
			cmd.CommandText = "delete from para_upload";
			cmd.ExecuteNonQuery();

			int k = 0, count = 0, effected = 0;
			
			while ((strHasil = sr3.ReadLine()) != null)
			{
				x = "insert into para_upload(filename,detail1,detail2,detail3,detail4,detail5," +
					"detail6,detail7,detail8,detail9,detail10,detail11,detail12,detail13,detail14,detail15,detail16,detail17) values(";
				int pos = 0;
				cmd.CommandText = "Select * from RFUPFILE where UPFILE_GROUP='3'";
				SqlDataReader dr = cmd.ExecuteReader();
				while(dr.Read())
				{
					int intStart,intLength;
					intStart = pos;
					intLength = dr.GetInt32(3);
					if (dr.GetString(2) == "N")
					{
						xTemp = strHasil.Substring(pos, intLength);
						l1 = dr.GetInt32(4);
						if(l1>0)
						{
							xTemp = xTemp.Substring(0, intLength - l1) + "." + xTemp.Substring(intLength - l1, l1);
						}
						x = x + xTemp;
					}
					else if (dr.GetString(2) == "C")
					{
						if (pos < strHasil.Length)
						{
							try
							{
								x = x + "'" + strHasil.Substring(pos, intLength).Trim() + "'";
							}
							catch (Exception)
							{
							}
						}
					}
					else if (dr.GetString(2) == "D")
					{
						string strTgl;
						strTgl =	strHasil.Substring(pos,intLength);
						strTgl = strTgl.Substring(2,2) + "/" + strTgl.Substring(0,2) + "/" + strTgl.Substring(4,4);
						x = x + "'" + strTgl + "'";
					}
					pos = pos + intLength;
					if((pos + intLength) < strHasil.Length)
						x=x+",";
				}
				
				dr.Close(); 
				x = x + ")";
				k++;
				cmdIn = conn.CreateCommand();
				cmdIn.CommandText = x;

				try
				{
					cmdIn.ExecuteNonQuery();
					effected++;
				}
				catch(Exception)
				{
					//sr3.Close(); 
				}
				
				count++;
			}


			//Label1.Text = count.ToString() + " records copied!";
			readResult.Text = "<b>No. of records updated: " + effected.ToString() + "<br>" + 
				"Total Number of records in file: " + count.ToString() + "</b>";
			sr3.Close(); 
			conn.Close();
		}

		protected void Button2_Click(object sender, System.EventArgs e)
		{
			//dmsConn = new Connection("Data Source=(local);Initial Catalog=SME;uid=sa;pwd=;Pooling=true");
			dmsConn = new Connection(System.Configuration.ConfigurationSettings.AppSettings["eSecurityConnectString"]);
			Connection	connSME = new Connection(), 
						connCON = new Connection(), 
						connCC = new Connection();
			
			dmsConn.QueryString = "select moduleid, db_ip, db_nama, db_loginid, db_loginpwd from rfmodule where moduleid in ('01', '20', '40')";
			dmsConn.ExecuteQuery();

			for (int i = 0; i < dmsConn.GetRowCount(); i++)
			{
				if (dmsConn.GetFieldValue(i, "moduleid") == "01")
					connSME = new Connection("Data Source=" + dmsConn.GetFieldValue(i, "db_ip") + ";Initial Catalog=" + dmsConn.GetFieldValue(i, "db_nama") + ";uid=" + dmsConn.GetFieldValue(i, "db_loginid") + ";pwd=" + dmsConn.GetFieldValue(i, "db_loginpwd") + ";Pooling=true");
				else if (dmsConn.GetFieldValue(i, "moduleid") == "20")
					connCC = new Connection("Data Source=" + dmsConn.GetFieldValue(i, "db_ip") + ";Initial Catalog=" + dmsConn.GetFieldValue(i, "db_nama") + ";uid=" + dmsConn.GetFieldValue(i, "db_loginid") + ";pwd=" + dmsConn.GetFieldValue(i, "db_loginpwd") + ";Pooling=true");
				else if (dmsConn.GetFieldValue(i, "moduleid") == "40")
					connCON = new Connection("Data Source=" + dmsConn.GetFieldValue(i, "db_ip") + ";Initial Catalog=" + dmsConn.GetFieldValue(i, "db_nama") + ";uid=" + dmsConn.GetFieldValue(i, "db_loginid") + ";pwd=" + dmsConn.GetFieldValue(i, "db_loginpwd") + ";Pooling=true");
			}

			dmsConn.QueryString = "select distinct eMAS_FILENAME from param_def";
			dmsConn.ExecuteQuery();
			DataTable dtParam = dmsConn.GetDataTable().Copy();
			DataTable dttc;
			string smeQuery = "", conQuery = "", ccQuery = "", paramQuery = "",
				smeTable = "", conTable = "", ccTable = "";

			for (int i = 0; i < dtParam.Rows.Count; i++)
			{
				smeTable = ""; conTable = ""; ccTable = "";

				dmsConn.QueryString = "select emas_desc, sme_table, sme_column, con_table, con_column, cc_table, cc_column, stored_proc from param_def where eMAS_FILENAME='" + dtParam.Rows[i]["emas_filename"].ToString() + "'";
				dmsConn.ExecuteQuery();
				dttc= dmsConn.GetDataTable().Copy();

				// Build query untuk insert
				// Use "insert into" for regular insert, exec stored_proc to use stored procedure
				if (dmsConn.GetRowCount() > 0)
				{
					if ((dmsConn.GetFieldValue(0, "sme_table") != "") && (dmsConn.GetFieldValue(0, "sme_column") != ""))
					{
						//smeQuery = "insert into " + dmsConn.GetFieldValue(0, "sme_table") + " (";
						smeQuery = "exec " + dmsConn.GetFieldValue(0, "stored_proc") + " ";
						smeTable = dmsConn.GetFieldValue(0, "sme_table");
					}
					if ((dmsConn.GetFieldValue(0, "con_table") != "") && (dmsConn.GetFieldValue(0, "con_column") != ""))
					{
						//conQuery = "insert into " + dmsConn.GetFieldValue(0, "con_table") + " (";
						conQuery = "exec " + dmsConn.GetFieldValue(0, "stored_proc") + " ";
						conTable = dmsConn.GetFieldValue(0, "con_table");
					}
					if ((dmsConn.GetFieldValue(0, "cc_table") != "") && (dmsConn.GetFieldValue(0, "cc_column") != ""))
					{
						//ccQuery = "insert into " + dmsConn.GetFieldValue(0, "cc_table") + " (";
						ccQuery = "exec " + dmsConn.GetFieldValue(0, "stored_proc") + " ";
						ccTable = dmsConn.GetFieldValue(0, "cc_table");
					}

					paramQuery = "select ";
					
					// Loop nge-build query untuk select values di para_upload
					for (int blah = 0; blah < dmsConn.GetRowCount(); blah++)
					{
						if (blah == (dmsConn.GetRowCount() - 1))
							paramQuery += dmsConn.GetFieldValue(blah, "emas_desc") + " from para_upload where fileName = '" + dtParam.Rows[i]["emas_filename"].ToString() + "'";
						else
							paramQuery += dmsConn.GetFieldValue(blah, "emas_desc") + ", ";
					}
				}

				// Loop nge-build query untuk insert
				// If using stored procedure comment this loop
				/*
				for (int j = 0; j < dttc.Rows.Count; j++)
				{
					if ((dttc.Rows[j]["sme_table"].ToString() != "") && (dttc.Rows[j]["sme_column"].ToString() != ""))
					{
						if (j == (dttc.Rows.Count - 1))
							smeQuery += dttc.Rows[j]["sme_column"].ToString() + ") values ('";
						else
							smeQuery += dttc.Rows[j]["sme_column"].ToString() + ", ";
					}

					if ((dttc.Rows[j]["con_table"].ToString() != "") && (dttc.Rows[j]["con_column"].ToString() != ""))
					{
						if (j == (dttc.Rows.Count - 1))
							conQuery += dttc.Rows[j]["con_column"].ToString() + ") values ('";
						else
							conQuery += dttc.Rows[j]["con_column"].ToString() + ", ";
					}

					if ((dttc.Rows[j]["cc_table"].ToString() != "") && (dttc.Rows[j]["cc_column"].ToString() != ""))
					{
						if (j == (dttc.Rows.Count - 1))
							ccQuery += dttc.Rows[j]["cc_column"].ToString() + ") values ('";
						else
							ccQuery += dttc.Rows[j]["cc_column"].ToString() + ", ";
					}
				}
				*/
			
				// Loop nge-build values untuk di-insert
				dmsConn.QueryString = paramQuery;
				dmsConn.ExecuteQuery();
				DataTable dtInsert = dmsConn.GetDataTable().Copy();

				// if later on eMAS send status if it's update, insert or delete...
				// Remove these 3 lines below and fix the logic
				if (smeTable != "")
				{
					connSME.QueryString = "update " + smeTable + " set active = '0'";
					connSME.ExecuteNonQuery();
				}
				if (conTable != "")
				{
					connCON.QueryString = "update " + conTable + " set active = '0'";
					connCON.ExecuteNonQuery();
				}
				if (ccTable != "")
				{
					connCC.QueryString = "update " + ccTable + " set active = '0'";
					connCC.ExecuteNonQuery();
				}

				for (int k = 0; k < dtInsert.Rows.Count; k++)
				{
					string insertQuery = "";

					if (smeQuery.Trim() != "")
					{
						//insertQuery = smeQuery + dtInsert.Rows[k][0].ToString() + "', '" + dtInsert.Rows[k][1].ToString() + "')";
						insertQuery = smeQuery + "'" + dtInsert.Rows[k][0].ToString() + "', '" + dtInsert.Rows[k][1].ToString() + "'";
						connSME.QueryString = insertQuery;
						connSME.ExecuteNonQuery();
					}
					if (conQuery.Trim() != "")
					{
						//insertQuery = conQuery + dtInsert.Rows[k][0].ToString() + "', '" + dtInsert.Rows[k][1].ToString() + "')";
						insertQuery = conQuery + "'" + dtInsert.Rows[k][0].ToString() + "', '" + dtInsert.Rows[k][1].ToString() + "'";
						connCON.QueryString = insertQuery;
					}
					if (ccQuery.Trim() != "")
					{
						//insertQuery = ccQuery + dtInsert.Rows[k][0].ToString() + "', '" + dtInsert.Rows[k][1].ToString() + "')";
						insertQuery = ccQuery + "'" + dtInsert.Rows[k][0].ToString() + "', '" + dtInsert.Rows[k][1].ToString() + "'";
						connCC.QueryString = insertQuery;
					}
					//dmsConn.ExecuteNonQuery();
				}
			}
			updateResult.Text = "<b>Number of parameter tables affected: " + 
				dtParam.Rows.Count + "</b>";
		}
	}
}
