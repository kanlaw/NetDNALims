using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.IO;
using System.Data;

namespace IMClient.Logic.Sql
{
    public  class SqlLiteHelper
    {
        /// <summary>
        /// 本地数据库
        /// </summary>
        public  string localdbPath = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_LocalDB_Name;

        /// <summary>
        /// sql文件存放位置
        /// </summary>
        public  string localdb_SqlFile_Path = AppDomain.CurrentDomain.BaseDirectory + "Compents//Sql//";

        #region 数据库初始化
        //创建一个空的数据库
        public void createNewDatabase()
        {
            if (!File.Exists(localdbPath))
            {
                //创建数据库
                SQLiteConnection.CreateFile(localdbPath);
                string sqlFilePath = AppDomain.CurrentDomain.BaseDirectory + SysParams.Sys_LocalDB_SQL_Dir;
                //创建表
                createTableAll(sqlFilePath);
            }
        }

        /// <summary>
        /// 创建初始库结构
        /// </summary>
        /// <param name="sqlFilePath"></param>
        public void createTableAll(string sqlFilePath)
        {
            if (Directory.Exists(sqlFilePath))
            {
                DirectoryInfo di = new DirectoryInfo(sqlFilePath);
                if (di.GetFiles().Length > 0)
                {
                    using (SQLiteConnection m_dbConnection = conn())
                    {
                        try
                        {
                            m_dbConnection.Open();
                            for (int i = 0; i < di.GetFiles().Length; i++)
                            {
                                FileInfo fi = di.GetFiles()[i];
                                using (StreamReader sr = fi.OpenText())
                                {
                                    string sql = sr.ReadToEnd();
                                    createTable(sql, m_dbConnection);
                                }
                            }
                        }
                        catch (Exception ex)
                        {

                        }
                        finally
                        {
                            m_dbConnection.Close();
                        }
                    }
                }
            }
        }
        public void createTable(string sql, SQLiteConnection m_dbConnection)
        {
            //string sql = "create table highscores (name varchar(20), score int)";
         
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        public SQLiteConnection conn()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source="+ localdbPath + ";Version=3;");
            return m_dbConnection;
        }
        #endregion

        #region 离线任务记录

        /// <summary>
        /// 添加任务
        /// </summary>
        /// <param name="fileId"></param>
        /// <param name="fileName"></param>
        /// <param name="filePath"></param>
        /// <param name="fileSize"></param>
        /// <param name="deleted"></param>
        /// <param name="Operation"></param>
        public void AddMission(string fileId,string fileName, string filePath, 
            string fileSize, int deleted, int Operation,string fromUser,string toUser)
        {
            this.createNewDatabase();

            string sql = "INSERT INTO FileOperation(filename, filepath, filesize, deleted, Operation,fileId,fromUser,toUser) values('{0}','{1}',{2},{3},{4},'{5}','{6}','{7}')";
            sql = string.Format(sql, fileName, filePath, fileSize, deleted.ToString(), Operation.ToString(),fileId,fromUser,toUser);
            using (SQLiteConnection m_dbConnection = conn())
            {
                try
                {
                    m_dbConnection.Open();
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    //throw;
                }
                finally {
                    m_dbConnection.Close();
                }
              ;
            }
        }

        /// <summary>
        /// 记录任务
        /// </summary>
        /// <param name="fileId"></param>
        public void DeletedMission(string fileId)
        {
            this.createNewDatabase();
            string sql = "delete from FileOperation where fileid='"+fileId+"'";
          
            using (SQLiteConnection m_dbConnection = conn())
            {
                try
                {
                    m_dbConnection.Open();
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    //throw;
                }
                finally
                {
                    m_dbConnection.Close();
                }
              
            }
        }

        public DataTable retMission(string fromUser,string toUser)
        {
            this.createNewDatabase();
            string sql = "select * from fileOperation where fromuser='"+fromUser+"' and toUser='"+toUser+"' and deleted=0 and Operation=" + (int)OperationType.Download;
            using (SQLiteConnection m_dbConnection = conn())
            {
                try
                {
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    m_dbConnection.Open();
                    SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    return null;
                    //throw;
                }
                finally
                {
                    m_dbConnection.Close();
                }
             ;
            }
        }

        #endregion

        #region 最近联系人
 
        /// <summary>
        /// 添加最近联系人
        /// </summary>
        /// <param name="id"></param>
        /// <param name="SelfUserId"></param>
        /// <param name="SelfRealName"></param>
        /// <param name="linkUserId"></param>
        /// <param name="linkRealName"></param>
        public void AddRecent(string id, string SelfUserId, string SelfRealName,
            string linkUserId, string linkRealName)
        {
            this.createNewDatabase();

            string sql = "INSERT INTO Recent_Link(id, SelfUserId, SelfRealName, linkUserId, linkRealName) values('{0}','{1}','{2}','{3}','{4}')";
            sql = string.Format(sql, id, SelfUserId, SelfRealName, linkUserId, linkRealName);
            using (SQLiteConnection m_dbConnection = conn())
            {
                try
                {
                    m_dbConnection.Open();
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    //throw;
                }
                finally
                {
                    m_dbConnection.Close();
                }
              ;
            }
        }

        /// <summary>
        /// 判断最近联系人是否存在
        /// </summary>
        /// <param name="SelfUserId"></param>
        /// <param name="linkUserId"></param>
        /// <returns></returns>
        public bool existRecent(string SelfUserId, string linkUserId)
        {
            bool result = true;
            this.createNewDatabase();
            string sql = "select count(*) from Recent_Link where SelfUserId='" + SelfUserId + "' and linkUserId='"+linkUserId+"'";
            using (SQLiteConnection m_dbConnection = conn())
            {
                try
                {
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    m_dbConnection.Open();
                    int count = Convert.ToInt32(command.ExecuteScalar());
                    if (count == 0)
                    {
                        result = false;
                    }
                  
                }
                catch (Exception ex)
                {
                    result = true;
                    //throw;
                }
                finally
                {
                    m_dbConnection.Close();
                }

            }
            return result;
        }

       /// <summary>
       /// 删除最近联系人
       /// </summary>
       /// <param name="selfUser"></param>
       /// <param name="linkUser"></param>
        public void DeletedRecent(string selfUser,string linkUser)
        {
            this.createNewDatabase();
            string sql = "delete from Recent_Link where SelfUserId=" + selfUser + " and linkUserId=" + linkUser;

            using (SQLiteConnection m_dbConnection = conn())
            {
                try
                {
                    m_dbConnection.Open();
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {

                    //throw;
                }
                finally
                {
                    m_dbConnection.Close();
                }

            }
        }

        /// <summary>
        /// 获取相关最近联系人
        /// </summary>
        /// <param name="selfUser"></param>
        /// <returns></returns>
        public DataTable retRecent(string selfUser)
        {
            this.createNewDatabase();
            string sql = "select * from Recent_Link where SelfUserId='" + selfUser + "'";
            using (SQLiteConnection m_dbConnection = conn())
            {
                try
                {
                    SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
                    m_dbConnection.Open();
                    SQLiteDataAdapter da = new SQLiteDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    return null;
                    //throw;
                }
                finally
                {
                    m_dbConnection.Close();
                }
             
            }
        }

        #endregion;
    }
}
