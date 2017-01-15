using System;
using Gtk;


using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Reflection;
using Tweetinvi;
using Tweetinvi.Models;
using Tweetinvi.Parameters;
using Tweetinvi.Streaming;
using Stream = Tweetinvi.Stream;
using System.IO;
using System.Diagnostics;
using Tweetinvi.Exceptions; // Handle Exceptions
using Tweetinvi.Core.Extensions; // Extension methods provided by Tweetinvi
using Tweetinvi.Models.DTO; // Data Transfer Objects for Serialization
using Tweetinvi.Json; // JSON static classes to get json from Twitter.
using Tweetinvi.Models.Entities;
using System.Text.RegularExpressions;
//using SQL;
using System.Timers;
//using SQLite;
//using SQLitePCL;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using System.Drawing;

using System.Data.SQLite;
//using SQLitePCL;
using System.Data.Entity;
//using System.Data.Common;
using Newtonsoft;
using Newtonsoft.Json;
using System.Resources;



//using SQL;

//using MySql;
//using MySql.Data.MySqlClient;
namespace TwitterClock
{
	public class Xristes
	{


		public int ID_Users { get; set; }

		public string Onoma { get; set; }

		public string AccessToken { get; set; }

		public string AccessTokenSec { get; set; }



		public override string ToString()
		{
			return string.Format("[Person: ID_Xristis={0}, Onoma={1}, AccessTok={2},AccessTokSec={3}]", ID_Users, Onoma, AccessToken, AccessTokenSec);
		}
	}
	public class Anartiseis
	{


		public int ID_Anartiseis { get; set; }

		public int ID_AnXr { get; set; }
		public string keimeno { get; set; }
		public System.DateTime Hmerominia { get; set; }
		public string image { get; set; }

		public override string ToString()
		{
			return string.Format("[Person: ID_Anartiseis={0}, ID_AnXr={1}, keimeno={2},Hmerominia={3},image={4}]", ID_Anartiseis, ID_AnXr, keimeno, Hmerominia, image);
		}
	}
	class DB_Connect
	{

		public List<string> return_AnartisiKeimeno(int ID, List<DateTime> Dates)
		{
			int i = 0;
			List<string> apotelesmata = new List<string>();
			List<string> hmerominies = new List<string>();
			List<DateTime> dates = new List<DateTime>();
			List<string> results = new List<string>();

			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;

			using (SQLiteConnection connect = new SQLiteConnection(@path4))
			{

				connect.Open();
				using (SQLiteCommand fmd = connect.CreateCommand())
				{
					fmd.CommandText = @"SELECT keimeno,Hmerominia FROM Anartiseis WHERE ID_AnXr='" + ID + "';";
					fmd.CommandType = CommandType.Text;

					SQLiteDataReader r = fmd.ExecuteReader();
					string ena;
					string dio;
					string repeatEna;
					string repeatdio;
					string repeattria;

					string[] repetition = new string[2];
					int result;
					DateTime A;
					DateTime B;
					DateTime C;
					while (r.Read())
					{
						i++;
						apotelesmata.Add(Convert.ToString(r["keimeno"]));
						hmerominies.Add(Convert.ToString(r["Hmerominia"]));
						dates.Add(Convert.ToDateTime(r["Hmerominia"]));


					}
					dates.Sort((a, b) => a.CompareTo(b));


					for (int q = 0; q < apotelesmata.Count; q++)
					{


						for (int p = 0; p < apotelesmata.Count; p++)
						{
							A = Convert.ToDateTime(hmerominies[p]);

							result = DateTime.Compare(A, dates[q]);
							if (result == 0)
							{
								ena = hmerominies[q];
								dio = apotelesmata[p];

								results.Add(apotelesmata[p]);

							}
						}
					}

					return results;
				}

			}
		}
		public List<string> return_AnartisiRepeats(int ID, List<DateTime> Dates, List<string> text)
		{
			int i = 0;
			List<string> apotelesmata = new List<string>();
			List<string> hmerominies = new List<string>();
			List<int> repeats = new List<int>();
			List<int> repeatsw = new List<int>();
			List<int> repeatsm = new List<int>();
			List<int> repeatsRes = new List<int>();
			List<int> repeatswRes = new List<int>();
			List<int> repeatsmRes = new List<int>();
			List<string> merge = new List<string>();
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;

			//List<string[]> Final = new List<string[]>();
			//string[,] apotelesmata = new string[3, 100];
			using (SQLiteConnection connect = new SQLiteConnection(@path4))
			{

				connect.Open();
				using (SQLiteCommand fmd = connect.CreateCommand())
				{
					fmd.CommandText = @"SELECT keimeno,Hmerominia,DaysRepeat,weeksRepeat,monthsRepeat FROM Anartiseis WHERE ID_AnXr='" + ID + "';";
					fmd.CommandType = CommandType.Text;

					SQLiteDataReader r = fmd.ExecuteReader();
					string ena;
					string dio;
					int repeatEna;
					int repeatdio;
					int repeattria;

					string[] repetition = new string[2];
					int result;
					DateTime A;
					DateTime B;
					DateTime C;
					while (r.Read())
					{
						i++;
						apotelesmata.Add(Convert.ToString(r["keimeno"]));
						hmerominies.Add(Convert.ToString(r["Hmerominia"]));
						//if(Convert.r["DaysRepeat"]!= 0&& r["weeksRepeat"] !=0 && r["monthsRepeat"]!=0)
						repeats.Add(Convert.ToInt32(r["DaysRepeat"]));
						repeatsw.Add(Convert.ToInt32(r["weeksRepeat"]));
						repeatsm.Add(Convert.ToInt32(r["monthsRepeat"]));

					}
					for (int q = 0; q < apotelesmata.Count; q++)
					{


						for (int p = 0; p < apotelesmata.Count; p++)
						{
							A = Convert.ToDateTime(hmerominies[p]);

							result = DateTime.Compare(A, Dates[q]);
							if (result == 0 && apotelesmata[p] == text[q])
							{

								repeatsRes.Add(repeats[p]);
								repeatswRes.Add(repeatsw[p]);
								repeatsmRes.Add(repeatsm[p]);





							}
						}
					}
					for (int g = 0; g < repeats.Count; g++)
					{
						merge.Add(repeatsmRes[g] + " Μηνες, " + repeatswRes[g] + " Εβδομαδες, " + repeatsRes[g] + " Ημερες");
					}
					return merge;

				}

			}
		}


		public List<DateTime> return_AnartisiHmerominia(int ID)
		{
			int i = 0;
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			List<string> apotelesmata = new List<string>();
			List<DateTime> dates = new List<DateTime>();
			//string[,] apotelesmata = new string[3, 100];
			using (SQLiteConnection connect = new SQLiteConnection(@path4))
			{
				connect.Open();
				using (SQLiteCommand fmd = connect.CreateCommand())
				{
					fmd.CommandText = @"SELECT Hmerominia FROM Anartiseis WHERE ID_AnXr='" + ID + "';";
					fmd.CommandType = CommandType.Text;

					SQLiteDataReader r = fmd.ExecuteReader();



					while (r.Read())
					{
						i++;
						apotelesmata.Add(Convert.ToString(r["Hmerominia"]));
						dates.Add(Convert.ToDateTime(r["Hmerominia"]));
					}
					dates.Sort((a, b) => a.CompareTo(b));

					return dates;
				}
			}
		}

		public int return_id(string onomaXristis)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			using (SQLiteConnection connect = new SQLiteConnection(@path4))
			{
				connect.Open();

				using (SQLiteCommand fmd = connect.CreateCommand())
				{
					fmd.CommandText = @"SELECT ID_Users FROM Xristes WHERE Onoma='" + onomaXristis + "';";
					fmd.CommandType = CommandType.Text;
					SQLiteDataReader r = fmd.ExecuteReader();
					int roda = 1;
					while (r.Read())
					{

						roda = Convert.ToInt32(r["ID_Users"]);

					}
					return roda;
				}
			}
		}
		public int return_idAn(string keimeno, System.DateTime date)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			using (SQLiteConnection connect = new SQLiteConnection(@path4))
			{
				connect.Open();

				using (SQLiteCommand fmd = connect.CreateCommand())
				{
					fmd.CommandText = @"SELECT ID_Anartiseis FROM Anartiseis WHERE keimeno='" + keimeno + "'AND Hmerominia='" + date + "' LIMIT 1;";
					fmd.CommandType = CommandType.Text;
					SQLiteDataReader r = fmd.ExecuteReader();
					int roda = 1;
					while (r.Read())
					{

						roda = Convert.ToInt32(r["ID_Anartiseis"]);

					}
					return roda;
				}
			}
		}
		public string find_onoma(int id)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			using (SQLiteConnection connect = new SQLiteConnection(@path4))
			{
				connect.Open();

				using (SQLiteCommand fmd = connect.CreateCommand())
				{
					fmd.CommandText = @"SELECT Onoma FROM Xristes WHERE ID_Users='" + id + "';";
					fmd.CommandType = CommandType.Text;
					SQLiteDataReader r = fmd.ExecuteReader();
					string roda = "";
					while (r.Read())
					{

						roda = Convert.ToString(r["Onoma"]);

					}
					return roda;
				}
			}
		}
		public List<string> return_onoma()
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			List<string> lista = new List<string>();
			int k = 0;
			using (SQLiteConnection connect = new SQLiteConnection(@path4))
			{
				connect.Open();
				using (SQLiteCommand fmd = connect.CreateCommand())
				{
					fmd.CommandText = @"SELECT Onoma FROM Xristes";
					fmd.CommandType = CommandType.Text;
					SQLiteDataReader r = fmd.ExecuteReader();
					int roda = 1;

					while (r.Read())
					{
						lista.Add(Convert.ToString(r["Onoma"]));

					}
					return lista;

				}
			}
		}


		public string[,] return_dates()
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			using (SQLiteConnection connect = new SQLiteConnection(@path4))
			{
				string[,] creds = new string[2, 100];
				connect.Open();
				using (SQLiteCommand fmd = connect.CreateCommand())
				{
					fmd.CommandText = @"SELECT ID_Anartiseis,Hmerominia FROM Anartiseis; ";
					fmd.CommandType = CommandType.Text;
					SQLiteDataReader r = fmd.ExecuteReader();
					int i = 1;
					while (r.Read())
					{


						creds[0, i] = Convert.ToString(r["Hmerominia"]);
						creds[1, i] = Convert.ToString(r["ID_Anartiseis"]);


					}
				}
				return creds;
			}
		}
		public string[] return_credentials(string onoma_xristi)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			string[] creds = new string[2];
			using (SQLiteConnection connect = new SQLiteConnection(@path4))
			{

				connect.Open();
				using (SQLiteCommand fmd = connect.CreateCommand())
				{
					fmd.CommandText = @"SELECT * FROM Xristes WHERE Onoma='" + onoma_xristi + "'; ";
					fmd.CommandType = CommandType.Text;
					SQLiteDataReader r = fmd.ExecuteReader();

					creds[0] = Convert.ToString(r["AccessToken"]);
					creds[1] = Convert.ToString(r["AccessTokenSec"]);
					return creds;


				}

			}
		}
		public static List<string> GetCredentials(int id)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			List<string> ImportedFiles = new List<string>();
			using (SQLiteConnection connect = new SQLiteConnection(@path4))
			{
				connect.Open();
				using (SQLiteCommand fmd = connect.CreateCommand())
				{
					fmd.CommandText = @"SELECT  AccessToken,AccessTokenSec FROM Xristes WHERE ID_Users='" + id + "';";
					fmd.CommandType = CommandType.Text;
					SQLiteDataReader r = fmd.ExecuteReader();
					while (r.Read())
					{
						ImportedFiles.Add(Convert.ToString(r["AccessToken"]));
						ImportedFiles.Add(Convert.ToString(r["AccessTokenSec"]));

					}
				}
			}
			return ImportedFiles;
		}
		public static List<string> GetImportedFileList(string onoma)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			List<string> ImportedFiles = new List<string>();
			using (SQLiteConnection connect = new SQLiteConnection(@path4))
			{
				connect.Open();
				using (SQLiteCommand fmd = connect.CreateCommand())
				{
					fmd.CommandText = @"SELECT  * FROM Xristes WHERE Onoma='" + onoma + "';";
					fmd.CommandType = CommandType.Text;
					SQLiteDataReader r = fmd.ExecuteReader();
					while (r.Read())
					{
						ImportedFiles.Add(Convert.ToString(r["AccessToken"]));
						ImportedFiles.Add(Convert.ToString(r["AccessTokenSec"]));

					}
				}
			}
			return ImportedFiles;
		}
		public void DeleteAnartisi(string text, string Date)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			//some DateTime value, e.g. DateTime.Now;
			using (var conn = new SQLiteConnection(@path4))
			{
				conn.Open();

				using (var cmd = new SQLiteCommand(conn))
				{
					using (var transaction = conn.BeginTransaction())
					{
						cmd.CommandText =
							   "DELETE FROM Anartiseis WHERE keimeno = '" + text + "' AND Hmerominia = '" + Date + "';";
						cmd.ExecuteNonQuery();


						transaction.Commit();


					}
				}
			}
		}
		public static void DeleteAnartisiNormal(int id)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			//some DateTime value, e.g. DateTime.Now;
			using (var conn = new SQLiteConnection(@path4))
			{
				conn.Open();

				using (var cmd = new SQLiteCommand(conn))
				{
					using (var transaction = conn.BeginTransaction())
					{
						cmd.CommandText =
							   "DELETE FROM Anartiseis WHERE ID_Anartiseis = '" + id + "' ;";
						cmd.ExecuteNonQuery();


						transaction.Commit();


					}
				}
			}
		}
		public void DeleteUser(string onoma_xristis)
		{
			int id = 1;
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			//some DateTime value, e.g. DateTime.Now;
			id = return_id(onoma_xristis);
			using (var conn = new SQLiteConnection(@path4))
			{
				conn.Open();

				using (var cmd = new SQLiteCommand(conn))
				{
					using (var transaction = conn.BeginTransaction())
					{
						cmd.CommandText =
							   "DELETE FROM Anartiseis WHERE ID_AnXr = '" + id + "';";
						cmd.ExecuteNonQuery();


						transaction.Commit();


					}
					using (var transaction = conn.BeginTransaction())
					{
						cmd.CommandText =
							   "DELETE FROM Xristes WHERE Onoma = '" + onoma_xristis + "';";
						cmd.ExecuteNonQuery();


						transaction.Commit();
					}
				}

				conn.Close();
			}
		}
		public void InsertDat(string onoma, string AccessToken, string AccessTokenSec)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			using (var conn = new SQLiteConnection(@path4))
			{
				conn.Open();
				using (var cmd = new SQLiteCommand(conn))
				{
					using (var transaction = conn.BeginTransaction())
					{

						cmd.CommandText =
							   "INSERT INTO Xristes (Onoma, AccessToken,AccessTokenSec) VALUES ('" + onoma + "', '" + AccessToken + "','" + AccessTokenSec + "');";
						cmd.ExecuteNonQuery();
						Console.WriteLine("{0}  {1}", AccessToken, AccessTokenSec);

						transaction.Commit();
					}
				}

				conn.Close();
			}
		}

		public void InsertAnartisi(string keimeno, System.DateTime date, int ID_AnXr, string image, int days, int weeks, int months)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			System.DateTime dateTimeVariable = date;
			//some DateTime value, e.g. DateTime.Now;
			using (var conn = new SQLiteConnection(@path4))
			{
				conn.Open();

				using (var cmd = new SQLiteCommand(conn))
				{
					using (var transaction = conn.BeginTransaction())
					{

						cmd.CommandText =
							   "INSERT INTO Anartiseis (keimeno, Hmerominia,ID_AnXr,image,DaysRepeat,weeksRepeat,monthsRepeat) VALUES ('" + keimeno + "', '" + dateTimeVariable + "','" + ID_AnXr + "','" + image + "','" + days + "','" + weeks + "','" + months + "');";
						cmd.ExecuteNonQuery();


						transaction.Commit();
					}
				}

				conn.Close();
			}
		}
		public static void InsertAnartisiNormal(string keimeno, DateTime date, int ID_AnXr, string image, int days, int weeks, int months)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			//System.DateTime dateTimeVariable = date;
			//some DateTime valsue, e.g. DateTime.Now;
			using (var conn = new SQLiteConnection(@path4))
			{
				conn.Open();

				using (var cmd = new SQLiteCommand(conn))
				{
					using (var transaction = conn.BeginTransaction())
					{

						cmd.CommandText =
							   "INSERT INTO Anartiseis (keimeno, Hmerominia,ID_AnXr,image,DaysRepeat,weeksRepeat,monthsRepeat) VALUES ('" + keimeno + "', '" + date + "','" + ID_AnXr + "','" + image + "','" + days + "','" + weeks + "','" + months + "');";
						cmd.ExecuteNonQuery();


						transaction.Commit();
					}
				}

				conn.Close();
			}
		}
		public void UpdateAnartisi(string keimeno, System.DateTime date, int ID_AnXr, string image, int days, int weeks, int months)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			System.DateTime dateTimeVariable = date;
			//some DateTime value, e.g. DateTime.Now;
			using (var conn = new SQLiteConnection(@path4))
			{
				conn.Open();

				using (var cmd = new SQLiteCommand(conn))
				{
					using (var transaction = conn.BeginTransaction())
					{

						cmd.CommandText =
							   "UPDATE Anartiseis (keimeno, Hmerominia,ID_AnXr,image,DaysRepeat,weeksRepeat,monthsRepeat) VALUES ('" + keimeno + "', '" + dateTimeVariable + "','" + ID_AnXr + "','" + image + "','" + days + "','" + weeks + "','" + months + "');";
						cmd.ExecuteNonQuery();


						transaction.Commit();
					}
				}

				conn.Close();
			}
		}
		public int checkonoma(string onoma)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			List<string> list = new List<string>();
			using (SQLiteConnection connect = new SQLiteConnection(@path4))
			{
				connect.Open();
				using (SQLiteCommand fmd = connect.CreateCommand())
				{
					fmd.CommandText = @"SELECT Onoma FROM Xristes";
					fmd.CommandType = CommandType.Text;
					SQLiteDataReader r = fmd.ExecuteReader();
					int apotelesma = 0;
					while (r.Read())
					{

						list.Add(Convert.ToString(r["Onoma"]));

					}
					//Console.WriteLine(list[0]);

					foreach (string prime in list)
					{

						if (prime == onoma)
						{
							apotelesma = 1;

						}

					}
					return apotelesma;
				}
			}

		}
		public void InsertId(int id)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			using (var conn = new SQLiteConnection(@path4))
			{
				conn.Open();

				using (var cmd = new SQLiteCommand(conn))
				{
					using (var transaction = conn.BeginTransaction())
					{

						cmd.CommandText =
							   "INSERT INTO Xristes (ID_Users,Onoma, AccessToken,AccessTokenSec) VALUES ('" + id + "');";
						cmd.ExecuteNonQuery();


						transaction.Commit();
					}
				}

				conn.Close();
			}
		}
		public void InsertOnoma(string onoma)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			using (var conn = new SQLiteConnection(@path4))
			{
				conn.Open();

				using (var cmd = new SQLiteCommand(conn))
				{
					using (var transaction = conn.BeginTransaction())
					{

						cmd.CommandText =
							   "INSERT INTO Xristes (Onoma) VALUES ('" + onoma + "');";
						cmd.ExecuteNonQuery();


						transaction.Commit();
					}
				}

				conn.Close();
			}
		}
		public static void post(string keimeno, string Acc, string AccSec, string image)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			//string path = Environment.CurrentDirectory;
			//string path4 = data + path + path3;

			var credens = new TwitterCredentials("0rbIj70OY04gB6nbMuXaCArGl", "8sBsmmv6UbLBv3OjnPRDSwiopDlezfcAoxuFKRLyiqDxZ1wrt5", Acc, AccSec);
			Auth.SetCredentials(credens);
			if (image == "")
			{
				var second = Tweet.PublishTweet(keimeno);
			}
			else {


				string filePath = @Environment.CurrentDirectory + @"\TwitterClockImages\" + image;
				byte[] file = File.ReadAllBytes(filePath);
				var tweet = Tweet.PublishTweetWithImage(keimeno, file);
				//	var imageURL = tweet.Entities.Medias.First().MediaURL;

				//byte[] image1 = File.ReadAllBytes(@"C:\Users\user\Desktop\twitter.jpg");

				//var media = Upload.UploadImage(image1);

				//var tweet = Tweet.PublishTweet(keimeno, new PublishTweetOptionalParameters
				//{
				//	Medias = new List<IMedia> { media }
				//});
			}

		}
		public void Insertcred(int id, string AccessToken, string AccessTokenSec)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			using (var conn = new SQLiteConnection(@path4))
			{
				conn.Open();

				using (var cmd = new SQLiteCommand(conn))
				{
					using (var transaction = conn.BeginTransaction())
					{

						cmd.CommandText =
							   "INSERT INTO Xristes ( AccessToken,AccessTokenSec) VALUES ('" + AccessToken + "','" + AccessTokenSec + "') WHERE ID_Xristis=;";
						cmd.ExecuteNonQuery();


						transaction.Commit();
					}
				}

				conn.Close();
			}
		}



		public void elenhosAnartisis(string keimeno, DateTime hmeres, string image, int daysR, int weeksR, int monthsR, int tautotita)
		{
			System.DateTime datenow = new System.DateTime();
			string mera = Convert.ToString(hmeres);
			datenow = DateTime.Now;
			double days = (hmeres - datenow).TotalDays;
			double minutes;
			double seconds;
			List<string> credential1 = new List<string>();
			int taut_an;
			//k = Convert.ToString(days);
			//hmeres = Convert.ToString(date);
			DateTime nea = new DateTime();
			DateTime dio = new DateTime();
			dio = hmeres;
			if (days < 1.00 && days > 0.00)
			{
				minutes = (hmeres - datenow).TotalMinutes;
				//k = Convert.ToString(minutes);
				//	Console.WriteLine("lepta ------> {0}", k);
				if (minutes < 10.00 && minutes >= 0.0)
				{
					seconds = (hmeres - datenow).TotalSeconds;
					//k = Convert.ToString(seconds);
					if (seconds < 600.00 && seconds >= 0.00)
					{
						credential1.Clear();
						credential1 = GetCredentials(tautotita);
						taut_an = return_idAn(keimeno, dio);

						Time1(taut_an, seconds, keimeno, dio, tautotita, image, daysR, weeksR, monthsR, credential1[0], credential1[1]);

					}
				}
			}
		}
		public static void Time()
		{
			int k = 1;

			System.Timers.Timer aTimer = new System.Timers.Timer();
			aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent);
			aTimer.Interval = 600000;

			aTimer.Enabled = true;

			//Console.WriteLine("Press \'q\' to quit the sample.");

			while (k != 1) ;


		}
		private static void OnTimedEvent(object source, ElapsedEventArgs e)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			using (SQLiteConnection connect = new SQLiteConnection(@path4))
			{

				List<int> ID = new List<int>();
				List<string> keimeno = new List<string>();
				List<string> Hmerominia = new List<string>();
				List<int> Id_Xristis = new List<int>();
				List<string> credential = new List<string>();
				List<int> repeatDays = new List<int>();
				List<int> repeatWeeks = new List<int>();
				List<int> repeatMonths = new List<int>();
				string image;


				string k;
				connect.Open();
				using (SQLiteCommand fmd = connect.CreateCommand())
				{
					System.DateTime datenow = System.DateTime.Now;

					//	Console.WriteLine("hello world2222");
					string anartisi;
					double days;
					double minutes;
					double seconds;
					int id;
					System.DateTime hmeres = new System.DateTime();
					fmd.CommandText = @"SELECT * FROM Anartiseis; ";
					fmd.CommandType = CommandType.Text;
					SQLiteDataReader r = fmd.ExecuteReader();
					int i = 0;
					while (r.Read())
					{
						//Console.WriteLine("hello world333");

						Hmerominia.Add(Convert.ToString(r["Hmerominia"]));
						hmeres = Convert.ToDateTime(r["Hmerominia"]);
						id = Convert.ToInt32(r["ID_Anartiseis"]);
						days = (hmeres - datenow).TotalDays;

						k = Convert.ToString(days);
						//hmeres = Convert.ToString(date);
						//Console.WriteLine("{0}  {1}",k,hmeres);
						if (days < 1.00 && days > 0.00)
						{


							minutes = (hmeres - datenow).TotalMinutes;
							k = Convert.ToString(minutes);
							//	Console.WriteLine("lepta ------> {0}", k);
							if (minutes < 10.00 && minutes >= 0.0)
							{
								seconds = (hmeres - datenow).TotalSeconds;
								k = Convert.ToString(seconds);
								if (seconds < 600.00 && seconds >= 0.00)
								{
									int rdays, rweeks, rmonths;
									int tautotita;
									//keimeno.Add(Convert.ToString(r["keimeno"]));
									//Id_Xristis.Add(Convert.ToInt32(r["ID_AnXr"]));
									ID.Add(Convert.ToInt32(r["ID_Anartiseis"]));
									repeatDays.Add(Convert.ToInt32(r["DaysRepeat"]));
									repeatDays.Add(Convert.ToInt32(r["weeksRepeat"]));
									repeatDays.Add(Convert.ToInt32(r["monthsRepeat"]));
									rdays = Convert.ToInt32(r["DaysRepeat"]);
									rweeks = Convert.ToInt32(r["weeksRepeat"]);

									rmonths = Convert.ToInt32(r["monthsRepeat"]);

									anartisi = Convert.ToString(r["keimeno"]);
									tautotita = Convert.ToInt32(r["ID_AnXr"]);
									image = Convert.ToString(r["image"]);
									credential.Clear();
									credential = GetCredentials(tautotita);
									//Console.WriteLine("{0} {1}", credential[0], credential[1]);
									Time1(tautotita, seconds, anartisi, hmeres, tautotita, image, rdays, rweeks, rmonths, credential[0], credential[1]);
									//l.DeleteAnartisi(Convert.ToInt32(r["ID_Anartiseis"]));
								}
								//		Console.WriteLine("deutera ------> {0}", k);

							}
						}
					}


					//for (int i = 0; i < Hmerominia.Count; i++)
					//{
					//Console.WriteLine("hello world333");


					// days = (date - datenow).TotalDays;
					//hmeres = Convert.ToString(days);
					//Console.WriteLine("{0}",hmeres);
					//	}
					//Console.WriteLine(keimeno[0]);

				}


			}
		}
		public static void Time1(int id, double secs, string keimeno, DateTime dio, int tautotita, string image, int daysR, int weeksR, int monthsR, string Acc, string AccSec)
		{
			//Console.WriteLine("Mpika");
			int k = 1;
			int s;

			s = Convert.ToInt32(secs);
			Console.WriteLine("{0}", Convert.ToString(s));
			s = s * 1000;
			Console.WriteLine(Convert.ToString(s));
			System.Timers.Timer aTimer = new System.Timers.Timer();
			aTimer.Elapsed += (sender, e) => { OnTimedEvent1(id, keimeno, dio, tautotita, image, daysR, weeksR, monthsR, Acc, AccSec); };
			aTimer.Interval = s;
			aTimer.AutoReset = false;
			aTimer.Enabled = true;

			//Console.WriteLine("Press \'q\' to quit the sample.");
			//k = Convert.ToInt32(Console.ReadLine());
			while (k != 1) ;
		}
		private static void OnTimedEvent1(int id, string keimeno, DateTime dio, int tautotita, string image, int daysR, int weeksR, int monthsR, string AccessTok, string AccessToksec)
		{
			Console.WriteLine("{0} {1} {2}", keimeno, AccessTok, AccessToksec);
			post(keimeno, AccessTok, AccessToksec, image);
			//nea = hmeres.AddMonths(monthsR);
			//dio = nea;
			//nea = dio.AddDays(weeksR * 7);

			dio = dio.AddDays(daysR);
			dio = dio.AddDays(weeksR * 7);
			dio = dio.AddMonths(monthsR);


			//taut_an = return_idAn(keimeno, hmeres);

			DeleteAnartisiNormal(id);

			InsertAnartisiNormal(keimeno, dio, tautotita, image, daysR, weeksR, monthsR);

		}
		public static void Time2()
		{
			string[] credits = new string[2];

			System.Timers.Timer aTimer = new System.Timers.Timer();
			aTimer.Elapsed += new ElapsedEventHandler(OnTimedEvent2);
			aTimer.Interval = 5000;
			aTimer.Enabled = true;

			Console.WriteLine("Press \'q\' to quit the sample.");

			while (Console.ReadLine() != "1") ;
		}
		private static void OnTimedEvent2(object source, ElapsedEventArgs e)
		{
			Console.WriteLine("Hello World!");
		}
	}

	public class Storeage : Window
	{
		int name;
		Label epilogi;
		TreeView treeview;
		public Storeage(int id) : base("Αναρτησεις")
		{

			Gtk.TreeIter iter;
			SetDefaultSize(-1, 500);
			BorderWidth = 10;
			DB_Connect k = new DB_Connect();
			name = id;
			string[] repeats = new string[4];
			List<string> textes = new List<string>();
			List<string> repetition = new List<string>();

			List<DateTime> dates = new List<DateTime>();
			dates = k.return_AnartisiHmerominia(id);

			textes = k.return_AnartisiKeimeno(id, dates);
			repetition = k.return_AnartisiRepeats(id, dates, textes);

			HBox hbox = new HBox(false, 10);
			Add(hbox);

			ScrolledWindow sw = new ScrolledWindow();
			sw.SetPolicy(PolicyType.Never, PolicyType.Automatic);
			hbox.PackStart(sw, false, false, 0);
			ListStore model = new Gtk.ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));

			treeview = new TreeView(model);
			sw.Add(treeview);


			TreeViewColumn Text = new TreeViewColumn();
			Text.Title = "Κειμενο";
			TreeViewColumn Date = new TreeViewColumn();
			Date.Title = "Ημερομηνια";
			TreeViewColumn Repeat = new TreeViewColumn();
			Repeat.Title = "Επαναληψη";
			treeview.AppendColumn(Text);
			treeview.AppendColumn(Date);
			treeview.AppendColumn(Repeat);

			CellRendererText TextesCell = new CellRendererText();

			// Add the cell to the column
			Text.PackStart(TextesCell, true);

			// Do the same for the song title column
			CellRendererText DatesCell = new CellRendererText();
			Date.PackStart(DatesCell, true);

			CellRendererText RepeatCell = new CellRendererText();
			Repeat.PackStart(RepeatCell, true);

			Text.AddAttribute(TextesCell, "text", 0);
			Date.AddAttribute(DatesCell, "text", 1);
			Repeat.AddAttribute(RepeatCell, "text", 2);



			for (int i = 0; i < +dates.Count; i++)
			{
				//repeats = textes[i];
				model.AppendValues(textes[i], Convert.ToString(dates[i]), repetition[i]);
			}



			Alignment align = new Alignment(0.5f, 0.0f, 0.0f, 0.0f);
			hbox.PackEnd(align, false, false, 0);
			//epilogi = new Label("dokimi");
			Button frame = new Button("Διαγραφη");
			Button edit = new Button("Διαμορφωση");
			align.Add(frame);

			frame.Clicked += Diagrafi;
			edit.Clicked += Edit;
			Alignment elign = new Alignment(0.5f, 0.0f, 0.0f, 0.0f);
			hbox.PackEnd(elign, false, false, 0);
			elign.Add(edit);
			//VBox vbox = new VBox(false, 8);
			//vbox.BorderWidth = 8;
			//frame.Add(vbox);

			//typeLabel = new Label();
			//vbox.PackStart(edit, false, false, 0);
			//iconImage = new Gtk.Image();
			//vbox.PackStart(iconImage, false, false, 0);
			//accelLabel = new Label();
			//vbox.PackStart(accelLabel, false, false, 0);
			//nameLabel = new Label();
			//vbox.PackStart(nameLabel, false, false, 0);
			//idLabel = new Label();
			//vbox.PackStart(idLabel, false, false, 0);

			treeview.Selection.Mode = Gtk.SelectionMode.Single;
			treeview.CursorChanged += new EventHandler(tree_CursorChanged);
			//
			ShowAll();
		}
		void Diagrafi(object sender, EventArgs args)
		{
			DB_Connect l = new DB_Connect();
			l.DeleteAnartisi(epilogi1, epilogi2);
			this.Destroy();
			Application.Init();
			new Storeage(name);
			Application.Run();
		}
		void Edit(object sender, EventArgs args)
		{
			this.Destroy();

			Application.Init();
			new NewPost(name, epilogi1, epilogi2, epilogi3);
			Application.Run();

		}

		string epilogi1, epilogi2, epilogi3;
		void tree_CursorChanged(object sender, EventArgs e)
		{
			TreeSelection selection = (sender as TreeView).Selection;
			TreeModel model;
			TreeIter iter;
			// THE ITER WILL POINT TO THE SELECTED ROW
			if (selection.GetSelected(out model, out iter))
			{
				epilogi1 = Convert.ToString(model.GetValue(iter, 0));
				epilogi2 = Convert.ToString(model.GetValue(iter, 1));
				epilogi3 = Convert.ToString(model.GetValue(iter, 2));
			}
		}


	}



	public class Control : Window
	{

		public Control(int id) : base("Tree")
		{
			DB_Connect k = new DB_Connect();
			List<string> textes = new List<string>();
			List<DateTime> dates = new List<DateTime>();
			dates = k.return_AnartisiHmerominia(id);

			textes = k.return_AnartisiKeimeno(id, dates);

			SetDefaultSize(400, 300);
			SetPosition(WindowPosition.Center);
			DeleteEvent += delegate { Application.Quit(); };

			TreeView tree = new TreeView();

			TreeViewColumn languages = new TreeViewColumn();
			languages.Title = "Programming languages";
			TreeViewColumn marketCol = new TreeViewColumn();
			marketCol.Title = "Market";
			tree.AppendColumn(languages);
			tree.AppendColumn(marketCol);

			//CellRendererText cell = new CellRendererText();
			//languages.PackStart(cell, true);
			//languages.AddAttribute(cell, "text", 0);

			//CellRendererText coll = new CellRendererText();
			//marketCol.PackStart(coll, true);
			//marketCol.AddAttribute(coll, "text", 0);
			ListStore musicListStore = new ListStore(typeof(string), typeof(string));
			//TreeStore treestore = new TreeStore(typeof(string), typeof(string));

			//		TreeIter iter = treestore.AppendValues("Παλαιωτερες Αναρτησεις");
			//treestore.AppendValues(iter, "Python");
			tree.Model = musicListStore;
			//iter = treestore.AppendValues("Compiling languages");
			//		treestore.AppendValues(iter, "C#","f");
			//		treestore.AppendValues(iter, "C++","f");
			//		treestore.AppendValues(iter, "C","F");
			//		treestore.AppendValues(iter, "Java","f");
			//		iter = treestore.AppendValues("human languages","F");
			//		treestore.AppendValues(iter, "C#","f");
			//		treestore.AppendValues(iter, "C++");
			//		treestore.AppendValues(iter, "C");
			//		treestore.AppendValues(iter, "Java");

			// Create the text cell that will display the artist name
			CellRendererText languagesCell = new CellRendererText();

			// Add the cell to the column
			languages.PackStart(languagesCell, true);

			// Do the same for the song title column
			CellRendererText marketColCell = new CellRendererText();
			marketCol.PackStart(marketColCell, true);

			languages.AddAttribute(languagesCell, "text", 0);
			marketCol.AddAttribute(marketColCell, "text", 1);
			//		tree.Model = treestore;
			for (int i = 0; i < textes.Count; i++)
			{
				musicListStore.AppendValues(textes[i], dates[i]);
			}
			Button btn1 = new Button("koumpi");
			Fixed fix = new Fixed();
			fix.Put(btn1, 50, 50);
			//fix.Put(tree, 10, 10);
			//Add(fix);
			Add(tree);
			//Add(fix);
			ShowAll();
		}
		void Back(object sender, EventArgs args)
		{
			this.Destroy();
			Application.Init();
			new SharpApp();
			Application.Run();


		}
	}


	public class NewPost : Window
	{
		public static List<string> GetCredentials(int id)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			List<string> ImportedFiles = new List<string>();
			using (SQLiteConnection connect = new SQLiteConnection(@path4))
			{
				connect.Open();
				using (SQLiteCommand fmd = connect.CreateCommand())
				{
					fmd.CommandText = @"SELECT  AccessToken,AccessTokenSec FROM Xristes WHERE ID_Users='" + id + "';";
					fmd.CommandType = CommandType.Text;
					SQLiteDataReader r = fmd.ExecuteReader();
					while (r.Read())
					{
						ImportedFiles.Add(Convert.ToString(r["AccessToken"]));
						ImportedFiles.Add(Convert.ToString(r["AccessTokenSec"]));

					}
				}
			}
			return ImportedFiles;
		}
		Label label;
		ComboBox clock;
		ComboBox mins;
		ComboBox repeatD;
		ComboBox repeatW;
		ComboBox repeatM;
		Entry repeatY;
		Label labelDays;
		Label labelWeeks;
		Label labelMonths;
		Entry text;
		static Label plirofories;
		Label hour;
		int tautotita;
		Label minute;
		Label keimeno;
		Label warning;
		string username;
		Calendar cal;
		public List<string> DateResolve(string Date)
		{
			List<string> lista = new List<string>();
			char separatingChars1 = ' ';
			string[] numbers = new string[10];
			char separatingChars3 = ':';
			string[] time = new string[3];
			numbers = Date.Split(separatingChars1);
			numbers[0] = numbers[0].Replace(" ", "");

			numbers[1] = numbers[1].Replace(" ", "");
			time = numbers[1].Split(separatingChars3);
			lista.Add(time[0]);
			lista.Add(time[1]);
			lista.Add(numbers[0]);
			return lista;

		}
		public NewPost(int id, string importText, string importDate, string importRepeats) : base("Tree")//,string importDate,string importHour,string importMin,string importD,string importW,string importM) : base("Tree")
		{
			tautotita = id;
			DB_Connect f = new DB_Connect();
			username = f.find_onoma(id);
			string[] timeloop = new string[24];
			string[] days = new string[11];
			string[] weeks = new string[21];
			List<string> times = new List<string>();
			string[] months = new string[26];

			string[] minloop = new string[60];
			for (int s = 0; s <= 25; s++)
			{
				if (s <= 10)
				{
					days[s] = Convert.ToString(s);
					weeks[s] = Convert.ToString(s);

					months[s] = Convert.ToString(s);

				}
				else if (s > 10 && s <= 20)
				{
					weeks[s] = Convert.ToString(s);

					months[s] = Convert.ToString(s);

				}
				else if (s > 20)
				{

					months[s] = Convert.ToString(s);

				}

			}
			for (int i = 0; i < 24; i++)
			{
				if (i < 10)
				{
					timeloop[i] = "0" + Convert.ToString(i);
				}
				else {
					timeloop[i] = Convert.ToString(i);
				}
			}
			for (int k = 0; k < 60; k++)
			{
				if (k < 10)
				{
					minloop[k] = "0" + Convert.ToString(k);

				}
				else {
					minloop[k] = Convert.ToString(k);
				}
			}
			SetDefaultSize(650, 500);
			SetPosition(WindowPosition.Center);
			Button btn1 = new Button("Δημιουργια Αναρτησης");
			Label timeset = new Label("Ωρα       Λεπτο");
			btn1.Clicked += enter;
			Label repeat = new Label("Επαναληψη αναρτησης καθε:");
			Label repeatdays = new Label("ημερες");
			Label repeatweeks = new Label("εβδομαδες");
			Label repeatmonths = new Label("μηνες:");

			text = new Entry(importText);
			cal = new Calendar();

			cal.DaySelected += OnDaySelected;
			text.Changed += OnChangedDialog;
			clock = new ComboBox(timeloop);
			mins = new ComboBox(minloop);

			repeatD = new ComboBox(days);
			repeatW = new ComboBox(weeks);
			repeatM = new ComboBox(months);
			//repeatY = new Entry("Χρονια");
			repeatD.Changed += repetition;
			repeatW.Changed += repetitionWeeks;
			repeatM.Changed += repetitionmonths;
			clock.Changed += Hour;
			mins.Changed += Minus;
			label = new Label("");
			plirofories = new Label("");
			keimeno = new Label();
			text.SetSizeRequest(400, 35);
			hour = new Label();
			minute = new Label();
			Fixed fix = new Fixed();
			Label labelrepeats = new Label("Επαναληψεις καθε \n Μερες: \n Εβδομαδες: \n Μηνες:");

			labelDays = new Label();
			labelWeeks = new Label();
			labelMonths = new Label();
			fix.Put(labelDays, 90, 215);
			fix.Put(labelWeeks, 90, 230);
			fix.Put(labelMonths, 90, 246);
			fix.Put(labelrepeats, 20, 200);

			fix.Put(repeatD, 450, 170);
			fix.Put(repeatW, 450, 200);
			fix.Put(repeat, 450, 150);
			fix.Put(timeset, 450, 80);

			fix.Put(repeatM, 450, 230);
			fix.Put(repeatdays, 510, 170);
			fix.Put(repeatweeks, 510, 200);
			fix.Put(repeatmonths, 510, 230);
			warning = new Label();
			fix.Put(warning, 250, 300);
			Label first = new Label("Χαρακτηρες:");
			Label second = new Label("Ημερομηνiα:");
			Label third = new Label("Ωρα:");
			fix.Put(hour, 70, 150);
			fix.Put(minute, 90, 150);
			fix.Put(first, 30, 90);
			fix.Put(second, 30, 120);
			fix.Put(third, 30, 150);
			Button Btn7 = new Button("Προσθηκη εικονας");
			fix.Put(Btn7, 400, 350);
			Btn7.Clicked += image;
			fix.Put(clock, 440, 100);
			fix.Put(mins, 500, 100);

			fix.Put(btn1, 440, 50);
			fix.Put(text, 20, 50);
			fix.Put(cal, 220, 100);
			fix.Put(label, 110, 120);
			fix.Put(plirofories, 100, 90);
			fix.Put(keimeno, 50, 90);
			if (importDate != "")
			{
				times = DateResolve(importDate);
				hour.Text = times[0];
				minute.Text = times[1];
				label.Text = times[2];

			}
			Add(fix);
			ShowAll();
		}
		string arxeio = "";

		void enter(object sender, EventArgs args)
		{

			int weeks, days, months;
			DB_Connect t = new DB_Connect();
			if (repeatD.ActiveText == null)
			{
				days = 0;
			}
			else
			{
				days = Convert.ToInt32(repeatD.ActiveText);

			}
			if (repeatW.ActiveText == null)
			{
				weeks = 0;
			}
			else
			{
				weeks = Convert.ToInt32(repeatW.ActiveText);

			}
			if (repeatM.ActiveText == null)
			{
				months = 0;
			}
			else
			{
				months = Convert.ToInt32(repeatM.ActiveText);

			}
			//keimeno = text.Text;
			if (text.Text != null && label.Text.Length > 0 && clock.ActiveText != null && mins.ActiveText != null)
			{
				//this.Destroy();

				DateTime date = new System.DateTime(cal.Year, cal.Month + 1, cal.Day, Convert.ToInt32(hour.Text), Convert.ToInt32(minute.Text), 0);
				if (arxeio != null)
				{
					t.InsertAnartisi(text.Text, date, tautotita, arxeio, days, weeks, months);
				}
				else
				{
					t.InsertAnartisi(text.Text, date, tautotita, "image", days, weeks, months);
				}
				t.elenhosAnartisis(text.Text, date, arxeio, days, weeks, months, tautotita);

				this.Destroy();

			}
			else
			{
				warning.Text = "Προσοχη!!Δεν προσδιορησατε ολα τα απαραιτητα στοιχεια!!";

			}
		}
		void Hour(object sender, EventArgs args)
		{
			hour.Text = clock.ActiveText;

		}
		void repetition(object sender, EventArgs args)
		{
			labelDays.Text = repeatD.ActiveText;

		}
		void repetitionWeeks(object sender, EventArgs args)
		{
			labelWeeks.Text = repeatW.ActiveText;

		}
		void repetitionmonths(object sender, EventArgs args)
		{
			labelMonths.Text = repeatM.ActiveText;

		}
		void Minus(object sender, EventArgs args)
		{

			minute.Text = mins.ActiveText;

		}

		public void image(object sender, System.EventArgs e)
		{
			string data = "Data Source=";
			string without;
			string extension;
			Gtk.FileChooserDialog fc =
			new Gtk.FileChooserDialog("Choose the file to open",
										this,
									  FileChooserAction.Open,
										"Cancel", ResponseType.Cancel,
										"Open", ResponseType.Accept);
			FileFilter filter = new FileFilter();
			filter.Name = "Image files";
			filter.AddPattern("*.jpg");
			filter.AddPattern("*.jpeg");
			filter.AddPattern("*.png");
			filter.AddPattern("*.tif");
			filter.AddPattern("*.bmp");
			filter.AddPattern("*.gif");
			filter.AddPattern("*.tiff");
			fc.Filter = filter;
			string path = Environment.CurrentDirectory;
			string path5 = path + @"\TwitterClockImages";
			DB_Connect k = new DB_Connect();
			List<string> credentials = new List<string>();
			//string name = k.find_onoma(tautotita);
			//	credentials = GetCredentials(tautotita);
			if (!System.IO.Directory.Exists(path5))
			{
				System.IO.Directory.CreateDirectory(path5);
			}
			string source = "";
			if (fc.Run() == (int)ResponseType.Accept)
			{

				System.IO.FileStream file = System.IO.File.OpenRead(fc.Filename);

				source = fc.Filename;

				arxeio = System.IO.Path.GetFileName(source);

				file.Close();



				//label.Text = Environment.CurrentDirectory;
				try
				{
					//	byte[] bytes = File.ReadAllBytes(Environment.CurrentDirectory + @"\TwitterClockImages\" + arxeio);

				}
				catch (UnauthorizedAccessException)
				{
					warning.Text = "Η προσβαση στο αρχειο που επιλεξατε απαγορευεται";
				}
				catch (ArgumentException)
				{
					label.Text = "Einai keno";
				}
				catch (PathTooLongException)
				{
					label.Text = "path too long";
				}
				catch (DirectoryNotFoundException)
				{
					label.Text = "directory not found";
				}
				catch (FileNotFoundException)
				{
					label.Text = "file ot found";
				}
				catch (IOException ex)
				{
					warning.Text = ex.Message;

				}
				catch (NotSupportedException)
				{
					label.Text = "NotSupported";
				}

			}
			string path3 = path5 + @"\" + @arxeio;
			string image2 = path3;
			without = System.IO.Path.GetFileNameWithoutExtension(fc.Fi‌​lename);
			extension = System.IO.Path.GetExtension(fc.Filename);
			fc.Destroy();
			//System.IO.File.Create(path3);
			if (!File.Exists(path3))
			{
				try
				{
					File.Copy(source, path3);
					//byte[] bytes=File.ReadAllBytes(source);
				}
				catch (UnauthorizedAccessException)
				{
					warning.Text = "Η προσβαση στο αρχειο που επιλεξατε απαγορευεται";
				}
				catch (ArgumentException)
				{
					label.Text = "Einai keno";
				}
				catch (PathTooLongException)
				{
					label.Text = "path too long";
				}
				catch (DirectoryNotFoundException)
				{
					label.Text = "directory not found";
				}
				catch (FileNotFoundException)
				{
					label.Text = "file ot found";
				}
				catch (IOException ex)
				{
					//warning.Text = "Εχει προστεθει παλαιωτερα εικονα με το ιδιο ονομα";

				}

				catch (NotSupportedException)
				{
					label.Text = "NotSupported";
				}
			}
			else {
				int loop = 0;

				while (File.Exists(image2))
				{
					image2 = path5 + @"\" + without + Convert.ToString(loop) + extension;
					loop++;

				}


				try
				{
					arxeio = without + Convert.ToString(loop) + extension;
					File.Copy(source, image2);

				}
				catch (UnauthorizedAccessException)
				{
					warning.Text = "Η προσβαση στο αρχειο που επιλεξατε απαγορευεται";
				}
				catch (ArgumentException)
				{
					label.Text = "Einai keno";
				}
				catch (PathTooLongException)
				{
					label.Text = "path too long";
				}
				catch (DirectoryNotFoundException)
				{
					label.Text = "directory not found";
				}
				catch (FileNotFoundException)
				{
					label.Text = "file ot found";
				}
				catch (IOException ex)
				{
					//warning.Text = "Εχει προστεθει παλαιωτερα εικονα με το ιδιο ονομα";

				}

				catch (NotSupportedException)
				{
					label.Text = "NotSupported";
				}


			}

		}

		void OnDaySelected(object sender, EventArgs args)
		{
			label.Text = cal.Day + "/" + cal.Month + 1 + "/" + cal.Year;
		}
		void OnChangedDialog(object sender, EventArgs args)
		{
			string size;
			int number;
			//Entry text = (Entry)sender;
			size = text.Text;
			number = size.Length;

			plirofories.Text = Convert.ToString(140 - size.Length);

		}

	}
	public class Warning : Window
	{
		string username;
		void Delete(object sender, EventArgs args)
		{
			DB_Connect k = new DB_Connect();
			k.DeleteUser(username);
			this.Destroy();
			this.Destroy();
		}
		public Warning(string name) : base("Διαγραφη")
		{
			SetDefaultSize(150, 100);
			SetPosition(WindowPosition.Center);
			username = name;
			Label label = new Label();
			label.Text = "         Διαγραφη Χρηστη.Ολες οι αναρτησεις θα διαγραφθουν\n        εισαστε βεβαιοι;";
			Button btn1 = new Button("OK");
			btn1.Clicked += Delete;
			btn1.SetSizeRequest(100, 40);
			Fixed fix = new Fixed();
			fix.Put(label, 0, 15);
			fix.Put(btn1, 120, 50);
			Add(fix);
			ShowAll();
		}
	}
	public class ChangeSettings : Window
	{
		Label label;
		string username;
		static int id;
		Label User;
		Button btn1 = new Button("Αποθηκευση");
		Button btn2 = new Button("Νεο λογαριασμος Twitter");
		Button btn3 = new Button("Εισαγωγη");

		Entry pincode = new Entry();
		Fixed fix = new Fixed();

		Entry newname = new Entry();
		public ChangeSettings(string name) : base(name)
		{
			Button btn4 = new Button("<<Πισω");

			username = name;
			DB_Connect k = new DB_Connect();
			id = k.return_id(name);
			SetDefaultSize(500, 500);
			SetPosition(WindowPosition.Center);
			newname = new Entry(username);
			fix = new Fixed();
			label = new Label();
			btn1.Clicked += ChangeName;
			btn2.Clicked += UpdateCreds;
			fix.Put(btn1, 200, 50);
			fix.Put(btn2, 300, 50);
			fix.Put(btn4, 30, 400);
			btn4.Clicked += Back;
			fix.Put(newname, 30, 50);
			fix.Put(label, 150, 100);


			Add(fix);
			ShowAll();
		}
		void ChangeName(object sender, EventArgs args)
		{

			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			DB_Connect o = new DB_Connect();
			int check = o.checkonoma(newname.Text);
			if (check == 0)
			{
				using (var conn = new SQLiteConnection(@path4))
				{
					conn.Open();
					using (var cmd = new SQLiteCommand(conn))
					{
						using (var transaction = conn.BeginTransaction())
						{

							cmd.CommandText =
								 "UPDATE Xristes SET Onoma='" + newname.Text + "' WHERE Onoma = '" + username + "'; ";
							cmd.ExecuteNonQuery();


							transaction.Commit();
						}
					}

					conn.Close();

				}
				this.Destroy();
				Application.Init();
				new SharpApp();
				Application.Run();
			}

			else
			{
				label.Text = "Το ονομα χρησιμοποιητε ηδη,ξαναπροσπαθηστε";
			}
		}
		void UpdateCreds(object sender, EventArgs args)
		{
			label.Text = "Συνδεεστε στο Twitter.παρακαλω περιμενετε";


			fix.Put(pincode, 30, 150);
			fix.Put(btn3, 200, 150);
			btn3.Clicked += eisagogi;
			Add(fix);
			ShowAll();
			var appCredentials = new TwitterCredentials("0rbIj70OY04gB6nbMuXaCArGl", "8sBsmmv6UbLBv3OjnPRDSwiopDlezfcAoxuFKRLyiqDxZ1wrt5");
			var authenticationContext = AuthFlow.InitAuthentication(appCredentials);
			credits = authenticationContext;

			//credits = Convert.ToString(authenticationContext);
			try
			{

				Process.Start(authenticationContext.AuthorizationURL);
			}
			catch (NullReferenceException)
			{
				label.Text = "Πιθανον δεν υπαρχει συνδεση στο διαδικτυο,προσπαθηστε αργοτερα";
			}
		}
		Tweetinvi.Models.IAuthenticationContext credits;

		void eisagogi(object sender, EventArgs args)
		{
			string data = "Data Source=";
			string path3 = @"\Anartiseis.db";

			string path = Environment.CurrentDirectory;
			string path4 = data + path + path3;
			var userCredentials = AuthFlow.CreateCredentialsFromVerifierCode(pincode.Text, credits);

			using (var conn = new SQLiteConnection(@path4))
			{
				conn.Open();
				using (var cmd = new SQLiteCommand(conn))
				{
					using (var transaction = conn.BeginTransaction())
					{

						cmd.CommandText =
							   "UPDATE Xristes SET AccessToken='" + userCredentials.AccessToken + "',AccessTokenSec='" + userCredentials.AccessTokenSecret + "' WHERE Onoma = '" + username + "'; ";
						cmd.ExecuteNonQuery();


						transaction.Commit();
					}
				}

				conn.Close();

			}


			this.Destroy();
			Application.Init();
			new SharpApp();
			Application.Run();
		}
		void Back(object sender, EventArgs args)
		{
			this.Destroy();
			Application.Init();
			new ControlPanel(username);
			Application.Run();
		}
	}
	public class ControlPanel : Window
	{
		string username;
		static int id;
		Label User;
		public ControlPanel(string name) : base(name)
		{
			username = name;
			DB_Connect k = new DB_Connect();
			id = k.return_id(name);
			SetDefaultSize(500, 500);
			SetPosition(WindowPosition.Center);

			DeleteEvent += delegate { Application.Quit(); };

			User = new Label(name);
			Label label = new Label("Συνδεδεμενος Χρηστης:");
			Button btn1 = new Button("Νεα Αναρτηση");
			btn1.Clicked += Insert;
			Button Btn2 = new Button("Προβολη Αναρτησεων");
			Button Btn5 = new Button("Διαγραφη Χρηστη");

			Btn2.Clicked += Show;
			Button btn4 = new Button("Εξοδος");
			btn4.Clicked += Back;
			Btn5.Clicked += warning;

			//Label instruct = new Label("(Συνδεθητε σε εναν νεο λογαριασμο Twitter\nκαι πατηστε)");
			Button btn3 = new Button("Ρυθμισεις λογαριασμου");
			Fixed fix = new Fixed();
			btn3.Clicked += UpdateCreds;
			fix.Put(btn3, 20, 130);
			fix.Put(btn4, 20, 200);
			fix.Put(Btn5, 20, 300);

			//	fix.Put(instruct, 190, 130);
			fix.Put(label, 12, 12);
			fix.Put(User, 170, 12);
			fix.Put(btn1, 20, 70);
			fix.Put(Btn2, 20, 100);
			Add(fix);
			ShowAll();

		}
		void UpdateCreds(object sender, EventArgs args)
		{
			Application.Init();
			new ChangeSettings(username);
			Application.Run();
		}
		void Insert(object sender, EventArgs args)
		{
			Application.Init();
			new NewPost(id, "", "", "");
			Application.Run();

		}
		void warning(object sender, EventArgs args)
		{
			Application.Init();
			//new Control(id);
			//new ShowPosts();
			new Warning(username);
			Application.Run();

		}
		void Show(object sender, EventArgs args)
		{
			Application.Init();
			//new Control(id);
			//new ShowPosts();
			new Storeage(id);
			Application.Run();

		}
		void Back(object sender, EventArgs args)
		{
			this.Destroy();
			Application.Init();
			new SharpApp();
			Application.Run();


		}
	}

	class SharpApp : Window
	{
		List<string> names = new List<string>();

		ComboBox cb;
		Label User;
		int f;
		Label label;
		public SharpApp() : base("TwitterClock")
		{
			DB_Connect k = new DB_Connect();
			names = k.return_onoma();
			//List<string> lista = new List<string>() ;
			//String[] distros = new String[lista.Count];
			string[] distros = new string[names.Count];
			for (int i = 0; i < names.Count; i++)
			{
				distros[i] = names[i];
			}

			SetDefaultSize(500, 500);
			SetPosition(WindowPosition.Center);

			DeleteEvent += delegate { Application.Quit(); };
			Fixed fix = new Fixed();
			cb = new ComboBox(distros);
			cb.Changed += OnChanged;
			Button btn1 = new Button("Button");
			btn1.Sensitive = false;
			Button btn2 = new Button("Εισοδος");
			btn2.Clicked += Eisodos;
			Button btn3 = new Button(Stock.Close);
			Button btn4 = new Button("Νεος Χρηστης");
			label = new Label("Επιλεξτε Χρηστη");
			User = new Label("Κανενας Χρηστης");
			btn4.Clicked += OnClick;
			fix.Put(btn2, 60, 85);
			fix.Put(label, 20, 30);
			fix.Put(cb, 20, 60);
			fix.Put(btn4, 350, 20);

			Add(fix);

			ShowAll();

		}
		void Eisodos(object sender, EventArgs args)
		{
			//int id=0;
			//DB_Connect k = new DB_Connect();
			//ComboBox cb = (ComboBox)sender;
			//id=k.return_id(cb.ActiveText);
			this.Destroy();
			Application.Init();
			new ControlPanel(cb.ActiveText);
			Application.Run();


		}
		void OnChanged(object sender, EventArgs args)
		{
			ComboBox cb = (ComboBox)sender;
			label.Text = cb.ActiveText;

		}
		void OnClick(object sender, EventArgs args)
		{
			this.Destroy();

			Application.Init();
			new newwindow();
			Application.Run();

		}

	}
	class newwindow : Window
	{
		Tweetinvi.Models.IAuthenticationContext credits;
		int y = 0;
		static int s = 2;
		static Timer aTimer = new Timer();
		static int flag = 0;

		public static int Timer(string name)
		{
			int k = 1;

			var appCredentials = new TwitterCredentials("0rbIj70OY04gB6nbMuXaCArGl", "8sBsmmv6UbLBv3OjnPRDSwiopDlezfcAoxuFKRLyiqDxZ1wrt5");
			var authenticationContext = AuthFlow.InitAuthentication(appCredentials);
			//credits = Convert.ToString(authenticationContext);
			try
			{
				Process.Start(authenticationContext.AuthorizationURL);
				s = 0;
			}
			catch (System.NullReferenceException)
			{
				s = 1;
			}

			return s;



			while (k != 1) ;


		}

		private static void OnTimedEvent3(Tweetinvi.Models.IAuthenticationContext credits, string name)
		{
			DB_Connect k = new DB_Connect();
			string[] credents = new string[2];
			if (flag == 1)
			{


			}
		}

		string[] credents = new string[2];
		//string credits;
		Button btn1 = new Button("Δημιουργια Χρηστη");
		Button btn2 = new Button("Εισαγωγη");
		Fixed fix = new Fixed();
		Entry entryname = new Entry();

		Label entry = new Label();
		Label label;
		int elenhos;

		public newwindow() : base("Νεος Χρηστης")
		{
			DB_Connect k = new DB_Connect();
			//smf okc
			SetDefaultSize(500, 500);
			SetPosition(WindowPosition.Center);

			DeleteEvent += delegate { Application.Quit(); };

			Label entrylabe = new Label();
			btn1.Sensitive = false;
			entryname.Changed += OnChanged;

			Button btn3 = new Button("<<Πισω");
			//Button btn4 = new Button("Νεος Χρηστης");
			btn1.Clicked += OnClick;
			btn3.Clicked += Back;
			label = new Label("(Συνδεθειτε στην υπηρεσια Twitter με\n τον λογαριασμο σας πριν συνεχισετε)");
			//fix.Put(btn3, 10, 200);
			fix.Put(entryname, 10, 60);
			fix.Put(label, 10, 100);
			fix.Put(btn1, 10, 20);
			fix.Put(btn3, 10, 400);

			Add(fix);
			ShowAll();


		}
		void Back(object sender, EventArgs args)
		{
			this.Destroy();
			Application.Init();
			new SharpApp();
			Application.Run();


		}
		void OnChanged(object sender, EventArgs args)
		{
			btn1.Sensitive = true;


		}

		Entry pincode = new Entry();

		void OnClick(object sender, EventArgs args)
		{

			int o = 0;
			DB_Connect k = new DB_Connect();
			string onomaXristi = entryname.Text;
			int check = k.checkonoma(onomaXristi);
			label.Text = Convert.ToString(check);
			if (check == 0)
			{
				var appCredentials = new TwitterCredentials("0rbIj70OY04gB6nbMuXaCArGl", "8sBsmmv6UbLBv3OjnPRDSwiopDlezfcAoxuFKRLyiqDxZ1wrt5");
				var authenticationContext = AuthFlow.InitAuthentication(appCredentials);
				credits = authenticationContext;

				//credits = Convert.ToString(authenticationContext);
				try
				{

					Process.Start(authenticationContext.AuthorizationURL);
					o = 0;
				}
				catch (System.NullReferenceException)
				{
					o = 1;
				}
				//		//o=Timer(onomaXristi);
				if (o == 0)
				{
					fix.Put(pincode, 100, 200);
					label.Text = "Συνδεεσται στο Twitter.Παρακαλω περιμενετε";
					entry.Text = "Εισαγεται τον κωδικο επιβεβαιωσης εδω";
					fix.Put(entry, 100, 230);
					fix.Put(btn2, 270, 195);
					btn2.Clicked += Methodos;
					Add(fix);
					//btn1.Sensitive = true;
					ShowAll();
				}
				else
				{
					label.Text = "Πιθανον δεν υπαρχει συνδεση στο διαδικτυο,προσπαθηστε ργοτερα";

				}


			}
			else
			{
				label.Text = "Το ονομα χρησιμοποιητε ηδη,παρακαλω ξαναπροσπαθηστε";
			}


		}
		void Methodos(object sender, EventArgs args)
		{
			Tweetinvi.Models.IAuthenticationContext credentials;

			credentials = credits;

			//.Text = pincode.Text;
			flag = 1;
			string onomaXristi = entryname.Text;

			DB_Connect k = new DB_Connect();

			var userCredentials = AuthFlow.CreateCredentialsFromVerifierCode(pincode.Text, credentials);

			k.InsertDat(onomaXristi, userCredentials.AccessToken, userCredentials.AccessTokenSecret);

			this.Destroy();     //User(credents, onomaXristi, pincode.Text);
			Application.Init();
			new SharpApp();
			Application.Run();


		}


	}
	class MainClass
	{
		public static void Main(string[] args)
		{
			DB_Connect k = new DB_Connect();
			DB_Connect.Time();
			Application.Init();
			new SharpApp();
			Application.Run();
		}
	}
}




